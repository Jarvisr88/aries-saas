namespace DevExpress.Utils.KeyboardHandler
{
    using System;
    using System.Windows.Forms;

    public abstract class KeyboardHandler
    {
        private object context;

        protected KeyboardHandler();
        public static Keys GetModifierKeys();
        public virtual bool HandleKey(Keys keyData);
        public virtual bool HandleKeyPress(char character, Keys modifier);
        public virtual bool HandleKeyUp(Keys keys);
        public virtual bool IsValidChar(char c);

        public object Context { get; set; }

        public static bool IsShiftPressed { get; }

        public static bool IsControlPressed { get; }

        public static bool IsAltPressed { get; }

        public static DevExpress.Utils.KeyboardHandler.KeyState KeyState { get; }
    }
}

