namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAcroFormRadioGroupField : PdfAcroFormVisualField
    {
        private PdfAcroFormRadioGroupFieldController<PdfRectangle> buttonController;
        private PdfAcroFormButtonStyle buttonStyle;
        private bool shouldPressedAppearance;

        public PdfAcroFormRadioGroupField(string name, int pageNumber) : base(name, pageNumber)
        {
            this.shouldPressedAppearance = true;
            this.buttonController = new PdfAcroFormRadioGroupFieldController<PdfRectangle>();
        }

        internal PdfAcroFormRadioGroupField(string name, int pageNumber, PdfAcroFormRadioGroupFieldController<PdfRectangle> buttonController) : base(name, pageNumber)
        {
            this.shouldPressedAppearance = true;
            this.buttonController = buttonController;
        }

        public void AddButton(string name, PdfRectangle rect)
        {
            this.buttonController.AddButton(name, rect);
        }

        public void ClearButtons()
        {
            this.buttonController.ClearButtons();
        }

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent)
        {
            PdfButtonFormField radioGroupRootField = new PdfButtonFormField(this, document, parent);
            PdfPage page = document.Pages[base.PageNumber - 1];
            int buttonCount = this.buttonController.ButtonCount;
            string name = string.Empty;
            for (int i = 0; i < buttonCount; i++)
            {
                PdfAcroFormRadioGroupButton<PdfRectangle> button = this.buttonController.GetButton(i);
                PdfWidgetAnnotation radioButtonWidget = new PdfWidgetAnnotation(page, this.CreateWidgetBuilder(button.Rect).SetButtonStyle(this.buttonStyle)) {
                    AppearanceName = "Off"
                };
                PdfButtonFormField field1 = new PdfButtonFormField(document, radioGroupRootField, new PdfRadioGroupFieldAppearance(this), button.Name, radioButtonWidget);
                if (this.buttonController.SelectedIndex == i)
                {
                    name = button.Name;
                }
            }
            if (!string.IsNullOrEmpty(name))
            {
                radioGroupRootField.SetValue(name, fontSearch);
            }
            return radioGroupRootField;
        }

        public PdfAcroFormButtonStyle ButtonStyle
        {
            get => 
                this.buttonStyle;
            set => 
                this.buttonStyle = value;
        }

        public int SelectedIndex
        {
            get => 
                this.buttonController.SelectedIndex;
            set => 
                this.buttonController.SelectedIndex = value;
        }

        public int RadioButtonCount =>
            this.buttonController.ButtonCount;

        public bool ShouldGeneratePressedAppearance
        {
            get => 
                this.shouldPressedAppearance;
            set => 
                this.shouldPressedAppearance = value;
        }

        protected internal override PdfInteractiveFormFieldFlags Flags =>
            (base.Flags | PdfInteractiveFormFieldFlags.Radio) | PdfInteractiveFormFieldFlags.NoToggleToOff;
    }
}

