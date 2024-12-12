namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlScrollPrevCommand : TabControlCommand
    {
        protected TabControlScrollPrevCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            ((TabControlScrollView) base.Control.View).ScrollPrev();
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Visible = base.Control.View is TabControlScrollView;
            if (base.Control.View is TabControlScrollView)
            {
                state.Enabled = ((TabControlScrollView) base.Control.View).CanScrollPrev;
            }
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_ScrollPrev;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_ScrollPrevDescription;
    }
}

