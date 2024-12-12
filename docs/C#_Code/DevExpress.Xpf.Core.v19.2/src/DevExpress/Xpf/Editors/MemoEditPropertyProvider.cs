namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class MemoEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        static MemoEditPropertyProvider()
        {
            ButtonEditPropertyProvider.IsTextEditableProperty.OverrideMetadata(typeof(MemoEditPropertyProvider), new PropertyMetadata(false));
        }

        public MemoEditPropertyProvider(TextEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;

        protected override bool GetFocusPopupOnOpenInternal() => 
            true;
    }
}

