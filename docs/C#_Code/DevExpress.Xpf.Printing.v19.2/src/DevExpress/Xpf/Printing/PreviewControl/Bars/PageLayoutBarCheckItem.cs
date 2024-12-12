namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows.Input;

    public class PageLayoutBarCheckItem : PreviewBarCheckItem
    {
        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            if (base.Command is PageLayoutCommandButton)
            {
                base.CommandParameter = ((PageLayoutCommandButton) base.Command).PageLayoutSettings;
                string str = this.TryGetName(((PageLayoutCommandButton) base.Command).PageLayoutSettings);
                if (!string.IsNullOrEmpty(str))
                {
                    base.SetValue(DocumentViewerControl.BarItemNameProperty, str);
                }
            }
        }

        private string TryGetName(PageLayoutSettings pageLayoutSettings)
        {
            switch (pageLayoutSettings.PageDisplayMode)
            {
                case PageDisplayMode.Single:
                    return "bSinglePage";

                case PageDisplayMode.Columns:
                    return ((pageLayoutSettings.ColumnCount == 2) ? "bTwoPages" : string.Empty);

                case PageDisplayMode.Wrap:
                    return "bWrapPages";
            }
            return string.Empty;
        }
    }
}

