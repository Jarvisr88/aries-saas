namespace DevExpress.Data.Mask
{
    using System;

    public class LegacyMaskManagerCore : MaskManagerStated<LegacyMaskManagerState>
    {
        private readonly LegacyMaskInfo info;
        private readonly bool saveLiteral;
        private readonly char blank;
        private readonly bool ignoreMaskBlank;

        public LegacyMaskManagerCore(LegacyMaskInfo info, char blank, bool saveLiteral, bool ignoreMaskBlank);
        public override bool Backspace();
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public override bool Delete();
        protected override int GetCursorPosition(LegacyMaskManagerState state);
        protected override string GetDisplayText(LegacyMaskManagerState state);
        protected override string GetEditText(LegacyMaskManagerState state);
        protected override object GetEditValue(LegacyMaskManagerState state);
        protected override int GetSelectionAnchor(LegacyMaskManagerState state);
        public override bool Insert(string insertion);
        public override void SelectAll();
        public override void SetInitialEditText(string initialEditText);
        public override void SetInitialEditValue(object initialEditValue);

        public override bool IsMatch { get; }

        public override bool IsFinal { get; }

        public override bool IsEditValueAssignedAsFormattedText { get; }
    }
}

