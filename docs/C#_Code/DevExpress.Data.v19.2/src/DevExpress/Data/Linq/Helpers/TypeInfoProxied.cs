namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.ComponentModel;

    public class TypeInfoProxied : TypeInfoBase
    {
        private readonly PropertyDescriptorCollection uiDescriptors;
        private readonly PropertyDescriptor[] workerDescriptors;

        public TypeInfoProxied(PropertyDescriptorCollection workerThreadDescriptors, Type designTimeType);
        public override object GetUIThreadRow(object rowInfo);
        public override object GetWorkerThreadRowInfo(object workerRow);
        public static bool IsNotThreadSafe(Type type);

        public override PropertyDescriptorCollection UIDescriptors { get; }
    }
}

