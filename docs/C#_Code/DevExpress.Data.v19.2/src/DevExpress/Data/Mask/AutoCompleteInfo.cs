namespace DevExpress.Data.Mask
{
    using System;

    public class AutoCompleteInfo
    {
        private readonly DevExpress.Data.Mask.DfaAutoCompleteType autoCompleteType;
        private readonly char autoCompleteChar;

        public AutoCompleteInfo(DevExpress.Data.Mask.DfaAutoCompleteType autoCompleteType, char autoCompleteChar);

        public DevExpress.Data.Mask.DfaAutoCompleteType DfaAutoCompleteType { get; }

        public char AutoCompleteChar { get; }
    }
}

