namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class DateEditVisualClientOwner : VisualClientOwner
    {
        public DateEditVisualClientOwner(PopupBaseEdit editor) : base(editor)
        {
        }

        protected override FrameworkElement FindEditor()
        {
            throw new NotImplementedException();
        }

        protected override bool ProcessKeyDownInternal(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override bool ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SetupEditor()
        {
            throw new NotImplementedException();
        }

        public override void SyncProperties(bool syncDataSource)
        {
            throw new NotImplementedException();
        }
    }
}

