namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    internal sealed class InplaceEditorOwner : InplaceEditorOwnerBase
    {
        private readonly Action<IAction> enqueueAction;
        private readonly Action<KeyEventArgs> processKeyDown;
        private readonly Func<MouseButtonEventArgs, bool> performNavigationOnLeftButtonDown;

        public InplaceEditorOwner(FrameworkElement owner, Action<KeyEventArgs> processKeyDown, Func<MouseButtonEventArgs, bool> performNavigationOnLeftButtonDown, Action<IAction> enqueueAction) : base(owner, true)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            if (processKeyDown == null)
            {
                throw new ArgumentNullException("processKeyDown");
            }
            if (performNavigationOnLeftButtonDown == null)
            {
                throw new ArgumentNullException("performNavigationOnLeftButtonDown");
            }
            this.enqueueAction = enqueueAction;
            this.processKeyDown = processKeyDown;
            this.performNavigationOnLeftButtonDown = performNavigationOnLeftButtonDown;
            owner.PreviewMouseDown += new MouseButtonEventHandler(this.OnPreviewMouseDown);
            owner.PreviewMouseUp += new MouseButtonEventHandler(this.OnPreviewMouseUp);
        }

        public void CommitCurrentCellEditor()
        {
            InplaceEditorBase currentCellEditor = base.CurrentCellEditor;
            if (currentCellEditor == null)
            {
                InplaceEditorBase local1 = currentCellEditor;
            }
            else
            {
                currentCellEditor.CommitEditor(true);
            }
        }

        protected override bool CommitEditing() => 
            false;

        protected override void EnqueueImmediateAction(IAction action)
        {
            this.enqueueAction(action);
        }

        protected override string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value) => 
            originalDisplayText;

        protected override bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText)
        {
            displayText = originalDisplayText;
            return false;
        }

        protected override bool NeedsKey(Key key, ModifierKeys modifiers, bool defaultValue) => 
            ((base.CurrentCellEditor == null) || ((key != Key.Down) || ((modifiers & ModifierKeys.Alt) != ModifierKeys.Alt))) ? base.NeedsKey(key, modifiers, defaultValue) : true;

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.ProcessMouseLeftButtonDown(e);
            }
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.ProcessMouseLeftButtonUp(e);
            }
        }

        protected override bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e) => 
            this.performNavigationOnLeftButtonDown(e);

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            this.processKeyDown(e);
        }

        protected override FrameworkElement FocusOwner =>
            null;

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode =>
            DevExpress.Xpf.Core.EditorShowMode.Default;

        protected override bool EditorSetInactiveAfterClick =>
            false;

        protected override Type OwnerBaseType =>
            base.owner.GetType();
    }
}

