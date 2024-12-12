namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public abstract class InputController
    {
        private readonly Dictionary<InputShortcut, ICommand> shortcuts = new Dictionary<InputShortcut, ICommand>();
        private readonly List<ModifierKeys> pressedModifiers = new List<ModifierKeys>();
        private Key pressedKey = Key.None;
        private MouseInputAction currentMouseAction = MouseInputAction.None;
        private MouseWheelScrollingDirection scrollingDirection = MouseWheelScrollingDirection.None;
        private IPreviewModel model;

        protected void AddShortcut(InputShortcut shortcut, ICommand command)
        {
            Guard.ArgumentNotNull(shortcut, "shortcut");
            Guard.ArgumentNotNull(command, "command");
            if (this.shortcuts.ContainsKey(shortcut))
            {
                throw new ArgumentException("This shortcut has already been added");
            }
            this.shortcuts.Add(shortcut, command);
        }

        private static Key Convert(Key key) => 
            (key != Key.Prior) ? ((key != Key.Next) ? key : Key.Next) : Key.Prior;

        protected abstract void CreateShortcuts();
        private void ExecuteShortcut(InputShortcut shortcut)
        {
            ICommand command;
            if (this.shortcuts.TryGetValue(shortcut, out command) && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private void FillPressedModifiers()
        {
            this.pressedModifiers.Clear();
            ModifierKeys modifiers = Keyboard.Modifiers;
            if (AreModifiersPressed)
            {
                if (ModifierKeysHelper.IsAltPressed(modifiers))
                {
                    this.pressedModifiers.Add(ModifierKeys.Alt);
                }
                if (ModifierKeysHelper.IsCtrlPressed(modifiers))
                {
                    this.pressedModifiers.Add(ModifierKeys.Control);
                }
                if (ModifierKeysHelper.IsShiftPressed(modifiers))
                {
                    this.pressedModifiers.Add(ModifierKeys.Shift);
                }
                if (ModifierKeysHelper.IsWinPressed(modifiers))
                {
                    this.pressedModifiers.Add(ModifierKeys.Windows);
                }
            }
        }

        public void HandleKeyDown(Key key)
        {
            if ((key != Key.None) && !IsModifierKey(key))
            {
                key = Convert(key);
                this.pressedKey = key;
                this.FillPressedModifiers();
                this.ExecuteShortcut(new KeyShortcut(this.pressedModifiers.ToArray(), this.pressedKey));
            }
        }

        public void HandleMouseDown(MouseButton mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButton.Left:
                    this.currentMouseAction = MouseInputAction.LeftClick;
                    break;

                case MouseButton.Middle:
                    this.currentMouseAction = MouseInputAction.MiddleClick;
                    break;

                case MouseButton.Right:
                    this.currentMouseAction = MouseInputAction.RightClick;
                    break;

                default:
                    break;
            }
            this.FillPressedModifiers();
            this.ExecuteShortcut(new MouseShortcut(this.pressedModifiers.ToArray(), this.currentMouseAction, MouseWheelScrollingDirection.None));
        }

        public void HandleMouseWheel(int delta)
        {
            this.scrollingDirection = MouseWheelScrollingDirection.None;
            if (delta > 0)
            {
                this.scrollingDirection = MouseWheelScrollingDirection.Up;
            }
            if (delta < 0)
            {
                this.scrollingDirection = MouseWheelScrollingDirection.Down;
            }
            this.FillPressedModifiers();
            this.ExecuteShortcut(new MouseShortcut(this.pressedModifiers.ToArray(), MouseInputAction.WheelClick, this.scrollingDirection));
        }

        private static bool IsModifierKey(Key key)
        {
            if ((key != Key.LWin) && (key != Key.RWin))
            {
                switch (key)
                {
                    case Key.LeftShift:
                    case Key.RightShift:
                    case Key.LeftCtrl:
                    case Key.RightCtrl:
                    case Key.LeftAlt:
                    case Key.RightAlt:
                        break;

                    default:
                        return false;
                }
            }
            return true;
        }

        protected internal void TryAddShortcutForPSCommand(InputShortcut shortcut, PrintingSystemCommand psCommand)
        {
            DelegateCommand<object> command;
            PreviewModelBase model = this.Model as PreviewModelBase;
            if ((model != null) && model.Commands.TryGetValue(psCommand, out command))
            {
                this.AddShortcut(shortcut, command);
            }
        }

        public IPreviewModel Model
        {
            get => 
                this.model;
            set
            {
                if (!ReferenceEquals(this.model, value))
                {
                    this.model = value;
                    if (this.model != null)
                    {
                        this.shortcuts.Clear();
                        this.CreateShortcuts();
                    }
                }
            }
        }

        public static bool AreModifiersPressed =>
            !ModifierKeysHelper.NoModifiers(Keyboard.Modifiers);
    }
}

