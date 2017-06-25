/*
       This file is part of Terraria Inventory Editor
                            Copyright © 2017 Jose Luis, Anthony Wolfe

    Terraria Inventory Editor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Terraria Inventory Editor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Terraria Inventory Editor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace TerrariaInvEdit.Tools
{
    public class FilteredBindingList<T> : BindingList<T>, IBindingListView
    {
        public List<T> OriginalList { get; } = new List<T>();

        #region Searching

        protected override bool SupportsSearchingCore => true;

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            var propInfo = typeof(T).GetProperty(prop.Name);
            T item;

            if (key != null)
                for (var i = 0; i < Count; ++i)
                {
                    item = Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            return -1;
        }

        public int Find(string property, object key)
        {
            // Check the properties for a property with the specified name.
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            return FindCore(prop, key);
        }

        #endregion Searching

        #region Sorting

        private ArrayList sortedList;
        private FilteredBindingList<T> unsortedItems;
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSortedValue;
        protected override PropertyDescriptor SortPropertyCore => sortPropertyValue;
        protected override ListSortDirection SortDirectionCore => sortDirectionValue;


        public void ApplySort(string propertyName, ListSortDirection direction)
        {
            // Check the properties for a property with the specified name.
            var prop = TypeDescriptor.GetProperties(typeof(T))[propertyName];

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                throw new ArgumentException(propertyName + " is not a valid property for type:" + typeof(T).Name);
            ApplySortCore(prop, direction);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            var interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                unsortedItems = new FilteredBindingList<T>();

                if (sortPropertyValue != null)
                    foreach (object item in Items)
                    {
                        unsortedItems.Add((T) item);
                        sortedList.Add(prop.GetValue(item));
                    }
                // Call Sort on the ArrayList.
                sortedList.Sort();
                T temp;

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    sortedList.Reverse();

                for (var i = 0; i < Count; i++)
                {
                    var position = Find(prop.Name, sortedList[i]);
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
                if (string.IsNullOrEmpty(Filter))
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
                // If the property type does not implement IComparable, let the user know.
            {
                throw new InvalidOperationException("Cannot sort by " + prop.Name + ". This" + prop.PropertyType +
                                                    " does not implement IComparable");
            }
        }

        protected override void RemoveSortCore()
        {
            RaiseListChangedEvents = false;
            // Ensure the list has been sorted.
            if (unsortedItems != null && OriginalList.Count > 0)
            {
                Clear();
                if (Filter != null)
                {
                    unsortedItems.Filter = Filter;
                    foreach (var item in unsortedItems)
                        Add(item);
                }
                else
                {
                    foreach (var item in OriginalList)
                        Add(item);
                }
                isSortedValue = false;
                RaiseListChangedEvents = true;
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
            if (IsSortedCore && itemIndex > 0 && itemIndex == Count - 1)
            {
                ApplySortCore(sortPropertyValue, sortDirectionValue);
                base.EndNew(itemIndex);
            }
        }

        #endregion Sorting

        #region AdvancedSorting

        public bool SupportsAdvancedSorting => false;
        public ListSortDescriptionCollection SortDescriptions => null;

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new NotSupportedException();
        }

        #endregion AdvancedSorting

        #region Filtering

        private string filterValue;
        public bool SupportsFiltering => true;

        public void RemoveFilter()
        {
            if (Filter != null) Filter = null;
        }

        public string Filter
        {
            get => filterValue;
            set
            {
                if (filterValue == value) return;
                // If the value is not null or empty, but doesn't
                // match expected format, throw an exception.
                if (!string.IsNullOrEmpty(value) &&
                    !Regex.IsMatch(value, BuildRegExForFilterFormat(), RegexOptions.Singleline))
                    throw new ArgumentException("Filter is not in the format: propName[<>=]'value'.");

                //Turn off list-changed events.
                RaiseListChangedEvents = false;

                // If the value is null or empty, reset list.
                if (string.IsNullOrEmpty(value))
                {
                    ResetList();
                }
                else
                {
                    var count = 0;
                    var matches = value.Split(new[] {" AND "}, StringSplitOptions.RemoveEmptyEntries);
                    var Filters = new SingleFilterInfo[matches.Length];

                    // Check to see if the filter was set previously.
                    // Also, check if current filter is a subset of the previous filter.
                    // Also, check if another filter was added
                    if (filterValue != null && matches.Length !=
                        filterValue.Split(new[] {" AND "}, StringSplitOptions.RemoveEmptyEntries).Length ||
                        !string.IsNullOrEmpty(filterValue) && !value.Contains(filterValue))
                        ResetList();

                    while (count < matches.Length)
                    {
                        var filterPart = matches[count];

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
            var regex = new StringBuilder();

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
            ClearItems();
            foreach (var t in OriginalList)
                Items.Add(t);
            if (IsSortedCore)
                ApplySortCore(SortPropertyCore, SortDirectionCore);
        }


        protected override void OnListChanged(ListChangedEventArgs e)
        {
            // If the list is reset, check for a filter. If a filter 
            // is applied don't allow items to be added to the list.
            if (e.ListChangedType == ListChangedType.Reset)
                if (Filter == null || Filter == "")
                    AllowNew = true;
                else
                    AllowNew = false;
            // Add the new item to the original list.
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                OriginalList.Add(this[e.NewIndex]);
                if (!string.IsNullOrEmpty(Filter))
                    //if (Filter == null || Filter == "")
                {
                    var cachedFilter = Filter;
                    Filter = "";
                    Filter = cachedFilter;
                }
            }
            // Remove the new item from the original list.
            if (e.ListChangedType == ListChangedType.ItemDeleted)
                OriginalList.RemoveAt(e.NewIndex);

            base.OnListChanged(e);
        }


        internal void ApplyFilters(SingleFilterInfo[] filters)
        {
            var results = new HashSet<T>();

            foreach (var filterParts in filters)
            {
                // Check to see if the property type we are filtering by implements
                // the IComparable interface.
                var interfaceType = TypeDescriptor.GetProperties(typeof(T))[filterParts.PropName].PropertyType
                    .GetInterface("IComparable");

                if (interfaceType == null)
                    throw new InvalidOperationException("Filtered property must implement IComparable.");
                // Check each value and add to the results list.
                foreach (var item in this)
                    if (filterParts.PropDesc.GetValue(item) != null)
                    {
                        var compareValue = filterParts.PropDesc.GetValue(item) as IComparable;
                        var result = compareValue.ToString().ToLower().CompareTo(filterParts.CompareValue);
                        {
                            if (filterParts.OperatorValue == FilterOperator.Contains && compareValue.ToString()
                                    .ToLower().Contains(filterParts.CompareValue.ToString().ToLower()))
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
            ClearItems();
            foreach (var itemFound in results)
                Add(itemFound);
        }

        internal SingleFilterInfo ParseFilter(string filterPart)
        {
            var filterInfo = new SingleFilterInfo();
            filterInfo.OperatorValue = DetermineFilterOperator(filterPart);

            var filterStringParts = filterPart.Split((char) filterInfo.OperatorValue);
            filterInfo.PropName = filterStringParts[0].Replace("[", "").Replace("]", "").Replace(" AND ", "").Trim();

            // Get the property descriptor for the filter property name.
            var filterPropDesc = TypeDescriptor.GetProperties(typeof(T))[filterInfo.PropName];

            // Convert the filter compare value to the property type.
            if (filterPropDesc == null)
                throw new InvalidOperationException(
                    string.Format("Specified property to filter {0} on does not exist on type: {1}",
                        filterInfo.PropName, typeof(T).Name));

            filterInfo.PropDesc = filterPropDesc;

            var comparePartNoQuotes = StripOffQuotes(filterStringParts[1]);
            try
            {
                var converter = TypeDescriptor.GetConverter(filterPropDesc.PropertyType);
                filterInfo.CompareValue = converter.ConvertFromString(comparePartNoQuotes);
            }
            catch (NotSupportedException)
            {
                throw new InvalidOperationException("Specified filter value " + comparePartNoQuotes +
                                                    " can not be converted from string. Implement a type converter for " +
                                                    filterPropDesc.PropertyType);
            }
            return filterInfo;
        }

        internal FilterOperator DetermineFilterOperator(string filterPart)
        {
            // Determine the filter's operator.
            if (Regex.IsMatch(filterPart, "[^>^<]="))
                return FilterOperator.EqualTo;
            if (Regex.IsMatch(filterPart, "[^>^<]"))
                return FilterOperator.Contains;
            if (Regex.IsMatch(filterPart, "<[^>^=]"))
                return FilterOperator.LessThan;
            if (Regex.IsMatch(filterPart, "[^<]>[^=]"))
                return FilterOperator.GreaterThan;
            return FilterOperator.None;
        }

        internal static string StripOffQuotes(string filterPart)
        {
            if (filterPart[0] == ' ')
                filterPart = filterPart.Remove(0, 1);
            // Strip off quotes in compare value if they are present.
            if (Regex.IsMatch(filterPart, "'.+'"))
            {
                var quote = filterPart.IndexOf('\'');
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
        internal object CompareValue;
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