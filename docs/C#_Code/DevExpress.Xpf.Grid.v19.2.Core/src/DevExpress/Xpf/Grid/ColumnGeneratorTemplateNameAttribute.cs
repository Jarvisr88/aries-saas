namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class ColumnGeneratorTemplateNameAttribute : Attribute
    {
        public ColumnGeneratorTemplateNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}

