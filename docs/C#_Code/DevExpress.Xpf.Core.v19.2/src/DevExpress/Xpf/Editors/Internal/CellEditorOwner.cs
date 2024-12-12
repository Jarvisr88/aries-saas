namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class CellEditorOwner : InplaceEditorOwnerBase
    {
        public CellEditorOwner(TokenEditor owner) : base(owner, true)
        {
        }

        protected override bool CommitEditing() => 
            true;

        protected internal override void EnqueueImmediateAction(IAction action)
        {
            this.OwnerBox.ImmediateActionsManager.EnqueueAction(action);
        }

        protected internal override string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value) => 
            originalDisplayText;

        protected internal override bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText)
        {
            displayText = originalDisplayText;
            return true;
        }

        protected override bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e) => 
            true;

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            this.OwnerBox.ProcessKeyDownFromCellEditor(e);
        }

        public TokenEditor OwnerBox =>
            base.owner as TokenEditor;

        protected override FrameworkElement FocusOwner =>
            this.OwnerBox;

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode =>
            DevExpress.Xpf.Core.EditorShowMode.MouseDown;

        protected override bool EditorSetInactiveAfterClick
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override Type OwnerBaseType =>
            typeof(TokenEditor);
    }
}

