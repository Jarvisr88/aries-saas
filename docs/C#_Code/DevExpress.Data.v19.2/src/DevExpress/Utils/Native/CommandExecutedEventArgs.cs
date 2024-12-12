namespace DevExpress.Utils.Native
{
    using System;

    public class CommandExecutedEventArgs : EventArgs
    {
        private readonly object objectToSelect;
        private readonly object commandParameter;
        private readonly DevExpress.Utils.Native.HistoryItem historyItem;
        private readonly DevExpress.Utils.Native.CommandAction commandAction;

        public CommandExecutedEventArgs(object objectToSelect)
        {
            this.objectToSelect = objectToSelect;
        }

        public CommandExecutedEventArgs(object objectToSelect, object commandParameter)
        {
            this.objectToSelect = objectToSelect;
            this.commandParameter = commandParameter;
        }

        public CommandExecutedEventArgs(object objectToSelect, object commandParameter, DevExpress.Utils.Native.HistoryItem historyItem, DevExpress.Utils.Native.CommandAction commandAction)
        {
            this.objectToSelect = objectToSelect;
            this.commandParameter = commandParameter;
            this.historyItem = historyItem;
            this.commandAction = commandAction;
        }

        public object ObjectToSelect =>
            this.objectToSelect;

        public object CommandParameter =>
            this.commandParameter;

        public DevExpress.Utils.Native.HistoryItem HistoryItem =>
            this.historyItem;

        public DevExpress.Utils.Native.CommandAction CommandAction =>
            this.commandAction;
    }
}

