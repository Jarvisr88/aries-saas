namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class VisibleToSourceRowsListMapper : VisibleToSourceRowsMapper
    {
        private readonly LohPooled.OrdinaryList<int> Map;
        private readonly LohPooled.OrdinaryDictionary<int, int> ReverseMap;
        private readonly List<int> InvalidValidInvalidValidInvalidMapPoints;
        private int _IsChangedEnoughForSmartCounter;

        public VisibleToSourceRowsListMapper(VisibleToSourceRowsMapper mapper);
        public VisibleToSourceRowsListMapper(IEnumerable<int> initialVisibleState, int hintCount);
        private void ClearReverseCache(bool asFullyValid = false);
        public override void Dispose();
        private void ExtendInvalidBucketStartingAtPosTo(int invalidBucketPos, int lastInvalidPlusOne);
        public override int GetListSourceIndex(int visibleIndex);
        public override int? GetVisibleIndex(int listSourceIndex);
        private int? GetVisibleIndexAndClearReverseCache(int listSourceIndex, bool asFullyValid = false);
        public override int? HideRow(int sourceIndex);
        private int? HideRowAndClearReverseCache(int sourceIndex, bool asFullyValid = false);
        public override void InsertRow(int sourceIndex, int? visibleIndex = new int?());
        private void InvalidateReverseMap(int firstInvalid, int lastInvalidPlusOne);
        public override void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public override void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public override int? RemoveRow(int sourceIndex);
        public override void SetRange(int startPos, int[] newValues);
        public override void SetValue(int visibleIndex, int sourceIndex);
        public override void ShowRow(int sourceIndex, int visibleIndex);
        public override int[] ToArray();
        public override IEnumerable<int> ToEnumerable();
        private void ValidateReverseMap(int firstValid, int lastValidPlusOne);

        public override int VisibleRowCount { get; }

        public override bool IsSetRangeAble { get; }

        public override bool IsReadOnly { get; }

        public bool IsChangedEnoughForSmart { get; }
    }
}

