namespace DevExpress.Utils.Commands
{
    using DevExpress.Utils.Svg;
    using System;
    using System.Drawing;

    public abstract class Command
    {
        private DevExpress.Utils.Commands.CommandSourceType commandSourceType;
        private bool hideDisabled;

        protected Command();
        public virtual bool CanExecute();
        public virtual ICommandUIState CreateDefaultCommandUIState();
        public virtual void Execute();
        public abstract void ForceExecute(ICommandUIState state);
        public virtual void UpdateUIState(ICommandUIState state);
        protected abstract void UpdateUIStateCore(ICommandUIState state);
        protected internal virtual void UpdateUIStateViaService(ICommandUIState state);

        public abstract string MenuCaption { get; }

        public abstract string Description { get; }

        public virtual System.Drawing.Image Image { get; }

        public virtual System.Drawing.Image LargeImage { get; }

        public virtual DevExpress.Utils.Svg.SvgImage SvgImage { get; }

        public virtual DevExpress.Utils.Commands.CommandSourceType CommandSourceType { get; set; }

        public bool HideDisabled { get; set; }

        public virtual bool ShowsModalDialog { get; }

        protected virtual bool ShouldBeExecutedOnKeyUpInSilverlightEnvironment { get; }

        protected internal bool InnerShouldBeExecutedOnKeyUpInSilverlightEnvironment { get; }

        protected internal virtual IServiceProvider ServiceProvider { get; }

        public virtual string KeyTip { get; }

        public virtual bool SupportsImage { get; }
    }
}

