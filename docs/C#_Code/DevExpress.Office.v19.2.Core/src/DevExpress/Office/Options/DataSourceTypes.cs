namespace DevExpress.Office.Options
{
    using System;

    [Flags]
    public enum DataSourceTypes
    {
        Sql = 1,
        Excel = 2,
        EntityFramework = 4,
        Object = 8,
        Json = 0x10,
        Federation = 0x20,
        All = 0x3f
    }
}

