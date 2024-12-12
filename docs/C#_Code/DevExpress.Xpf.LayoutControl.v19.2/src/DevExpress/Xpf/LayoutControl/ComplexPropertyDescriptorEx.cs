namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Data.Access;
    using System;
    using System.ComponentModel;

    internal class ComplexPropertyDescriptorEx : ComplexPropertyDescriptorReflection
    {
        public ComplexPropertyDescriptorEx(object sourceObject, string path) : base(sourceObject, path)
        {
        }

        public static PropertyDescriptor GetIsReady(object sourceObject, string path) => 
            new ComplexPropertyDescriptorEx(sourceObject, path).Last;
    }
}

