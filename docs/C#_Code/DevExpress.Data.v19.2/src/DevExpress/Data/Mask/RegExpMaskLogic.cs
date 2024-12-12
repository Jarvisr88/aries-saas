namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class RegExpMaskLogic
    {
        internal Dfa regExp;
        internal bool IsAutoComplete;

        public RegExpMaskLogic(Dfa regExp, bool isAutoComplete);
        public RegExpMaskLogic(string regExp, CultureInfo culture, bool isAutoComplete);
        private string AutoCompleteResultProcessing(string origin);
        private string AutoCompleteTailPreprocessing(string head, string tail);
        private MaskLogicResult CreateResult(string result, string cursorBase);
        internal string ExactsAppend(string before);
        internal string ExactsTruncate(string before);
        public MaskLogicResult GetBackspaceResult(string head, string tail);
        public MaskLogicResult GetDeleteResult(string head, string tail);
        public string GetMaskedText(string text, char anySymbolHolder);
        public MaskLogicResult GetReplaceResult(string head, string replaced, string tail, string inserted);
        private string GetTail(string oldHead, string oldTail, string head);
        private MaskLogicResult InsertCaseByOneChar(bool isSmart, string head, string replaced, string inserted, string tail);
        private MaskLogicResult InsertCaseByOneChar(bool isSmart, string head, string replaced, string inserted, string tail, bool isLastChance);
        private bool InsertCaseByOneCharBody(out string nextHead, out string nextFull, bool isSmart, string head, string replaced, char input, string tail);
        private MaskLogicResult InsertCaseTailNonExacts(string appended, string tailNonexacts);
        private MaskLogicResult InsertCaseTailPlain(string appended, string tail);
        public bool IsFinal(string text);
        public bool IsMatch(string text);
        public bool IsValidCursorPosition(string text, int testedPositionInEditText);
        public bool IsValidStart(string text);
        public string OptimisticallyExpand(string baseText);
        internal string RemoveExacts(string stringBefore, string stringWithExacts);
        internal string RestoreExacts(string stringBefore, string stringWithoutExacts);
        internal string SmartAppend(string before, char input);
        internal string SmartAutoComplete(string before);
        private static string StringWithoutLastChar(string str);
    }
}

