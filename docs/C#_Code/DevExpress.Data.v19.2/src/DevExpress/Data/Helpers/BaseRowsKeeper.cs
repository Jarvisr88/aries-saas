namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class BaseRowsKeeper : IDisposable
    {
        public const int DataRowsLevel = -1;
        private DataController controller;
        private readonly Dictionary<int, Dictionary<object, object>> hash;
        private KeyValuePair<int, Dictionary<object, object>>[] levels;
        private bool allRecordsSelected;
        public static readonly object NullObject;

        static BaseRowsKeeper();
        protected BaseRowsKeeper(DataController controller);
        public void Clear();
        public bool Contains(object rowKey, int level);
        public void Dispose();
        protected virtual bool GetAllRecordsSelected();
        protected int GetControllerRow(int listSourceRow);
        protected GroupRowInfo GetGroupRow(int listSourceRow, int level);
        public object GetGroupRowKeyEx(GroupRowInfo group);
        protected int GetListSourceRow(GroupRowInfo group);
        protected int GetListSourceRow(int controllerRow);
        public object GetRowKey(GroupRowInfo group);
        public object GetRowKey(int listSourceRow);
        private object GetValue(GroupRowInfo group);
        protected internal void RemoveSelected(object rowKey, int level);
        public bool Restore(object rowKey, object row);
        protected internal abstract void RestoreCore(object row, int level, object value);
        public void RestoreFromStream(Stream stream);
        protected virtual object RestoreLevelObject(TypedBinaryReader reader);
        public abstract void Save();
        protected virtual bool SaveLevelObject(TypedBinaryWriter writer, object obj);
        public void SaveToStream(Stream stream);
        protected void SetSelected(object rowKey, int level, object value);

        public bool AnyGroupRows { get; }

        public bool AnyDataRows { get; }

        protected bool AllRecordsSelected { get; set; }

        protected Dictionary<int, Dictionary<object, object>> Hash { get; }

        protected DataController Controller { get; }

        protected BaseDataControllerHelper Helper { get; }

        public bool IsEmpty { get; }

        public int Count { get; }

        protected internal KeyValuePair<int, Dictionary<object, object>>[] Levels { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseRowsKeeper.<>c <>9;
            public static Func<KeyValuePair<int, Dictionary<object, object>>, int> <>9__37_0;

            static <>c();
            internal int <get_Levels>b__37_0(KeyValuePair<int, Dictionary<object, object>> pair);
        }
    }
}

