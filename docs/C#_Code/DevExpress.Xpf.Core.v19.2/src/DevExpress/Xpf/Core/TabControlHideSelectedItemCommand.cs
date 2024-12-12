namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlHideSelectedItemCommand : TabControlCommand
    {
        protected TabControlHideSelectedItemCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            base.Control.HideTabItem(base.Control.SelectedIndex, true);
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Enabled = state.Visible = base.Control.SelectedIndex != -1;
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_HideSelectedItem;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_HideSelectedItemDescription;
    }
}

