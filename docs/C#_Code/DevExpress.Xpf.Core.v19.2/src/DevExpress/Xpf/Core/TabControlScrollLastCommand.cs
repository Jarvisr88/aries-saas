namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlScrollLastCommand : TabControlCommand
    {
        protected TabControlScrollLastCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            ((TabControlScrollView) base.Control.View).ScrollLast();
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Visible = base.Control.View is TabControlScrollView;
            if (base.Control.View is TabControlScrollView)
            {
                state.Enabled = ((TabControlScrollView) base.Control.View).CanScrollNext;
            }
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_ScrollLast;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_ScrollLastDescription;
    }
}

