namespace DevExpress.Xpf.Grid
{
    using System;

    internal class BindingParseResult
    {
        private readonly string[] properties;
        private readonly bool row;

        private BindingParseResult(string[] properties, bool row)
        {
            this.properties = properties;
            this.row = row;
        }

        internal static BindingParseResult CreateEmpty() => 
            new BindingParseResult(new string[0], false);

        internal static BindingParseResult CreateForProperties(string[] properties) => 
            new BindingParseResult(properties, false);

        internal static BindingParseResult CreateForRow() => 
            new BindingParseResult(new string[0], true);

        public string[] Properties =>
            this.properties;

        public bool Row =>
            this.row;
    }
}

