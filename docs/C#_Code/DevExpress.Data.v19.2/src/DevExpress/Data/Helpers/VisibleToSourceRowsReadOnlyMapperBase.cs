namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class VisibleToSourceRowsReadOnlyMapperBase : VisibleToSourceRowsMapper
    {
        protected VisibleToSourceRowsReadOnlyMapperBase();
        public override int? HideRow(int sourceIndex);
        public override void InsertRow(int sourceIndex, int? visibleIndex = new int?());
        public override void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public override void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public override int? RemoveRow(int sourceIndex);
        public override void ShowRow(int sourceIndex, int visibleIndex);

        public override bool IsReadOnly { get; }
    }
}

