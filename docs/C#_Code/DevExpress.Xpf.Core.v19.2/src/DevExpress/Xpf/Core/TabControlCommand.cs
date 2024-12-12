namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public abstract class TabControlCommand : Command
    {
        private readonly DXTabControl control;

        protected TabControlCommand(DXTabControl control)
        {
            Guard.ArgumentNotNull(control, "control");
            this.control = control;
        }

        public override string MenuCaption =>
            TabControlLocalizer.GetString(this.MenuCaptionStringId);

        public override string Description =>
            TabControlLocalizer.GetString(this.DescriptionStringId);

        protected internal abstract TabControlStringId MenuCaptionStringId { get; }

        protected internal abstract TabControlStringId DescriptionStringId { get; }

        protected internal DXTabControl Control =>
            this.control;
    }
}

