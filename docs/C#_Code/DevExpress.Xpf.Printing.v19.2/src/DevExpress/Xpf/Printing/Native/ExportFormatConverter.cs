namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public static class ExportFormatConverter
    {
        public static PrintingSystemCommand ToExportCommand(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Pdf:
                    return PrintingSystemCommand.ExportPdf;

                case ExportFormat.Htm:
                    return PrintingSystemCommand.ExportHtm;

                case ExportFormat.Mht:
                    return PrintingSystemCommand.ExportMht;

                case ExportFormat.Rtf:
                    return PrintingSystemCommand.ExportRtf;

                case ExportFormat.Docx:
                    return PrintingSystemCommand.ExportDocx;

                case ExportFormat.Xls:
                    return PrintingSystemCommand.ExportXls;

                case ExportFormat.Xlsx:
                    return PrintingSystemCommand.ExportXlsx;

                case ExportFormat.Csv:
                    return PrintingSystemCommand.ExportCsv;

                case ExportFormat.Txt:
                    return PrintingSystemCommand.ExportTxt;

                case ExportFormat.Image:
                    return PrintingSystemCommand.ExportGraphic;

                case ExportFormat.Xps:
                    return PrintingSystemCommand.ExportXps;
            }
            throw new ArgumentException("format");
        }

        public static ExportFormat ToExportFormat(PrintingSystemCommand command)
        {
            switch (command)
            {
                case PrintingSystemCommand.ExportGraphic:
                case PrintingSystemCommand.SendGraphic:
                    return ExportFormat.Image;

                case PrintingSystemCommand.ExportPdf:
                case PrintingSystemCommand.SendPdf:
                    return ExportFormat.Pdf;

                case PrintingSystemCommand.ExportTxt:
                case PrintingSystemCommand.SendTxt:
                    return ExportFormat.Txt;

                case PrintingSystemCommand.ExportCsv:
                case PrintingSystemCommand.SendCsv:
                    return ExportFormat.Csv;

                case PrintingSystemCommand.ExportMht:
                case PrintingSystemCommand.SendMht:
                    return ExportFormat.Mht;

                case PrintingSystemCommand.ExportXls:
                case PrintingSystemCommand.SendXls:
                    return ExportFormat.Xls;

                case PrintingSystemCommand.ExportXlsx:
                case PrintingSystemCommand.SendXlsx:
                    return ExportFormat.Xlsx;

                case PrintingSystemCommand.ExportRtf:
                case PrintingSystemCommand.SendRtf:
                    return ExportFormat.Rtf;

                case PrintingSystemCommand.ExportDocx:
                case PrintingSystemCommand.SendDocx:
                    return ExportFormat.Docx;

                case PrintingSystemCommand.ExportHtm:
                    return ExportFormat.Htm;

                case PrintingSystemCommand.ExportXps:
                case PrintingSystemCommand.SendXps:
                    return ExportFormat.Xps;
            }
            throw new ArgumentException("command");
        }

        public static PrintingSystemCommand ToSendCommand(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Pdf:
                    return PrintingSystemCommand.SendPdf;

                case ExportFormat.Mht:
                    return PrintingSystemCommand.SendMht;

                case ExportFormat.Rtf:
                    return PrintingSystemCommand.SendRtf;

                case ExportFormat.Docx:
                    return PrintingSystemCommand.SendDocx;

                case ExportFormat.Xls:
                    return PrintingSystemCommand.SendXls;

                case ExportFormat.Xlsx:
                    return PrintingSystemCommand.SendXlsx;

                case ExportFormat.Csv:
                    return PrintingSystemCommand.SendCsv;

                case ExportFormat.Txt:
                    return PrintingSystemCommand.SendTxt;

                case ExportFormat.Image:
                    return PrintingSystemCommand.SendGraphic;

                case ExportFormat.Xps:
                    return PrintingSystemCommand.SendXps;
            }
            throw new ArgumentException("format");
        }
    }
}

