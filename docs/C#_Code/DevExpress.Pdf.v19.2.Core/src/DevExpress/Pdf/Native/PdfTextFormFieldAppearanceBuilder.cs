namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextFormFieldAppearanceBuilder : PdfTextFieldAppearanceBuilder<PdfTextFormField>
    {
        public PdfTextFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfTextFormField formField, IPdfExportFontProvider fontSearch, PdfRgbaColor backgroundColor) : base(widget, formField, fontSearch, backgroundColor)
        {
        }

        public PdfTextFieldMeasurer CreateMeasurer() => 
            new PdfTextFieldMeasurer(this.CreateStringFormat(), base.FormField, this, this.Multiline);

        protected override void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            PdfTextFormField formField = base.FormField;
            string text = formField.Text;
            if (!string.IsNullOrEmpty(text))
            {
                PdfInteractiveFormFieldFlags flags = formField.Flags;
                int? maxLen = formField.MaxLen;
                int num = (maxLen != null) ? maxLen.GetValueOrDefault() : 0;
                if (flags.HasFlag(PdfInteractiveFormFieldFlags.Comb) && (num != 0))
                {
                    base.StartDrawTextBox(constructor, null);
                    char[] chArray = text.ToCharArray();
                    double num2 = constructor.BoundingBox.Width / ((double) num);
                    string str2 = chArray[0].ToString();
                    int num3 = Math.Min(chArray.Length, num);
                    double textWidth = base.GetTextWidth(str2);
                    double xOffset = (num2 - textWidth) / 2.0;
                    PdfTextJustification textJustification = formField.TextJustification;
                    if (textJustification == PdfTextJustification.Centered)
                    {
                        xOffset += num2 * ((num / 2) - (num3 / 2));
                    }
                    else if (textJustification == PdfTextJustification.RightJustified)
                    {
                        xOffset += num2 * (num - num3);
                    }
                    base.DrawTextBoxText(constructor, xOffset, base.CalculateCenteredLineYOffset(contentRectangle), str2);
                    for (int i = 1; i < num3; i++)
                    {
                        str2 = chArray[i].ToString();
                        double num7 = base.GetTextWidth(str2);
                        base.DrawTextBoxText(constructor, num2 + ((textWidth - num7) / 2.0), 0.0, str2);
                        textWidth = num7;
                    }
                    base.EndDrawTextBox(constructor);
                }
                else
                {
                    base.DrawTextField(constructor, contentRectangle, text);
                }
            }
        }

        protected override void DrawSolidBorder(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            base.DrawSolidBorder(constructor, contentRectangle);
            if (base.FormField.Flags.HasFlag(PdfInteractiveFormFieldFlags.Comb))
            {
                int? maxLen = base.FormField.MaxLen;
                this.DrawTextCombs(constructor, contentRectangle, (maxLen != null) ? maxLen.GetValueOrDefault() : 0);
            }
        }

        protected override bool Multiline =>
            base.FormField.Flags.HasFlag(PdfInteractiveFormFieldFlags.Multiline);
    }
}

