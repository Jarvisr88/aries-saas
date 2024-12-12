namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskManagerCore : MaskManager
    {
        protected readonly bool IsOperatorMask;
        protected DateTime? fCurrentValue;
        protected DateTime? fUndoValue;
        protected DateTime? fInitialEditValue;
        protected DateTimeMaskFormatInfo fFormatInfo;
        protected int fSelectedFormatInfoIndex;
        protected DateTimeElementEditor fCurrentElementEditor;
        protected string fInitialMask;
        protected DateTimeFormatInfo fInitialDateTimeFormatInfo;
        protected readonly bool AllowNull;
        private DateTime cachedValue;
        private int cachedIndex;
        private int cachedDCP;
        private int cachedDSA;
        private string cachedDT;
        public static bool AlwaysTodayOnClearSelectAll;

        [Obsolete("Use DateTimeMaskManager(string mask, bool isOperatorMask, CultureInfo culture, bool allowNull) instead")]
        public DateTimeMaskManagerCore(string mask, bool isOperatorMask, CultureInfo culture);
        public DateTimeMaskManagerCore(string mask, bool isOperatorMask, CultureInfo culture, bool allowNull);
        protected bool ApplyCurrentElementEditor();
        public override bool Backspace();
        private bool BackspaceOrDelete();
        public void ClearFromSelectAll();
        protected virtual DateTime CorrectInsertValue(DateTime inserted);
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public override bool Delete();
        public override bool FlushPendingEditActions();
        protected virtual DateTime GetClearValue();
        public override string GetCurrentEditText();
        public override object GetCurrentEditValue();
        protected DateTimeElementEditor GetCurrentElementEditor();
        private int GetFormatIndexFromPosition(int position);
        private DateTime GetTodayWithInitialKind();
        public override bool Insert(string insertion);
        private bool IsNextSeparatorSkipInput(string insertion);
        protected void KillCurrentElementEditor();
        public override void SelectAll();
        public override void SetInitialEditText(string initialEditText);
        public void SetInitialEditValue(DateTime? initialEditValue);
        public override void SetInitialEditValue(object initialEditValue);
        public override bool SpinDown();
        public override bool SpinUp();
        public override bool Undo();
        private void VerifyCache();

        protected DateTime? CurrentValue { get; }

        protected DateTime? UndoValue { get; }

        protected DateTime NonEmptyCurrentValue { get; }

        protected internal DateTimeMaskFormatInfo FormatInfo { get; }

        protected int SelectedFormatInfoIndex { get; }

        protected DateTimeMaskFormatElementEditable SelectedElement { get; }

        protected bool IsElementEdited { get; }

        public override string DisplayText { get; }

        public override int DisplayCursorPosition { get; }

        public override int DisplaySelectionAnchor { get; }

        public override bool CanUndo { get; }
    }
}

