namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [TypeConverter(typeof(#Lqe))]
    public class SimpleBorder : Border
    {
        private System.Drawing.Color #eUb;
        private SimpleBorderStyle #JEd;

        public SimpleBorder()
        {
            this.ResetColor();
            this.ResetStyle();
        }

        public SimpleBorder(SimpleBorderStyle style, System.Drawing.Color color) : this()
        {
            this.#JEd = style;
            this.#eUb = color;
        }

        public override Border Clone()
        {
            SimpleBorder border1 = new SimpleBorder();
            SimpleBorder border2 = new SimpleBorder();
            border2.Color = this.Color;
            SimpleBorder local1 = border2;
            SimpleBorder local2 = border2;
            local2.Style = this.Style;
            return local2;
        }

        public override void Draw(Graphics g, Rectangle bounds, Sides sides)
        {
            Draw(g, bounds, this.#JEd, this.#eUb, sides);
        }

        public static void Draw(Graphics g, Rectangle bounds, SimpleBorderStyle borderStyle, System.Drawing.Color borderColor)
        {
            Draw(g, bounds, borderStyle, borderColor, Sides.Left | Sides.Bottom | Sides.Right | Sides.Top);
        }

        public static void Draw(Graphics g, Rectangle bounds, SimpleBorderStyle borderStyle, System.Drawing.Color borderColor, Sides sides)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                switch (borderStyle)
                {
                    case SimpleBorderStyle.Solid:
                    {
                        Pen pen = new Pen(borderColor);
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
                        }
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
                        }
                        pen.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.DoubleSolid:
                    {
                        Pen pen2 = new Pen(borderColor);
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen2, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
                            g.DrawLine(pen2, bounds.Left, bounds.Top + 1, bounds.Right - 1, bounds.Top + 1);
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen2, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
                            g.DrawLine(pen2, bounds.Right - 2, bounds.Top, bounds.Right - 2, bounds.Bottom - 1);
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen2, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
                            g.DrawLine(pen2, bounds.Left, bounds.Bottom - 2, bounds.Right - 1, bounds.Bottom - 2);
                        }
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen2, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
                            g.DrawLine(pen2, bounds.Left + 1, bounds.Top, bounds.Left + 1, bounds.Bottom - 1);
                        }
                        pen2.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.SinglePixelRoundedSolid:
                    {
                        Pen pen3 = new Pen(borderColor);
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen3, bounds.Left + 1, bounds.Top, bounds.Right - 2, bounds.Top);
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen3, (int) (bounds.Right - 1), (int) (bounds.Top + 1), (int) (bounds.Right - 1), (int) (bounds.Bottom - 2));
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen3, (int) (bounds.Left + 1), (int) (bounds.Bottom - 1), (int) (bounds.Right - 2), (int) (bounds.Bottom - 1));
                        }
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen3, bounds.Left, bounds.Top + 1, bounds.Left, bounds.Bottom - 2);
                        }
                        pen3.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.SinglePixelRoundedTopSolid:
                    {
                        Pen pen4 = new Pen(borderColor);
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen4, bounds.Left + 1, bounds.Top, bounds.Right - 2, bounds.Top);
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen4, (int) (bounds.Right - 1), (int) (bounds.Top + 1), (int) (bounds.Right - 1), (int) (bounds.Bottom - 1));
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen4, (int) (bounds.Left + 1), (int) (bounds.Bottom - 1), (int) (bounds.Right - 2), (int) (bounds.Bottom - 1));
                        }
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen4, bounds.Left, bounds.Top + 1, bounds.Left, bounds.Bottom - 1);
                        }
                        pen4.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.DoublePixelRoundedSolid:
                    {
                        Pen pen5 = new Pen(borderColor);
                        Point[] points = new Point[9];
                        points[0] = new Point(bounds.Left, bounds.Top + 2);
                        points[1] = new Point(bounds.Left + 2, bounds.Top);
                        points[2] = new Point(bounds.Right - 3, bounds.Top);
                        points[3] = new Point(bounds.Right - 1, bounds.Top + 2);
                        points[4] = new Point(bounds.Right - 1, bounds.Bottom - 3);
                        points[5] = new Point(bounds.Right - 3, bounds.Bottom - 1);
                        points[6] = new Point(bounds.Left + 2, bounds.Bottom - 1);
                        points[7] = new Point(bounds.Left, bounds.Bottom - 3);
                        points[8] = new Point(bounds.Left, bounds.Top + 2);
                        g.DrawLines(pen5, points);
                        pen5.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.Dashed:
                        ControlPaint.DrawBorder(g, bounds, borderColor, 1, ((sides & Sides.Left) == Sides.Left) ? ButtonBorderStyle.Dashed : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Top) == Sides.Top) ? ButtonBorderStyle.Dashed : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Right) == Sides.Right) ? ButtonBorderStyle.Dashed : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Bottom) == Sides.Bottom) ? ButtonBorderStyle.Dashed : ButtonBorderStyle.None);
                        return;

                    case SimpleBorderStyle.Dotted:
                        ControlPaint.DrawBorder(g, bounds, borderColor, 1, ((sides & Sides.Left) == Sides.Left) ? ButtonBorderStyle.Dotted : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Top) == Sides.Top) ? ButtonBorderStyle.Dotted : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Right) == Sides.Right) ? ButtonBorderStyle.Dotted : ButtonBorderStyle.None, borderColor, 1, ((sides & Sides.Bottom) == Sides.Bottom) ? ButtonBorderStyle.Dotted : ButtonBorderStyle.None);
                        return;

                    case SimpleBorderStyle.Raised:
                    case SimpleBorderStyle.Sunken:
                    {
                        System.Drawing.Color controlLightLight;
                        System.Drawing.Color controlDarkDark;
                        if (borderColor == SystemColors.Control)
                        {
                            controlLightLight = SystemColors.ControlLightLight;
                            controlDarkDark = SystemColors.ControlDarkDark;
                        }
                        else
                        {
                            controlLightLight = ControlPaint.LightLight(borderColor);
                            controlDarkDark = ControlPaint.DarkDark(borderColor);
                        }
                        Pen pen6 = new Pen((borderStyle == SimpleBorderStyle.Raised) ? controlLightLight : controlDarkDark);
                        Pen pen7 = new Pen((borderStyle == SimpleBorderStyle.Raised) ? controlDarkDark : controlLightLight);
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen6, new Point(bounds.Left, bounds.Bottom - 2), bounds.Location);
                        }
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen6, bounds.Location, new Point(bounds.Right - 1, bounds.Top));
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen7, new Point(bounds.Right - 1, bounds.Top + 1), new Point(bounds.Right - 1, bounds.Bottom - 1));
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen7, new Point(bounds.Right - 1, bounds.Bottom - 1), new Point(bounds.Left, bounds.Bottom - 1));
                        }
                        pen6.Dispose();
                        pen7.Dispose();
                        if (borderColor == SystemColors.Control)
                        {
                            controlLightLight = SystemColors.ControlLight;
                            controlDarkDark = SystemColors.ControlDark;
                        }
                        else
                        {
                            controlLightLight = ControlPaint.Light(borderColor);
                            controlDarkDark = ControlPaint.Dark(borderColor);
                        }
                        pen6 = new Pen((borderStyle == SimpleBorderStyle.Raised) ? controlLightLight : controlDarkDark);
                        pen7 = new Pen((borderStyle == SimpleBorderStyle.Raised) ? controlDarkDark : controlLightLight);
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen6, new Point(bounds.Left + 1, bounds.Bottom - 3), new Point(bounds.Left + 1, bounds.Top + 1));
                        }
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen6, new Point(bounds.Left + 1, bounds.Top + 1), new Point(bounds.Right - 2, bounds.Top + 1));
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen7, new Point(bounds.Right - 2, bounds.Top + 2), new Point(bounds.Right - 2, bounds.Bottom - 2));
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen7, new Point(bounds.Right - 2, bounds.Bottom - 2), new Point(bounds.Left + 1, bounds.Bottom - 2));
                        }
                        pen6.Dispose();
                        pen7.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.RaisedInner:
                    case SimpleBorderStyle.SunkenInner:
                    {
                        System.Drawing.Color controlLightLight;
                        System.Drawing.Color controlDark;
                        if (borderColor == SystemColors.Control)
                        {
                            controlLightLight = SystemColors.ControlLightLight;
                            controlDark = SystemColors.ControlDark;
                        }
                        else
                        {
                            controlLightLight = ControlPaint.LightLight(borderColor);
                            controlDark = ControlPaint.Dark(borderColor);
                        }
                        Pen pen8 = new Pen((borderStyle == SimpleBorderStyle.RaisedInner) ? controlLightLight : controlDark);
                        Pen pen9 = new Pen((borderStyle == SimpleBorderStyle.RaisedInner) ? controlDark : controlLightLight);
                        if ((sides & Sides.Left) == Sides.Left)
                        {
                            g.DrawLine(pen8, new Point(bounds.Left, bounds.Bottom - 2), bounds.Location);
                        }
                        if ((sides & Sides.Top) == Sides.Top)
                        {
                            g.DrawLine(pen8, bounds.Location, new Point(bounds.Right - 1, bounds.Top));
                        }
                        if ((sides & Sides.Right) == Sides.Right)
                        {
                            g.DrawLine(pen9, new Point(bounds.Right - 1, bounds.Top + 1), new Point(bounds.Right - 1, bounds.Bottom - 1));
                        }
                        if ((sides & Sides.Bottom) == Sides.Bottom)
                        {
                            g.DrawLine(pen9, new Point(bounds.Right - 1, bounds.Bottom - 1), new Point(bounds.Left, bounds.Bottom - 1));
                        }
                        pen8.Dispose();
                        pen9.Dispose();
                        return;
                    }
                    case SimpleBorderStyle.Bump:
                    case SimpleBorderStyle.Etched:
                        System.Drawing.Color controlLightLight;
                        System.Drawing.Color controlDark;
                        if (borderColor == SystemColors.Control)
                        {
                            controlLightLight = SystemColors.ControlLightLight;
                            controlDark = SystemColors.ControlDark;
                        }
                        else
                        {
                            controlLightLight = ControlPaint.Light(borderColor);
                            controlDark = ControlPaint.Dark(borderColor);
                        }
                        ControlPaint.DrawBorder(g, bounds, (borderStyle == SimpleBorderStyle.Bump) ? controlDark : controlLightLight, ButtonBorderStyle.Solid);
                        ControlPaint.DrawBorder(g, new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2), (borderStyle == SimpleBorderStyle.Bump) ? controlDark : controlLightLight, ButtonBorderStyle.Solid);
                        ControlPaint.DrawBorder(g, new Rectangle(bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1), (borderStyle == SimpleBorderStyle.Bump) ? controlLightLight : controlDark, ButtonBorderStyle.Solid);
                        return;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is SimpleBorder))
            {
                return false;
            }
            SimpleBorder border = this;
            SimpleBorder border2 = (SimpleBorder) obj;
            return (base.Equals(obj) && ((border.Color == border2.Color) && (border.Style == border2.Style)));
        }

        public override int GetBorderWidth() => 
            GetBorderWidth(this.#JEd);

        public static int GetBorderWidth(SimpleBorderStyle borderStyle) => 
            (borderStyle == SimpleBorderStyle.None) ? 0 : (((borderStyle == SimpleBorderStyle.DoubleSolid) || ((borderStyle - 8) <= SimpleBorderStyle.Solid)) ? 2 : 1);

        public override int GetHashCode() => 
            this.GetHashCode();

        public override System.Drawing.Color GetPrimaryColor() => 
            this.#eUb;

        public virtual void ResetColor()
        {
            this.Color = SystemColors.ControlDark;
        }

        public virtual void ResetStyle()
        {
            this.Style = SimpleBorderStyle.Solid;
        }

        public virtual bool ShouldSerializeColor() => 
            this.Color != SystemColors.ControlDark;

        public virtual bool ShouldSerializeStyle() => 
            this.Style != SimpleBorderStyle.Solid;

        [Category("Appearance"), Description("The base color of the border."), RefreshProperties(RefreshProperties.All)]
        public System.Drawing.Color Color
        {
            get => 
                this.#eUb;
            set
            {
                if (this.#eUb != value)
                {
                    this.#eUb = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte ColorAlpha
        {
            get
            {
                System.Drawing.Color color = this.Color;
                return color.A;
            }
            set => 
                this.Color = System.Drawing.Color.FromArgb(value, this.Color);
        }

        [Category("Appearance"), Description("The style of the border."), RefreshProperties(RefreshProperties.All)]
        public SimpleBorderStyle Style
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

