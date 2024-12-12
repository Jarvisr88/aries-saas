namespace DevExpress.Office.Model
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ColorHSL
    {
        private static readonly ColorHSL defaultValue;
        private const float MaxAngle = 2.16E+07f;
        private const float MaxThousandthOfPercentage = 100000f;
        private float hue;
        private float saturation;
        private float luminance;
        public static ColorHSL DefaultValue =>
            defaultValue;
        public static ColorHSL FromColorRGB(Color color) => 
            new ColorHSL((Math.Max(Math.Max(color.R, color.G), color.B) == Math.Min(Math.Min(color.R, color.G), color.B)) ? 0.6666667f : (color.GetHue() / 360f), color.GetSaturation(), color.GetBrightness());

        public static Color CalculateColorRGB(Color color, double tint) => 
            ((color == DXColor.Empty) || (tint == 0.0)) ? color : FromColorRGB(color).ApplyTint(tint).ToRgb();

        public ColorHSL(float hue, float saturation, float luminance)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.luminance = luminance;
        }

        public ColorHSL(int hue, int saturation, int luminance) : this((float) (((float) hue) / 2.16E+07f), (float) (((float) saturation) / 100000f), (float) (((float) luminance) / 100000f))
        {
        }

        public int Hue
        {
            get => 
                this.GetIntValue(this.hue, 2.16E+07f);
            set => 
                this.hue = this.GetFloatValue(this.GetValidValue((float) value), 2.16E+07f);
        }
        public int Saturation
        {
            get => 
                this.GetIntValue(this.saturation, 100000f);
            set => 
                this.saturation = this.GetFloatValue(this.GetValidValue((float) value), 100000f);
        }
        public int Luminance
        {
            get => 
                this.GetIntValue(this.luminance, 100000f);
            set => 
                this.luminance = this.GetFloatValue(this.GetValidValue((float) value), 100000f);
        }
        public float FloatHue
        {
            get => 
                this.hue;
            set => 
                this.hue = this.GetValidValue(value);
        }
        public float FloatSaturation
        {
            get => 
                this.saturation;
            set => 
                this.saturation = this.GetValidValue(value);
        }
        public float FloatLuminance
        {
            get => 
                this.luminance;
            set => 
                this.luminance = this.GetValidValue(value);
        }
        private float GetValidValue(float value) => 
            (value > 1f) ? 1f : ((value < 0f) ? 0f : value);

        private int GetIntValue(float value, float maxValue) => 
            (int) Math.Round((double) (value * maxValue));

        private float GetFloatValue(float value, float maxValue) => 
            value / maxValue;

        public unsafe Color ToRgb()
        {
            float num = (this.luminance < 0.5) ? (this.luminance * (1f + this.saturation)) : ((this.luminance + this.saturation) - (this.luminance * this.saturation));
            float num2 = (2f * this.luminance) - num;
            float[] numArray = new float[] { this.hue + 0.3333333f, this.hue, this.hue - 0.3333333f };
            for (int i = 0; i < 3; i++)
            {
                if (numArray[i] < 0f)
                {
                    float* singlePtr1 = &(numArray[i]);
                    singlePtr1[0]++;
                }
                if (numArray[i] > 1f)
                {
                    float* singlePtr2 = &(numArray[i]);
                    singlePtr2[0]--;
                }
                numArray[i] = ((6f * numArray[i]) >= 1f) ? ((((6f * numArray[i]) < 1f) || ((6f * numArray[i]) >= 3f)) ? ((((6f * numArray[i]) < 3f) || ((6f * numArray[i]) >= 4f)) ? num2 : (num2 + ((num - num2) * (4f - (6f * numArray[i]))))) : num) : (num2 + (((num - num2) * 6f) * numArray[i]));
            }
            return DXColor.FromArgb(this.ToIntValue(numArray[0]), this.ToIntValue(numArray[1]), this.ToIntValue(numArray[2]));
        }

        private int ToIntValue(float value) => 
            this.FixIntValue((int) Math.Round((double) (255f * value), 0));

        private int FixIntValue(int value) => 
            (value < 0) ? 0 : ((value > 0xff) ? 0xff : value);

        private ColorHSL ApplyTint(double tint)
        {
            if (tint < 0.0)
            {
                this.luminance *= 1f + ((float) tint);
            }
            if (tint > 0.0)
            {
                this.luminance = (this.luminance * (1f - ((float) tint))) + ((float) tint);
            }
            return this;
        }

        public ColorHSL GetComplementColor()
        {
            this.hue += (this.hue > 0.5) ? -0.5f : 0.5f;
            return this;
        }

        public ColorHSL ApplyHue(int value)
        {
            this.FloatHue = ((float) value) / 2.16E+07f;
            return this;
        }

        public ColorHSL ApplyHueMod(int value)
        {
            this.hue = (this.hue * value) / 100000f;
            this.FixHue();
            return this;
        }

        public ColorHSL ApplyHueOffset(int value)
        {
            this.hue += ((float) value) / 2.16E+07f;
            this.FixHue();
            return this;
        }

        private void FixHue()
        {
            if (this.hue > 1f)
            {
                this.hue -= (int) this.hue;
            }
        }

        public ColorHSL ApplySaturation(int value)
        {
            this.FloatSaturation = ((float) value) / 100000f;
            return this;
        }

        public ColorHSL ApplySaturationMod(int value)
        {
            this.saturation = (this.saturation * value) / 100000f;
            return this;
        }

        public ColorHSL ApplySaturationOffset(int value)
        {
            this.saturation += ((float) value) / 100000f;
            return this;
        }

        public ColorHSL ApplyLuminance(int value)
        {
            this.FloatLuminance = ((float) value) / 100000f;
            return this;
        }

        public ColorHSL ApplyLuminanceMod(int value)
        {
            this.luminance = (this.luminance * value) / 100000f;
            return this;
        }

        public ColorHSL ApplyLuminanceOffset(int value)
        {
            this.luminance += ((float) value) / 100000f;
            return this;
        }

        static ColorHSL()
        {
            defaultValue = new ColorHSL(0, 0, 0);
        }
    }
}

