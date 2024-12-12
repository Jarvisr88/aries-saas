namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class NumericMaskLogic
    {
        private readonly int maxDigitsBeforeDecimalSeparator;
        private readonly int maxDigitsAfterDecimalSeparator;
        private readonly int minDigitsBeforeDecimalSeparator;
        private readonly int minDigitsAfterDecimalSeparator;
        private readonly CultureInfo culture;
        private readonly bool allowHideDecimalSeparatorWhenPossibleWithoutMeaningChange;
        private static char[] allDigits;

        static NumericMaskLogic();
        protected NumericMaskLogic();
        public NumericMaskLogic(int maxDigitsBeforeDecimalSeparator, int minDigitsBeforeDecimalSeparator, int minDigitsAfterDecimalSeparator, int maxDigitsAfterDecimalSeparator, CultureInfo culture, bool allowHideDecimalSeparatorWhenPossibleWithoutMeaningChange);
        private MaskLogicResult CreateResult(string resultCandidate, int cursorBase);
        private static string Decrement(string number);
        public static string Div100(string input);
        private MaskLogicResult GetClimbModuloResult(string head, string tail);
        private MaskLogicResult GetDiveModuloResult(string head, string tail, bool canChSign, out bool chSign);
        public MaskLogicResult GetEditResult(string head, string replaced, string tail, string inserted);
        public MaskLogicResult GetSpinResult(string head, string tail, bool isModuloDecrement, bool canChSign, out bool chSign);
        private static string Increment(string number);
        private static string Mul10(string input);
        public static string Mul100(string input);
        private string PatchTailIfEmpty(string tail);
        private static string RefineInput(string dirtyInput, CultureInfo refineCulture);
        private static string SubtractWithCarry(string number);
    }
}

