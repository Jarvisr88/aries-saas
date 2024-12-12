namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class CommandSetBase
    {
        protected bool fCommandVisibilityChanged;
        private bool dirty;

        public event EventHandler CommandVisibilityChanged;

        protected CommandSetBase()
        {
        }

        protected abstract bool ContainsCommand(PrintingSystemCommand command);
        public void EnableCommand(bool value, params PrintingSystemCommand[] commands)
        {
            foreach (PrintingSystemCommand command in commands)
            {
                this.EnableCommand(value, command);
            }
        }

        protected abstract void EnableCommand(bool value, PrintingSystemCommand command);
        public abstract bool HasEnabledCommand(PrintingSystemCommand[] commands);
        protected void ResumeRaiseCommandChanged()
        {
            if (this.fCommandVisibilityChanged && (this.CommandVisibilityChanged != null))
            {
                this.CommandVisibilityChanged(this, EventArgs.Empty);
            }
        }

        internal void SetCommandVisibility(PrintingSystemCommand command, CommandVisibility visibility, Priority priority, PrintingSystemBase ps)
        {
            this.SuspendRaiseCommandChanged();
            this.SetCommandVisibilityCore(command, visibility, priority, ps);
            this.ResumeRaiseCommandChanged();
        }

        internal void SetCommandVisibility(PrintingSystemCommand[] commands, CommandVisibility visibility, Priority priority, PrintingSystemBase ps)
        {
            this.SuspendRaiseCommandChanged();
            foreach (PrintingSystemCommand command in commands)
            {
                this.SetCommandVisibilityCore(command, visibility, priority, ps);
            }
            this.ResumeRaiseCommandChanged();
        }

        protected abstract void SetCommandVisibilityCore(PrintingSystemCommand command, CommandVisibility visibility, Priority priority, PrintingSystemBase ps);
        protected void SuspendRaiseCommandChanged()
        {
            this.fCommandVisibilityChanged = false;
        }

        public static CommandVisibility ToCommandVisibility(bool value) => 
            value ? CommandVisibility.All : CommandVisibility.None;

        public void UpdateOpenAndClosePreviewCommands(bool enable)
        {
            this.EnableCommand(enable, PrintingSystemCommand.Open);
            this.EnableCommand(enable, PrintingSystemCommand.ClosePreview);
        }

        public void UpdateStopPageBuildingCommand(bool enable, PrintingSystemBase ps)
        {
            this.EnableCommand(enable, PrintingSystemCommand.StopPageBuilding);
            this.SetCommandVisibility(PrintingSystemCommand.StopPageBuilding, ToCommandVisibility(enable), Priority.High, ps);
        }

        public bool Dirty
        {
            get => 
                this.dirty;
            set => 
                this.dirty = value;
        }
    }
}

