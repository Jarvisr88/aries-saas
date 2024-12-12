namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.ComponentModel;

    internal class TypeInfoNoQueryable : TypeInfoBase
    {
        private readonly PropertyDescriptorCollection uiDescriptors;

        public TypeInfoNoQueryable(Type designTimeType);
        public override object GetUIThreadRow(object rowInfo);
        public override object GetWorkerThreadRowInfo(object workerRow);

        public override PropertyDescriptorCollection UIDescriptors { get; }

        private class NoQueryablePropertyDescriptor : PropertyDescriptor
        {
            private readonly System.Type Type;

            public NoQueryablePropertyDescriptor(string name, System.Type type);
            public override bool CanResetValue(object component);
            public override object GetValue(object component);
            public override void ResetValue(object component);
            public override void SetValue(object component, object value);
            public override bool ShouldSerializeValue(object component);

            public override System.Type ComponentType { get; }

            public override bool IsReadOnly { get; }

            public override System.Type PropertyType { get; }
        }
    }
}

