namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Container<T>
    {
        public Container(T content)
        {
            this = (Container) new Container<T>();
            this.Content = content;
        }

        public T Content { get; private set; }
        public override int GetHashCode() => 
            (this.Content == null) ? 0 : this.Content.GetHashCode();

        public static bool operator ==(Container<T> t1, Container<T> t2) => 
            Equals(t1.Content, t2.Content);

        public static bool operator !=(Container<T> t1, Container<T> t2) => 
            !(t1 == t2);

        public override bool Equals(object obj) => 
            (obj is Container<T>) && (this == ((Container<T>) obj));
    }
}

