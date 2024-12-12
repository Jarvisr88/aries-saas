namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Xml;

    public class LayoutControlWriteElementToXMLEventArgs : EventArgs
    {
        public LayoutControlWriteElementToXMLEventArgs(XmlWriter xml, FrameworkElement element)
        {
            this.Xml = xml;
            this.Element = element;
        }

        public FrameworkElement Element { get; private set; }

        public XmlWriter Xml { get; private set; }
    }
}

