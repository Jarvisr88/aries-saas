namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MetadataWorkspaceInfo : RuntimeWrapper
    {
        public MetadataWorkspaceInfo(object value) : base(EdmConst.MetadataWorkspace, value)
        {
        }

        public IEnumerable<object> GetItems(DataSpace dataSpace)
        {
            IEnumerable source = base.GetMethodAccessor("GetItems").Invoke(() => new object[] { (int) dataSpace }) as IEnumerable;
            return ((source != null) ? source.Cast<object>() : ((IEnumerable<object>) new object[0]));
        }

        protected override bool CheckOnlyTypeName =>
            true;
    }
}

