namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class MultiColorLinearGradient : LinearGradient
    {
        private LinearGradientColorPosition[] #jre;

        public MultiColorLinearGradient()
        {
            this.ResetIntermediateColors();
        }

        public MultiColorLinearGradient(Color startColor, Color middleColor, Color endColor, float angle) : this(startColor, positionArray1, endColor, angle, BackgroundFillRotationType.None)
        {
            LinearGradientColorPosition[] positionArray1 = new LinearGradientColorPosition[] { new LinearGradientColorPosition(middleColor, 0.5f) };
        }

        public MultiColorLinearGradient(Color startColor, Color middleColor, Color endColor, float angle, BackgroundFillRotationType rotationType) : this(startColor, positionArray1, endColor, angle, rotationType)
        {
            LinearGradientColorPosition[] positionArray1 = new LinearGradientColorPosition[] { new LinearGradientColorPosition(middleColor, 0.5f) };
        }

        public MultiColorLinearGradient(Color startColor, LinearGradientColorPosition[] intermediateColors, Color endColor, float angle, BackgroundFillRotationType rotationType) : this()
        {
            base.StartColor = startColor;
            this.#jre = intermediateColors;
            base.EndColor = endColor;
            base.Angle = angle;
            base.RotationType = rotationType;
        }

        public override BackgroundFill Clone()
        {
            MultiColorLinearGradient gradient1 = new MultiColorLinearGradient();
            MultiColorLinearGradient gradient2 = new MultiColorLinearGradient();
            gradient2.Angle = this.Angle;
            MultiColorLinearGradient local2 = gradient2;
            MultiColorLinearGradient local3 = gradient2;
            local3.EndColor = this.EndColor;
            MultiColorLinearGradient local1 = local3;
            local1.IntermediateColors = this.IntermediateColors;
            local1.StartColor = base.StartColor;
            return local1;
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Sides side)
        {
            Draw(g, bounds, gradientBounds, base.StartColor, this.IntermediateColors, base.EndColor, base.#lwe(side));
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color startColor, Color middleColor, Color endColor, float angle)
        {
            LinearGradientColorPosition[] intermediateColors = new LinearGradientColorPosition[] { new LinearGradientColorPosition(middleColor, 0.5f) };
            Draw(g, bounds, gradientBounds, startColor, intermediateColors, endColor, angle);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle gradientBounds, Color startColor, LinearGradientColorPosition[] intermediateColors, Color endColor, float angle)
        {
            if ((bounds.Width > 0) && ((bounds.Height > 0) && ((gradientBounds.Width > 0) && (gradientBounds.Height > 0))))
            {
                LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(gradientBounds.Left - 0.01f, gradientBounds.Top - 0.01f, (float) gradientBounds.Width, (float) gradientBounds.Height), startColor, endColor, angle);
                if ((intermediateColors != null) && (intermediateColors.Length != 0))
                {
                    int count = intermediateColors.Length + 2;
                    ColorBlend blend = new ColorBlend(count) {
                        Colors = new Color[count]
                    };
                    blend.Colors[0] = startColor;
                    blend.Colors[count - 1] = endColor;
                    blend.Positions = new float[count];
                    blend.Positions[0] = 0f;
                    blend.Positions[count - 1] = 1f;
                    int index = 0;
                    while (true)
                    {
                        if (index >= intermediateColors.Length)
                        {
                            brush.InterpolationColors = blend;
                            break;
                        }
                        blend.Colors[index + 1] = intermediateColors[index].Color;
                        blend.Positions[index + 1] = intermediateColors[index].Position;
                        index++;
                    }
                }
                g.FillRectangle(brush, bounds);
                brush.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj != null) && (obj is MultiColorLinearGradient))
            {
                MultiColorLinearGradient gradient = this;
                MultiColorLinearGradient gradient2 = (MultiColorLinearGradient) obj;
                if (base.Equals(obj))
                {
                    if (gradient.IntermediateColors == null)
                    {
                        return (gradient2.IntermediateColors == null);
                    }
                    if ((gradient2.IntermediateColors != null) && (gradient.IntermediateColors.Length == gradient2.IntermediateColors.Length))
                    {
                        for (int i = 0; i < gradient.IntermediateColors.Length; i++)
                        {
                            if ((gradient.IntermediateColors[i] == null) || gradient.IntermediateColors[i].Equals(gradient2.IntermediateColors[i]))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public override Brush GetBrush(Rectangle bounds, Sides side)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(bounds.Left - 0.01f, bounds.Top - 0.01f, (float) bounds.Width, (float) bounds.Height), base.StartColor, base.EndColor, base.#lwe(side));
            if ((this.#jre != null) && (this.#jre.Length != 0))
            {
                int count = this.#jre.Length + 2;
                ColorBlend blend = new ColorBlend(count) {
                    Colors = new Color[count]
                };
                blend.Colors[0] = base.StartColor;
                blend.Colors[count - 1] = base.EndColor;
                blend.Positions = new float[count];
                blend.Positions[0] = 0f;
                blend.Positions[count - 1] = 1f;
                int index = 0;
                while (true)
                {
                    if (index >= this.#jre.Length)
                    {
                        brush.InterpolationColors = blend;
                        break;
                    }
                    blend.Colors[index + 1] = this.#jre[index].Color;
                    blend.Positions[index + 1] = this.#jre[index].Position;
                    index++;
                }
            }
            return brush;
        }

        public override int GetHashCode() => 
            this.GetHashCode();

        public virtual void ResetIntermediateColors()
        {
            this.IntermediateColors = new LinearGradientColorPosition[0];
        }

        public virtual bool ShouldSerializeIntermediateColors() => 
            (this.IntermediateColors != null) && (this.IntermediateColors.Length != 0);

        [Category("Appearance"), Description("The intermediate colors of the gradient."), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(#Hqe))]
        public LinearGradientColorPosition[] IntermediateColors
        {
            get => 
                this.#jre;
            set
            {
                if (this.#jre != value)
                {
                    this.#jre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

