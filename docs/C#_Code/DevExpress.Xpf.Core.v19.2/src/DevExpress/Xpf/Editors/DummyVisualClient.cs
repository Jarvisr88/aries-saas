namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class DummyVisualClient : VisualClientOwner
    {
        public DummyVisualClient(PopupBaseEdit editor) : base(editor)
        {
        }

        protected override FrameworkElement FindEditor() => 
            null;

        protected override bool ProcessKeyDownInternal(KeyEventArgs e) => 
            true;

        protected override bool ProcessPreviewKeyDownInternal(KeyEventArgs e) => 
            true;

        protected override void SetupEditor()
        {
        }

        public override void SyncProperties(bool syncDataSource)
        {
        }
    }
}

