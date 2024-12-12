namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;

    public class VisibleToSourceRowsFixedIdentityMapper : VisibleToSourceRowsReadOnlyMapperBase
    {
        private readonly int _Count;

        public VisibleToSourceRowsFixedIdentityMapper(int count);
        public override int GetListSourceIndex(int visibleIndex);
        public override int? GetVisibleIndex(int listSourceIndex);
        public override int[] ToArray();
        public override IEnumerable<int> ToEnumerable();

        public override int VisibleRowCount { get; }
    }
}

