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

        void recal()
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
            catch { }
            recal();
        }

        private void gText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gTrack.Value = byte.Parse(gText.Text);
            }
            catch { }
            recal();
        }

        private void bText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bTrack.Value = byte.Parse(bText.Text);
            }
            catch { }
            recal();
        }
    }
}
