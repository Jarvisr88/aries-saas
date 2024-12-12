namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public class CellValue
    {
        public CellValue(object row, string property, object value)
        {
            this.Value = value;
            this.Property = property;
            this.Row = row;
        }

        public object Row { get; private set; }

        public string Property { get; private set; }

        public object Value { get; private set; }
    }
}

