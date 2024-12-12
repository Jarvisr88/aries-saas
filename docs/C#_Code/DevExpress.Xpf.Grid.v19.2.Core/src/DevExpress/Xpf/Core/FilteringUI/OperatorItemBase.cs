namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class OperatorItemBase
    {
        internal OperatorItemBase(BaseEditSettings[] editSettings)
        {
            BaseEditSettings[] settingsArray1 = editSettings;
            if (editSettings == null)
            {
                BaseEditSettings[] local1 = editSettings;
                settingsArray1 = new BaseEditSettings[0];
            }
            this.<EditSettings>k__BackingField = settingsArray1;
        }

        public string Caption { get; set; }

        public ImageSource Image { get; set; }

        public DataTemplate OperandTemplate { get; set; }

        public abstract string CustomFunctionName { get; }

        internal BaseEditSettings[] EditSettings { get; }
    }
}

