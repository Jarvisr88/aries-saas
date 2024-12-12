namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public class PSCommandsSendTypeConverter : PSCommandsTypeConverter
    {
        private static readonly PrintingSystemCommand[] commands;

        static PSCommandsSendTypeConverter()
        {
            List<PrintingSystemCommand> list = new List<PrintingSystemCommand>();
            foreach (PrintingSystemCommand command in PSCommandHelper.SendCommands)
            {
                if (command != PrintingSystemCommand.SendXps)
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

