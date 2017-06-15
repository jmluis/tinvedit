using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TerrariaInvEdit.Tools
{
    public class FilteredBindingList<T> : BindingList<T>, IBindingListView
    {
        private List<T> originalListValue = new List<T>();
        public List<T> OriginalList { get { return originalListValue; } }

        public FilteredBindingList()
        {
        }

        #region Searching
        protected override bool SupportsSearchingCore { get { return true; } }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);
            T item;

            if (key != null)
            {
                // Loop through the items to see if the key
                // value matches the property value.
                for (int i = 0; i < Count; ++i)
                {
                    item = (T)Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }

        public int Find(string property, object key)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            else
                return FindCore(prop, key);
        }

        #endregion Searching

        #region Sorting
        private ArrayList sortedList;
        private FilteredBindingList<T> unsortedItems;
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        protected override bool SupportsSortingCore { get { return true; } }
        protected override bool IsSortedCore { get { return isSortedValue; } }
        protected override PropertyDescriptor SortPropertyCore { get { return sortPropertyValue; } }
        protected override ListSortDirection SortDirectionCore { get { return sortDirectionValue; } }


        public void ApplySort(string propertyName, ListSortDirection direction)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T))[propertyName];

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                throw new ArgumentException(propertyName + " is not a valid property for type:" + typeof(T).Name);
            else
                ApplySortCore(prop, direction);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                unsortedItems = new FilteredBindingList<T>();

                if (sortPropertyValue != null)
                {
                    // Loop through each item, adding it the the sortedItems ArrayList.
                    foreach (Object item in this.Items)
                    {
                        unsortedItems.Add((T)item);
                        sortedList.Add(prop.GetValue(item));
                    }
                }
                // Call Sort on the ArrayList.
                sortedList.Sort();
                T temp;

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    sortedList.Reverse();

                for (int i = 0; i < this.Count; i++)
                {
                    int position = Find(prop.Name, sortedList[i]);
                    if (position != i && position > 0)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = temp;
                    }
                }

                isSortedValue = true;

                // If the list does not have a filter applied, 
                // raise the ListChanged event so bound controls refresh their values.
                // Pass -1 for the index since this is a Reset.
                if (String.IsNullOrEmpty(Filter))
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
                // If the property type does not implement IComparable, let the user know.
                throw new InvalidOperationException("Cannot sort by " + prop.Name + ". This" + prop.PropertyType.ToString() + " does not implement IComparable");
        }

        protected override void RemoveSortCore()
        {
            this.RaiseListChangedEvents = false;
            // Ensure the list has been sorted.
            if (unsortedItems != null && originalListValue.Count > 0)
            {
                this.Clear();
                if (Filter != null)
                {
                    unsortedItems.Filter = this.Filter;
                    foreach (T item in unsortedItems)
                        this.Add(item);
                }
                else
                {
                    foreach (T item in originalListValue)
                        this.Add(item);
                }
                isSortedValue = false;
                this.RaiseListChangedEvents = true;
                // Raise the list changed event, indicating a reset, and index of -1.
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        public void RemoveSort()
        {
            RemoveSortCore();
        }


        public override void EndNew(int itemIndex)
        {
            // Check to see if the item is added to the end of the list,
            // and if so, re-sort the list.
            if (IsSortedCore && itemIndex > 0 && itemIndex == this.Count - 1)
            {
                ApplySortCore(this.sortPropertyValue, this.sortDirectionValue);
                base.EndNew(itemIndex);
            }
        }

        #endregion Sorting

        #region AdvancedSorting
        public bool SupportsAdvancedSorting { get { return false; } }
        public ListSortDescriptionCollection SortDescriptions { get { return null; } }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new NotSupportedException();
        }

        #endregion AdvancedSorting

        #region Filtering
        private string filterValue = null;
        public bool SupportsFiltering { get { return true; } }
        public void RemoveFilter() { if (Filter != null) Filter = null; }

        public string Filter
        {
            get
            {
                return filterValue;
            }
            set
            {
                if (filterValue == value) return;
                // If the value is not null or empty, but doesn't
                // match expected format, throw an exception.
                if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, BuildRegExForFilterFormat(), RegexOptions.Singleline))
                    throw new ArgumentException("Filter is not in the format: propName[<>=]'value'.");

                //Turn off list-changed events.
                RaiseListChangedEvents = false;

                // If the value is null or empty, reset list.
                if (string.IsNullOrEmpty(value))
                    ResetList();
                else
                {
                    int count = 0;
                    string[] matches = value.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
                    SingleFilterInfo[] Filters = new SingleFilterInfo[matches.Length];

                    // Check to see if the filter was set previously.
                    // Also, check if current filter is a subset of the previous filter.
                    // Also, check if another filter was added
                    if ((filterValue != null && matches.Length != filterValue.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries).Length) || (!String.IsNullOrEmpty(filterValue) && !value.Contains(filterValue)))
                        ResetList();

                    while (count < matches.Length)
                    {
                        string filterPart = matches[count].ToString();

                        Filters[count] = ParseFilter(filterPart);
                        count++;
                    }
                    ApplyFilters(Filters);
                }
                // Set the filter value and turn on list changed events.
                filterValue = value;
                RaiseListChangedEvents = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        // Build a regular expression to determine if 
        // filter is in correct format.
        public static string BuildRegExForFilterFormat()
        {
            StringBuilder regex = new StringBuilder();

            // Look for optional literal brackets, 
            // followed by word characters or space.
            regex.Append(@"\[?[\w\s]+\]?\s?");

            // Add the operators: > < or =.
            regex.Append(@"[><=^]");

            //Add optional space followed by optional quote and
            // any character followed by the optional quote.
            regex.Append(@"\s?'?.+'?");

            return regex.ToString();
        }

        private void ResetList()
        {
            this.ClearItems();        
            foreach (T t in originalListValue)
                this.Items.Add(t);
            if (IsSortedCore)
                ApplySortCore(SortPropertyCore, SortDirectionCore);
        }


        protected override void OnListChanged(ListChangedEventArgs e)
        {
            // If the list is reset, check for a filter. If a filter 
            // is applied don't allow items to be added to the list.
            if (e.ListChangedType == ListChangedType.Reset)
            {
                if (Filter == null || Filter == "")
                    AllowNew = true;
                else
                    AllowNew = false;
            }
            // Add the new item to the original list.
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                OriginalList.Add(this[e.NewIndex]);
                if (!String.IsNullOrEmpty(Filter))
                //if (Filter == null || Filter == "")
                {
                    string cachedFilter = this.Filter;
                    this.Filter = "";
                    this.Filter = cachedFilter;
                }
            }
            // Remove the new item from the original list.
            if (e.ListChangedType == ListChangedType.ItemDeleted)
                OriginalList.RemoveAt(e.NewIndex);

            base.OnListChanged(e);
        }


        internal void ApplyFilters(SingleFilterInfo[] filters)
        {
            HashSet<T> results = new HashSet<T>();

            foreach (SingleFilterInfo filterParts in filters)
            {
                // Check to see if the property type we are filtering by implements
                // the IComparable interface.
                Type interfaceType = TypeDescriptor.GetProperties(typeof(T))[filterParts.PropName].PropertyType.GetInterface("IComparable");

                if (interfaceType == null)
                    throw new InvalidOperationException("Filtered property must implement IComparable.");
                // Check each value and add to the results list.
                foreach (T item in this)
                {
                    if (filterParts.PropDesc.GetValue(item) != null)
                    {
                        IComparable compareValue = filterParts.PropDesc.GetValue(item) as IComparable;
                        int result = compareValue.ToString().ToLower().CompareTo(filterParts.CompareValue);
                        {
                            if (filterParts.OperatorValue == FilterOperator.Contains && compareValue.ToString().ToLower().Contains(filterParts.CompareValue.ToString().ToLower()))
                                results.Add(item);
                            else if (filterParts.OperatorValue == FilterOperator.EqualTo && result == 0)
                                results.Add(item);
                            else if (filterParts.OperatorValue == FilterOperator.GreaterThan && result > 0)
                                results.Add(item);
                            else if (filterParts.OperatorValue == FilterOperator.LessThan && result < 0)
                                results.Add(item);
                            else if (compareValue.ToString() == "(Empty)")
                                results.Add(item);
                        }
                    }
                }
            }
            this.ClearItems();
            foreach (T itemFound in results)
            {
                this.Add(itemFound);
            }
        }

        internal SingleFilterInfo ParseFilter(string filterPart)
        {
            SingleFilterInfo filterInfo = new SingleFilterInfo();
            filterInfo.OperatorValue = DetermineFilterOperator(filterPart);

            string[] filterStringParts = filterPart.Split(new char[] { (char)filterInfo.OperatorValue });
            filterInfo.PropName = filterStringParts[0].Replace("[", "").Replace("]", "").Replace(" AND ", "").Trim();

            // Get the property descriptor for the filter property name.
            PropertyDescriptor filterPropDesc = TypeDescriptor.GetProperties(typeof(T))[filterInfo.PropName];

            // Convert the filter compare value to the property type.
            if (filterPropDesc == null)
                throw new InvalidOperationException(string.Format("Specified property to filter {0} on does not exist on type: {1}", filterInfo.PropName, typeof(T).Name));

            filterInfo.PropDesc = filterPropDesc;

            string comparePartNoQuotes = StripOffQuotes(filterStringParts[1]);
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(filterPropDesc.PropertyType);
                filterInfo.CompareValue = converter.ConvertFromString(comparePartNoQuotes);
            }
            catch (NotSupportedException)
            {
                throw new InvalidOperationException("Specified filter value " + comparePartNoQuotes + " can not be converted from string. Implement a type converter for " + filterPropDesc.PropertyType.ToString());
            }
            return filterInfo;
        }

        internal FilterOperator DetermineFilterOperator(string filterPart)
        {
            // Determine the filter's operator.
            if (Regex.IsMatch(filterPart, "[^>^<]="))
                return FilterOperator.EqualTo;
            else if (Regex.IsMatch(filterPart, "[^>^<]"))
                return FilterOperator.Contains;
            else if (Regex.IsMatch(filterPart, "<[^>^=]"))
                return FilterOperator.LessThan;
            else if (Regex.IsMatch(filterPart, "[^<]>[^=]"))
                return FilterOperator.GreaterThan;
            else
                return FilterOperator.None;
        }

        internal static string StripOffQuotes(string filterPart)
        {
            if (filterPart[0] == ' ')
                filterPart = filterPart.Remove(0, 1);
            // Strip off quotes in compare value if they are present.
            if (Regex.IsMatch(filterPart, "'.+'"))
            {
                int quote = filterPart.IndexOf('\'');
                filterPart = filterPart.Remove(quote, 1);
                quote = filterPart.LastIndexOf('\'');
                filterPart = filterPart.Remove(quote, 1);
                filterPart = filterPart.Trim();
            }
            return filterPart;
        }

        #endregion Filtering
    }

    public struct SingleFilterInfo
    {
        internal string PropName;
        internal PropertyDescriptor PropDesc;
        internal Object CompareValue;
        internal FilterOperator OperatorValue;
    }

    // Enum to hold filter operators. The chars 
    // are converted to their integer values.
    public enum FilterOperator
    {
        EqualTo = '=',
        LessThan = '<',
        GreaterThan = '>',
        Contains = '^',
        None = ' '
    }
}