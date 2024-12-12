namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class PdfAcroFormChoiceFieldController
    {
        private readonly List<PdfOptionsFormFieldOption> values;
        private IList<int> selectedIndices;
        private bool multiSelect;
        private bool editable;
        private string editableValue;

        public PdfAcroFormChoiceFieldController()
        {
            this.values = new List<PdfOptionsFormFieldOption>();
            this.selectedIndices = new List<int>();
        }

        public PdfAcroFormChoiceFieldController(PdfAcroFormChoiceFieldController controller)
        {
            this.values = new List<PdfOptionsFormFieldOption>();
            this.selectedIndices = new List<int>();
            this.values = new List<PdfOptionsFormFieldOption>(controller.values);
            this.selectedIndices = new List<int>(controller.selectedIndices);
            this.multiSelect = controller.multiSelect;
            this.editable = controller.editable;
            this.editableValue = controller.editableValue;
        }

        public void AddValue(string displayValue)
        {
            this.AddValue(displayValue, displayValue);
        }

        public void AddValue(string displayValue, string exportValue)
        {
            Guard.ArgumentNotNull(displayValue, "displayValue");
            if (string.IsNullOrEmpty(exportValue))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectAcroFormExportValue), "exportValue");
            }
            this.values.Add(new PdfOptionsFormFieldOption(exportValue, displayValue));
        }

        public void ClearSelection()
        {
            this.selectedIndices.Clear();
            this.editableValue = null;
        }

        public void ClearValues()
        {
            this.values.Clear();
        }

        private void SelectIndex(int index)
        {
            if (this.multiSelect)
            {
                this.selectedIndices.Add(index);
            }
            else
            {
                List<int> list1 = new List<int>();
                list1.Add(index);
                this.selectedIndices = list1;
            }
        }

        public bool SelectValue(string exportValue)
        {
            if (this.editable)
            {
                this.editableValue = exportValue;
                return true;
            }
            int count = this.values.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.values[i].Text == exportValue)
                {
                    this.SelectIndex(i);
                    return true;
                }
            }
            return false;
        }

        public void SetSelected(int index, bool value)
        {
            if (this.values.Count == 0)
            {
                throw new ArgumentOutOfRangeException("index", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgCantSetSelectedIndexWithoutValues));
            }
            if ((index < 0) || (index >= this.values.Count))
            {
                throw new ArgumentOutOfRangeException("index", string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectSelectedIndexValue), this.values.Count - 1));
            }
            if (value)
            {
                this.SelectIndex(index);
            }
            else
            {
                this.selectedIndices.Remove(index);
            }
        }

        public List<PdfOptionsFormFieldOption> Values =>
            new List<PdfOptionsFormFieldOption>(this.values);

        public IList<string> SelectedValues
        {
            get
            {
                if ((this.editableValue != null) && this.editable)
                {
                    List<string> list1 = new List<string>();
                    list1.Add(this.editableValue);
                    return list1;
                }
                List<string> list = new List<string>();
                foreach (int num in this.selectedIndices)
                {
                    list.Add(this.values[num].Text);
                }
                return list;
            }
        }

        public IList<int> SelectedIndices =>
            this.selectedIndices;

        public bool Editable
        {
            get => 
                this.editable;
            set
            {
                if (this.editable != value)
                {
                    IList<string> selectedValues = this.SelectedValues;
                    this.ClearSelection();
                    this.editable = value;
                    if (selectedValues.Count > 0)
                    {
                        foreach (PdfOptionsFormFieldOption option in this.values)
                        {
                            if (option.Text == selectedValues[0])
                            {
                                this.SelectValue(option.Text);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public bool MultiSelect
        {
            get => 
                this.multiSelect;
            set => 
                this.multiSelect = value;
        }
    }
}

