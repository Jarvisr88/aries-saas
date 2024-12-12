namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Design;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(PaddingInfoTypeConverter))]
    public struct PaddingInfo : IEquatable<PaddingInfo>
    {
        internal static readonly PaddingInfo Undefined;
        public static readonly PaddingInfo Empty;
        private float left;
        private float right;
        private float top;
        private float bottom;
        private float dpi;
        public static implicit operator PaddingInfo(int offset) => 
            new PaddingInfo(offset, offset, 0, 0);

        public static bool operator !=(PaddingInfo pad1, PaddingInfo pad2) => 
            !(pad1 == pad2);

        public static bool operator ==(PaddingInfo pad1, PaddingInfo pad2) => 
            pad1.Equals(pad2);

        private static float ValidateZeroRestrictedValue(float value, string paramName)
        {
            if (value < 0f)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
            return value;
        }

        [Description("Specifies padding for all the element's sides."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PaddingInfo.All"), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int All
        {
            get => 
                ((this.Left != this.Top) || ((this.Left != this.Right) || (this.Left != this.Bottom))) ? -1 : this.Left;
            set
            {
                this.Left = value;
                this.Top = value;
                this.Right = value;
                this.Bottom = value;
            }
        }
        [Description("Gets or sets the padding value for the left edge."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PaddingInfo.Left"), RefreshProperties(RefreshProperties.All)]
        public int Left
        {
            get => 
                (int) Math.Floor((double) this.left);
            set => 
                this.SetLeft((float) value);
        }
        [Description("Gets or sets the padding value for the right edge."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PaddingInfo.Right"), RefreshProperties(RefreshProperties.All)]
        public int Right
        {
            get => 
                (int) Math.Floor((double) this.right);
            set => 
                this.SetRight((float) value);
        }
        [Description("Gets or sets the padding value for the top edge."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PaddingInfo.Top"), RefreshProperties(RefreshProperties.All)]
        public int Top
        {
            get => 
                (int) Math.Floor((double) this.top);
            set => 
                this.SetTop((float) value);
        }
        [Description("Gets or sets the padding value for the bottom edge."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PaddingInfo.Bottom"), RefreshProperties(RefreshProperties.All)]
        public int Bottom
        {
            get => 
                (int) Math.Floor((double) this.bottom);
            set => 
                this.SetBottom((float) value);
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public float Dpi
        {
            get => 
                this.dpi;
            set
            {
                this.Update(value);
                this.dpi = value;
            }
        }
        [Browsable(false)]
        public bool IsEmpty =>
            (this.left == 0f) && ((this.right == 0f) && ((this.top == 0f) && (this.bottom == 0f)));
        public PaddingInfo(int left, int right, int top, int bottom) : this(left, right, top, bottom, (float) 96f)
        {
        }

        public PaddingInfo(int left, int right, int top, int bottom, float dpi) : this(dpi)
        {
            this.SetLeft((float) left);
            this.SetRight((float) right);
            this.SetTop((float) top);
            this.SetBottom((float) bottom);
        }

        private PaddingInfo(float left, float right, float top, float bottom, float dpi)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
            this.dpi = dpi;
        }

        public PaddingInfo(float dpi) : this((float) 0f, (float) 0f, (float) 0f, (float) 0f, dpi)
        {
        }

        public PaddingInfo(int all, float dpi) : this(all, all, all, all, dpi)
        {
        }

        public PaddingInfo(PaddingInfo src, float dpi) : this(src.left, src.right, src.top, src.bottom, src.dpi)
        {
            this.Dpi = dpi;
        }

        public PaddingInfo(int left, int right, int top, int bottom, GraphicsUnit graphicsUnit) : this(left, right, top, bottom, GraphicsDpi.UnitToDpi(graphicsUnit))
        {
        }

        public PaddingInfo(GraphicsUnit graphicsUnit) : this(GraphicsDpi.UnitToDpi(graphicsUnit))
        {
        }

        private void Update(float dpi)
        {
            if (dpi == 0f)
            {
                throw new ArgumentException("dpi");
            }
            if ((this.dpi != 0f) && (this.dpi != dpi))
            {
                this.left = this.ConvertToInt32(this.left, dpi);
                this.right = this.ConvertToInt32(this.right, dpi);
                this.top = this.ConvertToInt32(this.top, dpi);
                this.bottom = this.ConvertToInt32(this.bottom, dpi);
            }
        }

        public bool Equals(PaddingInfo other)
        {
            PaddingInfo info = new PaddingInfo(other, this.dpi);
            return ((this.left == info.left) && ((this.right == info.right) && ((this.top == info.top) && (this.bottom == info.bottom))));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaddingInfo))
            {
                return false;
            }
            PaddingInfo other = (PaddingInfo) obj;
            return this.Equals(other);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<int, int, int, int, int>((int) this.top, (int) this.left, (int) this.right, (int) this.bottom, (int) this.dpi);

        public void RotatePaddingCounterclockwise(int numberOfTimes)
        {
            float left = 0f;
            for (int i = 0; i < numberOfTimes; i++)
            {
                left = this.left;
                this.left = this.bottom;
                this.bottom = this.right;
                this.right = this.top;
                this.top = left;
            }
        }

        public RectangleF Deflate(RectangleF rect, float dpi)
        {
            float left = rect.Left + this.Convert((float) this.Left, dpi);
            float top = rect.Top + this.Convert((float) this.Top, dpi);
            return RectangleF.FromLTRB(left, top, Math.Max(left, rect.Right - this.Convert((float) this.Right, dpi)), Math.Max(top, rect.Bottom - this.Convert((float) this.Bottom, dpi)));
        }

        private float Convert(float val, float toDpi) => 
            GraphicsUnitConverter.Convert(val, this.dpi, toDpi);

        private int ConvertToInt32(float val, float toDpi) => 
            System.Convert.ToInt32(this.Convert(val, toDpi));

        public RectangleF Inflate(RectangleF rect, float dpi) => 
            RectangleF.FromLTRB(rect.Left - this.Convert((float) this.Left, dpi), rect.Top - this.Convert((float) this.Top, dpi), rect.Right + this.Convert((float) this.Right, dpi), rect.Bottom + this.Convert((float) this.Bottom, dpi));

        public SizeF Inflate(SizeF size, float dpi) => 
            new SizeF(this.InflateWidth(size.Width, dpi), this.InflateHeight(size.Height, dpi));

        public SizeF Deflate(SizeF size, float dpi) => 
            new SizeF(this.DeflateWidth(size.Width, dpi), this.DeflateHeight(size.Height, dpi));

        public float InflateWidth(float width, float dpi) => 
            width + this.Convert(this.left + this.right, dpi);

        public float DeflateWidth(float width, float dpi) => 
            width - this.Convert(this.left + this.right, dpi);

        public float InflateHeight(float height, float dpi) => 
            height + this.Convert(this.top + this.bottom, dpi);

        public float DeflateHeight(float height, float dpi) => 
            height - this.Convert(this.top + this.bottom, dpi);

        public float InflateWidth(float width) => 
            width + (this.left + this.right);

        public float DeflateWidth(float width) => 
            width - (this.left + this.right);

        public float InflateHeight(float height) => 
            height + (this.top + this.bottom);

        public float DeflateHeight(float height) => 
            height - (this.top + this.bottom);

        internal PaddingInfo Scale(float scaleFactor) => 
            new PaddingInfo(MathMethods.Scale(this.left, (double) scaleFactor), MathMethods.Scale(this.right, (double) scaleFactor), MathMethods.Scale(this.top, (double) scaleFactor), MathMethods.Scale(this.bottom, (double) scaleFactor), this.dpi);

        private void SetLeft(float value)
        {
            this.left = ValidateZeroRestrictedValue(value, "Left");
        }

        private void SetRight(float value)
        {
            this.right = ValidateZeroRestrictedValue(value, "Right");
        }

        private void SetTop(float value)
        {
            this.top = ValidateZeroRestrictedValue(value, "Top");
        }

        private void SetBottom(float value)
        {
            this.bottom = ValidateZeroRestrictedValue(value, "Bottom");
        }

        static PaddingInfo()
        {
            Undefined = new PaddingInfo(-1f, -1f, -1f, -1f, 1f);
            Empty = new PaddingInfo(96f);
        }
    }
}

