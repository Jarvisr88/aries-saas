namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class DateEditNavigatorWithTimePickerStyleSettings : DateEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            DateEdit o = (DateEdit) editor;
            if (!o.IsPropertySet(TextEdit.MaskProperty))
            {
                o.Mask = "G";
            }
        }

        protected override DateEditPopupContentType GetPopupContentType() => 
            DateEditPopupContentType.NavigatorWithTimePicker;

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            editor.IsReadOnly ? base.GetPopupFooterButtons(editor) : PopupFooterButtons.OkCancel;

        public bool ClosePopupOnDateNavigatorDateSelected { get; set; }
    }
}

