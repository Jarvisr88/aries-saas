namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;

    public static class DataContextHelper
    {
        public static void DataContextAction(IDataContextService dataContextService, DataContextOptions options, Action<DataContext> action);
    }
}

