namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfListBoxSettings : PdfChoiceEditorSettings
    {
        private readonly bool multiselect;
        private readonly IList<string> selectedValues;
        private IList<int> selectedIndices;

        public PdfListBoxSettings(IPdfExportFontProvider fontSearch, PdfDocumentArea area, PdfChoiceFormField field, int rotationAngle, bool readOnly) : base(fontSearch, area, field, rotationAngle, readOnly)
        {
            this.multiselect = field.Flags.HasFlag(PdfInteractiveFormFieldFlags.MultiSelect);
            IList<string> list1 = (field.SelectedValues == null) ? new List<string>() : field.SelectedValues;
            this.selectedValues = list1;
        }

        public bool Multiselect =>
            this.multiselect;

        public IList<int> SelectedIndices
        {
            get
            {
                if (this.selectedIndices == null)
                {
                    IList<PdfOptionsFormFieldOption> values = base.Values;
                    if (values == null)
                    {
                        this.selectedIndices = new List<int>(0);
                    }
                    else
                    {
                        int count = values.Count;
                        this.selectedIndices = new List<int>(count);
                        foreach (string str in this.selectedValues)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (values[i].Text.Equals(str))
                                {
                                    this.selectedIndices.Add(i);
                                    break;
                                }
                            }
                        }
                    }
                }
                return this.selectedIndices;
            }
        }

        public override PdfEditorType EditorType =>
            PdfEditorType.ListBox;

        public override object EditValue =>
            this.selectedValues;
    }
}

