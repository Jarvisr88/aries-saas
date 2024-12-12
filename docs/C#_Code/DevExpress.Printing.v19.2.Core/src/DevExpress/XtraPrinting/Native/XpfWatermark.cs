namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Drawing;
    using System;

    public class XpfWatermark : Watermark
    {
        private byte[] imageArray;

        public override void CopyFrom(Watermark watermark);
        public void CopyFrom(XpfWatermark watermark);
        public override bool Equals(object obj);
        public override int GetHashCode();

        [XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; set; }

        [XtraSerializableProperty]
        public string ImageBase64 { get; set; }

        public byte[] ImageArray { get; set; }
    }
}

