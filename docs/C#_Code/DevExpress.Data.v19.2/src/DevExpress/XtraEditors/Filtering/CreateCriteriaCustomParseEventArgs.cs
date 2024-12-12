namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CreateCriteriaCustomParseEventArgs : EventArgs
    {
        private string filterText;

        public CreateCriteriaCustomParseEventArgs(string filterText);

        public string FilterText { get; }

        public CriteriaOperator Criteria { get; set; }
    }
}

