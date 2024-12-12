namespace DevExpress.Xpf.Editors
{
    public class DateEditCalendarStyleSettings : DateEditStyleSettingsBase
    {
        protected override DateEditPopupContentType GetPopupContentType() => 
            DateEditPopupContentType.Calendar;

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            PopupFooterButtons.None;
    }
}

