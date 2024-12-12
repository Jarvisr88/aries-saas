namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class LinkPreviewHelper : PreviewHelper
    {
        private readonly DevExpress.Xpf.Printing.LinkBase link;
        private readonly string themeName;

        public LinkPreviewHelper(DevExpress.Xpf.Printing.LinkBase link, string themeName = null)
        {
            Guard.ArgumentNotNull(link, "link");
            this.link = link;
            this.themeName = themeName;
        }

        protected override void CreateDocumentIfEmpty()
        {
            this.link.CreateIfEmpty(true);
        }

        protected override DocumentPreviewWindow CreatePreviewWindow(Window owner, string title, CommandBarStyle commandBarStyle)
        {
            DocumentPreviewWindow window = base.CreatePreviewWindow(owner, title, commandBarStyle);
            if (this.link is LegacyPrintableComponentLink)
            {
                if (!string.IsNullOrEmpty(this.themeName))
                {
                    ThemeManager.SetThemeName(window, this.themeName);
                }
                this.RemoveUnsupportedCommands(window.PreviewControl);
                this.HideUnsupportedExportFormats(window.PreviewControl.HiddenExportFormats);
            }
            return window;
        }

        private void HideUnsupportedExportFormats(ICollection<ExportFormat> hiddenExportFormats)
        {
            foreach (ExportFormat format in Enum.GetValues(typeof(ExportFormat)))
            {
                if ((format != ExportFormat.Pdf) && (format != ExportFormat.Image))
                {
                    hiddenExportFormats.Add(format);
                }
            }
        }

        private void RemoveUnsupportedCommands(DocumentPreviewControl previewControl)
        {
            DocumentCommandProvider provider = new DocumentCommandProvider();
            ObservableCollection<IControllerAction> observables = (previewControl.CommandBarStyle == CommandBarStyle.Ribbon) ? provider.RibbonActions : provider.Actions;
            RemoveAction item = new RemoveAction();
            item.ElementName = "bOpen";
            observables.Add(item);
            RemoveAction action2 = new RemoveAction();
            action2.ElementName = "bSave";
            observables.Add(action2);
            RemoveAction action3 = new RemoveAction();
            action3.ElementName = "bPageSetup";
            observables.Add(action3);
            RemoveAction action4 = new RemoveAction();
            action4.ElementName = "bWatermark";
            observables.Add(action4);
            RemoveAction action5 = new RemoveAction();
            action5.ElementName = "bDocumentMap";
            observables.Add(action5);
            RemoveAction action6 = new RemoveAction();
            action6.ElementName = "bParameters";
            observables.Add(action6);
            RemoveAction action7 = new RemoveAction();
            action7.ElementName = "bScale";
            observables.Add(action7);
            RemoveAction action8 = new RemoveAction();
            action8.ElementName = "bFind";
            observables.Add(action8);
            previewControl.CommandProvider = provider;
        }

        protected override void StopPageBuilding()
        {
            this.link.StopPageBuilding();
        }

        protected override object DocumentSource =>
            this.link;
    }
}

