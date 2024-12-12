namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [TypeConverter(typeof(#Gqe))]
    public class HatchedBackgroundFill : BackgroundFill
    {
        private Color #qTb;
        private Color #rTb;

        public HatchedBackgroundFill()
        {
            this.ResetColor1();
            this.ResetColor2();
        }

        public HatchedBackgroundFill(Color color) : this(color, Color.Transparent)
        {
        }

        public HatchedBackgroundFill(Color color1, Color color2) : this()
        {
            this.#qTb = color1;
            this.#rTb = color2;
        }

        public override BackgroundFill Clone()
        {
            HatchedBackgroundFill fill1 = new HatchedBackgroundFill();
            HatchedBackgroundFill fill2 = new HatchedBackgroundFill();
            fill2.Color1 = this.Color1;
            HatchedBackgroundFill local1 = fill2;
            HatchedBackgroundFill local2 = fill2;
            local2.Color2 = this.Color2;
            return local2;
        }

        public static void Draw(Graphics g, Rectangle bounds, Color color1, Color color2)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                Brush hatchedBrush = DrawingHelper.GetHatchedBrush(color1, color2);
                g.FillRectangle(hatchedBrush, bounds);
                hatchedBrush.Dispose();
            }
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, Sides side)
        {
            Draw(g, bounds, this.Color1, this.Color2);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is HatchedBackgroundFill))
            {
                return false;
            }
            HatchedBackgroundFill fill = this;
            HatchedBackgroundFill fill2 = (HatchedBackgroundFill) obj;
            return (base.Equals(obj) && ((fill.Color1 == fill2.Color1) && (fill.Color2 == fill2.Color2)));
        }

        public override Brush GetBrush(Rectangle bounds, Sides side) => 
            DrawingHelper.GetHatchedBrush(this.#qTb, this.#rTb);

        public override int GetHashCode() => 
            this.GetHashCode();

        public virtual void ResetColor1()
        {
            this.Color1 = SystemColors.Control;
        }

        public virtual void ResetColor2()
        {
            this.Color2 = Color.Transparent;
        }

        public virtual bool ShouldSerializeColor1() => 
            this.Color1 != SystemColors.Control;

        public virtual bool ShouldSerializeColor2() => 
            this.Color2 != Color.Transparent;

        [Category("Appearance"), Description("The first color of the background fill."), RefreshProperties(RefreshProperties.All)]
        public Color Color1
        {
            get => 
                this.#qTb;
            set
            {
                if (this.#qTb != value)
                {
                    this.#qTb = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The second color of the background fill."), RefreshProperties(RefreshProperties.All)]
        public Color Color2
        {
            get => 
                this.#rTb;
            set
            {
                if (this.#rTb != value)
                {
                    this.#rTb = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

