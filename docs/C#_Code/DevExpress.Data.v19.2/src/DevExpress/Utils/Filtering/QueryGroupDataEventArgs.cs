namespace DevExpress.Utils.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class QueryGroupDataEventArgs : QueryDataEventArgs<GroupData>
    {
        internal QueryGroupDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected sealed override GroupData CreateData(IDictionary<string, object> memberValues) => 
            new GroupData(memberValues);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool RootQuery =>
            !base.Result.HasParentValues;

        public string GroupPropertyPath =>
            base.Result.GetPath() ?? base.PropertyPath;

        public object[] ParentValues =>
            UniqueValues.RestoreNulls(base.Result.GetParentValues(), null);

        public CriteriaOperator ParentCriteria =>
            base.Result.GetParentCriteria();
    }
}

