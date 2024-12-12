namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;

    public class TimeSpanMaskManagerCore : MaskManager
    {
        static TimeSpanMaskManagerCore();
        public TimeSpanMaskManagerCore(string mask, bool isOperatorMask, TimeSpanCultureInfoBase cultureInfo, bool allowNull, bool moveValueWithNavigation, bool spinNextPartOnCycling, TimeSpanMaskInputMode inputMode, TimeSpanMaskPart defaultHiddenPart, bool allowNegativeValue, TimeSpan dayDuration);
        protected bool ApplyCurrentElementEditor();
        private bool ApplyElementEditor(TimeSpanElementEditor editor, int formatInfoIndex, bool raiseEvents, bool killCurrentEditor);
        public override bool Backspace();
        private bool BackspaceOrDelete(bool isBackspace);
        public void ClearFromSelectAll();
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        private void CursorToDefaultPart();
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public override bool Delete();
        public override bool FlushPendingEditActions();
        protected virtual TimeSpanMaskManagerValue GetClearValue();
        public override string GetCurrentEditText();
        public override object GetCurrentEditValue();
        protected TimeSpanElementEditor GetCurrentElementEditor();
        private int GetDefaultEditableIndex();
        private int GetFormatIndexFromPosition(int position);
        public void GotFocus();
        public override bool Insert(string insertion);
        private bool IsGroupHidden(int index);
        internal bool IsNavigationSymbol(string insertion);
        private bool IsNextSeparatorSkipInput(string insertion);
        protected void KillCurrentElementEditor();
        public void LostFocus();
        public override void SelectAll();
        private void SetCurrentValue(TimeSpanMaskManagerValue newValue);
        public override void SetInitialEditText(string initialEditText);
        public override void SetInitialEditValue(object initialEditValue);
        private TimeSpanMaskManagerValue SetInitialLongEditValue(long initialEditValue);
        private TimeSpanMaskManagerValue SetInitialTimeSpanEditValue(TimeSpan? initialEditValue);
        private bool SpinAction(Func<TimeSpanElementEditor, SpinActionResult> action);
        public override bool SpinDown();
        private void SpinNegate(TimeSpanElementEditor editor, bool currentNegateValue);
        public override bool SpinUp();
        public override bool Undo();

        internal static bool AlwaysZeroOnClearSelectAll { get; set; }

        protected int SelectedFormatInfoIndex { get; private set; }

        protected TimeSpanMaskManagerValue CurrentValue { get; private set; }

        protected TimeSpanMaskManagerValue UndoValue { get; private set; }

        protected TimeSpanMaskManagerValue InitialEditValue { get; private set; }

        protected TimeSpanElementEditor CurrentElementEditor { get; private set; }

        protected TimeSpanMaskFormatInfo FormatInfo { get; private set; }

        protected TimeSpanMaskManagerValue NonEmptyCurrentValue { get; }

        protected bool IsElementEdited { get; }

        protected bool IsModified { get; private set; }

        protected TimeSpanCultureInfoBase CultureInfo { get; private set; }

        protected bool IsOperatorMask { get; private set; }

        protected bool MoveValueWithNavigation { get; private set; }

        protected bool SpinNextPartOnCycling { get; private set; }

        protected bool IsNegative { get; private set; }

        protected TimeSpanMaskInputMode InputMode { get; private set; }

        protected TimeSpanMaskPart DefaultHiddenPart { get; private set; }

        protected bool AllowNull { get; private set; }

        protected bool AllowNegativeValue { get; private set; }

        protected TimeSpan DayDuration { get; private set; }

        protected bool IsFocused { get; private set; }

        protected TimeSpanMaskFormatElementEditable SelectedElement { get; }

        public override string DisplayText { get; }

        public override int DisplayCursorPosition { get; }

        public override int DisplaySelectionAnchor { get; }

        public override bool CanUndo { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanMaskManagerCore.<>c <>9;
            public static Predicate<TimeSpanMaskFormatElement> <>9__112_0;

            static <>c();
            internal bool <CursorHome>b__112_0(TimeSpanMaskFormatElement x);
        }
    }
}

