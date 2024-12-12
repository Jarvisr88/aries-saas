namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class RegExpMaskManagerCore : MaskManagerCommon<MaskManagerPlainTextState>
    {
        internal RegExpMaskLogic logic;
        private bool showPlaceHolders;
        private char anySymbolPlaceHolder;
        private bool isOptimistic;

        public RegExpMaskManagerCore(string regExp, bool reverseDfa, bool isAutoComplete, bool isOptimistic, bool showPlaceHolders, char anySymbolPlaceHolder, CultureInfo managerCultureInfo);
        protected bool Apply(MaskLogicResult result, MaskManagerStated<MaskManagerPlainTextState>.StateChangeType changeType);
        public override bool Backspace();
        protected override MaskManagerPlainTextState CreateStateForApply(string editText, int cursorPosition, int selectionAnchor);
        public override bool Delete();
        protected override int GetCursorPosition(MaskManagerPlainTextState state);
        protected override string GetDisplayText(MaskManagerPlainTextState state);
        protected override string GetEditText(MaskManagerPlainTextState state);
        protected override MaskManagerPlainTextState GetEmptyState();
        protected override int GetSelectionAnchor(MaskManagerPlainTextState state);
        public override bool Insert(string insertion);
        protected override bool IsValidCursorPosition(int testedPosition);
        public override void SelectAll();
        public override void SetInitialEditText(string initialEditText);

        public override bool IsEditValueAssignedAsFormattedText { get; }

        public override bool IsMatch { get; }

        public override bool IsFinal { get; }
    }
}

