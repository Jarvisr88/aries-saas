namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class PenInfo : IDisposable
    {
        private readonly float penWidth;

        public PenInfo(System.Drawing.Pen pen, System.Drawing.Brush brush, bool hasPermanentFill)
        {
            Guard.ArgumentNotNull(pen, "Pen");
            this.Pen = pen;
            this.Brush = brush;
            this.HasPermanentFill = hasPermanentFill;
            this.penWidth = this.CalculatePenWidth();
        }

        public void ApplyBoundingCaps(float scaleFactor)
        {
            CreateCustomCap createCap = <>c.<>9__36_0 ??= (capInfo, capScaleFactor) => capInfo.CreateBoundingCap(capScaleFactor);
            this.ApplyCapsCore(scaleFactor, createCap);
        }

        public void ApplyCaps(float scaleFactor)
        {
            CreateCustomCap createCap = <>c.<>9__35_0 ??= (capInfo, capScaleFactor) => capInfo.CreateCap(capScaleFactor);
            this.ApplyCapsCore(scaleFactor, createCap);
        }

        private void ApplyCapsCore(float scaleFactor, CreateCustomCap createCap)
        {
            if (this.Pen != null)
            {
                float capScaleFactor = this.CaculateCapScaleFactor(this.Pen.Width, scaleFactor);
                if (this.StartCap != null)
                {
                    using (CustomLineCap cap = createCap(this.StartCap, capScaleFactor))
                    {
                        this.Pen.CustomStartCap = cap;
                    }
                }
                if (this.EndCap != null)
                {
                    using (CustomLineCap cap2 = createCap(this.EndCap, capScaleFactor))
                    {
                        this.Pen.CustomEndCap = cap2;
                    }
                }
            }
        }

        internal float CaculateCapScaleFactor(float penWidth, float scaleFactor)
        {
            int num = this.ConvertPenWidth(penWidth, scaleFactor);
            int num2 = 0x6338;
            float num3 = 1f;
            if ((num != 0.0) && (num2 > num))
            {
                num3 = ((float) num2) / ((float) num);
            }
            return num3;
        }

        private float CalculatePenWidth()
        {
            switch (this.Pen.PenType)
            {
                case PenType.SolidColor:
                    return ((this.Pen.Color.A == 0) ? 0f : this.Pen.Width);

                case PenType.PathGradient:
                    return this.GetGradientBrushWidth(((PathGradientBrush) this.Brush).InterpolationColors);

                case PenType.LinearGradient:
                {
                    LinearGradientBrush brush = (LinearGradientBrush) this.Brush;
                    return ((brush.Blend == null) ? this.GetGradientBrushWidth(brush.InterpolationColors) : this.GetGradientBrushWidth(brush.LinearColors));
                }
            }
            return this.Pen.Width;
        }

        internal int ConvertPenWidth(float penWidth, float scaleFactor)
        {
            double num2 = 9525.0;
            double num3 = ((double) scaleFactor) / num2;
            int num4 = Floor((this.ConvertToEmuSafe(penWidth) * num3) + 0.5);
            if (num4 < 1)
            {
                num4 = 1;
            }
            return Normalize(((double) num4) / num3);
        }

        internal int ConvertToEmuSafe(float value) => 
            (this.Converter != null) ? this.Converter(value) : ((int) value);

        public void Dispose()
        {
            if (this.Pen != null)
            {
                this.Pen.Dispose();
                this.Pen = null;
            }
            if (this.StartCap != null)
            {
                this.StartCap.Dispose();
                this.StartCap = null;
            }
            if (this.EndCap != null)
            {
                this.EndCap.Dispose();
                this.EndCap = null;
            }
            if (this.Brush != null)
            {
                this.Brush.Dispose();
                this.Brush = null;
            }
        }

        private static int Floor(double d) => 
            (int) d;

        private float GetGradientBrushWidth(ColorBlend colorBlend) => 
            this.GetGradientBrushWidth(colorBlend.Colors);

        private float GetGradientBrushWidth(Color[] colors)
        {
            foreach (Color color in colors)
            {
                if (color.A > 0)
                {
                    return this.Pen.Width;
                }
            }
            return 0f;
        }

        private static int Normalize(double value)
        {
            double d = (value >= 0.0) ? (value + 0.5) : (value - 0.5);
            return Floor(d);
        }

        public void ResetCaps()
        {
            if (this.Pen != null)
            {
                if (this.Pen.StartCap == LineCap.Custom)
                {
                    this.Pen.StartCap = LineCap.Flat;
                }
                if (this.Pen.EndCap == LineCap.Custom)
                {
                    this.Pen.EndCap = LineCap.Flat;
                }
            }
        }

        public System.Drawing.Pen Pen { get; private set; }

        public System.Drawing.Brush Brush { get; private set; }

        public float PenWidth =>
            (this.Pen.Alignment == PenAlignment.Outset) ? (this.penWidth * 2f) : this.penWidth;

        public LayoutUnitsToEmuConverter Converter { get; set; }

        public bool HasPermanentFill { get; private set; }

        public bool HasCaps =>
            (this.StartCap != null) || (this.EndCap != null);

        public LineCapInfo StartCap { get; set; }

        public LineCapInfo EndCap { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PenInfo.<>c <>9 = new PenInfo.<>c();
            public static PenInfo.CreateCustomCap <>9__35_0;
            public static PenInfo.CreateCustomCap <>9__36_0;

            internal CustomLineCap <ApplyBoundingCaps>b__36_0(LineCapInfo capInfo, float capScaleFactor) => 
                capInfo.CreateBoundingCap(capScaleFactor);

            internal CustomLineCap <ApplyCaps>b__35_0(LineCapInfo capInfo, float capScaleFactor) => 
                capInfo.CreateCap(capScaleFactor);
        }

        private delegate CustomLineCap CreateCustomCap(LineCapInfo capInfo, float capScaleFactor);

        public delegate int LayoutUnitsToEmuConverter(float value);
    }
}

