namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Printing;
    using DevExpress.Printing.Native;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Design;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;

    [TypeConverter(typeof(WatermarkConverter))]
    public class Watermark : PageWatermark
    {
        private string pageRange = "";

        public virtual void CopyFrom(Watermark watermark)
        {
            Guard.ArgumentNotNull(watermark, "watermark");
            base.CopyFromInternal(watermark);
            this.pageRange = watermark.pageRange;
        }

        protected internal override void Draw(IGraphics gr, RectangleF rect, int pageIndex, int pageCount)
        {
            if (this.NeedDraw(pageIndex, pageCount))
            {
                base.Draw(gr, rect, pageIndex, pageCount);
            }
        }

        public override bool Equals(object obj)
        {
            Watermark watermark = obj as Watermark;
            return ((watermark != null) && (Equals(this.pageRange, watermark.pageRange) && base.Equals(obj)));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool NeedDraw(int pageIndex, int pageCount) => 
            new PageIndexValidator(this.pageRange).ValidateIndex(pageIndex);

        private void RestoreCore(XtraSerializer serializer, object path)
        {
            serializer.CustomObjectConverter = ImageSourceConverter.Instance;
            serializer.DeserializeObject(this, path, "XtraPrintingWatermark");
        }

        public void RestoreFromRegistry(string path)
        {
            this.RestoreCore(new RegistryXtraSerializer(), path);
        }

        public void RestoreFromStream(Stream stream)
        {
            this.RestoreCore(new XmlXtraSerializer(), stream);
        }

        public void RestoreFromXml(string xmlFile)
        {
            this.RestoreCore(new XmlXtraSerializer(), xmlFile);
        }

        private void SaveCore(XtraSerializer serializer, object path)
        {
            serializer.CustomObjectConverter = ImageSourceConverter.Instance;
            serializer.SerializeObject(this, path, "XtraPrintingWatermark");
        }

        public void SaveToRegistry(string path)
        {
            this.SaveCore(new RegistryXtraSerializer(), path);
        }

        public void SaveToStream(Stream stream)
        {
            this.SaveCore(new XmlXtraSerializer(), stream);
        }

        public void SaveToXml(string xmlFile)
        {
            this.SaveCore(new XmlXtraSerializer(), xmlFile);
        }

        [XtraSerializableProperty, Description("Gets or sets the range of pages which contain a watermark."), DefaultValue(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.Watermark.PageRange")]
        public string PageRange
        {
            get => 
                this.pageRange;
            set => 
                this.pageRange = PageRangeParser.ValidateString(value);
        }
    }
}

