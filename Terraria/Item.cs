using System.Windows.Forms;
using TerrariaInvEdit.Tools;
using System.Drawing;

namespace TerrariaInvEdit.Terraria
{
    public struct Item
    {
        private byte prefix;
        public byte Prefix
        {
            get
            {
                return prefix;
            }
            set
            {
                if (value < 0 || value == 0xFF || value > Constants.PrefixNames.Count)
                    prefix = 0;
                else
                    prefix = value;
            }
        }

        private int stack;
	public bool IsFavorite { get; set; }

        public byte ShoeSlot { get; set; }
        public byte HandOffSlot { get; set; }
        public byte BalloonSlot { get; set; }
        public byte WaistSlot { get; set; }
        public byte HandOnSlot { get; set; }
        public byte FaceSlot { get; set; }
        public byte NeckSlot { get; set; }
        public byte WingSlot { get; set; }
        public byte FrontSlot { get; set; }
        public byte BackSlot { get; set; }
        public byte ShieldSlot { get; set; }
        public byte HeadSlot { get; set; }
        public byte BodySlot { get; set; }
        public byte LegSlot { get; set; }

        public int Stack
        {
            get { return stack; }
            set
            {
                if (value < 0)
                    stack = 0;
                else
                    stack = value;
            }
        }

        public string ItemNick;
        public string ItemName { get; set; }
        public Color? Color { get; set; }


        public int ItemID;

        public int Index { get; set; }
        public int MaxStack { get; set; }

        public override string ToString()
        {
            return ItemName;
        }

        internal static void AddComboBoxPrefix(ComboBox comboPrefix)
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
    }
}