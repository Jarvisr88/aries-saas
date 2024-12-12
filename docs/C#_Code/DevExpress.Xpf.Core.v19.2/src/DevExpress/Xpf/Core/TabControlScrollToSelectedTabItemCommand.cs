namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlScrollToSelectedTabItemCommand : TabControlCommand
    {
        protected TabControlScrollToSelectedTabItemCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            ((TabControlScrollView) base.Control.View).ScrollToSelectedTabItem(true);
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Visible = base.Control.View is TabControlScrollView;
            if (base.Control.View is TabControlScrollView)
            {
                state.Enabled = ((TabControlScrollView) base.Control.View).CanScrollToSelectedTabItem;
            }
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_ScrollToSelectedTabItem;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_ScrollToSelectedTabItemDescription;
    }
}

