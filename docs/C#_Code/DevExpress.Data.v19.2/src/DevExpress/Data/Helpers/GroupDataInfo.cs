namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class GroupDataInfo
    {
        private object[] values;
        private int level;
        private int? hashcode;

        public GroupDataInfo(object[] values, int level);
        private bool Compare(object v1, object v2);
        public override bool Equals(object obj);
        public override int GetHashCode();
        private static int GetHashCode(object value);

        public int Level { get; }

        public object[] Values { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupDataInfo.<>c <>9;
            public static Func<object, int> <>9__8_0;

            static <>c();
            internal int <GetHashCode>b__8_0(object o);
        }
    }
}

