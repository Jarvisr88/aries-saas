namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DocumentInplaceEditorOwner : InplaceEditorOwnerBase
    {
        public DocumentInplaceEditorOwner(DocumentPresenterControl owner) : base(owner, false)
        {
        }

        protected override bool CommitEditing()
        {
            // Unresolved stack state at '0000006F'
        }

        protected override void EnqueueImmediateAction(IAction action)
        {
            this.Presenter.ImmediateActionsManager.EnqueueAction(action);
        }

        protected override string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value) => 
            originalDisplayText;

        protected override bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText)
        {
            displayText = originalDisplayText;
            return true;
        }

        protected override bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers();
            if ((e.Key == Key.Tab) && (((keyboardModifiers & ModifierKeys.None) | ModifierKeys.Shift) > ModifierKeys.None))
            {
                e.Handled = true;
                this.Presenter.EditingStrategy.NavigateToNextField(keyboardModifiers == ModifierKeys.None);
            }
            else if ((e.Key == Key.Return) && ((keyboardModifiers & ModifierKeys.Control) > ModifierKeys.None))
            {
                e.Handled = true;
                this.Presenter.EditingStrategy.EndEditing();
            }
        }

        internal DocumentPresenterControl Presenter =>
            (DocumentPresenterControl) base.owner;

        protected override FrameworkElement FocusOwner
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DocumentInplaceEditorBase CurrentCellEditor
        {
            get => 
                (DocumentInplaceEditorBase) base.CurrentCellEditor;
            set => 
                base.CurrentCellEditor = value;
        }

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode =>
            DevExpress.Xpf.Core.EditorShowMode.MouseDown;

        protected override bool EditorSetInactiveAfterClick =>
            false;

        protected override Type OwnerBaseType =>
            typeof(DocumentPresenterControl);

        public PreviewPageControl Page { get; set; }

        public Grid VisualHost { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentInplaceEditorOwner.<>c <>9 = new DocumentInplaceEditorOwner.<>c();
            public static Func<DocumentInplaceEditorBase, bool> <>9__25_0;
            public static Func<bool> <>9__25_1;
            public static Action<DocumentInplaceEditorBase> <>9__25_2;

            internal bool <CommitEditing>b__25_0(DocumentInplaceEditorBase x) => 
                x.PostEditor(true);

            internal bool <CommitEditing>b__25_1() => 
                false;

            internal void <CommitEditing>b__25_2(DocumentInplaceEditorBase x)
            {
                x.HideEditor(true);
            }
        }
    }
}

