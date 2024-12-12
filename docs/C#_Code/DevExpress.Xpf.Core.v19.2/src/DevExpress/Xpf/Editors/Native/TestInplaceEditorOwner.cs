namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class TestInplaceEditorOwner : InplaceEditorOwnerBase
    {
        public TestInplaceEditorOwner(TestInplaceContainer container) : base(container, true)
        {
        }

        protected override bool CommitEditing() => 
            true;

        protected internal override void EnqueueImmediateAction(IAction action)
        {
            this.Container.EnqueueImmediateAction(action);
        }

        protected internal override string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value) => 
            originalDisplayText;

        protected internal override bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText)
        {
            displayText = originalDisplayText;
            return true;
        }

        protected override bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Container.PerformNavigationOnLeftButtonDown(e.OriginalSource as DependencyObject);
            return true;
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            this.Container.ProcessKeyDown(e);
        }

        private TestInplaceContainer Container =>
            (TestInplaceContainer) base.owner;

        protected override FrameworkElement FocusOwner =>
            base.owner;

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode =>
            DevExpress.Xpf.Core.EditorShowMode.MouseDown;

        protected override bool EditorSetInactiveAfterClick =>
            false;

        protected override bool CanCommitEditing =>
            true;

        protected override Type OwnerBaseType =>
            typeof(TestInplaceContainer);
    }
}

