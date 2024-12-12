namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public abstract class BaseSelectionCollection<T> : IDisposable
    {
        private Dictionary<T, object> rows;
        private DevExpress.Data.Selection.SelectionController selectionController;

        protected BaseSelectionCollection(DevExpress.Data.Selection.SelectionController selectionController);
        protected internal virtual int CalcCRC();
        public static int CalcCRC(int[] source);
        public void Clear();
        public virtual void Dispose();
        protected internal abstract T GetRowObjectByControllerRow(int controllerRow);
        public bool GetRowSelected(int controllerRow);
        public object GetRowSelectedObject(int controllerRow);
        protected internal bool GetSelected(T row);
        protected internal Dictionary<T, object> GetSelectedDictionary();
        protected object GetSelectedObject(T row);
        protected virtual void OnSelectionChanged(SelectionChangedEventArgs e);
        public void SetRowSelected(int controllerRow, bool selected, object selectionObject);
        protected void SetSelected(int controllerRow, T row, bool selected, object selectionObject);
        protected virtual void SetSelectionObject(T row, object selectionObject);

        protected DataController Controller { get; }

        protected DevExpress.Data.Selection.SelectionController SelectionController { get; }

        public int Count { get; }

        protected Dictionary<T, object> Rows { get; }
    }
}

