namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class CustomEqualsAttributesEventArgs : EventArgs
    {
        private System.ComponentModel.PropertyDescriptor descriptorCore;

        public CustomEqualsAttributesEventArgs(object objA, object objB, System.ComponentModel.PropertyDescriptor descriptor);

        public object ObjA { get; private set; }

        public object ObjB { get; private set; }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; }

        public bool CustomEquals { get; set; }
    }
}

