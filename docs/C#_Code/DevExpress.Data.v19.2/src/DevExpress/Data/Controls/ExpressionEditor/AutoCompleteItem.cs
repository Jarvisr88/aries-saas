namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoCompleteItem
    {
        public AutoCompleteItem(string value, string description, AutoCompleteItemKind itemKind);

        public string Value { get; set; }

        public string Description { get; set; }

        public AutoCompleteItemKind ItemKind { get; }
    }
}

