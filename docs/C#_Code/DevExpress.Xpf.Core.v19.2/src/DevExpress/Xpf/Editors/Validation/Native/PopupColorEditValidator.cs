namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Media;

    public class PopupColorEditValidator : StrategyValidatorBase
    {
        public PopupColorEditValidator(PopupColorEdit editor) : base(editor)
        {
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource)
        {
            object obj2 = base.ProcessConversion(value, updateEditorSource);
            if (obj2 == null)
            {
                return null;
            }
            string str = obj2.ToString();
            try
            {
                return ColorConverter.ConvertFromString(str);
            }
            catch
            {
                return null;
            }
        }
    }
}

