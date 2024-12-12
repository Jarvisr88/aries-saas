namespace DevExpress.Office
{
    using System;
    using System.Runtime.CompilerServices;

    public class OfficePropertyChangedEventArgs : EventArgs
    {
        public OfficePropertyChangedEventArgs(DevExpress.Office.PropertyKey propertyKey)
        {
            this.PropertyKey = propertyKey;
        }

        public DevExpress.Office.PropertyKey PropertyKey { get; private set; }

        public bool Handled { get; set; }
    }
}

