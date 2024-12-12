namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlSelectPrevCommand : TabControlCommand
    {
        protected TabControlSelectPrevCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            base.Control.SelectPrev();
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Visible = base.Control.SelectedIndex != -1;
            state.Enabled = base.Control.SelectedIndex > 0;
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_SelectPrev;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_SelectPrevDescription;
    }
}

