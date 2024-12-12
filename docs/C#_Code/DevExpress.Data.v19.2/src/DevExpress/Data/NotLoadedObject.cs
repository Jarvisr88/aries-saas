namespace DevExpress.Data
{
    using System;

    public class NotLoadedObject
    {
        public static readonly NotLoadedObject Instance;

        static NotLoadedObject();
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();
    }
}

