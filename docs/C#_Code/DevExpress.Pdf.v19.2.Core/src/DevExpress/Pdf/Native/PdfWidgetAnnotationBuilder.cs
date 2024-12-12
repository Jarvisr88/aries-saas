namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfWidgetAnnotationBuilder : PdfAnnotationBuilder, IPdfWidgetAnnotationBuilder, IPdfAnnotationBuilder
    {
        private PdfAnnotationBorderStyleBuilder borderStyleBuilder;
        private PdfWidgetAppearanceCharacteristicsBuilder appearanceCharacteristicsBuilder;

        public PdfWidgetAnnotationBuilder(PdfRectangle rect) : base(rect)
        {
        }

        public PdfWidgetAppearanceCharacteristics CreateAppearanceCharacteristics()
        {
            if (this.appearanceCharacteristicsBuilder != null)
            {
                return this.appearanceCharacteristicsBuilder.CreateAppearanceCharacteristics();
            }
            PdfWidgetAppearanceCharacteristicsBuilder appearanceCharacteristicsBuilder = this.appearanceCharacteristicsBuilder;
            return null;
        }

        public PdfAnnotationBorderStyle CreateBorderStyle()
        {
            if (this.borderStyleBuilder != null)
            {
                return this.borderStyleBuilder.CreateBorderStyle();
            }
            PdfAnnotationBorderStyleBuilder borderStyleBuilder = this.borderStyleBuilder;
            return null;
        }

        public PdfWidgetAnnotationBuilder SetAppearance(PdfAcroFormFieldAppearance appearance)
        {
            this.AppearanceCharacteristicsBuilder.SetAppearance(appearance);
            PdfAcroFormBorderAppearance borderAppearance = appearance.BorderAppearance;
            if (borderAppearance != null)
            {
                this.BorderStyleBuilder.SetBorderAppearance(borderAppearance);
            }
            return this;
        }

        public PdfWidgetAnnotationBuilder SetButtonStyle(PdfAcroFormButtonStyle buttonStyle)
        {
            this.AppearanceCharacteristicsBuilder.SetButtonStyle(buttonStyle);
            return this;
        }

        public PdfWidgetAnnotationBuilder SetRotation(PdfAcroFormFieldRotation rotation)
        {
            this.AppearanceCharacteristicsBuilder.SetRotation(rotation);
            return this;
        }

        private PdfAnnotationBorderStyleBuilder BorderStyleBuilder
        {
            get
            {
                this.borderStyleBuilder ??= new PdfAnnotationBorderStyleBuilder();
                return this.borderStyleBuilder;
            }
        }

        private PdfWidgetAppearanceCharacteristicsBuilder AppearanceCharacteristicsBuilder
        {
            get
            {
                this.appearanceCharacteristicsBuilder ??= new PdfWidgetAppearanceCharacteristicsBuilder();
                return this.appearanceCharacteristicsBuilder;
            }
        }
    }
}

