namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;

    public class VisibleToSourceRowsReadOnlyProxyMapper : VisibleToSourceRowsReadOnlyMapperBase
    {
        private readonly VisibleToSourceRowsMapper Target;

        public VisibleToSourceRowsReadOnlyProxyMapper(VisibleToSourceRowsMapper _Target);
        public override int GetListSourceIndex(int visibleIndex);
        public override int? GetVisibleIndex(int listSourceIndex);
        public override int[] ToArray();
        public override IEnumerable<int> ToEnumerable();

        public override int VisibleRowCount { get; }
    }
}

