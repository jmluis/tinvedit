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
using System.Drawing;
using System.Windows.Forms;

namespace TerrariaInvEdit.UI.Forms
{
    public partial class ColorChoose : Form
    {
        public Color curColor;

        public ColorChoose(Color col)
        {
            InitializeComponent();
            rTrack.Value = col.R;
            gTrack.Value = col.G;
            bTrack.Value = col.B;
            recal();
        }

        private void rTrack_ValueChanged(object sender, EventArgs e)
        {
            recal();
        }

        private void gTrack_ValueChanged(object sender, EventArgs e)
        {
            recal();
        }

        private void bTrack_ValueChanged(object sender, EventArgs e)
        {
            recal();
        }

        private void recal()
        {
            curColor = Color.FromArgb(rTrack.Value, gTrack.Value, bTrack.Value);

            rText.Text = rTrack.Value.ToString();
            gText.Text = gTrack.Value.ToString();
            bText.Text = bTrack.Value.ToString();

            resultBox.BackColor = curColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rTrack.Value = byte.Parse(rText.Text);
            }
            catch
            {
            }
            recal();
        }

        private void gText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gTrack.Value = byte.Parse(gText.Text);
            }
            catch
            {
            }
            recal();
        }

        private void bText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bTrack.Value = byte.Parse(bText.Text);
            }
            catch
            {
            }
            recal();
        }
    }
}