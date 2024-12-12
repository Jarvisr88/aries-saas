namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [TypeConverter(typeof(#Iqe))]
    public class LinearGradientColorPosition
    {
        private System.Drawing.Color #eUb;
        private float #2cc;

        [Category("Action"), Description("Occurs after a property is changed.")]
        public event EventHandler PropertyChanged;

        public LinearGradientColorPosition()
        {
            this.ResetColor();
            this.ResetPosition();
        }

        public LinearGradientColorPosition(System.Drawing.Color color, float position)
        {
            this.#eUb = color;
            this.#2cc = position;
        }

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (this.#Sj != null)
            {
                this.#Sj(this, e);
            }
        }

        public virtual void ResetColor()
        {
            this.Color = SystemColors.Control;
        }

        public virtual void ResetPosition()
        {
            this.Position = 0.5f;
        }

        public virtual bool ShouldSerializeColor() => 
            this.Color != SystemColors.Control;

        public virtual bool ShouldSerializePosition() => 
            !(this.Position == 0.5f);

        [Category("Appearance"), Description("The color of the gradient step."), RefreshProperties(RefreshProperties.All)]
        public virtual System.Drawing.Color Color
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
        public virtual byte ColorAlpha
        {
            get
            {
                System.Drawing.Color color = this.Color;
                return color.A;
            }
            set => 
                this.Color = System.Drawing.Color.FromArgb(value, this.Color);
        }

        [Category("Appearance"), Description("The percentage of distance along the gradient line.  This value is a decimal between 0 and 1.")]
        public virtual float Position
        {
            get => 
                this.#2cc;
            set
            {
                if ((value < 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException(#G.#eg(0x2f04), value, ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x298e)));
                }
                if (this.#2cc != value)
                {
                    this.#2cc = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

