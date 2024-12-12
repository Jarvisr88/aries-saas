namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Point<T> where T: IComparable<T>
    {
        public Point(T x, T y)
        {
            this = (Point) new Point<T>();
            this.X = x;
            this.Y = y;
        }

        public T X { get; set; }
        public T Y { get; set; }
        public static bool operator ==(Point<T> size1, Point<T> size2) => 
            (size1.X.CompareTo(size2.X) == 0) && (size1.Y.CompareTo(size2.Y) == 0);

        public static bool operator !=(Point<T> size1, Point<T> size2) => 
            (size1.X.CompareTo(size2.X) != 0) || (size1.Y.CompareTo(size2.Y) != 0);

        public override bool Equals(object obj) => 
            (obj is Point<T>) && (this == ((Point<T>) obj));

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}

