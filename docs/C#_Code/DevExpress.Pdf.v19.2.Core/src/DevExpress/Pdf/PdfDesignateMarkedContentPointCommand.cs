namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDesignateMarkedContentPointCommand : PdfCommand
    {
        internal const string Name = "MP";
        private string tag;

        protected PdfDesignateMarkedContentPointCommand()
        {
        }

        internal PdfDesignateMarkedContentPointCommand(PdfStack operands)
        {
            this.ParseTag(operands);
        }

        public PdfDesignateMarkedContentPointCommand(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectMarkedContentTag), "tag");
            }
            this.tag = tag;
        }

        protected void ParseTag(PdfStack operands)
        {
            this.tag = operands.PopName(true);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(new PdfName(this.tag));
            writer.WriteSpace();
            writer.WriteString("MP");
        }

        public string Tag =>
            this.tag;
    }
}

