namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class PreviewBarCheckItem : BarCheckItem
    {
        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            CommandBase base2 = newCommand as CommandBase;
            if (base2 == null)
            {
                base.ClearValue(FrameworkContentElement.StyleProperty);
            }
            else
            {
                base.DataContext = base2;
                base.Style = base.TryFindResource(typeof(PreviewBarCheckItem)) as Style;
            }
        }
    }
}

