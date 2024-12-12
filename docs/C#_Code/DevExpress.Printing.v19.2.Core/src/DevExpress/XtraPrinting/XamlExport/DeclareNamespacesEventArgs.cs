namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Runtime.CompilerServices;

    public class DeclareNamespacesEventArgs : EventArgs
    {
        public DeclareNamespacesEventArgs(XamlWriter writer)
        {
            this.Writer = writer;
        }

        public XamlWriter Writer { get; private set; }
    }
}

