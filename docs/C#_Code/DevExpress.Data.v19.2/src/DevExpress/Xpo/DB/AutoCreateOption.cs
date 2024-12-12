namespace DevExpress.Xpo.DB
{
    using System;

    [Serializable]
    public enum AutoCreateOption
    {
        DatabaseAndSchema,
        SchemaOnly,
        None,
        SchemaAlreadyExists
    }
}

