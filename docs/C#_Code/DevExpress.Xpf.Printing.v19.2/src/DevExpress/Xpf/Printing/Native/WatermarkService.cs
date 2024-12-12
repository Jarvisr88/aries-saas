namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class WatermarkService : IWatermarkService
    {
        private WatermarkEditor watermarkEditor;
        private LegacyWatermarkEditorViewModel wmeViewModel;

        public event EventHandler<WatermarkServiceEventArgs> EditCompleted;

        public void Edit(Window ownerWindow, Page page, int pagesCount, Watermark currentWatermark)
        {
            Guard.ArgumentNotNull(page, "page");
            Guard.ArgumentPositive(pagesCount, "pagesCount");
            Guard.ArgumentNotNull(currentWatermark, "currentWatermark");
            XpfWatermark watermark = WatermarkPageHelper.CopyXpfWatermark(currentWatermark);
            Page page2 = WatermarkPageHelper.CopyPage(page);
            this.EditInternal(ownerWindow, page2, pagesCount, watermark);
        }

        public void Edit(Window ownerWindow, XtraPageSettingsBase pageSettings, int pagesCount, Watermark currentWatermark)
        {
            Guard.ArgumentNotNull(pageSettings, "pageSettings");
            Guard.ArgumentPositive(pagesCount, "pagesCount");
            Guard.ArgumentNotNull(currentWatermark, "currentWatermark");
            XpfWatermark watermark = WatermarkPageHelper.CopyXpfWatermark(currentWatermark);
            Page page = WatermarkPageHelper.CreatePageStub(pageSettings);
            this.EditInternal(ownerWindow, page, pagesCount, watermark);
        }

        private void EditInternal(Window ownerWindow, Page page, int pagesCount, XpfWatermark watermark)
        {
            this.wmeViewModel = new LegacyWatermarkEditorViewModel();
            this.wmeViewModel.PageCount = pagesCount;
            this.wmeViewModel.Page = page;
            this.wmeViewModel.Watermark = watermark;
            this.watermarkEditor = new WatermarkEditor();
            this.watermarkEditor.Model = this.wmeViewModel;
            this.watermarkEditor.Owner = ownerWindow;
            if (ownerWindow != null)
            {
                this.watermarkEditor.FlowDirection = ownerWindow.FlowDirection;
            }
            this.watermarkEditor.Closed += new EventHandler(this.watermarkEditor_Closed);
            this.watermarkEditor.ShowDialog();
        }

        private void watermarkEditor_Closed(object sender, EventArgs e)
        {
            this.watermarkEditor.Closed -= new EventHandler(this.watermarkEditor_Closed);
            WatermarkEditor editor = (WatermarkEditor) sender;
            if (this.EditCompleted != null)
            {
                this.EditCompleted(this, new WatermarkServiceEventArgs(this.wmeViewModel.Watermark, editor.DialogResult));
            }
        }
    }
}

