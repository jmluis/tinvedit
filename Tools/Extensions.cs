using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TerraLimb;

namespace TerrariaInvEdit.Tools
{
    public static class Extensions
    {
        /// System.Drawing.Color ext
        public static Color FromRgba(this Color Color, int Red, int Green, int Blue, int Alpha)
        {
            return Color.FromArgb(Alpha, Red, Green, Blue);
        }

        /// TerraLib.Item ext
        internal static void AddComboBoxPrefixes(ComboBox comboPrefix)
        {
            comboPrefix.Items.Clear();
            for (int i = 0; i < Constants.PrefixNames.Count; i++)
            {
                comboPrefix.Items.Add(Constants.PrefixNames[i]);
            }
        }

        internal static void AddListBoxItems(ListBox listBox1)
        {
            FilteredBindingList<Item> Items = new FilteredBindingList<Item>();
            foreach (Item Item in Constants.Items.Values) { Items.Add(Item); }
            listBox1.DataSource = new BindingSource(Items, null);
            listBox1.Sorted = true;
        }

        //TerraLimb.Buff ext
        public static void AddComboBoxBuffs(ComboBox box)
        {
            box.Items.Clear();
            foreach (KeyValuePair<int, string> kvp in Constants.BuffNames)
            {
                if (kvp.Key == 0)
                {
                    box.Items.Add("(Empty)");
                    continue;
                }
                box.Items.Add(kvp.Value);
            }
        }
    }
}
