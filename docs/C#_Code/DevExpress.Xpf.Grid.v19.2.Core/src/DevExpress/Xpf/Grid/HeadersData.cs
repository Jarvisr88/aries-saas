namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;

    public class HeadersData : ColumnsRowDataBase
    {
        public HeadersData(DataTreeBuilder treeBuilder) : base(treeBuilder, null)
        {
        }

        internal override object MatchKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

