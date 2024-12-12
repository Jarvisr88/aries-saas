namespace DevExpress.Utils.Commands
{
    using DevExpress.Services;
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Svg;
    using System;
    using System.Drawing;
    using System.Reflection;

    public abstract class ControlCommand<TControl, TCommandId, TLocalizedStringId> : Command where TControl: class, ICommandAwareControl<TCommandId>, IServiceProvider where TCommandId: struct where TLocalizedStringId: struct
    {
        private readonly TControl control;

        protected ControlCommand(TControl control);
        protected internal virtual ICommandExecutionListenerService GetCommandExecutionListener();
        protected internal virtual System.Drawing.Image LoadImage();
        protected internal virtual System.Drawing.Image LoadLargeImage();
        protected internal virtual DevExpress.Utils.Svg.SvgImage LoadSvgImage();
        protected virtual void NotifyBeginCommandExecution(ICommandUIState state);
        protected virtual void NotifyEndCommandExecution(ICommandUIState state);

        public TControl Control { get; }

        protected internal override IServiceProvider ServiceProvider { get; }

        public abstract TCommandId Id { get; }

        protected abstract XtraLocalizer<TLocalizedStringId> Localizer { get; }

        public abstract TLocalizedStringId MenuCaptionStringId { get; }

        public abstract TLocalizedStringId DescriptionStringId { get; }

        public override string MenuCaption { get; }

        public override string Description { get; }

        public virtual string ImageName { get; }

        protected abstract string ImageResourcePrefix { get; }

        protected virtual string SvgImageResourcePrefix { get; }

        protected virtual Assembly ImageResourceAssembly { get; }

        public override System.Drawing.Image Image { get; }

        public override System.Drawing.Image LargeImage { get; }

        public override bool SupportsImage { get; }

        public override DevExpress.Utils.Svg.SvgImage SvgImage { get; }
    }
}

