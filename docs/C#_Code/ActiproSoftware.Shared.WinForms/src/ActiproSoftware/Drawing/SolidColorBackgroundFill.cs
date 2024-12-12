namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [TypeConverter(typeof(#Mqe))]
    public class SolidColorBackgroundFill : BackgroundFill
    {
        private System.Drawing.Color #eUb;

        public SolidColorBackgroundFill()
        {
            this.ResetColor();
        }

        public SolidColorBackgroundFill(System.Drawing.Color color) : this()
        {
            this.#eUb = color;
        }

        public override BackgroundFill Clone()
        {
            SolidColorBackgroundFill fill1 = new SolidColorBackgroundFill();
            SolidColorBackgroundFill fill2 = new SolidColorBackgroundFill();
            fill2.Color = this.Color;
            return fill2;
        }

        public static void Draw(Graphics g, Rectangle bounds, System.Drawing.Color color)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                Brush brush = new SolidBrush(color);
                g.FillRectangle(brush, bounds);
                brush.Dispose();
            }
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, Sides side)
        {
            Brush brush = new SolidBrush(this.#eUb);
            g.FillRectangle(brush, bounds);
            brush.Dispose();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is SolidColorBackgroundFill))
            {
                return false;
            }
            SolidColorBackgroundFill fill = this;
            SolidColorBackgroundFill fill2 = (SolidColorBackgroundFill) obj;
            return (base.Equals(obj) && (fill.Color == fill2.Color));
        }

        public override Brush GetBrush(Rectangle bounds, Sides side) => 
            new SolidBrush(this.#eUb);

        public override int GetHashCode() => 
            this.GetHashCode();

        public virtual void ResetColor()
        {
            this.Color = SystemColors.Control;
        }

        public virtual bool ShouldSerializeColor() => 
            this.Color != SystemColors.Control;

        [Category("Appearance"), Description("The color of the background fill."), RefreshProperties(RefreshProperties.All)]
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
    }
}

