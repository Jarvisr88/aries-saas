namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlsContentFeatBase : XlsContentBase
    {
        protected XlsContentFeatBase()
        {
        }

        public override void Read(XlReader reader, int size)
        {
        }

        public XlsFeatureType FeatureType { get; set; }
    }
}

