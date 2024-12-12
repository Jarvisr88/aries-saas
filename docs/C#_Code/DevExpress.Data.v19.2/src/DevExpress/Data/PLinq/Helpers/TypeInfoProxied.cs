namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.ComponentModel;

    internal class TypeInfoProxied : TypeInfoBase
    {
        private readonly PropertyDescriptorCollection uiDescriptors;
        private readonly PropertyDescriptor[] workerDescriptors;

        public TypeInfoProxied(PropertyDescriptorCollection workerThreadDescriptors, Type designTimeType);
        public override object GetUIThreadRow(object rowInfo);
        public override object GetWorkerThreadRowInfo(object workerRow);

        public override PropertyDescriptorCollection UIDescriptors { get; }

        private class ProxyPropertyDescriptor : PropertyDescriptor
        {
            private readonly System.Type Type;
            private readonly int Index;

            public ProxyPropertyDescriptor(string name, System.Type type, int index);
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

