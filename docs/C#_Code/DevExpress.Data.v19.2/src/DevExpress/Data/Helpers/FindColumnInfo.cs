namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class FindColumnInfo
    {
        public override string ToString();

        public IDataColumnInfo Column { get; set; }

        public string PropertyName { get; set; }
    }
}

