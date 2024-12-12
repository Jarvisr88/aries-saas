namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class MouseShortcut : InputShortcut
    {
        public MouseShortcut(MouseInputAction mouseAction) : this(new ModifierKeys[0], mouseAction, MouseWheelScrollingDirection.None)
        {
        }

        public MouseShortcut(ModifierKeys modifier, MouseInputAction mouseAction) : this(keysArray1, mouseAction, MouseWheelScrollingDirection.None)
        {
            ModifierKeys[] keysArray1 = new ModifierKeys[] { modifier };
        }

        public MouseShortcut(ModifierKeys[] modifiers, MouseInputAction mouseAction) : this(modifiers, mouseAction, MouseWheelScrollingDirection.None)
        {
        }

        public MouseShortcut(MouseInputAction mouseAction, MouseWheelScrollingDirection scrollingDirection) : this(new ModifierKeys[0], mouseAction, scrollingDirection)
        {
        }

        public MouseShortcut(ModifierKeys modifier, MouseInputAction mouseAction, MouseWheelScrollingDirection scrollingDirection) : this(keysArray1, mouseAction, scrollingDirection)
        {
            ModifierKeys[] keysArray1 = new ModifierKeys[] { modifier };
        }

        public MouseShortcut(ModifierKeys[] modifiers, MouseInputAction mouseAction, MouseWheelScrollingDirection scrollingDirection) : base(modifiers)
        {
            this.MouseAction = mouseAction;
            this.ScrollingDirection = scrollingDirection;
        }

        public override bool Equals(object obj)
        {
            MouseShortcut shortcut = obj as MouseShortcut;
            return ((shortcut != null) && (base.AreModifierArraysEqual(base.Modifiers, shortcut.Modifiers) && ((this.MouseAction == shortcut.MouseAction) && (this.ScrollingDirection == shortcut.ScrollingDirection))));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public MouseInputAction MouseAction { get; set; }

        public MouseWheelScrollingDirection ScrollingDirection { get; set; }

        public override string DisplayString
        {
            get
            {
                string displayString = base.DisplayString;
                return $"{displayString}+{this.MouseAction}+Scroll {this.ScrollingDirection}";
            }
        }
    }
}

