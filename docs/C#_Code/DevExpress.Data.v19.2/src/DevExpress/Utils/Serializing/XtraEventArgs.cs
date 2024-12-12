namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    public class XtraEventArgs : EventArgs
    {
        private XtraPropertyInfo info;

        public XtraEventArgs(XtraPropertyInfo info)
        {
            this.info = info;
        }

        public XtraPropertyInfo Info =>
            this.info;
    }
}

