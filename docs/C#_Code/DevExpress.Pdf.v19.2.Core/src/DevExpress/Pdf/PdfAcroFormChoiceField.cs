namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfAcroFormChoiceField : PdfAcroFormCommonVisualField
    {
        private PdfAcroFormChoiceFieldController controller;

        protected PdfAcroFormChoiceField(string name, int pageNumber, PdfRectangle rectangle) : base(name, pageNumber, rectangle)
        {
            this.controller = new PdfAcroFormChoiceFieldController();
        }

        public void AddValue(string displayValue)
        {
            this.controller.AddValue(displayValue);
        }

        public void AddValue(string displayValue, string exportValue)
        {
            this.controller.AddValue(displayValue, exportValue);
        }

        internal void ApplyController(PdfAcroFormChoiceFieldController controller)
        {
            this.controller = new PdfAcroFormChoiceFieldController(controller);
        }

        public void ClearSelection()
        {
            this.controller.ClearSelection();
        }

        public void ClearValues()
        {
            this.controller.ClearValues();
        }

        public bool SelectValue(string exportValue) => 
            this.controller.SelectValue(exportValue);

        public void SetSelected(int index, bool value)
        {
            this.controller.SetSelected(index, value);
        }

        protected PdfAcroFormChoiceFieldController Controller =>
            this.controller;

        internal List<PdfOptionsFormFieldOption> Values =>
            this.controller.Values;

        internal IList<string> SelectedValues =>
            this.controller.SelectedValues;

        internal bool IsMultiSelect =>
            this.controller.MultiSelect;
    }
}

