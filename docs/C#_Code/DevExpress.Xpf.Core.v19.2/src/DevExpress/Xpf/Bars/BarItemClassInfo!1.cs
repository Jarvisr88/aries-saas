namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemClassInfo<T> where T: class
    {
        public Type ItemType { get; set; }

        public CreateObjectMethod<T> CreateMethod { get; set; }
    }
}

