namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Size<T> where T: IComparable<T>
    {
        public Size(T width, T height)
        {
            this = (Size) new Size<T>();
            this.Width = width;
            this.Height = height;
        }

        public T Height { get; set; }
        public bool IsEmpty
        {
            get
            {
                T other = default(T);
                if (this.Width.CompareTo(other) == 0)
                {
                    return true;
                }
                other = default(T);
                return (this.Height.CompareTo(other) == 0);
            }
        }
        public T Width { get; set; }
        public static bool operator ==(Size<T> size1, Size<T> size2) => 
            (size1.Width.CompareTo(size2.Width) == 0) && (size1.Height.CompareTo(size2.Height) == 0);

        public static bool operator !=(Size<T> size1, Size<T> size2) => 
            (size1.Width.CompareTo(size2.Width) != 0) || (size1.Height.CompareTo(size2.Height) != 0);

        public override bool Equals(object obj) => 
            (obj is Size<T>) && (this == ((Size<T>) obj));

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}

