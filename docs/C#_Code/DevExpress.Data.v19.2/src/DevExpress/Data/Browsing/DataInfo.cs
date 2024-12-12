namespace DevExpress.Data.Browsing
{
    using System;

    public class DataInfo : DataPair
    {
        private string displayName;

        public DataInfo(object source, string member, string displayName);

        public string DisplayName { get; }
    }
}

