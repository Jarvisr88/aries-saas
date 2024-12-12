namespace DevExpress.Data.Utils
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public sealed class AnnotationAttributesKey : IEquatable<AnnotationAttributesKey>
    {
        internal static readonly AnnotationAttributesKey Empty;

        static AnnotationAttributesKey();
        private AnnotationAttributesKey(Type componentType, string name, Type propertyType);
        public bool Equals(AnnotationAttributesKey other);
        public sealed override bool Equals(object obj);
        public static AnnotationAttributesKey FromPropertyDescriptor(PropertyDescriptor descriptor);
        public sealed override int GetHashCode();
        public static bool Match(AnnotationAttributesKey key, Type type);
        public sealed override string ToString();

        public string Name { get; private set; }

        public Type ComponentType { get; private set; }

        public Type PropertyType { get; private set; }
    }
}

