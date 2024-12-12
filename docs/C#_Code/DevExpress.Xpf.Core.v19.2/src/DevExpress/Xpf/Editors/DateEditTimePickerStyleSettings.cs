namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class DateEditTimePickerStyleSettings : DateEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            DateEdit o = (DateEdit) editor;
            if (!o.IsPropertySet(TextEdit.MaskProperty))
            {
                o.Mask = "T";
            }
        }

        protected override DateEditPopupContentType GetPopupContentType() => 
            DateEditPopupContentType.TimePicker;

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            editor.IsReadOnly ? base.GetPopupFooterButtons(editor) : PopupFooterButtons.OkCancel;
    }
}

