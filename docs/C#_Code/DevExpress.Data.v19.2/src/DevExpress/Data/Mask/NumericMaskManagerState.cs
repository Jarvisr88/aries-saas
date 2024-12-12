namespace DevExpress.Data.Mask
{
    using System;

    public class NumericMaskManagerState : MaskManagerPlainTextState
    {
        private readonly bool fIsNull;
        private readonly bool fIsNegative;
        private readonly bool fIsSelectAll;
        public static readonly NumericMaskManagerState NullInstance;

        static NumericMaskManagerState();
        private NumericMaskManagerState();
        public NumericMaskManagerState(string editText, bool isNegative);
        public NumericMaskManagerState(string editText, int cursorPosition, int selectionAnchor, bool isNegative);
        public override bool IsSame(MaskManagerState comparedState);

        public bool IsNegative { get; }

        public bool IsNull { get; }

        public bool IsSelectAll { get; }
    }
}

