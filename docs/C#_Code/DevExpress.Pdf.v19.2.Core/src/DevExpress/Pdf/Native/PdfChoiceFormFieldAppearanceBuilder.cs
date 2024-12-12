namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfChoiceFormFieldAppearanceBuilder : PdfTextBasedFormFieldAppearanceBuilder<PdfChoiceFormField>
    {
        public static PdfColor SelectionForeColor = new PdfColor(new double[3]);
        public static PdfColor SelectionBackColor;

        static PdfChoiceFormFieldAppearanceBuilder()
        {
            double[] components = new double[] { 0.6, 0.75686, 0.8549 };
            SelectionBackColor = new PdfColor(components);
        }

        public PdfChoiceFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfChoiceFormField formField, IPdfExportFontProvider fontSearch, PdfRgbaColor backgroundColor) : base(widget, formField, fontSearch, backgroundColor)
        {
        }

        protected override void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            PdfChoiceFormField formField = base.FormField;
            IList<string> list = formField.SelectedValues ?? new List<string>();
            if (!base.FormField.IsCombo)
            {
                IList<PdfOptionsFormFieldOption> options = formField.Options;
                if (options != null)
                {
                    double left = contentRectangle.Left;
                    double right = contentRectangle.Right;
                    double top = contentRectangle.Top;
                    double num4 = base.FontSize + 1.0;
                    int topIndex = formField.TopIndex;
                    int count = options.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i >= topIndex)
                        {
                            PdfColor selectionForeColor;
                            double bottom = top - num4;
                            PdfRectangle rect = new PdfRectangle(left, bottom, right, top);
                            top = bottom;
                            PdfOptionsFormFieldOption option2 = options[i];
                            if (!list.Contains(option2.Text))
                            {
                                selectionForeColor = null;
                            }
                            else
                            {
                                constructor.SetColorForNonStrokingOperations(SelectionBackColor);
                                constructor.FillRectangle(rect);
                                selectionForeColor = SelectionForeColor;
                            }
                            this.DrawTextBox(constructor, rect, option2.ExportText, selectionForeColor);
                        }
                    }
                }
            }
            else if (list.Count > 0)
            {
                string exportText = null;
                IList<PdfOptionsFormFieldOption> options = formField.Options;
                if (options != null)
                {
                    foreach (PdfOptionsFormFieldOption option in options)
                    {
                        if (option.Text.Equals(list[0]))
                        {
                            exportText = option.ExportText;
                            break;
                        }
                    }
                }
                string text = exportText;
                if (exportText == null)
                {
                    string local2 = exportText;
                    text = list[0];
                }
                this.DrawTextBox(constructor, contentRectangle, text, null);
            }
        }

        private void DrawTextBox(PdfFormCommandConstructor constructor, PdfRectangle clipRect, string text, PdfColor color)
        {
            double num;
            base.StartDrawTextBox(constructor, color);
            switch (base.FormField.TextJustification)
            {
                case PdfTextJustification.LeftJustified:
                    num = clipRect.Left + 1.0;
                    break;

                case PdfTextJustification.Centered:
                    num = clipRect.Left + ((clipRect.Width - base.GetTextWidth(text)) / 2.0);
                    break;

                case PdfTextJustification.RightJustified:
                    num = (clipRect.Right - base.GetTextWidth(text)) - 1.0;
                    break;

                default:
                    num = 0.0;
                    break;
            }
            base.DrawTextBoxText(constructor, num, base.CalculateCenteredLineYOffset(clipRect), text);
            base.EndDrawTextBox(constructor);
        }
    }
}

