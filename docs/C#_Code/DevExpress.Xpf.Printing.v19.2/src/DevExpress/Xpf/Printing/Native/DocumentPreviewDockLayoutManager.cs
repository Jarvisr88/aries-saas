namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows.Input;

    public class DocumentPreviewDockLayoutManager : DockLayoutManager
    {
        protected override void CheckLicense()
        {
        }

        protected override void OwnerWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key != Key.Tab) || ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.None))
            {
                base.OwnerWindowPreviewKeyDown(sender, e);
            }
            else
            {
                DockLayoutManager container = LayoutHelper.FindLayoutOrVisualParentObject<DockLayoutManager>(base.Parent, false, null);
                if (container != null)
                {
                    container.GetCustomizationController().ShowDocumentSelectorForm();
                }
            }
        }
    }
}

