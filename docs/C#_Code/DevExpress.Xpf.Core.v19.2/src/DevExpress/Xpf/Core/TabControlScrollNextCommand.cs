namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlScrollNextCommand : TabControlCommand
    {
        protected TabControlScrollNextCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            ((TabControlScrollView) base.Control.View).ScrollNext();
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
            TabControlStringId.MenuCmd_ScrollNext;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_ScrollNextDescription;
    }
}

