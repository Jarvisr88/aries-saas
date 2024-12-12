namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.ComponentModel;

    internal abstract class TypeInfoBase
    {
        protected TypeInfoBase();
        public abstract object GetUIThreadRow(object rowInfo);
        public abstract object GetWorkerThreadRowInfo(object workerRow);

        public abstract PropertyDescriptorCollection UIDescriptors { get; }
    }
}

