namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelColumnFilterClauseType
    {
        public ExcelColumnFilterClauseType(ClauseType? clause)
        {
            this.Operator = clause;
            if (this.Operator != null)
            {
                this.Caption = OperationHelper.GetMenuStringByType(clause.Value);
            }
        }

        public ClauseType? Operator { get; private set; }

        public string Caption { get; private set; }
    }
}

