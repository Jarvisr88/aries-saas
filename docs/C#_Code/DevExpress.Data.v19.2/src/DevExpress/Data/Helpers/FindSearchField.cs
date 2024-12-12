namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class FindSearchField
    {
        private FindColumnInfo column;
        private string[] _values;

        public FindSearchField(string name, string value);
        public FindSearchField(string name, string[] values);
        internal void AddValue(string value);
        private void UpdateCase();

        public FindColumnInfo Column { get; set; }

        public IDataColumnInfo ColumnInfo { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string[] Values { get; set; }
    }
}

