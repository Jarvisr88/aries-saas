namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class DrawingTextIdentifier : OfficeDrawingIntPropertyBase
    {
        private const int coeff = 0x10000;

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.TextIndex = base.Value / 0x10000;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Value *= 0x10000;
            base.Write(writer);
        }

        public override bool Complex =>
            false;
    }
}

