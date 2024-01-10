using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace milano88.UI.Controls
{
    public class MSControlBox : Control
    {
        private BufferedGraphics _bufGraphics;
        private bool _isMouseHover = false;

        private void UpdateGraphicsBuffer()
        {
            if (Width > 0 && Height > 0)
            {
                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                context.MaximumBuffer = new Size(Width + 1, Height + 1);
                _bufGraphics = context.Allocate(CreateGraphics(), ClientRectangle);
            }
        }

        public MSControlBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            UpdateGraphicsBuffer();
            Size = new Size(45, 30);
        }

        public enum Type { CloseBox, MinimizeBox, MaximizeBox }
        private Type _type;
        [Category("Custom Properties")]
        [DefaultValue(Type.CloseBox)]
        public Type ControlBoxType
        {
            get { return _type; }
            set
            {
                _type = value;
                Invalidate();
            }
        }

        private Color _normalColor = Color.Gainsboro;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color NormalColor
        {
            get { return _normalColor; }
            set
            {
                _normalColor = value;
                Invalidate();
            }
        }

        private Color _hoverColor = Color.LightCoral;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "LightCoral")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set
            {
                _hoverColor = value;
                Invalidate();
            }
        }

        private Color _iconColor = Color.Gray;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color IconColor
        {
            get { return _iconColor; }
            set
            {
                _iconColor = value;
                Invalidate();
            }
        }

        private Color _iconColorHover = Color.White;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "White")]
        public Color IconColorHover
        {
            get { return _iconColorHover; }
            set
            {
                _iconColorHover = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public override Image BackgroundImage { get => base.BackgroundImage; set { } }
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set { } }
        [Browsable(false)]
        public override Font Font { get => base.Font; set { } }
        [Browsable(false)]
        public override string Text { get => base.Text; set { } }
        [Browsable(false)]
        public override Color ForeColor { get => base.ForeColor; set { } }

        protected override void OnPaint(PaintEventArgs e)
        {
            _bufGraphics.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            _bufGraphics.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            _bufGraphics.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            _bufGraphics.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _bufGraphics.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string closeIcon = ((char)0x2715).ToString();
            string minimizeIcon = ((char)0x2E0F).ToString();
            string maximizeIcon = ((char)0x25A1).ToString();

            if (_type == Type.CloseBox)
            {
                if (!_isMouseHover)
                {
                    using (SolidBrush brushNormal = new SolidBrush(_normalColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushNormal, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(closeIcon, new Font("Segoe UI Symbol", 10F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(closeIcon, new Font("Segoe UI Symbol", 10F)).Height / 2) - 1;
                        TextRenderer.DrawText(_bufGraphics.Graphics, closeIcon, new Font("Segoe UI Symbol", 10F), new Point(centerX, centerY), _iconColor);
                    }
                }
                else
                {
                    using (SolidBrush brushHover = new SolidBrush(_hoverColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushHover, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(closeIcon, new Font("Segoe UI Symbol", 10F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(closeIcon, new Font("Segoe UI Symbol", 10F)).Height / 2) - 1;
                        TextRenderer.DrawText(_bufGraphics.Graphics, closeIcon, new Font("Segoe UI Symbol", 10F), new Point(centerX, centerY), _iconColorHover);
                    }
                }
            }
            else if (_type == Type.MinimizeBox)
            {
                if (!_isMouseHover)
                {
                    using (SolidBrush brushNormal = new SolidBrush(_normalColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushNormal, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(minimizeIcon, new Font("Segoe UI Symbol", 7F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(minimizeIcon, new Font("Segoe UI Symbol", 7F)).Height / 2) - 4;
                        TextRenderer.DrawText(_bufGraphics.Graphics, minimizeIcon, new Font("Segoe UI Symbol", 7F), new Point(centerX, centerY), _iconColor);
                    }
                }
                else
                {
                    using (SolidBrush brushHover = new SolidBrush(_hoverColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushHover, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(minimizeIcon, new Font("Segoe UI Symbol", 7F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(minimizeIcon, new Font("Segoe UI Symbol", 7F)).Height / 2) - 4;
                        TextRenderer.DrawText(_bufGraphics.Graphics, minimizeIcon, new Font("Segoe UI Symbol", 7F), new Point(centerX, centerY), _iconColorHover);
                    }
                }
            }
            else if (_type == Type.MaximizeBox)
            {
                if (!_isMouseHover)
                {
                    using (SolidBrush brushNormal = new SolidBrush(_normalColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushNormal, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(maximizeIcon, new Font("Segoe UI Symbol", 10F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(maximizeIcon, new Font("Segoe UI Symbol", 10F)).Height / 2) - 1;
                        TextRenderer.DrawText(_bufGraphics.Graphics, maximizeIcon, new Font("Segoe UI Symbol", 10F), new Point(centerX, centerY), _iconColor);
                    }
                }
                else
                {
                    using (SolidBrush brushHover = new SolidBrush(_hoverColor))
                    {
                        _bufGraphics.Graphics.FillRectangle(brushHover, ClientRectangle);
                        int centerX = (Width / 2) - (TextRenderer.MeasureText(maximizeIcon, new Font("Segoe UI Symbol", 10F)).Width / 2);
                        int centerY = (Height / 2) - (TextRenderer.MeasureText(maximizeIcon, new Font("Segoe UI Symbol", 10F)).Height / 2);
                        TextRenderer.DrawText(_bufGraphics.Graphics, maximizeIcon, new Font("Segoe UI Symbol", 10F), new Point(centerX, centerY), _iconColorHover);
                    }
                }
            }

            _bufGraphics.Render(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (Parent != null && BackColor == Color.Transparent)
            {
                Rectangle rect = new Rectangle(Left, Top, Width, Height);
                _bufGraphics.Graphics.TranslateTransform(-rect.X, -rect.Y);
                try
                {
                    using (PaintEventArgs pea = new PaintEventArgs(_bufGraphics.Graphics, rect))
                    {
                        pea.Graphics.SetClip(rect);
                        InvokePaintBackground(Parent, pea);
                        InvokePaint(Parent, pea);
                    }
                }
                finally
                {
                    _bufGraphics.Graphics.TranslateTransform(rect.X, rect.Y);
                }
            }
            else
            {
                using (SolidBrush backColor = new SolidBrush(BackColor))
                    _bufGraphics.Graphics.FillRectangle(backColor, ClientRectangle);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateGraphicsBuffer();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isMouseHover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isMouseHover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

    }
}
