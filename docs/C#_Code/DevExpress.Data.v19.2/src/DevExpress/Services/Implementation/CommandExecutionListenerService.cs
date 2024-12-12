namespace DevExpress.Services.Implementation
{
    using DevExpress.Services;
    using DevExpress.Utils;
    using DevExpress.Utils.Commands;
    using System;

    public class CommandExecutionListenerService : ICommandExecutionListenerService, IBatchUpdateable, IBatchUpdateHandler
    {
        private DevExpress.Utils.BatchUpdateHelper batchUpdateHelper;

        public CommandExecutionListenerService()
        {
            this.batchUpdateHelper = new DevExpress.Utils.BatchUpdateHelper(this);
        }

        public virtual void BeginCommandExecution(Command command, ICommandUIState state)
        {
            this.BeginUpdate();
        }

        public void BeginUpdate()
        {
            this.BatchUpdateHelper.BeginUpdate();
        }

        public void CancelUpdate()
        {
            this.BatchUpdateHelper.EndUpdate();
        }

        void IBatchUpdateHandler.OnBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnCancelUpdate()
        {
        }

        void IBatchUpdateHandler.OnEndUpdate()
        {
        }

        void IBatchUpdateHandler.OnFirstBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnLastCancelUpdate()
        {
            this.OnLastEndUpdateCore();
        }

        void IBatchUpdateHandler.OnLastEndUpdate()
        {
            this.OnLastEndUpdateCore();
        }

        public virtual void EndCommandExecution(Command command, ICommandUIState state)
        {
            this.EndUpdate();
        }

        public void EndUpdate()
        {
            this.BatchUpdateHelper.EndUpdate();
        }

        protected internal virtual void OnLastEndUpdateCore()
        {
        }

        DevExpress.Utils.BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            this.BatchUpdateHelper.IsUpdateLocked;

        private DevExpress.Utils.BatchUpdateHelper BatchUpdateHelper =>
            this.batchUpdateHelper;
    }
}

