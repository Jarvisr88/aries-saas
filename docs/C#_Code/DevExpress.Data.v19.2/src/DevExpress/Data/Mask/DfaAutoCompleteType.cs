namespace DevExpress.Data.Mask
{
    using System;

    public enum DfaAutoCompleteType
    {
        public const DfaAutoCompleteType None = DfaAutoCompleteType.None;,
        public const DfaAutoCompleteType ExactChar = DfaAutoCompleteType.ExactChar;,
        public const DfaAutoCompleteType Final = DfaAutoCompleteType.Final;,
        public const DfaAutoCompleteType FinalOrExactBeforeNone = DfaAutoCompleteType.FinalOrExactBeforeNone;,
        public const DfaAutoCompleteType FinalOrExactBeforeFinal = DfaAutoCompleteType.FinalOrExactBeforeFinal;,
        public const DfaAutoCompleteType FinalOrExactBeforeFinalOrNone = DfaAutoCompleteType.FinalOrExactBeforeFinalOrNone;
    }
}

