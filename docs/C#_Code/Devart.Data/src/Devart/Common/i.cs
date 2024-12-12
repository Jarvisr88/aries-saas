namespace Devart.Common
{
    using System;
    using System.ComponentModel;

    internal class i : PropertyDescriptor
    {
        internal i(string A_0) : base(A_0, null)
        {
        }

        public override void a(object A_0)
        {
        }

        public override void a(object A_0, object A_1)
        {
        }

        public override bool b(object A_0) => 
            false;

        public override bool c(object A_0) => 
            false;

        public override object d(object A_0) => 
            A_0;

        public override Type System.ComponentModel.PropertyDescriptor.ComponentType =>
            typeof(object);

        public override bool System.ComponentModel.PropertyDescriptor.IsReadOnly =>
            true;

        public override Type System.ComponentModel.PropertyDescriptor.PropertyType =>
            typeof(object);
    }
}

