namespace DevExpress.Utils
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true), TypeConverter(typeof(PointFloatConverter))]
    public struct PointFloat
    {
        public static readonly PointFloat Empty;
        private float x;
        private float y;
        public PointFloat(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PointFloat(PointF pt) : this(pt.X, pt.Y)
        {
        }

        [Browsable(false)]
        public bool IsEmpty =>
            (this.x == 0f) && (this.y == 0f);
        [TypeConverter(typeof(SingleTypeConverter)), DXDisplayNameIgnore]
        public float X
        {
            get => 
                this.x;
            set => 
                this.x = value;
        }
        [TypeConverter(typeof(SingleTypeConverter)), DXDisplayNameIgnore]
        public float Y
        {
            get => 
                this.y;
            set => 
                this.y = value;
        }
        public static PointFloat operator +(PointFloat pt, Size sz) => 
            new PointFloat(pt.X + sz.Width, pt.Y + sz.Height);

        public static PointFloat operator -(PointFloat pt, Size sz) => 
            new PointFloat(pt.X - sz.Width, pt.Y - sz.Height);

        public static bool operator ==(PointFloat left, PointFloat right) => 
            (left.X == right.X) && (left.Y == right.Y);

        public static bool operator !=(PointFloat left, PointFloat right) => 
            !(left == right);

        public static implicit operator PointF(PointFloat point) => 
            new PointF(point.X, point.Y);

        public override bool Equals(object obj)
        {
            if (!(obj is PointFloat))
            {
                return false;
            }
            PointFloat num = (PointFloat) obj;
            return ((num.X == this.X) && ((num.Y == this.Y) && num.GetType().Equals(this.GetType())));
        }

        public override int GetHashCode() => 
            this.GetHashCode();

        public override string ToString() => 
            $"{{X={this.x}, Y={this.y}}}";

        public void Offset(float dx, float dy)
        {
            this.X += dx;
            this.Y += dy;
        }

        static PointFloat()
        {
        }
    }
}

