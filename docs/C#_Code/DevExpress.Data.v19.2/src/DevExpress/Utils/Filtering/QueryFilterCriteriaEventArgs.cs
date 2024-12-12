namespace DevExpress.Utils.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Diagnostics;

    public class QueryFilterCriteriaEventArgs : EventArgs
    {
        private static readonly object CriteriaNotSet = new object();
        private readonly string path;
        private readonly IValueViewModel value;
        private CriteriaOperator criteria;
        private object resultCore = CriteriaNotSet;

        protected QueryFilterCriteriaEventArgs(string path, IValueViewModel value)
        {
            this.path = path;
            this.value = value;
        }

        [DebuggerStepThrough]
        internal DevExpress.Utils.Filtering.QueryFilterCriteriaEventArgs Initialize(CriteriaOperator criteria)
        {
            this.resultCore = CriteriaNotSet;
            this.criteria = criteria;
            return this;
        }

        public string Path =>
            this.path;

        public IValueViewModel Value =>
            this.value;

        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaNotSet != this.resultCore) ? ((this.resultCore != null) ? ((CriteriaOperator) this.resultCore) : null) : this.criteria;
            set => 
                this.resultCore = value;
        }
    }
}

