namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Runtime.CompilerServices;

    public class GetPropertiesEventArgs : EventArgs
    {
        public GetPropertiesEventArgs(IPropertyDescriptor[] properties);

        public IPropertyDescriptor[] Properties { get; private set; }
    }
}

