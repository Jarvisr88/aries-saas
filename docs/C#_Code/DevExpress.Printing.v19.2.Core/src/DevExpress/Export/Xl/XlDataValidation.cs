namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlDataValidation
    {
        private readonly List<XlCellRange> ranges = new List<XlCellRange>();
        private readonly List<XlVariantValue> listValues = new List<XlVariantValue>();
        private XlValueObject criteria1 = XlValueObject.Empty;
        private XlValueObject criteria2 = XlValueObject.Empty;

        public XlDataValidation()
        {
            this.AllowBlank = true;
            this.ErrorMessage = string.Empty;
            this.ErrorTitle = string.Empty;
            this.InputPrompt = string.Empty;
            this.PromptTitle = string.Empty;
            this.ShowDropDown = true;
            this.ShowErrorMessage = true;
            this.ShowInputMessage = true;
        }

        public XlDataValidationType Type { get; set; }

        public IList<XlCellRange> Ranges =>
            this.ranges;

        public bool AllowBlank { get; set; }

        public XlDataValidationImeMode ImeMode { get; set; }

        public XlDataValidationOperator Operator { get; set; }

        public XlDataValidationErrorStyle ErrorStyle { get; set; }

        public string ErrorTitle { get; set; }

        public string ErrorMessage { get; set; }

        public string InputPrompt { get; set; }

        public string PromptTitle { get; set; }

        public bool ShowDropDown { get; set; }

        public bool ShowErrorMessage { get; set; }

        public bool ShowInputMessage { get; set; }

        public XlValueObject Criteria1
        {
            get => 
                this.criteria1;
            set
            {
                if (value == null)
                {
                    this.criteria1 = XlValueObject.Empty;
                }
                else
                {
                    this.criteria1 = value;
                }
            }
        }

        public XlValueObject Criteria2
        {
            get => 
                this.criteria2;
            set
            {
                if (value == null)
                {
                    this.criteria2 = XlValueObject.Empty;
                }
                else
                {
                    this.criteria2 = value;
                }
            }
        }

        public IList<XlVariantValue> ListValues =>
            this.listValues;

        public XlCellRange ListRange
        {
            get => 
                this.Criteria1.RangeValue;
            set => 
                this.Criteria1 = value;
        }

        protected internal bool IsExtended =>
            (!this.criteria1.IsRange || string.IsNullOrEmpty(this.criteria1.RangeValue.SheetName)) ? (this.criteria2.IsRange && !string.IsNullOrEmpty(this.criteria2.RangeValue.SheetName)) : true;
    }
}

