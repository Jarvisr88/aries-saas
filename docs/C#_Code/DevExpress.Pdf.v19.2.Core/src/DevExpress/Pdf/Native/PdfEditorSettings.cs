namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class PdfEditorSettings
    {
        private readonly PdfExportFont fontData;
        private readonly PdfDocumentArea documentArea;
        private readonly bool readOnly;
        private readonly bool usePasswordChar;
        private readonly PdfEditorBorder border;
        private readonly PdfTextJustification textJustification;
        private readonly PdfColor fontColor;
        private readonly double rotationAngle;
        private readonly string fieldName;
        private PdfRgbaColor backgroundColor;
        private double fontSize;

        public event EventHandler OnFontSizeChanged;

        protected PdfEditorSettings(IPdfExportFontProvider fontSearch, PdfDocumentArea area, PdfInteractiveFormField formField, int rotationAngle, bool readOnly)
        {
            PdfExportFontInfo fontInfo = formField.GetFontInfo(fontSearch);
            this.fontData = fontInfo.Font;
            this.fontSize = fontInfo.FontSize;
            this.documentArea = area;
            if (formField != null)
            {
                this.fieldName = formField.FullName;
                PdfWidgetAnnotation widget = formField.Widget;
                PdfInteractiveFormFieldFlags flags = formField.Flags;
                this.readOnly = readOnly || flags.HasFlag(PdfInteractiveFormFieldFlags.ReadOnly);
                this.usePasswordChar = flags.HasFlag(PdfInteractiveFormFieldFlags.Password);
                this.backgroundColor = PdfRgbaColor.Create(widget.BackgroundColor);
                this.border = new PdfEditorBorder(widget);
                this.textJustification = formField.TextJustification;
                this.rotationAngle = rotationAngle;
                IEnumerable<PdfCommand> appearanceCommands = formField.AppearanceCommands;
                if (appearanceCommands != null)
                {
                    foreach (PdfCommand command in appearanceCommands)
                    {
                        PdfSetColorCommand command2 = command as PdfSetColorCommand;
                        if (command2 != null)
                        {
                            this.fontColor = new PdfColor((double[]) command2.Components.Clone());
                        }
                        else
                        {
                            PdfSetRGBColorSpaceCommand command3 = command as PdfSetRGBColorSpaceCommand;
                            if (command3 != null)
                            {
                                double[] components = new double[] { command3.R, command3.G, command3.B };
                                this.fontColor = new PdfColor(components);
                            }
                            else
                            {
                                PdfSetGrayColorSpaceCommand command4 = command as PdfSetGrayColorSpaceCommand;
                                if (command4 != null)
                                {
                                    double[] components = new double[] { command4.Gray };
                                    this.fontColor = new PdfColor(components);
                                }
                                else
                                {
                                    PdfSetCMYKColorSpaceCommand command5 = command as PdfSetCMYKColorSpaceCommand;
                                    if (command5 == null)
                                    {
                                        continue;
                                    }
                                    double[] components = new double[] { command5.C, command5.M, command5.Y, command5.K };
                                    this.fontColor = new PdfColor(components);
                                }
                            }
                        }
                        break;
                    }
                }
                this.fontColor ??= new PdfColor(new double[3]);
            }
        }

        public PdfExportFont FontData =>
            this.fontData;

        public double FontSize
        {
            get => 
                this.fontSize;
            protected set
            {
                if (this.fontSize != value)
                {
                    this.fontSize = value;
                    this.OnFontSizeChanged(this, EventArgs.Empty);
                }
            }
        }

        public PdfDocumentArea DocumentArea =>
            this.documentArea;

        public bool ReadOnly =>
            this.readOnly;

        public bool UsePasswordChar =>
            this.usePasswordChar;

        public PdfRgbaColor BackgroundColor
        {
            get => 
                this.backgroundColor;
            set => 
                this.backgroundColor = value;
        }

        public PdfEditorBorder Border =>
            this.border;

        public PdfTextJustification TextJustification =>
            this.textJustification;

        public PdfColor FontColor =>
            this.fontColor;

        public double RotationAngle =>
            this.rotationAngle;

        public string FieldName =>
            this.fieldName;

        public abstract PdfEditorType EditorType { get; }

        public abstract object EditValue { get; }
    }
}

