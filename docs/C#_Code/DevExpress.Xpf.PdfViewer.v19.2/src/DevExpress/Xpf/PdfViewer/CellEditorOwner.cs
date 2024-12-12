namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class CellEditorOwner : InplaceEditorOwnerBase
    {
        public CellEditorOwner(FrameworkElement owner) : base(owner, false)
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
            if (e.Key == Key.Tab)
            {
                if (ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                {
                    this.Document.DocumentStateController.TabBackward();
                }
                else
                {
                    this.Document.DocumentStateController.TabForward();
                }
                e.Handled = true;
            }
        }

        internal PdfPresenterControl Presenter =>
            (PdfPresenterControl) base.owner;

        private PdfDocumentViewModel Document =>
            (PdfDocumentViewModel) this.Presenter.Document;

        protected override FrameworkElement FocusOwner
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode =>
            DevExpress.Xpf.Core.EditorShowMode.MouseDown;

        protected override bool EditorSetInactiveAfterClick =>
            false;

        protected override Type OwnerBaseType =>
            typeof(PdfPresenterControl);

        public PdfPageControl Page { get; set; }

        public PdfPageViewModel PageModel { get; set; }

        public Grid VisualHost { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellEditorOwner.<>c <>9 = new CellEditorOwner.<>c();
            public static Func<InplaceEditorBase, bool> <>9__10_0;
            public static Func<bool> <>9__10_1;
            public static Action<InplaceEditorBase> <>9__10_2;

            internal bool <CommitEditing>b__10_0(InplaceEditorBase x) => 
                x.PostEditor(true);

            internal bool <CommitEditing>b__10_1() => 
                false;

            internal void <CommitEditing>b__10_2(InplaceEditorBase x)
            {
                x.HideEditor(true);
            }
        }
    }
}

