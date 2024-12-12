namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class TypeProviderAttribute : Attribute
    {
        private System.Type type;

        public TypeProviderAttribute(System.Type type);

        protected System.Type Type { get; }
    }
}

