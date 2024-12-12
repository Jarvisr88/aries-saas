namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class DateEditPickerStyleSettings : DateEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            DateEdit o = (DateEdit) editor;
            if (!o.IsPropertySet(PopupBaseEdit.FocusPopupOnOpenProperty))
            {
                o.FocusPopupOnOpen = true;
            }
            if (!o.IsPropertySet(PopupBaseEdit.PopupMaxHeightProperty) || double.IsPositiveInfinity(o.PopupMaxHeight))
            {
                Point offset = new Point();
                o.PopupMaxHeight = ScreenHelper.GetNearestScreenRect(ScreenHelper.GetScreenPoint(o, offset), false).Height / 3.0;
            }
        }

        protected override DateEditPopupContentType GetPopupContentType() => 
            DateEditPopupContentType.DateTimePicker;
    }
}

