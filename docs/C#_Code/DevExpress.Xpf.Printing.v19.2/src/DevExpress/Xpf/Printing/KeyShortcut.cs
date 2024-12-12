namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class KeyShortcut : InputShortcut
    {
        public KeyShortcut(System.Windows.Input.Key key) : this(new ModifierKeys[0], key)
        {
        }

        public KeyShortcut(ModifierKeys modifier, System.Windows.Input.Key key) : this(keysArray1, key)
        {
            ModifierKeys[] keysArray1 = new ModifierKeys[] { modifier };
        }

        public KeyShortcut(ModifierKeys[] modifiers, System.Windows.Input.Key key) : base(modifiers)
        {
            this.Key = key;
        }

        public override bool Equals(object obj)
        {
            KeyShortcut shortcut = obj as KeyShortcut;
            return ((shortcut != null) && (base.AreModifierArraysEqual(base.Modifiers, shortcut.Modifiers) && (this.Key == shortcut.Key)));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public System.Windows.Input.Key Key { get; set; }

        public override string DisplayString
        {
            get
            {
                string displayString = base.DisplayString;
                return $"{displayString}+{this.Key}";
            }
        }
    }
}

