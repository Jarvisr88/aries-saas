namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class NumericMaskManager : MaskManagerStated<NumericMaskManagerState>
    {
        public static bool DefaultAllowHideDecimalSeparatorWhenPossibleWithoutMeaningChange;
        protected readonly bool AllowNull;
        private readonly NumericMaskLogic logic;
        private readonly string negativeSignString;
        private readonly NumericFormatter[] formatters;
        private TypeCode? editValueTypeCode;
        private bool canResetSignWithDelete;
        private bool canResetSignWithBackspace;

        [Obsolete("Use NumericMaskManager(string formatString, CultureInfo managerCultureInfo, bool allowNull) instead")]
        public NumericMaskManager(string formatString, CultureInfo managerCultureInfo);
        public NumericMaskManager(string formatString, CultureInfo managerCultureInfo, bool allowNull);
        public NumericMaskManager(string formatString, CultureInfo managerCultureInfo, bool allowNull, bool? allowHideDecimalSeparatorWhenPossibleWithoutMeaningChange = new bool?());
        public override bool Backspace();
        private static void CorrectOverprecisedToStringForSingleOrDouble(int officialPrec, int maxPrec, int digitsAfterDecimal, ref string work, object initialEditValue);
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public override bool Delete();
        private static int FirstNonZeroDigitPos(string str);
        private bool ForceEditValueTypeCode(TypeCode forcedCode);
        protected override int GetCursorPosition(NumericMaskManagerState numericState);
        protected override string GetDisplayText(NumericMaskManagerState numericState);
        protected override string GetEditText(NumericMaskManagerState numericState);
        protected override object GetEditValue(NumericMaskManagerState state);
        private NumericFormatter GetFormatter(NumericMaskManagerState state);
        protected override int GetSelectionAnchor(NumericMaskManagerState numericState);
        public override bool Insert(string insertion);
        private static bool IsJustDotsAndZeros(string input);
        protected override bool IsValid(NumericMaskManagerState newState);
        private bool IsValidInvariantCultureDecimal(string testedString);
        public override void SelectAll();
        public override void SetInitialEditText(string initialEditText);
        public override void SetInitialEditValue(object initialEditValue);
        public override bool SpinDown();
        private bool SpinKeys(bool isUp);
        public override bool SpinUp();
        private bool StateCursorPositionTo(int newPosition, bool forceSelection);
        private bool StateCursorPositionTo(int newPosition, bool forceSelection, bool isNeededKeyCheck);

        private bool IsSignedMask { get; }

        public override bool IsFinal { get; }
    }
}

