namespace DevExpress.Utils.Commands
{
    using System;

    public class DefaultCommandUIState : ICommandUIState
    {
        private bool isEnabled;
        private bool isChecked;
        private bool isVisible;

        public DefaultCommandUIState();

        public virtual bool Enabled { get; set; }

        public virtual bool Visible { get; set; }

        public virtual bool Checked { get; set; }

        public virtual object EditValue { get; set; }
    }
}

