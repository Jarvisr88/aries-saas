namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public class PSCommandsExportTypeConverter : PSCommandsTypeConverter
    {
        private static readonly PrintingSystemCommand[] commands;

        static PSCommandsExportTypeConverter()
        {
            List<PrintingSystemCommand> list = new List<PrintingSystemCommand>();
            foreach (PrintingSystemCommand command in PSCommandHelper.ExportCommands)
            {
                if (command != PrintingSystemCommand.ExportXps)
                {
                    list.Add(command);
                }
            }
            commands = list.ToArray();
            list.Clear();
        }

        protected override PrintingSystemCommand[] Commands =>
            commands;
    }
}

