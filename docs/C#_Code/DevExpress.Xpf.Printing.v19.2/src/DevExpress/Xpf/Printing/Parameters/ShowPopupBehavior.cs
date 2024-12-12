namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Windows.Input;

    public class ShowPopupBehavior : Behavior<ComboBoxEdit>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.PreviewMouseDown += new MouseButtonEventHandler(this.OnPreviewMouseDown);
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TokenEditorPresenter focusedToken;
            DevExpress.Xpf.Editors.Internal.TokenEditor tokenEditor = this.TokenEditor;
            if (tokenEditor != null)
            {
                focusedToken = tokenEditor.FocusedToken;
            }
            else
            {
                DevExpress.Xpf.Editors.Internal.TokenEditor local1 = tokenEditor;
                focusedToken = null;
            }
            if ((focusedToken != null) && (this.TokenEditor.FocusedToken.IsNewTokenEditorPresenter && this.TokenEditor.FocusedToken.IsMouseOver))
            {
                base.AssociatedObject.IsPopupOpen = !base.AssociatedObject.IsPopupOpen;
            }
        }

        private DevExpress.Xpf.Editors.Internal.TokenEditor TokenEditor =>
            base.AssociatedObject.EditCore as DevExpress.Xpf.Editors.Internal.TokenEditor;
    }
}

