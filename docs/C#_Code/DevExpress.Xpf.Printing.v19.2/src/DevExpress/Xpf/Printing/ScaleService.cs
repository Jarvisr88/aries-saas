namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.Windows;

    internal class ScaleService : IScaleService
    {
        private PrintingSystemPreviewModel model;
        private ScaleWindow scaleWindow;

        public void Scale(PrintingSystemPreviewModel model, Window ownerWindow)
        {
            Guard.ArgumentNotNull(model, "model");
            this.model = model;
            this.scaleWindow = new ScaleWindow(model.PrintingSystem.Document.ScaleFactor, model.PrintingSystem.Document.AutoFitToPagesWidth);
            this.scaleWindow.Owner = ownerWindow;
            if (ownerWindow != null)
            {
                this.scaleWindow.FlowDirection = ownerWindow.FlowDirection;
            }
            this.scaleWindow.ViewModel.ScaleApplied += new EventHandler<ScaleWindowViewModelEventArgs>(this.ViewModel_ScaleApplied);
            this.scaleWindow.ShowDialog();
        }

        private void ViewModel_ScaleApplied(object sender, ScaleWindowViewModelEventArgs e)
        {
            this.scaleWindow.ViewModel.ScaleApplied -= new EventHandler<ScaleWindowViewModelEventArgs>(this.ViewModel_ScaleApplied);
            if (e.ScaleMode == ScaleMode.AdjustToPercent)
            {
                this.model.PrintingSystem.Document.ScaleFactor = e.ScaleFactor;
            }
            else
            {
                if (e.ScaleMode != ScaleMode.FitToPageWidth)
                {
                    throw new NotSupportedException("ScaleMode");
                }
                this.model.PrintingSystem.Document.AutoFitToPagesWidth = e.PagesToFit;
            }
        }
    }
}

