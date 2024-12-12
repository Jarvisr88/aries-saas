namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    public class VmlShapeImageDataDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlShapeImageData imageData;

        public VmlShapeImageDataDestination(DestinationAndXmlBasedImporter importer, VmlShapeImageData imageData) : base(importer)
        {
            this.imageData = imageData;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.imageData.AlternateRef = this.Importer.ReadAttribute(reader, "althref");
            this.imageData.BiLevel = this.Importer.GetOnOffNullValue(reader, "bilevel");
            this.imageData.BlackLevel = this.Importer.ReadAttribute(reader, "blacklevel");
            this.imageData.ChromaKey = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "chromakey", Color.Empty);
            this.imageData.CropBottom = this.Importer.ReadAttribute(reader, "cropbottom");
            this.imageData.CropLeft = this.Importer.ReadAttribute(reader, "cropleft");
            this.imageData.CropRight = this.Importer.ReadAttribute(reader, "cropright");
            this.imageData.CropTop = this.Importer.ReadAttribute(reader, "croptop");
            this.imageData.DetectMouseClick = this.Importer.GetOnOffNullValue(reader, "detectmouseclick");
            this.imageData.EmbossColor = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "embosscolor", Color.Empty);
            this.imageData.Gain = this.Importer.ReadAttribute(reader, "gain");
            this.imageData.Gamma = this.Importer.ReadAttribute(reader, "gamma");
            this.imageData.Grayscale = this.Importer.GetOnOffNullValue(reader, "grayscale");
            this.imageData.Href = this.Importer.ReadAttribute(reader, "href", "urn:schemas-microsoft-com:office:office");
            this.imageData.Id = this.Importer.ReadAttribute(reader, "id");
            string str = this.Importer.ReadAttribute(reader, "movie", "urn:schemas-microsoft-com:office:office");
            if (!string.IsNullOrEmpty(str))
            {
                this.imageData.Movie = new float?(this.Importer.GetWpSTFloatValue(str, NumberStyles.Float, 0f));
            }
            string str2 = this.Importer.ReadAttribute(reader, "oleid", "urn:schemas-microsoft-com:office:office");
            if (!string.IsNullOrEmpty(str2))
            {
                this.imageData.OleId = new float?(this.Importer.GetWpSTFloatValue(str2, NumberStyles.Float, 0f));
            }
            this.imageData.RecolorTarget = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "recolortarget", Color.Empty);
            this.imageData.Src = this.Importer.ReadAttribute(reader, "src");
            this.imageData.Title = this.Importer.ReadAttribute(reader, "title", "urn:schemas-microsoft-com:office:office");
            string str3 = this.Importer.ReadAttribute(reader, "relid", "urn:schemas-microsoft-com:office:office");
            if (string.IsNullOrEmpty(str3))
            {
                str3 = this.Importer.ReadAttribute(reader, "id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            }
            if (!string.IsNullOrEmpty(str3))
            {
                this.imageData.Image = this.Importer.LookupImageByRelationId(this.Importer.DocumentModel, str3, this.Importer.DocumentRootFolder);
            }
        }
    }
}

