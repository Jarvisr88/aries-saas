namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetRenderingIntentCommand : PdfCommand
    {
        internal const string Name = "ri";
        private readonly PdfRenderingIntent renderingIntent;

        internal PdfSetRenderingIntentCommand(PdfStack operands)
        {
            this.renderingIntent = PdfEnumToStringConverter.Parse<PdfRenderingIntent>(operands.PopName(true), false);
        }

        public PdfSetRenderingIntentCommand(PdfRenderingIntent renderingIntent)
        {
            this.renderingIntent = renderingIntent;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetRenderingIntent(this.renderingIntent);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(new PdfName(PdfEnumToStringConverter.Convert<PdfRenderingIntent>(this.renderingIntent, false)));
            writer.WriteSpace();
            writer.WriteString("ri");
        }

        public PdfRenderingIntent RenderingIntent =>
            this.renderingIntent;
    }
}

