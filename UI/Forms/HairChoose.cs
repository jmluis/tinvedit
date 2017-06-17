using System;
using System.Drawing;
using System.Windows.Forms;
using TerrariaInvEdit.Properties;

namespace TerrariaInvEdit.UI.Forms
{
    public partial class HairChoose : Form
    {
        public int CurrentHair;
        public Color HairColor;
        public HairChoose(int cur, Color hairColor)
        {
            InitializeComponent();
            CurrentHair = cur;
            HairColor = hairColor;
            recal();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (CurrentHair > 0)
            {
                CurrentHair--;
                recal();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (CurrentHair < 35)
            {
                CurrentHair++;
                recal();
            }
        }

        void recal()
        {
            Bitmap img = (Bitmap)Resources.ResourceManager.GetObject("Player_Hair_" + (CurrentHair + 1));
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (img.GetPixel(x, y).A < 255 || img.GetPixel(x, y).IsEmpty)
                        continue;
                    Color pixel = img.GetPixel(x, y);

                    img.SetPixel(x, y, ColorMultiplier(pixel, HairColor));

                }
            }
            pictureBox1.Image = img;
        }

        public Color ColorMultiplier(Color c1, Color c2)
        {
            int _r = Math.Min((c1.R + c2.R) / 2, 255);
            int _g = Math.Min((c1.G + c2.G) / 2, 255);
            int _b = Math.Min((c1.B + c2.B) / 2, 255);

            return Color.FromArgb(_r, _g, _b);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
