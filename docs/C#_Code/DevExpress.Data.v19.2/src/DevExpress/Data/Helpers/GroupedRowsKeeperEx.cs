namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.IO;
    using System;
    using System.Runtime.CompilerServices;

    public class GroupedRowsKeeperEx : BaseRowsKeeper
    {
        public GroupedRowsKeeperEx(DataController controller);
        protected override bool GetAllRecordsSelected();
        protected virtual int GetExpandedDataCount();
        protected internal override void RestoreCore(object row, int level, object value);
        protected override object RestoreLevelObject(TypedBinaryReader reader);
        public override void Save();
        protected override bool SaveLevelObject(TypedBinaryWriter writer, object obj);

        public int RecordsCount { get; set; }

        public virtual bool AllExpanded { get; }
    }
}

