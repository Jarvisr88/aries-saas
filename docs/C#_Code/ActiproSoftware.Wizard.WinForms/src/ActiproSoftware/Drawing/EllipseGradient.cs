namespace ActiproSoftware.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class EllipseGradient : Gradient
    {
        private Color #cre;
        private Color #dre;
        private Color #ere;

        public EllipseGradient()
        {
            this.ResetBackColor();
            this.ResetCenterColor();
            this.ResetOuterColor();
        }

        public EllipseGradient(Color centerColor, Color outerColor) : this(centerColor, outerColor, Color.Transparent)
        {
        }

        public EllipseGradient(Color centerColor, Color outerColor, Color backColor)
        {
            this.#dre = centerColor;
            this.#ere = outerColor;
            this.#cre = backColor;
        }

        public override BackgroundFill Clone()
        {
            EllipseGradient gradient1 = new EllipseGradient();
            EllipseGradient gradient2 = new EllipseGradient();
            gradient2.BackColor = this.BackColor;
            EllipseGradient local2 = gradient2;
            EllipseGradient local3 = gradient2;
            local3.CenterColor = this.CenterColor;
            EllipseGradient local1 = local3;
            local1.OuterColor = this.OuterColor;
            return local1;
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Sides side)
        {
            Draw(g, bounds, gradientBounds, this.CenterColor, this.OuterColor, this.BackColor);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color centerColor, Color outerColor)
        {
            Draw(g, bounds, gradientBounds, centerColor, outerColor, Color.Transparent);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color centerColor, Color outerColor, Color backColor)
        {
            if ((bounds.Width > 0) && ((bounds.Height > 0) && ((gradientBounds.Width > 0) && (gradientBounds.Height > 0))))
            {
                if (backColor != Color.Transparent)
                {
                    Brush brush2 = new SolidBrush(backColor);
                    g.FillRectangle(brush2, bounds);
                    brush2.Dispose();
                }
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(gradientBounds);
                PathGradientBrush brush = new PathGradientBrush(path) {
                    CenterColor = centerColor
                };
                brush.SurroundColors = new Color[] { outerColor };
                brush.CenterPoint = (PointF) new Point(gradientBounds.X + (gradientBounds.Width / 2), gradientBounds.Y + (gradientBounds.Height / 2));
                g.FillRectangle(brush, bounds);
                brush.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is EllipseGradient))
            {
                return false;
            }
            EllipseGradient gradient = this;
            EllipseGradient gradient2 = (EllipseGradient) obj;
            return ((gradient.BackColor == gradient2.BackColor) && ((gradient.CenterColor == gradient2.CenterColor) && (gradient.OuterColor == gradient2.OuterColor)));
        }

        public override Brush GetBrush(Rectangle bounds, Sides side)
        {
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(bounds);
            PathGradientBrush brush = new PathGradientBrush(path) {
                CenterColor = this.#dre
            };
            brush.SurroundColors = new Color[] { this.#ere };
            brush.CenterPoint = (PointF) new Point(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2));
            return brush;
        }

        public override int GetHashCode() => 
            this.#dre.GetHashCode();

        public virtual void ResetBackColor()
        {
            this.BackColor = Color.Transparent;
        }

        public virtual void ResetCenterColor()
        {
            this.CenterColor = SystemColors.Control;
        }

        public virtual void ResetOuterColor()
        {
            this.OuterColor = SystemColors.ControlDark;
        }

        public virtual bool ShouldSerializeBackColor() => 
            this.BackColor != Color.Transparent;

        public virtual bool ShouldSerializeCenterColor() => 
            this.CenterColor != SystemColors.Control;

        public virtual bool ShouldSerializeOuterColor() => 
            this.OuterColor != SystemColors.ControlDark;

        [Category("Appearance"), Description("The background color of the gradient."), RefreshProperties(RefreshProperties.All)]
        public Color BackColor
        {
            get => 
                this.#cre;
            set
            {
                if (this.#cre != value)
                {
                    this.#cre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The background color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte BackColorAlpha
        {
            get
            {
                Color backColor = this.BackColor;
                return backColor.A;
            }
            set => 
                this.BackColor = Color.FromArgb(value, this.BackColor);
        }

        [Category("Appearance"), Description("The center color of the gradient."), RefreshProperties(RefreshProperties.All)]
        public Color CenterColor
        {
            get => 
                this.#dre;
            set
            {
                if (this.#dre != value)
                {
                    this.#dre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The center color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte CenterColorAlpha
        {
            get
            {
                Color centerColor = this.CenterColor;
                return centerColor.A;
            }
            set => 
                this.CenterColor = Color.FromArgb(value, this.CenterColor);
        }

        [Category("Appearance"), Description("The outer color of the gradient."), RefreshProperties(RefreshProperties.All)]
        public Color OuterColor
        {
            get => 
                this.#ere;
            set
            {
                if (this.#ere != value)
                {
                    this.#ere = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The outer color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte OuterColorAlpha
        {
            get
            {
                Color outerColor = this.OuterColor;
                return outerColor.A;
            }
            set => 
                this.OuterColor = Color.FromArgb(value, this.OuterColor);
        }
    }
}

