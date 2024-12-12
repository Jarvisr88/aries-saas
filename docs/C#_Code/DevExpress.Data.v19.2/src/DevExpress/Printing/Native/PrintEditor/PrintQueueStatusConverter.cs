namespace DevExpress.Printing.Native.PrintEditor
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class PrintQueueStatusConverter
    {
        private static Dictionary<PrinterStatus, PreviewStringId> stringIds = new Dictionary<PrinterStatus, PreviewStringId>();

        static PrintQueueStatusConverter()
        {
            stringIds.Add(PrinterStatus.None, PreviewStringId.PrinterStatus_Ready);
            stringIds.Add(PrinterStatus.Paused, PreviewStringId.PrinterStatus_Paused);
            stringIds.Add(PrinterStatus.Error, PreviewStringId.PrinterStatus_Error);
            stringIds.Add(PrinterStatus.PendingDeletion, PreviewStringId.PrinterStatus_PendingDeletion);
            stringIds.Add(PrinterStatus.PaperJam, PreviewStringId.PrinterStatus_PaperJam);
            stringIds.Add(PrinterStatus.PaperOut, PreviewStringId.PrinterStatus_PaperOut);
            stringIds.Add(PrinterStatus.ManualFeed, PreviewStringId.PrinterStatus_ManualFeed);
            stringIds.Add(PrinterStatus.PaperProblem, PreviewStringId.PrinterStatus_PaperProblem);
            stringIds.Add(PrinterStatus.Offline, PreviewStringId.PrinterStatus_Offline);
            stringIds.Add(PrinterStatus.IOActive, PreviewStringId.PrinterStatus_IOActive);
            stringIds.Add(PrinterStatus.Busy, PreviewStringId.PrinterStatus_Busy);
            stringIds.Add(PrinterStatus.Printing, PreviewStringId.PrinterStatus_Printing);
            stringIds.Add(PrinterStatus.OutputBinFull, PreviewStringId.PrinterStatus_OutputBinFull);
            stringIds.Add(PrinterStatus.NotAvailable, PreviewStringId.PrinterStatus_NotAvailable);
            stringIds.Add(PrinterStatus.Waiting, PreviewStringId.PrinterStatus_Waiting);
            stringIds.Add(PrinterStatus.Processing, PreviewStringId.PrinterStatus_Processing);
            stringIds.Add(PrinterStatus.Initializing, PreviewStringId.PrinterStatus_Initializing);
            stringIds.Add(PrinterStatus.WarmingUp, PreviewStringId.PrinterStatus_WarmingUp);
            stringIds.Add(PrinterStatus.TonerLow, PreviewStringId.PrinterStatus_TonerLow);
            stringIds.Add(PrinterStatus.NoToner, PreviewStringId.PrinterStatus_NoToner);
            stringIds.Add(PrinterStatus.PagePunt, PreviewStringId.PrinterStatus_PagePunt);
            stringIds.Add(PrinterStatus.UserIntervention, PreviewStringId.PrinterStatus_UserIntervention);
            stringIds.Add(PrinterStatus.OutOfMemory, PreviewStringId.PrinterStatus_OutOfMemory);
            stringIds.Add(PrinterStatus.DoorOpen, PreviewStringId.PrinterStatus_DoorOpen);
            stringIds.Add(PrinterStatus.ServerUnknown, PreviewStringId.PrinterStatus_ServerUnknown);
            stringIds.Add(PrinterStatus.PowerSave, PreviewStringId.PrinterStatus_PowerSave);
            stringIds.Add(PrinterStatus.ServerOffline, PreviewStringId.PrinterStatus_ServerOffline);
            stringIds.Add(PrinterStatus.DriverUpdateNeeded, PreviewStringId.PrinterStatus_DriverUpdateNeeded);
        }

        public static string GetString(this PrinterStatus printQueueStatus)
        {
            if (printQueueStatus == PrinterStatus.None)
            {
                return PreviewLocalizer.Active.GetLocalizedString(stringIds[PrinterStatus.None]);
            }
            List<string> values = new List<string>();
            foreach (PrinterStatus status in Enum.GetValues(typeof(PrinterStatus)))
            {
                if ((status != PrinterStatus.None) && printQueueStatus.HasFlag(status))
                {
                    values.Add(PreviewLocalizer.Active.GetLocalizedString(stringIds[status]));
                }
            }
            return ((values.Count == 1) ? values[0] : string.Join(", ", values));
        }
    }
}

