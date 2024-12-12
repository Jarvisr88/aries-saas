namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FilterModelHelper
    {
        private static readonly Type[] AllBooleanEditSettings = new Type[] { typeof(CheckEditSettings), typeof(ToggleSwitchEditSettings) };
        private static readonly Type[] UnsupportedEditSettings;
        private static readonly Type[] UnsupportedComboBoxStyleSettings;

        static FilterModelHelper()
        {
            Type[] typeArray2 = new Type[11];
            typeArray2[0] = typeof(RatingEditSettings);
            typeArray2[1] = typeof(TrackBarEditSettings);
            typeArray2[2] = typeof(HyperlinkEditSettings);
            typeArray2[3] = typeof(ProgressBarEditSettings);
            typeArray2[4] = typeof(BarCodeEditSettings);
            typeArray2[5] = typeof(PasswordBoxEditSettings);
            typeArray2[6] = typeof(ColorEditSettings);
            typeArray2[7] = typeof(ImageEditSettings);
            typeArray2[8] = typeof(SparklineEditSettings);
            typeArray2[9] = typeof(MemoEditSettings);
            typeArray2[10] = typeof(PopupColorEditSettings);
            UnsupportedEditSettings = typeArray2;
            UnsupportedComboBoxStyleSettings = new Type[] { typeof(TokenComboBoxStyleSettings), typeof(CheckedTokenComboBoxStyleSettings), typeof(RadioTokenComboBoxStyleSettings), typeof(SearchControlStyleSettings), typeof(CheckedComboBoxStyleSettings), typeof(RadioComboBoxStyleSettings) };
        }

        private static ComboBoxEditSettings ConvertBoolEditSettings(Type columnType, Func<object, string> getDisplayText)
        {
            ComboBoxEditSettings settings = new ComboBoxEditSettings();
            List<object> list1 = new List<object>();
            list1.Add(true);
            list1.Add(false);
            List<object> source = list1;
            if (columnType == typeof(bool?))
            {
                source.Insert(0, null);
            }
            settings.ItemsSource = source.Select<object, CustomComboBoxItem>(delegate (object value) {
                CustomComboBoxItem item1 = new CustomComboBoxItem();
                item1.EditValue = value;
                item1.DisplayValue = getDisplayText(value);
                return item1;
            });
            settings.IsTextEditable = false;
            return settings;
        }

        private static ComboBoxEditSettings ConvertComboBoxEditSettings(ComboBoxEditSettings editSettings)
        {
            if (!IsUnsupportedComboBoxStyleSettings(editSettings.StyleSettings as BaseComboBoxStyleSettings))
            {
                return editSettings;
            }
            return new ComboBoxEditSettings { 
                ItemTemplate = editSettings.ItemTemplate,
                ItemTemplateSelector = editSettings.ItemTemplateSelector,
                ItemsSource = editSettings.ItemsSource,
                ValueMember = editSettings.ValueMember,
                DisplayMember = editSettings.DisplayMember,
                ApplyItemTemplateToSelectedItem = editSettings.ApplyItemTemplateToSelectedItem
            };
        }

        private static BaseEditSettings ConvertListBoxEditSettings(ListBoxEditSettings listBoxEditSettings)
        {
            ComboBoxEditSettings settings = new ComboBoxEditSettings {
                ItemTemplate = listBoxEditSettings.ItemTemplate,
                ItemTemplateSelector = listBoxEditSettings.ItemTemplateSelector,
                ItemsSource = listBoxEditSettings.ItemsSource,
                ValueMember = listBoxEditSettings.ValueMember,
                DisplayMember = listBoxEditSettings.DisplayMember,
                ApplyItemTemplateToSelectedItem = true
            };
            if (listBoxEditSettings.StyleSettings != null)
            {
                if (listBoxEditSettings.StyleSettings is CheckedListBoxEditStyleSettings)
                {
                    settings.StyleSettings = new CheckedComboBoxStyleSettings();
                }
                else if (listBoxEditSettings.StyleSettings is RadioListBoxEditStyleSettings)
                {
                    settings.StyleSettings = new RadioComboBoxStyleSettings();
                }
            }
            return settings;
        }

        public static object GetEffectiveFilterValue(object value, Type valueType) => 
            FilterHelperBase.CorrectFilterValueType(valueType, value);

        internal static BaseEditSettings GetSupportedEditSettings(FilteringColumn column) => 
            GetSupportedEditSettings(column.GetEditSettings(), column.Type, column.GetDisplayText);

        public static BaseEditSettings GetSupportedEditSettings(BaseEditSettings editSettings, Type columnType, Func<object, string> getDisplayText) => 
            (editSettings != null) ? (!IsBooleanEditSettings(editSettings) ? (!(editSettings is ComboBoxEditSettings) ? (!(editSettings is ListBoxEditSettings) ? (!IsUnsupportedEditSettings(editSettings) ? editSettings : new TextEditSettings()) : ConvertListBoxEditSettings((ListBoxEditSettings) editSettings)) : ConvertComboBoxEditSettings((ComboBoxEditSettings) editSettings)) : ConvertBoolEditSettings(columnType, getDisplayText)) : null;

        public static bool IsBooleanEditSettings(BaseEditSettings editSettings)
        {
            foreach (Type type in AllBooleanEditSettings)
            {
                if (type.IsInstanceOfType(editSettings))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsUnsupportedComboBoxStyleSettings(BaseComboBoxStyleSettings styleSettings)
        {
            if (styleSettings != null)
            {
                foreach (Type type in UnsupportedComboBoxStyleSettings)
                {
                    if (type.IsInstanceOfType(styleSettings))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsUnsupportedEditSettings(BaseEditSettings editSettings)
        {
            foreach (Type type in UnsupportedEditSettings)
            {
                if (type.IsInstanceOfType(editSettings))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

