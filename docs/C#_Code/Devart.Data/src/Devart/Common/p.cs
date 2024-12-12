namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [DefaultMember("Item")]
    internal class p : PropertyDescriptorCollection
    {
        public p(PropertyDescriptor[] A_0) : base(A_0)
        {
        }

        public override IEnumerator a() => 
            base.GetEnumerator();

        public override PropertyDescriptorCollection a(IComparer A_0) => 
            base.Sort(A_0);

        public override PropertyDescriptorCollection a(string[] A_0) => 
            base.Sort(A_0);

        public override PropertyDescriptor a(string A_0, bool A_1)
        {
            PropertyDescriptor descriptor = base.Find(A_0, A_1);
            return ((descriptor != null) ? descriptor : new i(A_0));
        }

        public override PropertyDescriptorCollection a(string[] A_0, IComparer A_1) => 
            base.Sort(A_0, A_1);

        public override PropertyDescriptorCollection b() => 
            base.Sort();

        public override PropertyDescriptor this[int index] =>
            base[A_0];

        public override PropertyDescriptor this[string name]
        {
            get
            {
                PropertyDescriptor descriptor = base[A_0];
                return ((descriptor != null) ? descriptor : new i(A_0));
            }
        }
    }
}

