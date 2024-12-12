namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PageLayoutBarSubItem : DocumentViewerBarSubItem
    {
        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            base.ClearValue(FrameworkContentElement.StyleProperty);
            CommandBase base2 = newCommand as CommandBase;
            if (base2 != null)
            {
                base.DataContext = base2;
                base.Style = base.TryFindResource(typeof(PageLayoutBarSubItem)) as Style;
            }
        }
    }
}

