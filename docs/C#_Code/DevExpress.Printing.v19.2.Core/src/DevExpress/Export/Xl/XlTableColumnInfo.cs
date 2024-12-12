namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlTableColumnInfo
    {
        public XlTableColumnInfo(string name)
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            this.Name = name;
        }

        public string Name { get; private set; }

        public XlDifferentialFormatting HeaderRowFormatting { get; set; }

        public XlDifferentialFormatting DataFormatting { get; set; }

        public XlDifferentialFormatting TotalRowFormatting { get; set; }
    }
}

