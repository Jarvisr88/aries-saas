namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class PopupColorEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        static PopupColorEditPropertyProvider()
        {
            Type forType = typeof(PopupColorEditPropertyProvider);
            ButtonEditPropertyProvider.IsTextEditableProperty.OverrideMetadata(forType, new PropertyMetadata(false));
        }

        public PopupColorEditPropertyProvider(PopupColorEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;
    }
}

