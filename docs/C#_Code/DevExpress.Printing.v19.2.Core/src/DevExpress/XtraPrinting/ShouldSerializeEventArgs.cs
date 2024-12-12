namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class ShouldSerializeEventArgs : EventArgs
    {
        public ShouldSerializeEventArgs(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }

        public bool? ShouldSerialize { get; set; }
    }
}

