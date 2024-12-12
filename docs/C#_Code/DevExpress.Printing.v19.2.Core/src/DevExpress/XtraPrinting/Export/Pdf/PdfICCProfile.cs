namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Printing;
    using System;
    using System.IO;

    public class PdfICCProfile : PdfDocumentStreamObject
    {
        public PdfICCProfile(bool compressed) : base(compressed)
        {
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("N", 3);
            using (Stream stream = ResFinder.GetManifestResourceStream("Core.PdfExport.ICCProfile.bin"))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                base.Stream.SetBytes(buffer);
            }
        }
    }
}

