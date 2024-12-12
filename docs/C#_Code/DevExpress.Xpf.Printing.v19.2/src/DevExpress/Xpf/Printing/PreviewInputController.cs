namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows.Input;

    public class PreviewInputController : InputController
    {
        protected override void CreateShortcuts()
        {
            base.TryAddShortcutForPSCommand(new MouseShortcut(ModifierKeys.Control, MouseInputAction.WheelClick, MouseWheelScrollingDirection.Up), PrintingSystemCommand.ZoomIn);
            base.TryAddShortcutForPSCommand(new MouseShortcut(ModifierKeys.Control, MouseInputAction.WheelClick, MouseWheelScrollingDirection.Down), PrintingSystemCommand.ZoomOut);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.Add), PrintingSystemCommand.ZoomIn);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.Subtract), PrintingSystemCommand.ZoomOut);
        }
    }
}

