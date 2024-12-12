namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ColumnGeneratorItemContext
    {
        internal ColumnGeneratorItemContext(DataControlBase control, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            this.Control = control;
            this.PropertyDescriptor = propertyDescriptor;
        }

        public DataControlBase Control { get; private set; }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; private set; }
    }
}

