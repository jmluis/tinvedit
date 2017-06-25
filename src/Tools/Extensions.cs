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
            for (var i = 0; i < Constants.Prefixes.Count; i++)
                comboPrefix.Items.Add(Constants.Prefixes[i]);
        }

        internal static void AddListBoxItems(ListBox listBox1)
        {
            var Items = new FilteredBindingList<Item>();
            foreach (var Item in Constants.Items.Values) Items.Add(Item);
            listBox1.DataSource = new BindingSource(Items, null);
            listBox1.Sorted = true;
        }

        //TerraLimb.Buff ext
        public static void AddComboBoxBuffs(ComboBox box)
        {
            box.Items.Clear();
            foreach (var kvp in Constants.Buffs)
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