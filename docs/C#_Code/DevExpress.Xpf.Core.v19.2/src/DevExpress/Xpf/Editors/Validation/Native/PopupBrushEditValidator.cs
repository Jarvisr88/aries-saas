namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows.Media;

    public class PopupBrushEditValidator : StrategyValidatorBase
    {
        public PopupBrushEditValidator(PopupBrushEditBase editor) : base(editor)
        {
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource)
        {
            Color color;
            object obj2 = base.ProcessConversion(value, updateEditorSource);
            LookUpEditableItem item = obj2 as LookUpEditableItem;
            return ((item != null) ? (!Text2ColorHelper.TryConvert(item.DisplayValue, out color) ? item.EditValue : new SolidColorBrush(color)) : obj2);
        }
    }
}

