namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class VisibleToSourceRowsDoublecheckMapper : VisibleToSourceRowsMapper
    {
        public readonly VisibleToSourceRowsListMapper ListMapper;
        public readonly VisibleToSourceRowsSmartMapper SmartMapper;

        public VisibleToSourceRowsDoublecheckMapper(VisibleToSourceRowsMapper parentMapper);
        public override void Dispose();
        private void Do(Action<VisibleToSourceRowsMapper> a);
        private R Do<R>(Func<VisibleToSourceRowsMapper, R> f);
        public override int GetListSourceIndex(int visibleIndex);
        public override int? GetVisibleIndex(int listSourceIndex);
        public override int? HideRow(int sourceIndex);
        public override void InsertRow(int sourceIndex, int? visibleIndex = new int?());
        internal static bool IsUse();
        public override void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public override void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public override int? RemoveRow(int sourceIndex);
        public override void ShowRow(int sourceIndex, int visibleIndex);
        public override int[] ToArray();
        public override IEnumerable<int> ToEnumerable();

        public override bool IsReadOnly { get; }

        public override int VisibleRowCount { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisibleToSourceRowsDoublecheckMapper.<>c <>9;
            public static Func<VisibleToSourceRowsMapper, bool> <>9__7_0;
            public static Func<VisibleToSourceRowsMapper, int> <>9__11_0;

            static <>c();
            internal bool <get_IsReadOnly>b__7_0(VisibleToSourceRowsMapper m);
            internal int <get_VisibleRowCount>b__11_0(VisibleToSourceRowsMapper m);
        }
    }
}

