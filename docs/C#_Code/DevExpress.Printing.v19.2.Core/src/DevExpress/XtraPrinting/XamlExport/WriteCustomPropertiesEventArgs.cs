namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Runtime.CompilerServices;

    public class WriteCustomPropertiesEventArgs : EventArgs
    {
        public WriteCustomPropertiesEventArgs(XamlWriter writer, object obj)
        {
            this.Writer = writer;
            this.Obj = obj;
        }

        public XamlWriter Writer { get; private set; }

        public object Obj { get; private set; }
    }
}

