namespace DevExpress.Xpo.DB
{
    using System;

    public class DataStoreSerialized : DataStoreSerializedBase
    {
        private readonly IDataStore _nested;

        public DataStoreSerialized(IDataStore nestedProvider)
        {
            this._nested = nestedProvider;
        }

        protected override ModificationResult ProcessModifyData(params ModificationStatement[] dmlStatements) => 
            this.NestedDataStore.ModifyData(dmlStatements);

        protected override SelectedData ProcessSelectData(params SelectStatement[] selects) => 
            this.NestedDataStore.SelectData(selects);

        protected override UpdateSchemaResult ProcessUpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables) => 
            this.NestedDataStore.UpdateSchema(doNotCreateIfFirstTableNotExist, tables);

        [Obsolete("SyncRoot is obsolette, use LockHelper.Lock() or LockHelper.LockAsync() instead.")]
        public override object SyncRoot =>
            this;

        public IDataStore NestedDataStore =>
            this._nested;

        public override DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            this.NestedDataStore.AutoCreateOption;
    }
}

