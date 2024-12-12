namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class PSCommandHelper
    {
        private static bool CommandInArray(PrintingSystemCommand[] commands, PrintingSystemCommand command);
        public static PrintingSystemCommand[] GetCommands();
        public static bool IsExportCommand(PrintingSystemCommand command);
        public static bool IsSendCommand(PrintingSystemCommand command);

        public static PrintingSystemCommand[] ExportCommands { get; }

        public static PrintingSystemCommand[] SendCommands { get; }

        public static PrintingSystemCommand[] PageExportCommands { get; }

        public static PrintingSystemCommand[] ContinuousExportCommands { get; }

        public static PrintingSystemCommand[] AllowOnlyContinuousExportCommands { get; }

        public static PrintingSystemCommand[] AllowOnlyContinuousSendCommands { get; }

        public static PrintingSystemCommand[] PageSendCommands { get; }

        public static PrintingSystemCommand[] ContinuousSendCommands { get; }
    }
}

