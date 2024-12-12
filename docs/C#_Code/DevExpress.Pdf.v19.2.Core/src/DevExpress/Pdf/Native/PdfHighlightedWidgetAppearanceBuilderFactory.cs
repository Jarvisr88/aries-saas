namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfHighlightedWidgetAppearanceBuilderFactory : PdfVisitorBasedFactory<PdfInteractiveFormField, IPdfAnnotationAppearanceBuilder>, IPdfInteractiveFormFieldVisitor
    {
        private readonly IPdfExportFontProvider fontSearch;
        private readonly PdfRgbaColor backgroundColor;

        public PdfHighlightedWidgetAppearanceBuilderFactory(IPdfExportFontProvider fontSearch, PdfRgbaColor backgroundColor)
        {
            this.fontSearch = fontSearch;
            this.backgroundColor = backgroundColor;
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfButtonFormField formField)
        {
            PdfInteractiveFormFieldFlags flags = formField.Flags;
            if (flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
            {
                base.SetResult(null);
            }
            else
            {
                PdfAcroFormButtonStyle check;
                bool isRadioButton = flags.HasFlag(PdfInteractiveFormFieldFlags.Radio);
                PdfWidgetAppearanceCharacteristics appearanceCharacteristics = formField.Widget.AppearanceCharacteristics;
                check = check = isRadioButton ? PdfAcroFormButtonStyle.Circle : PdfAcroFormButtonStyle.Check;
                if (appearanceCharacteristics != null)
                {
                    string caption = appearanceCharacteristics.Caption;
                    if (caption == "4")
                    {
                        check = PdfAcroFormButtonStyle.Check;
                    }
                    else if (caption == "l")
                    {
                        check = PdfAcroFormButtonStyle.Circle;
                    }
                    else if (caption == "H")
                    {
                        check = PdfAcroFormButtonStyle.Star;
                    }
                    else if (caption == "8")
                    {
                        check = PdfAcroFormButtonStyle.Cross;
                    }
                    else if (caption == "u")
                    {
                        check = PdfAcroFormButtonStyle.Diamond;
                    }
                    else if (caption == "n")
                    {
                        check = PdfAcroFormButtonStyle.Square;
                    }
                }
                PdfColor foreColor = null;
                if (formField.AppearanceCommands != null)
                {
                    foreach (PdfCommand command in formField.AppearanceCommands)
                    {
                        PdfSetRGBColorSpaceForNonStrokingOperationsCommand command2 = command as PdfSetRGBColorSpaceForNonStrokingOperationsCommand;
                        if (command2 != null)
                        {
                            double[] components = new double[] { command2.R, command2.G, command2.B };
                            foreColor = new PdfColor(components);
                        }
                        else
                        {
                            PdfSetGrayColorSpaceForNonStrokingOperationsCommand command3 = command as PdfSetGrayColorSpaceForNonStrokingOperationsCommand;
                            if (command3 != null)
                            {
                                double[] components = new double[] { command3.Gray };
                                foreColor = new PdfColor(components);
                            }
                            else
                            {
                                PdfSetCMYKColorSpaceForNonStrokingOperationsCommand command4 = command as PdfSetCMYKColorSpaceForNonStrokingOperationsCommand;
                                if (command4 == null)
                                {
                                    continue;
                                }
                                double[] components = new double[] { command4.C, command4.M, command4.Y, command4.K };
                                foreColor = new PdfColor(components);
                            }
                        }
                        break;
                    }
                }
                foreColor = new PdfColor(new double[1]);
                base.SetResult(new PdfButtonFormFieldAppearanceBuilder(formField.Widget, formField, check, foreColor, formField.Flags.HasFlag(PdfInteractiveFormFieldFlags.Radio) ? (formField.Widget.AppearanceName != "Off") : (((string) formField.Value) == formField.OnState), isRadioButton, this.backgroundColor));
            }
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfChoiceFormField formField)
        {
            base.SetResult(new PdfChoiceFormFieldAppearanceBuilder(formField.Widget, formField, this.fontSearch, this.backgroundColor));
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfInteractiveFormField formField)
        {
            base.SetResult(null);
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfTextFormField formField)
        {
            base.SetResult(new PdfTextFormFieldAppearanceBuilder(formField.Widget, formField, this.fontSearch, this.backgroundColor));
        }

        protected override void Visit(PdfInteractiveFormField input)
        {
            input.Accept(this);
        }
    }
}

