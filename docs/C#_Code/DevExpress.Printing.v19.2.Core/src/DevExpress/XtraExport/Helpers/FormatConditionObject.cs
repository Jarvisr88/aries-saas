namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public class FormatConditionObject
    {
        private XlDifferentialFormatting appearance;
        private FormatConditions condition;
        private bool applyToRow;
        private string columnName;
        private string expression;
        private object value1;
        private object value2;

        public XlDifferentialFormatting Appearance
        {
            get => 
                this.appearance;
            set => 
                this.appearance = value;
        }

        public FormatConditions Condition
        {
            get => 
                this.condition;
            set => 
                this.condition = value;
        }

        public bool ApplyToRow
        {
            get => 
                this.applyToRow;
            set => 
                this.applyToRow = value;
        }

        public string ColumnName
        {
            get => 
                this.columnName;
            set => 
                this.columnName = value;
        }

        public string Expression
        {
            get => 
                this.expression;
            set => 
                this.expression = value;
        }

        public object Value1
        {
            get => 
                this.value1;
            set => 
                this.value1 = value;
        }

        public object Value2
        {
            get => 
                this.value2;
            set => 
                this.value2 = value;
        }
    }
}

