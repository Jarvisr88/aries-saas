namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Runtime.CompilerServices;

    public class GetDataSourceDisplayNameEventArgs : EventArgs
    {
        public GetDataSourceDisplayNameEventArgs(string dataSourceDisplayName);

        public string DataSourceDisplayName { get; private set; }
    }
}

