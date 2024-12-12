namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class DefinedName : IDefinedName
    {
        public DefinedName(string name, string scope, bool isHidden, XlCellRange range, string refersTo)
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            Guard.ArgumentNotNull(range, "range");
            Guard.ArgumentIsNotNullOrEmpty(refersTo, "refersTo");
            this.Name = name;
            this.Scope = scope;
            this.IsHidden = isHidden;
            this.Range = range;
            this.RefersTo = refersTo;
        }

        public string Name { get; private set; }

        public string Scope { get; private set; }

        public bool IsHidden { get; private set; }

        public XlCellRange Range { get; private set; }

        public string RefersTo { get; private set; }

        public string Comment { get; set; }
    }
}

