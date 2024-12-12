namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;

    public class AsyncRowsInfo
    {
        private int invalidRowCount;
        private DateTime? rowsClearTime;
        private Dictionary<int, AsyncRowInfo> rows;

        public void Add(int index, AsyncRowInfo info);
        public void CheckRemoveInvalidRows(bool force);
        public List<KeyValuePair<int, AsyncRowInfo>> GetLoadingRows();
        public AsyncRowInfo GetRow(int index);
        public bool IsRowLoaded(int index);
        public void MakeAllRowsInvalid();
        public void OnLoaded(int index, object rowObject, object key);
        public void Remove(int index);

        public int Count { get; }

        protected internal Dictionary<int, AsyncRowInfo> Rows { get; }
    }
}

