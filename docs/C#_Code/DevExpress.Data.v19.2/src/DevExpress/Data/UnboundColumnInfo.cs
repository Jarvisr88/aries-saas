namespace DevExpress.Data
{
    using System;

    public class UnboundColumnInfo
    {
        private string name;
        private UnboundColumnType columnType;
        private Type dataType;
        private bool readOnly;
        private string expression;
        private bool requireValueConversion;
        private bool visible;
        private static Type[] dataTypes;

        static UnboundColumnInfo();
        public UnboundColumnInfo();
        public UnboundColumnInfo(string name, UnboundColumnType columnType, bool readOnly);
        public UnboundColumnInfo(string name, UnboundColumnType columnType, bool readOnly, string expression);
        public UnboundColumnInfo(string name, UnboundColumnType columnType, bool readOnly, string expression, bool visible);
        protected Type GetDataType();

        public string Expression { get; }

        public bool ReadOnly { get; set; }

        public bool Visible { get; set; }

        public string Name { get; set; }

        public Type DataType { get; set; }

        public UnboundColumnType ColumnType { get; set; }

        public bool RequireValueConversion { get; set; }
    }
}

