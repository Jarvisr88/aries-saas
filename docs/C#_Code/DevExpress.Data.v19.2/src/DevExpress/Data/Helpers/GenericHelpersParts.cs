namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal static class GenericHelpersParts
    {
        public class TypesArrayComparer : IEqualityComparer<Type[]>
        {
            public static readonly GenericHelpersParts.TypesArrayComparer Instance;

            static TypesArrayComparer();
            private TypesArrayComparer();
            public bool Equals(Type[] x, Type[] y);
            public int GetHashCode(Type[] types);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TypesDuple : IEquatable<GenericHelpersParts.TypesDuple>
        {
            public readonly Type T1;
            public readonly Type T2;
            public TypesDuple(Type t1, Type t2);
            public override int GetHashCode();
            public override bool Equals(object obj);
            public bool Equals(GenericHelpersParts.TypesDuple other);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TypesTriple : IEquatable<GenericHelpersParts.TypesTriple>
        {
            public readonly Type T1;
            public readonly Type T2;
            public readonly Type T3;
            public TypesTriple(Type t1, Type t2, Type t3);
            public override int GetHashCode();
            public override bool Equals(object obj);
            public bool Equals(GenericHelpersParts.TypesTriple other);
        }
    }
}

