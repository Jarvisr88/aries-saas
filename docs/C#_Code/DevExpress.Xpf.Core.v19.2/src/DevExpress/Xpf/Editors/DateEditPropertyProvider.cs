namespace DevExpress.Xpf.Editors
{
    using System;

    public class DateEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        public DateEditPropertyProvider(TextEdit editor) : base(editor)
        {
        }

        public override PopupFooterButtons GetPopupFooterButtons() => 
            (this.Editor.DateEditPopupContentType != DateEditPopupContentType.Calendar) ? base.GetPopupFooterButtons() : PopupFooterButtons.None;

        private DateEdit Editor =>
            (DateEdit) base.Editor;
    }
}

