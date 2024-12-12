namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Xml;

    public class LayoutControlReadElementFromXMLEventArgs : EventArgs
    {
        public LayoutControlReadElementFromXMLEventArgs(XmlReader xml, FrameworkElement element)
        {
            this.Xml = xml;
            this.Element = element;
        }

        public FrameworkElement Element { get; private set; }

        public XmlReader Xml { get; private set; }
    }
}

