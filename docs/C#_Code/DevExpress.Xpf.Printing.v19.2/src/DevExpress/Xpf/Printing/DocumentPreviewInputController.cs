namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows.Input;

    public class DocumentPreviewInputController : PreviewInputController
    {
        protected override void CreateShortcuts()
        {
            base.CreateShortcuts();
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.O), PrintingSystemCommand.Open);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.S), PrintingSystemCommand.Save);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.P), PrintingSystemCommand.Print);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.F), PrintingSystemCommand.Find);
            base.TryAddShortcutForPSCommand(new KeyShortcut(Key.Home), PrintingSystemCommand.ShowFirstPage);
            ModifierKeys[] modifiers = new ModifierKeys[] { ModifierKeys.Control, ModifierKeys.Shift };
            base.TryAddShortcutForPSCommand(new KeyShortcut(modifiers, Key.Prior), PrintingSystemCommand.ShowFirstPage);
            ModifierKeys[] keysArray2 = new ModifierKeys[] { ModifierKeys.Control, ModifierKeys.Shift };
            base.TryAddShortcutForPSCommand(new KeyShortcut(keysArray2, Key.Up), PrintingSystemCommand.ShowFirstPage);
            base.TryAddShortcutForPSCommand(new KeyShortcut(Key.End), PrintingSystemCommand.ShowLastPage);
            ModifierKeys[] keysArray3 = new ModifierKeys[] { ModifierKeys.Control, ModifierKeys.Shift };
            base.TryAddShortcutForPSCommand(new KeyShortcut(keysArray3, Key.Next), PrintingSystemCommand.ShowLastPage);
            ModifierKeys[] keysArray4 = new ModifierKeys[] { ModifierKeys.Control, ModifierKeys.Shift };
            base.TryAddShortcutForPSCommand(new KeyShortcut(keysArray4, Key.Down), PrintingSystemCommand.ShowLastPage);
            base.TryAddShortcutForPSCommand(new KeyShortcut(Key.Left), PrintingSystemCommand.ShowPrevPage);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.Prior), PrintingSystemCommand.ShowPrevPage);
            base.TryAddShortcutForPSCommand(new KeyShortcut(Key.Right), PrintingSystemCommand.ShowNextPage);
            base.TryAddShortcutForPSCommand(new KeyShortcut(ModifierKeys.Control, Key.Next), PrintingSystemCommand.ShowNextPage);
        }
    }
}

