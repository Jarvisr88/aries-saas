namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class CriteriaOperatorInfo
    {
        private string filterString;
        private string filterText;

        public CriteriaOperatorInfo(CriteriaOperator filterOperator, string filterText)
        {
            this.FilterOperator = filterOperator;
            if (this.FilterOperator != null)
            {
                this.filterString = this.FilterOperator.ToString();
            }
            this.filterText = filterText;
        }

        public override bool Equals(object obj)
        {
            CriteriaOperatorInfo info = obj as CriteriaOperatorInfo;
            return ((info != null) ? Equals(this.FilterString, info.FilterString) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString() => 
            this.FilterText;

        public CriteriaOperator FilterOperator { get; private set; }

        [XtraSerializableProperty]
        public string FilterString =>
            this.filterString;

        [XtraSerializableProperty]
        public string FilterText =>
            this.filterText;
    }
}

