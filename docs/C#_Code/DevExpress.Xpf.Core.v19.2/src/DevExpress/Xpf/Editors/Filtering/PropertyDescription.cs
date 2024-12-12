namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    internal class PropertyDescription
    {
        public string FieldName { get; set; }

        public string ColumnCaption { get; set; }

        public BaseEditSettings EditSettings { get; set; }
    }
}

