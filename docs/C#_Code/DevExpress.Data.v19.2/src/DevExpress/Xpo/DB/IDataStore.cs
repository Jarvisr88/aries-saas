namespace DevExpress.Xpo.DB
{
    using System;

    public interface IDataStore
    {
        ModificationResult ModifyData(params ModificationStatement[] dmlStatements);
        SelectedData SelectData(params SelectStatement[] selects);
        UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables);

        DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption { get; }
    }
}

