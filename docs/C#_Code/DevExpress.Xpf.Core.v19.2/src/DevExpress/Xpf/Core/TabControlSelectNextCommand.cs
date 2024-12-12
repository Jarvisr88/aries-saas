namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Commands;
    using DevExpress.Xpf.Core.Localization;
    using System;

    public class TabControlSelectNextCommand : TabControlCommand
    {
        protected TabControlSelectNextCommand(DXTabControl control) : base(control)
        {
        }

        public override void ForceExecute(ICommandUIState state)
        {
            base.Control.SelectNext();
        }

        protected override void UpdateUIStateCore(ICommandUIState state)
        {
            state.Visible = base.Control.SelectedIndex != -1;
            state.Enabled = base.Control.SelectedIndex < (base.Control.Items.Count - 1);
        }

        protected internal override TabControlStringId MenuCaptionStringId =>
            TabControlStringId.MenuCmd_SelectNext;

        protected internal override TabControlStringId DescriptionStringId =>
            TabControlStringId.MenuCmd_SelectNextDescription;
    }
}

