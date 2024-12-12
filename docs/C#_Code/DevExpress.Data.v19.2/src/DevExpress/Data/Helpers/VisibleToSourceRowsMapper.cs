namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class VisibleToSourceRowsMapper : IDisposable
    {
        protected VisibleToSourceRowsMapper();
        public virtual bool Contains(int listSourceIndex);
        public virtual void Dispose();
        public abstract int GetListSourceIndex(int visibleIndex);
        public abstract int? GetVisibleIndex(int listSourceIndex);
        public abstract int? HideRow(int sourceIndex);
        public abstract void InsertRow(int sourceIndex, int? visibleIndex = new int?());
        public abstract void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public abstract void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public abstract int? RemoveRow(int sourceIndex);
        public virtual void SetRange(int startPos, int[] newValues);
        public virtual void SetValue(int visibleIndex, int sourceIndex);
        public abstract void ShowRow(int sourceIndex, int visibleIndex);
        public abstract int[] ToArray();
        public abstract IEnumerable<int> ToEnumerable();

        public abstract bool IsReadOnly { get; }

        public abstract int VisibleRowCount { get; }

        public virtual bool IsSetRangeAble { get; }
    }
}

