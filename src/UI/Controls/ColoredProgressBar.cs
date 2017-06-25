using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TerrariaInvEdit.UI.Controls
{
    public class ColoredProgressBar : ProgressBar
    {
        private SolidBrush BaseBrush;
        private LinearGradientBrush BackgroudBrush;
        private LinearGradientBrush ForegroudBrush;
        private LinearGradientBrush HueBrush;
        private Pen Line;
        public override Color BackColor { get { return base.BackColor; } set { base.BackColor = value; InitBrushes(); Invalidate(); } }
        public override Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; InitBrushes(); Invalidate(); } }

        public ColoredProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ForeColor = Color.Green;
            BackColor = Color.DarkGreen;
            BaseBrush = new SolidBrush(Color.FromArgb(251, 251, 251));
            Line = new Pen(Color.FromArgb(80, Color.White));
        }

        public void InitBrushes()
        {
            Rectangle ProgressRect = GetProgressRect();
            Rectangle HighBar = new Rectangle(1, 1, ProgressRect.Width, (int)Math.Round(Math.Truncate(ProgressRect.Height * 0.45)));

            var ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] { 0f, 0.55f, 1f };
            ColorBlend.Colors = new[] { BackColor, ForeColor, BackColor };

            BackgroudBrush = new LinearGradientBrush(ProgressRect, BackColor, ForeColor, LinearGradientMode.Vertical);
            BackgroudBrush.InterpolationColors = ColorBlend;

            ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] { 0f, 0.35f, 0.65f, 1f };
            ColorBlend.Colors = new[] { Color.FromArgb(200, ForeColor), Color.FromArgb(100, BackColor), Color.FromArgb(100, BackColor), Color.FromArgb(200, ForeColor) };

            ForegroudBrush = new LinearGradientBrush(ProgressRect, ForeColor, BackColor, LinearGradientMode.Horizontal);
            ForegroudBrush.InterpolationColors = ColorBlend;
            ForegroudBrush.GammaCorrection = true;

            ColorBlend = new ColorBlend();
            ColorBlend.Positions = new[] { 0f, 0.3f, 1f };
            ColorBlend.Colors = new[] { Color.FromArgb(120, Color.White), Color.FromArgb(110, Color.White), Color.FromArgb(80, Color.White) };

            HueBrush = new LinearGradientBrush(HighBar, Color.White, Color.White, LinearGradientMode.Vertical);
            HueBrush.InterpolationColors = ColorBlend;
            HueBrush.GammaCorrection = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle Base = new Rectangle(0, 0, Width, Height);
            Rectangle ProgressRect = GetProgressRect();
            Rectangle HighBar = new Rectangle(1, 1, ProgressRect.Width, (int)Math.Round(Math.Truncate(ProgressRect.Height * 0.45)));

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, Base);
            Size size = new Size(-2, -2);
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
            Rectangle Result = new Rectangle(0, 0, Width, Height);
            Size size = new Size(-1, -1);
            Result.Inflate(size);
            Result.Width = (int)(Result.Width * ((double)Value / Maximum));
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