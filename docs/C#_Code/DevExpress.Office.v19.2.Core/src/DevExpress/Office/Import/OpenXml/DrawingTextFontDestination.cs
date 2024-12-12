namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class DrawingTextFontDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingTextFont textFont;

        public DrawingTextFontDestination(DestinationAndXmlBasedImporter importer, DrawingTextFont textFont) : base(importer)
        {
            Guard.ArgumentNotNull(textFont, "textFont");
            this.textFont = textFont;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.Importer.DocumentModel.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.DocumentModel.BeginUpdate();
            string str = this.Importer.ReadAttribute(reader, "typeface");
            if (!string.IsNullOrEmpty(str))
            {
                this.textFont.Typeface = str;
            }
            string str2 = this.Importer.ReadAttribute(reader, "panose");
            if (!string.IsNullOrEmpty(str2) && (str2.Length != 20))
            {
                this.Importer.ThrowInvalidFile();
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this.textFont.Panose = str2;
            }
            int num = this.Importer.GetIntegerValue(reader, "pitchFamily", 0);
            if (Math.Abs(num) > 0xff)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.textFont.PitchFamily = (byte) num;
            int num2 = this.Importer.GetIntegerValue(reader, "charset", 1);
            if (Math.Abs(num2) > 0xff)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.textFont.Charset = (byte) num2;
        }
    }
}

