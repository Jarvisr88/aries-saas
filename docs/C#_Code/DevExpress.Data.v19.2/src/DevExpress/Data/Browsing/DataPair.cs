namespace DevExpress.Data.Browsing
{
    using System;

    public class DataPair
    {
        private object source;
        private string member;

        public DataPair(object source, string member);

        public object Source { get; }

        public string Member { get; }

        public bool IsEmpty { get; }
    }
}

