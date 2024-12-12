namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PropertyKey : IConvertToInt<PropertyKey>
    {
        public static PropertyKey Undefined;
        public static PropertyKey Empty;
        private readonly int id;
        private bool isInitialized;
        public static PropertyKey Create(Type forType)
        {
            List<int> list = new List<int>();
            Type type = typeof(PropertyKey);
            foreach (FieldInfo info in forType.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static))
            {
                if (info.FieldType == type)
                {
                    PropertyKey key = (PropertyKey) info.GetValue(null);
                    if (!key.IsEmpty)
                    {
                        list.Add(key.id);
                    }
                }
            }
            if (list.Count == 0)
            {
                return new PropertyKey(0);
            }
            Comparison<int> comparison = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Comparison<int> local1 = <>c.<>9__0_0;
                comparison = <>c.<>9__0_0 = (x, y) => y - x;
            }
            list.Sort(comparison);
            return new PropertyKey(list[0] + 1);
        }

        public PropertyKey(int id)
        {
            this.id = id;
            this.isInitialized = true;
        }

        internal bool IsEmpty =>
            !this.isInitialized;
        public override bool Equals(object obj)
        {
            if (!(obj is PropertyKey))
            {
                return false;
            }
            PropertyKey key = (PropertyKey) obj;
            return ((key.id == this.id) && (key.isInitialized == this.isInitialized));
        }

        public override int GetHashCode() => 
            this.id;

        public static bool operator ==(PropertyKey key1, PropertyKey key2) => 
            key1.Equals(key2);

        public static bool operator !=(PropertyKey key1, PropertyKey key2) => 
            !key1.Equals(key2);

        public static PropertyKey operator |(PropertyKey key1, PropertyKey key2) => 
            new PropertyKey(key1.id | key2.id);

        public static PropertyKey operator &(PropertyKey key1, PropertyKey key2) => 
            new PropertyKey(key1.id & key2.id);

        public static PropertyKey operator ^(PropertyKey key1, PropertyKey key2) => 
            new PropertyKey(key1.id ^ key2.id);

        public static PropertyKey operator ~(PropertyKey key) => 
            new PropertyKey(~key.id);

        public static implicit operator PropertyKey(int id) => 
            new PropertyKey(id);

        public override string ToString() => 
            !(this == Undefined) ? (!(this == Empty) ? this.id.ToString() : "Empty") : "Undefined";

        int IConvertToInt<PropertyKey>.ToInt() => 
            this.id;

        PropertyKey IConvertToInt<PropertyKey>.FromInt(int value) => 
            new PropertyKey(value);

        static PropertyKey()
        {
            Undefined = new PropertyKey(-2147483648);
            Empty = new PropertyKey();
        }
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyKey.<>c <>9 = new PropertyKey.<>c();
            public static Comparison<int> <>9__0_0;

            internal int <Create>b__0_0(int x, int y) => 
                y - x;
        }
    }
}

