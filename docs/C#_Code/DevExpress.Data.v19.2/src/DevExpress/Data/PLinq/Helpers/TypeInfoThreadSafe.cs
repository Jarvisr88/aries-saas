namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.ComponentModel;

    internal class TypeInfoThreadSafe : TypeInfoBase
    {
        private readonly PropertyDescriptorCollection PropertyDescriptors;

        public TypeInfoThreadSafe(PropertyDescriptorCollection propertyDescriptors);
        public override object GetUIThreadRow(object rowInfo);
        public override object GetWorkerThreadRowInfo(object workerRow);

        public override PropertyDescriptorCollection UIDescriptors { get; }
    }
}

