namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections.Generic;

    public abstract class PSCommandHandlerBase
    {
        protected Dictionary<PrintingSystemCommand, object> handlers = new Dictionary<PrintingSystemCommand, object>();

        public PSCommandHandlerBase()
        {
            this.handlers.Add(PrintingSystemCommand.ExportCsv, new PrintingSystemCommandHandlerBase(this.HandleExportCsv));
            this.handlers.Add(PrintingSystemCommand.ExportGraphic, new PrintingSystemCommandHandlerBase(this.HandleExportGraphic));
            this.handlers.Add(PrintingSystemCommand.ExportHtm, new PrintingSystemCommandHandlerBase(this.HandleExportHtm));
            this.handlers.Add(PrintingSystemCommand.ExportMht, new PrintingSystemCommandHandlerBase(this.HandleExportMht));
            this.handlers.Add(PrintingSystemCommand.ExportPdf, new PrintingSystemCommandHandlerBase(this.HandleExportPdf));
            this.handlers.Add(PrintingSystemCommand.ExportRtf, new PrintingSystemCommandHandlerBase(this.HandleExportRtf));
            this.handlers.Add(PrintingSystemCommand.ExportDocx, new PrintingSystemCommandHandlerBase(this.HandleExportDocx));
            this.handlers.Add(PrintingSystemCommand.ExportTxt, new PrintingSystemCommandHandlerBase(this.HandleExportTxt));
            this.handlers.Add(PrintingSystemCommand.ExportXls, new PrintingSystemCommandHandlerBase(this.HandleExportXls));
            this.handlers.Add(PrintingSystemCommand.ExportXlsx, new PrintingSystemCommandHandlerBase(this.HandleExportXlsx));
            this.handlers.Add(PrintingSystemCommand.SendCsv, new PrintingSystemCommandHandlerBase(this.HandleSendCsv));
            this.handlers.Add(PrintingSystemCommand.SendGraphic, new PrintingSystemCommandHandlerBase(this.HandleSendGraphic));
            this.handlers.Add(PrintingSystemCommand.SendMht, new PrintingSystemCommandHandlerBase(this.HandleSendMht));
            this.handlers.Add(PrintingSystemCommand.SendPdf, new PrintingSystemCommandHandlerBase(this.HandleSendPdf));
            this.handlers.Add(PrintingSystemCommand.SendRtf, new PrintingSystemCommandHandlerBase(this.HandleSendRtf));
            this.handlers.Add(PrintingSystemCommand.SendDocx, new PrintingSystemCommandHandlerBase(this.HandleSendDocx));
            this.handlers.Add(PrintingSystemCommand.SendTxt, new PrintingSystemCommandHandlerBase(this.HandleSendTxt));
            this.handlers.Add(PrintingSystemCommand.SendXls, new PrintingSystemCommandHandlerBase(this.HandleSendXls));
            this.handlers.Add(PrintingSystemCommand.SendXlsx, new PrintingSystemCommandHandlerBase(this.HandleSendXlsx));
            this.handlers.Add(PrintingSystemCommand.StopPageBuilding, new PrintingSystemCommandHandlerBase(this.HandleStopPageBuilding));
        }

        protected abstract ExportFileHelperBase CreateExportFileHelper();
        protected virtual void DoExport(ExportOptionsBase options)
        {
            this.CreateExportFileHelper().ExecExport(options, this.DisabledExportModes);
        }

        private void HandleExportCsv(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Csv);
        }

        private void HandleExportDocx(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Docx);
        }

        private void HandleExportGraphic(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Image);
        }

        private void HandleExportHtm(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Html);
        }

        private void HandleExportMht(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Mht);
        }

        private void HandleExportPdf(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Pdf);
        }

        private void HandleExportRtf(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Rtf);
        }

        private void HandleExportTxt(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Text);
        }

        private void HandleExportXls(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Xls);
        }

        private void HandleExportXlsx(object[] args)
        {
            this.DoExport(this.PrintingSystemBase.ExportOptions.Xlsx);
        }

        private void HandleSendCsv(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Csv);
        }

        private void HandleSendDocx(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Docx);
        }

        private void HandleSendGraphic(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Image);
        }

        private void HandleSendMht(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Mht);
        }

        private void HandleSendPdf(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Pdf);
        }

        private void HandleSendRtf(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Rtf);
        }

        private void HandleSendTxt(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Text);
        }

        private void HandleSendXls(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Xls);
        }

        private void HandleSendXlsx(object[] args)
        {
            this.SendFileByEmail(this.PrintingSystemBase.ExportOptions.Xlsx);
        }

        private void HandleStopPageBuilding(object[] args)
        {
            this.PrintingSystemBase.Document.StopPageBuilding();
        }

        protected virtual void SendFileByEmail(ExportOptionsBase options)
        {
            this.CreateExportFileHelper().SendFileByEmail(options, this.DisabledExportModes);
        }

        protected abstract DevExpress.XtraPrinting.PrintingSystemBase PrintingSystemBase { get; }

        protected virtual IDictionary<Type, object[]> DisabledExportModes =>
            null;
    }
}

