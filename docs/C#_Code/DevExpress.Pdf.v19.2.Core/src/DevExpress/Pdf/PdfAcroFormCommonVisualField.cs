namespace DevExpress.Pdf
{
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class PdfAcroFormCommonVisualField : PdfAcroFormVisualField
    {
        private PdfRectangle rectangle;

        protected PdfAcroFormCommonVisualField(string name, int pageNumber, PdfRectangle rectangle) : base(name, pageNumber)
        {
            if (rectangle == null)
            {
                throw new ArgumentNullException("rect");
            }
            this.rectangle = rectangle;
        }

        internal virtual PdfAdditionalActions CreateAdditionalActions(PdfDocument document) => 
            null;

        protected internal virtual PdfCommandList CreateAppearanceCommands(IPdfExportFontProvider fontSearch, PdfInteractiveForm interactiveForm)
        {
            PdfAcroFormFieldAppearance appearance = base.Appearance;
            if (appearance == null)
            {
                return null;
            }
            PdfCommandList list = new PdfCommandList();
            PdfExportFont matchingFont = fontSearch.GetMatchingFont(appearance.FontFamily, appearance.FontStyle);
            PdfFont appearanceFont = matchingFont?.AppearanceFont;
            double fontSize = appearance.FontSize;
            if (appearanceFont != null)
            {
                list.Add(new PdfSetTextFontCommand(appearanceFont, fontSize));
                interactiveForm.Resources.AddFont(appearanceFont);
            }
            PdfRGBColor foreColor = appearance.ForeColor;
            if (foreColor == null)
            {
                list.Add(new PdfSetGrayColorSpaceForNonStrokingOperationsCommand(0.0));
            }
            else
            {
                list.Add(new PdfSetRGBColorSpaceForNonStrokingOperationsCommand(foreColor.R, foreColor.G, foreColor.B));
            }
            return list;
        }

        internal PdfWidgetAnnotation CreateWidget(PdfDocument document)
        {
            int count = document.Pages.Count;
            int pageNumber = base.PageNumber;
            if ((pageNumber > count) || (pageNumber < 1))
            {
                throw new ArgumentOutOfRangeException("pageNumber", string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPageNumber), count));
            }
            return new PdfWidgetAnnotation(document.Pages[pageNumber - 1], this.CreateWidgetBuilder(this.rectangle));
        }

        public PdfRectangle Rectangle
        {
            get => 
                this.rectangle;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Rectangle");
                }
                this.rectangle = value;
            }
        }

        public PdfAcroFormStringAlignment TextAlignment { get; set; }
    }
}

