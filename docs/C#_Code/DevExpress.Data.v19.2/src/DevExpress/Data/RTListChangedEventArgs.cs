namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class RTListChangedEventArgs
    {
        [ThreadStatic]
        private static ListChangedEventArgs eventArg;
        private static readonly Action<ListChangedEventArgs, int> oldIndexSet;
        private static readonly Action<ListChangedEventArgs, int> newIndexSet;
        private static readonly Action<ListChangedEventArgs, PropertyDescriptor> propertyDescriptorSet;
        private static readonly Action<ListChangedEventArgs, ListChangedType> listChangedTypeSet;
        private static readonly MethodInfo assignInfo;

        static RTListChangedEventArgs();
        private static void Assign<T>(ref T left, T right);
        public static ListChangedEventArgs Create(ListChangedType listChangedType, PropertyDescriptor propDesc);
        public static ListChangedEventArgs Create(ListChangedType listChangedType, int newIndex);
        public static ListChangedEventArgs Create(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc);
        public static ListChangedEventArgs Create(ListChangedType listChangedType, int newIndex, int oldIndex);
        public static ListChangedEventArgs Create(ListChangedType listChangedType, int newIndex, int oldIndex, PropertyDescriptor propDesc);
        private static ListChangedEventArgs GetListChangedEventArgsInternal(ListChangedType listChangedType, int newIndex, int oldIndex, PropertyDescriptor propDesc);
        private static Action<ListChangedEventArgs, T> GetSetter<T>(string fieldName);
    }
}

