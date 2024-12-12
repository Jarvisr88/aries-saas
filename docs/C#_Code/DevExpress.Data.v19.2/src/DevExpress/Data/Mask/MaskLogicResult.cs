namespace DevExpress.Data.Mask
{
    using System;

    public class MaskLogicResult
    {
        private string editText;
        private int cursorPosition;

        public MaskLogicResult(string editText, int cursorPosition);

        public string EditText { get; }

        public int CursorPosition { get; }
    }
}

