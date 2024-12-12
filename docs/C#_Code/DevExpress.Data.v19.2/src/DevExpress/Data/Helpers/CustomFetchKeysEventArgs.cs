namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomFetchKeysEventArgs : EventArgs
    {
        private CriteriaOperator where;
        private ServerModeOrderDescriptor[] order;
        private int skip;
        private int take;

        public CustomFetchKeysEventArgs(CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);

        public bool Handled { get; set; }

        public object[] Result { get; set; }

        public CriteriaOperator Where { get; }

        public ServerModeOrderDescriptor[] Order { get; }

        public int Skip { get; }

        public int Take { get; }
    }
}

