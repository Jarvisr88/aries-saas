namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;

    [TypeConverter(typeof(#Nqe))]
    public class TwoColorLinearGradient : LinearGradient
    {
        private float #an;
        private float #Ure;
        private TwoColorLinearGradientStyle #JEd;

        public TwoColorLinearGradient()
        {
            this.ResetFocus();
            this.ResetScale();
            this.ResetStyle();
        }

        public TwoColorLinearGradient(Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style) : this()
        {
            base.StartColor = startColor;
            base.EndColor = endColor;
            base.Angle = angle;
            this.#JEd = style;
        }

        public TwoColorLinearGradient(Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style, BackgroundFillRotationType rotationType) : this()
        {
            base.StartColor = startColor;
            base.EndColor = endColor;
            base.Angle = angle;
            this.#JEd = style;
            base.RotationType = rotationType;
        }

        public TwoColorLinearGradient(Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style, float focus, float scale) : this()
        {
            base.StartColor = startColor;
            base.EndColor = endColor;
            base.Angle = angle;
            this.#JEd = style;
            this.#an = focus;
            this.#Ure = scale;
        }

        public TwoColorLinearGradient(Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style, float focus, float scale, BackgroundFillRotationType rotationType) : this()
        {
            base.StartColor = startColor;
            base.EndColor = endColor;
            base.Angle = angle;
            this.#JEd = style;
            this.#an = focus;
            this.#Ure = scale;
            base.RotationType = rotationType;
        }

        public override BackgroundFill Clone()
        {
            TwoColorLinearGradient gradient1 = new TwoColorLinearGradient();
            TwoColorLinearGradient gradient2 = new TwoColorLinearGradient();
            gradient2.Angle = this.Angle;
            TwoColorLinearGradient local2 = gradient2;
            TwoColorLinearGradient local3 = gradient2;
            local3.EndColor = this.EndColor;
            TwoColorLinearGradient local1 = local3;
            local1.Focus = this.Focus;
            local1.Scale = this.Scale;
            local1.StartColor = base.StartColor;
            local1.Style = this.Style;
            return local1;
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Sides side)
        {
            Draw(g, bounds, gradientBounds, base.StartColor, base.EndColor, base.#lwe(side), this.Style, this.Focus, this.Scale);
        }

        public static void Draw(Graphics g, GraphicsPath path, Rectangle gradientBounds, Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style)
        {
            Draw(g, path, gradientBounds, startColor, endColor, angle, style, 0.5f, 1f);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style)
        {
            Draw(g, bounds, gradientBounds, startColor, endColor, angle, style, 0.5f, 1f);
        }

        public static void Draw(Graphics g, GraphicsPath path, Rectangle gradientBounds, Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style, float focus, float scale)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(gradientBounds.Left - 0.01f, gradientBounds.Top - 0.01f, (float) gradientBounds.Width, (float) gradientBounds.Height), startColor, endColor, angle);
            if (style == TwoColorLinearGradientStyle.TriangleBump)
            {
                brush.SetBlendTriangularShape(focus, scale);
            }
            else if (style == TwoColorLinearGradientStyle.SigmaBellBump)
            {
                brush.SetSigmaBellShape(focus, scale);
            }
            g.FillPath(brush, path);
            brush.Dispose();
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color startColor, Color endColor, float angle, TwoColorLinearGradientStyle style, float focus, float scale)
        {
            if ((bounds.Width > 0) && ((bounds.Height > 0) && ((gradientBounds.Width > 0) && (gradientBounds.Height > 0))))
            {
                LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(gradientBounds.Left - 0.01f, gradientBounds.Top - 0.01f, (float) gradientBounds.Width, (float) gradientBounds.Height), startColor, endColor, angle);
                if (style == TwoColorLinearGradientStyle.TriangleBump)
                {
                    brush.SetBlendTriangularShape(focus, scale);
                }
                else if (style == TwoColorLinearGradientStyle.SigmaBellBump)
                {
                    brush.SetSigmaBellShape(focus, scale);
                }
                g.FillRectangle(brush, bounds);
                brush.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is TwoColorLinearGradient))
            {
                return false;
            }
            TwoColorLinearGradient gradient = this;
            TwoColorLinearGradient gradient2 = (TwoColorLinearGradient) obj;
            return (base.Equals(obj) && ((gradient.Focus == gradient2.Focus) && ((gradient.Scale == gradient2.Scale) && (gradient.Style == gradient2.Style))));
        }

        public override Brush GetBrush(Rectangle bounds, Sides side)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(bounds.Left - 0.01f, bounds.Top - 0.01f, (float) bounds.Width, (float) bounds.Height), base.StartColor, base.EndColor, base.#lwe(side));
            TwoColorLinearGradientStyle style = this.#JEd;
            if (style == TwoColorLinearGradientStyle.TriangleBump)
            {
                brush.SetBlendTriangularShape(this.#an, this.#Ure);
            }
            else if (style == TwoColorLinearGradientStyle.SigmaBellBump)
            {
                brush.SetSigmaBellShape(this.#an, this.#Ure);
            }
            return brush;
        }

        public override int GetHashCode() => 
            this.GetHashCode();

        public virtual void ResetFocus()
        {
            this.Focus = 0.5f;
        }

        public virtual void ResetScale()
        {
            this.Scale = 1f;
        }

        public virtual void ResetStyle()
        {
            this.Style = TwoColorLinearGradientStyle.Normal;
        }

        public virtual bool ShouldSerializeFocus() => 
            !(this.Focus == 0.5f);

        public virtual bool ShouldSerializeScale() => 
            !(this.Scale == 1f);

        public virtual bool ShouldSerializeStyle() => 
            this.Style != TwoColorLinearGradientStyle.Normal;

        [Category("Appearance"), Description("The gradient focus.  This value is a decimal between 0 and 1.")]
        public float Focus
        {
            get => 
                this.#an;
            set
            {
                if ((value < 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException(#G.#eg(0x2f5e), value, ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2a75)));
                }
                if (this.#an != value)
                {
                    this.#an = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The gradient scale.  This value is a decimal between 0 and 1.")]
        public float Scale
        {
            get => 
                this.#Ure;
            set
            {
                if ((value < 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException(#G.#eg(0x2f67), value, ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2ac2)));
                }
                if (this.#Ure != value)
                {
                    this.#Ure = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The style of the gradient."), Editor("ActiproSoftware.Drawing.Design.TwoColorLinearGradientStyleEditor, ActiproSoftware.Shared.WinForms.Design, Version=20.1.402.0, Culture=neutral, PublicKeyToken=c27e062d3c1a4763", typeof(UITypeEditor))]
        public TwoColorLinearGradientStyle Style
        {
            get => 
                this.#JEd;
            set
            {
                if (this.#JEd != value)
                {
                    this.#JEd = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

