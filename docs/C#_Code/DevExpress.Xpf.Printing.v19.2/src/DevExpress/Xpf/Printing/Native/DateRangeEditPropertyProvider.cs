namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    internal class DateRangeEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        static DateRangeEditPropertyProvider()
        {
            ButtonEditPropertyProvider.IsTextEditableProperty.OverrideMetadata(typeof(DateRangeEditPropertyProvider), new PropertyMetadata(false));
        }

        public DateRangeEditPropertyProvider(DateRangeEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;

        protected override bool GetFocusPopupOnOpenInternal() => 
            true;
    }
}

