namespace DevExpress.Office.Model
{
    using DevExpress.Utils;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct ThemeColorIndex : IConvertToInt<ThemeColorIndex>, IComparable<ThemeColorIndex>
    {
        private readonly int m_value;
        public static readonly ThemeColorIndex None;
        public static readonly ThemeColorIndex Light1;
        public static readonly ThemeColorIndex Dark1;
        public static readonly ThemeColorIndex Light2;
        public static readonly ThemeColorIndex Dark2;
        public static readonly ThemeColorIndex Accent1;
        public static readonly ThemeColorIndex Accent2;
        public static readonly ThemeColorIndex Accent3;
        public static readonly ThemeColorIndex Accent4;
        public static readonly ThemeColorIndex Accent5;
        public static readonly ThemeColorIndex Accent6;
        public static readonly ThemeColorIndex Hyperlink;
        public static readonly ThemeColorIndex FollowedHyperlink;
        [DebuggerStepThrough]
        public ThemeColorIndex(int value)
        {
            this.m_value = value;
        }

        public override bool Equals(object obj) => 
            (obj is ThemeColorIndex) && (this.m_value == ((ThemeColorIndex) obj).m_value);

        public override int GetHashCode() => 
            this.m_value.GetHashCode();

        public override string ToString() => 
            this.m_value.ToString();

        public static ThemeColorIndex operator +(ThemeColorIndex index, int value) => 
            new ThemeColorIndex(index.m_value + value);

        public static int operator -(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value - index2.m_value;

        public static ThemeColorIndex operator -(ThemeColorIndex index, int value) => 
            new ThemeColorIndex(index.m_value - value);

        public static ThemeColorIndex operator ++(ThemeColorIndex index) => 
            new ThemeColorIndex(index.m_value + 1);

        public static ThemeColorIndex operator --(ThemeColorIndex index) => 
            new ThemeColorIndex(index.m_value - 1);

        public static bool operator <(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value < index2.m_value;

        public static bool operator <=(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value <= index2.m_value;

        public static bool operator >(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value > index2.m_value;

        public static bool operator >=(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value >= index2.m_value;

        public static bool operator ==(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value == index2.m_value;

        public static bool operator !=(ThemeColorIndex index1, ThemeColorIndex index2) => 
            index1.m_value != index2.m_value;

        public int ToInt() => 
            this.m_value;

        [DebuggerStepThrough]
        int IConvertToInt<ThemeColorIndex>.ToInt() => 
            this.m_value;

        [DebuggerStepThrough]
        ThemeColorIndex IConvertToInt<ThemeColorIndex>.FromInt(int value) => 
            new ThemeColorIndex(value);

        public int CompareTo(ThemeColorIndex other) => 
            (this.m_value >= other.m_value) ? ((this.m_value <= other.m_value) ? 0 : 1) : -1;

        static ThemeColorIndex()
        {
            None = new ThemeColorIndex(-1);
            Light1 = new ThemeColorIndex(0);
            Dark1 = new ThemeColorIndex(1);
            Light2 = new ThemeColorIndex(2);
            Dark2 = new ThemeColorIndex(3);
            Accent1 = new ThemeColorIndex(4);
            Accent2 = new ThemeColorIndex(5);
            Accent3 = new ThemeColorIndex(6);
            Accent4 = new ThemeColorIndex(7);
            Accent5 = new ThemeColorIndex(8);
            Accent6 = new ThemeColorIndex(9);
            Hyperlink = new ThemeColorIndex(10);
            FollowedHyperlink = new ThemeColorIndex(11);
        }
    }
}

