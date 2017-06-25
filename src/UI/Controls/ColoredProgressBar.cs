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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TerrariaInvEdit.UI.Controls
{
    public class ColoredProgressBar : ProgressBar
    {
        private LinearGradientBrush BackgroudBrush;
        private readonly SolidBrush BaseBrush;
        private LinearGradientBrush ForegroudBrush;
        private LinearGradientBrush HueBrush;
        private readonly Pen Line;

        public ColoredProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ForeColor = Color.Green;
            BackColor = Color.DarkGreen;
            BaseBrush = new SolidBrush(Color.FromArgb(251, 251, 251));
            Line = new Pen(Color.FromArgb(80, Color.White));
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                InitBrushes();
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                InitBrushes();
                Invalidate();
            }
        }

        public void InitBrushes()
        {
            var ProgressRect = GetProgressRect();
            var HighBar = new Rectangle(1, 1, ProgressRect.Width,
                (int) Math.Round(Math.Truncate(ProgressRect.Height * 0.45)));

            var ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] {0f, 0.55f, 1f};
            ColorBlend.Colors = new[] {BackColor, ForeColor, BackColor};

            BackgroudBrush = new LinearGradientBrush(ProgressRect, BackColor, ForeColor, LinearGradientMode.Vertical);
            BackgroudBrush.InterpolationColors = ColorBlend;

            ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] {0f, 0.35f, 0.65f, 1f};
            ColorBlend.Colors = new[]
            {
                Color.FromArgb(200, ForeColor), Color.FromArgb(100, BackColor), Color.FromArgb(100, BackColor),
                Color.FromArgb(200, ForeColor)
            };

            ForegroudBrush = new LinearGradientBrush(ProgressRect, ForeColor, BackColor, LinearGradientMode.Horizontal);
            ForegroudBrush.InterpolationColors = ColorBlend;
            ForegroudBrush.GammaCorrection = true;

            ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] {0f, 0.3f, 1f};
            ColorBlend.Colors = new[]
                {Color.FromArgb(120, Color.White), Color.FromArgb(110, Color.White), Color.FromArgb(80, Color.White)};

            HueBrush = new LinearGradientBrush(HighBar, Color.White, Color.White, LinearGradientMode.Vertical);
            HueBrush.InterpolationColors = ColorBlend;
            HueBrush.GammaCorrection = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var Base = new Rectangle(0, 0, Width, Height);
            var ProgressRect = GetProgressRect();
            var HighBar = new Rectangle(1, 1, ProgressRect.Width,
                (int) Math.Round(Math.Truncate(ProgressRect.Height * 0.45)));

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, Base);
            var size = new Size(-2, -2);
            Base.Inflate(size);
            e.Graphics.FillRectangle(BaseBrush, Base);

            e.Graphics.FillRectangle(BackgroudBrush, ProgressRect);
            e.Graphics.FillRectangle(ForegroudBrush, ProgressRect);

            e.Graphics.DrawLine(Line, 1, 1, 1, ProgressRect.Height);
            e.Graphics.DrawLine(Line, ProgressRect.Width, 1, ProgressRect.Width, ProgressRect.Height);

            e.Graphics.FillRectangle(HueBrush, HighBar);
        }

        public Rectangle GetProgressRect()
        {
            var Result = new Rectangle(0, 0, Width, Height);
            var size = new Size(-1, -1);
            Result.Inflate(size);
            Result.Width = (int) (Result.Width * ((double) Value / Maximum));
            if (Result.Width == 0) Result.Width = 1;

            return Result;
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InitBrushes();
        }
    }
}