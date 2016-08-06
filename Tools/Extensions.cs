using System.Drawing;

namespace TerrariaInvEdit.Tools
{
    public static class Extensions
    {
        public static Color FromRgba(this Color Color, int Red, int Green, int Blue, int Alpha)
        {
            return Color.FromArgb(Alpha, Red, Green, Blue);
        }
    }
}
