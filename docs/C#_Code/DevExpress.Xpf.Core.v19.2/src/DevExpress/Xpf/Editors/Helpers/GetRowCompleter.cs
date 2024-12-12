namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using System;
    using System.Runtime.CompilerServices;

    public class GetRowCompleter : IAsyncListServerDataViewCommandCompletedContainer
    {
        public GetRowCompleter(IAsyncListServerDataView view)
        {
            this.View = view;
        }

        public void Completed(object args)
        {
            CommandGetRow command = (CommandGetRow) args;
            if (this.CompletedInternal(command))
            {
                this.ProcessCommand(command);
            }
        }

        protected virtual bool CompletedInternal(CommandGetRow command)
        {
            if (command.Index < 0)
            {
                return false;
            }
            this.Row = command.Row;
            this.ControllerIndex = command.Index;
            return true;
        }

        protected virtual void ProcessCommand(CommandGetRow command)
        {
            this.View.NotifyLoaded(this.ControllerIndex);
        }

        public IAsyncListServerDataView View { get; private set; }

        public object Row { get; private set; }

        public int ControllerIndex { get; private set; }
    }
}

