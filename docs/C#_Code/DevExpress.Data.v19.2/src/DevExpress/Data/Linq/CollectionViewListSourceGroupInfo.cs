namespace DevExpress.Data.Linq
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CollectionViewListSourceGroupInfo : ListSourceGroupInfo
    {
        private List<object> _Summary;
        public List<ListSourceGroupInfo> ChildrenGroups;
        public IList Rows;

        public CollectionViewListSourceGroupInfo();

        public override List<object> Summary { get; }
    }
}

