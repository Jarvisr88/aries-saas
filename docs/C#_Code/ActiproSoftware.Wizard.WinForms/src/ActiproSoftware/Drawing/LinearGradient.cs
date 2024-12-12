namespace ActiproSoftware.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public abstract class LinearGradient : Gradient
    {
        private float #fre;
        private Color #gre;
        private BackgroundFillRotationType #hre;
        private Color #ire;

        internal float #lwe(Sides #Fwf)
        {
            if (this.#hre != BackgroundFillRotationType.None)
            {
                if (#Fwf == Sides.Right)
                {
                    return (this.#fre + 90f);
                }
                if (#Fwf != Sides.Bottom)
                {
                    if (#Fwf == Sides.Left)
                    {
                        return ((this.#hre != BackgroundFillRotationType.Side) ? (this.#fre + 90f) : (this.#fre + 270f));
                    }
                }
                else if (this.#hre == BackgroundFillRotationType.Side)
                {
                    return (this.#fre + 180f);
                }
            }
            return this.#fre;
        }

        public LinearGradient()
        {
            this.ResetAngle();
            this.ResetEndColor();
            this.ResetStartColor();
        }

        public LinearGradient(Color startColor, Color endColor) : this()
        {
            this.#ire = startColor;
            this.#gre = endColor;
        }

        public LinearGradient(Color startColor, Color endColor, float angle) : this(startColor, endColor)
        {
            this.#fre = angle;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is LinearGradient))
            {
                return false;
            }
            LinearGradient gradient = this;
            LinearGradient gradient2 = (LinearGradient) obj;
            return ((gradient.Angle == gradient2.Angle) && ((gradient.EndColor == gradient2.EndColor) && (gradient.StartColor == gradient2.StartColor)));
        }

        public override int GetHashCode() => 
            this.#fre.GetHashCode();

        public virtual void ResetAngle()
        {
            this.Angle = 0f;
        }

        public virtual void ResetEndColor()
        {
            this.EndColor = SystemColors.ControlDark;
        }

        public virtual void ResetStartColor()
        {
            this.StartColor = SystemColors.ControlLight;
        }

        public virtual bool ShouldSerializeAngle() => 
            !(this.Angle == 0f);

        public virtual bool ShouldSerializeEndColor() => 
            this.EndColor != SystemColors.ControlDark;

        public virtual bool ShouldSerializeStartColor() => 
            this.StartColor != SystemColors.ControlLight;

        [Category("Appearance"), Description("The angle, measured in degrees clockwise from the x-axis, that defines the orientation of the gradient.")]
        public float Angle
        {
            get => 
                this.#fre;
            set
            {
                if (this.#fre != value)
                {
                    this.#fre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The end color of the gradient."), RefreshProperties(RefreshProperties.All)]
        public Color EndColor
        {
            get => 
                this.#gre;
            set
            {
                if (this.#gre != value)
                {
                    this.#gre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The end color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte EndColorAlpha
        {
            get
            {
                Color endColor = this.EndColor;
                return endColor.A;
            }
            set => 
                this.EndColor = Color.FromArgb(value, this.EndColor);
        }

        [Category("Appearance"), Description("The type of rotation to perform when drawing the background fill for a side."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public BackgroundFillRotationType RotationType
        {
            get => 
                this.#hre;
            set
            {
                if (this.#hre != value)
                {
                    this.#hre = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The start color of the gradient."), RefreshProperties(RefreshProperties.All)]
        public Color StartColor
        {
            get => 
                this.#ire;
            set
            {
                if (this.#ire != value)
                {
                    this.#ire = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The start color alpha value."), DefaultValue((byte) 0xff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public byte StartColorAlpha
        {
            get
            {
                Color startColor = this.StartColor;
                return startColor.A;
            }
            set => 
                this.StartColor = Color.FromArgb(value, this.StartColor);
        }
    }
}

