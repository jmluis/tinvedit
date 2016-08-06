using System.Collections.Generic;
using System.Windows.Forms;

namespace TerrariaInvEdit.Terraria
{
    public class Buff
    {
        public int BuffID;
        public int BuffTime;
        public int Slot;

        public string BuffName;
        public string BuffDescription;

        public Buff(int id, int time, int slot)
        {
            Slot = slot;
            BuffID = id;

            if (time < 0)
                BuffTime = 0;
            else
                BuffTime = time;
            BuffName = Constants.BuffNames[BuffID];
            BuffDescription = Constants.BuffTips[BuffID];
        }

        public static void AddComboBoxItems(ComboBox box)
        {
            box.Items.Clear();
            foreach(KeyValuePair<int, string> kvp in Constants.BuffNames)
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

