namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class AttributesApplier
    {
        public static void ApplyBaseAttributes(IEdmPropertyInfo propertyInfo, Action<string> setDisplayMember, Action<string> setDisplayName, Action<string> setDisplayShortName, Action<string> setDescription, Action setReadOnly, Action<bool?> setEditable, Action setInvisible, Action setHidden, Action setRequired)
        {
            string str = propertyInfo.HasDisplayAttribute() ? propertyInfo.GetDisplayName() : null;
            string shortName = propertyInfo.Attributes.ShortName ?? str;
            string text2 = propertyInfo.Attributes.Name;
            string text3 = text2;
            if (text2 == null)
            {
                string local2 = text2;
                text3 = shortName;
            }
            string name = text3;
            if (propertyInfo.Attributes.NeedParenthesis())
            {
                shortName = string.IsNullOrEmpty(shortName) ? shortName : $"({shortName})";
                name = $"({name ?? propertyInfo.GetDisplayName()})";
            }
            if (shortName != null)
            {
                setDisplayShortName.Do<Action<string>>(x => x(shortName));
            }
            if (name != null)
            {
                setDisplayName.Do<Action<string>>(x => x(name));
            }
            setDisplayMember.Do<Action<string>>(x => x(propertyInfo.Name));
            string description = propertyInfo.Attributes.Description;
            if (!string.IsNullOrEmpty(description))
            {
                setDescription.Do<Action<string>>(x => x(description));
            }
            if ((propertyInfo.Attributes.IsReadOnly != null) ? propertyInfo.Attributes.IsReadOnly.Value : propertyInfo.IsReadOnly)
            {
                Action<Action> action = <>c.<>9__2_4;
                if (<>c.<>9__2_4 == null)
                {
                    Action<Action> local4 = <>c.<>9__2_4;
                    action = <>c.<>9__2_4 = x => x();
                }
                setReadOnly.Do<Action>(action);
            }
            bool? editable = propertyInfo.Attributes.AllowEdit;
            setEditable.Do<Action<bool?>>(x => x(editable));
            if (((propertyInfo.Attributes.Order != null) ? propertyInfo.Attributes.Order.Value : 0x7fffffff) < 0)
            {
                Action<Action> action = <>c.<>9__2_6;
                if (<>c.<>9__2_6 == null)
                {
                    Action<Action> local5 = <>c.<>9__2_6;
                    action = <>c.<>9__2_6 = x => x();
                }
                setInvisible.Do<Action>(action);
            }
            if (propertyInfo.Attributes.Hidden())
            {
                Action<Action> action = <>c.<>9__2_7;
                if (<>c.<>9__2_7 == null)
                {
                    Action<Action> local6 = <>c.<>9__2_7;
                    action = <>c.<>9__2_7 = x => x();
                }
                setHidden.Do<Action>(action);
            }
            if (propertyInfo.Attributes.Required())
            {
                Action<Action> action = <>c.<>9__2_8;
                if (<>c.<>9__2_8 == null)
                {
                    Action<Action> local7 = <>c.<>9__2_8;
                    action = <>c.<>9__2_8 = x => x();
                }
                setRequired.Do<Action>(action);
            }
        }

        public static void ApplyBaseAttributesForFilterColumn(IEdmPropertyInfo propertyInfo, IModelItem filterColumn)
        {
            ApplyBaseAttributes(propertyInfo, x => filterColumn.Properties["FieldName"].SetValue(x), x => filterColumn.Properties["ColumnCaption"].SetValue(x), null, null, null, null, null, null, null);
        }

        public static void ApplyBaseAttributesForLayoutItem(IEdmPropertyInfo propertyInfo, IModelItem layoutItem, IModelItem editor, Func<string, string> getLabelByPropertyName = null)
        {
            if (getLabelByPropertyName == null)
            {
                Func<string, string> func1 = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<string, string> local1 = <>c.<>9__7_0;
                    func1 = <>c.<>9__7_0 = x => x;
                }
                getLabelByPropertyName = func1;
            }
            Action<IModelItem, DependencyProperty, object> customSetValueMethod = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Action<IModelItem, DependencyProperty, object> local2 = <>c.<>9__7_1;
                customSetValueMethod = <>c.<>9__7_1 = delegate (IModelItem modelItem, DependencyProperty dp, object v) {
                    if (modelItem != null)
                    {
                        modelItem.SetValue(dp, v, false);
                    }
                };
            }
            Action<IModelItem, DependencyProperty, object> editorSetValue = GetMethodForSettingValue(customSetValueMethod);
            ApplyBaseAttributes(propertyInfo, x => layoutItem.Properties["Label"].SetValueIfNotSet(getLabelByPropertyName(x)), x => layoutItem.Properties["Label"].SetValueIfNotSet(x), null, x => layoutItem.Properties["ToolTip"].SetValueIfNotSet(x), () => editorSetValue(editor, BaseEdit.IsReadOnlyProperty, true), delegate (bool? x) {
                if ((x != null) && !x.Value)
                {
                    editorSetValue(editor, BaseEdit.IsReadOnlyProperty, true);
                }
            }, null, null, () => layoutItem.Properties["IsRequired"].SetValue(true));
        }

        public static void ApplyDisplayFormatAttributes(IEdmPropertyInfo propertyInfo, Action<string> setNullText, Action<string> setDisplayFormat, Action setNotConvertEmptyStringsToNull)
        {
            if (!string.IsNullOrEmpty(propertyInfo.Attributes.NullDisplayText))
            {
                setNullText.Do<Action<string>>(x => x(propertyInfo.Attributes.NullDisplayText));
            }
            if (!propertyInfo.Attributes.ConvertEmptyStringToNull)
            {
                Action<Action> action = <>c.<>9__0_1;
                if (<>c.<>9__0_1 == null)
                {
                    Action<Action> local1 = <>c.<>9__0_1;
                    action = <>c.<>9__0_1 = x => x();
                }
                setNotConvertEmptyStringsToNull.Do<Action>(action);
            }
            if (!string.IsNullOrEmpty(propertyInfo.Attributes.DataFormatString))
            {
                setDisplayFormat.Do<Action<string>>(x => x(propertyInfo.Attributes.DataFormatString));
            }
        }

        public static void ApplyDisplayFormatAttributesForEditor(IEdmPropertyInfo propertyInfo, Func<IModelItem> getOrCreateEditor, Action<IModelItem, DependencyProperty, object> setValueMethod = null)
        {
            Action<IModelItem, DependencyProperty, object> setValue = GetMethodForSettingValue(setValueMethod);
            ApplyDisplayFormatAttributes(propertyInfo, x => setValue(getOrCreateEditor(), BaseEdit.NullTextProperty, x), x => setValue(getOrCreateEditor(), BaseEdit.DisplayFormatStringProperty, x), () => setValue(getOrCreateEditor(), BaseEdit.ShowNullTextForEmptyValueProperty, false));
        }

        public static void ApplyDisplayFormatAttributesForEditSettings(IEdmPropertyInfo propertyInfo, Func<IModelItem> getOrCreateEditSettings, Action<IModelItem, DependencyProperty, object> setValueMethod = null)
        {
            Action<IModelItem, DependencyProperty, object> setValue = GetMethodForSettingValue(setValueMethod);
            ApplyDisplayFormatAttributes(propertyInfo, x => setValue(getOrCreateEditSettings(), BaseEditSettings.NullTextProperty, x), x => setValue(getOrCreateEditSettings(), BaseEditSettings.DisplayFormatProperty, x), null);
        }

        public static void ApplyMaskAttributes(MaskInfo maskInfo, Action<string> setMask, Action setUseMaskAsDisplayFormat, Action<CultureInfo> setCulture, Action setAutomaticallyAdvanceCaret, Action<char> setPlaceHolder, Action setNotSaveLiteral, Action setNotShowPlaceHolders, Action setNotIgnoreBlank)
        {
            if (!maskInfo.IsDefaultMask)
            {
                setMask.Do<Action<string>>(x => x(maskInfo.Mask));
            }
            if (maskInfo.UseAsDisplayFormat)
            {
                Action<Action> action = <>c.<>9__1_1;
                if (<>c.<>9__1_1 == null)
                {
                    Action<Action> local1 = <>c.<>9__1_1;
                    action = <>c.<>9__1_1 = x => x();
                }
                setUseMaskAsDisplayFormat.Do<Action>(action);
            }
            if (maskInfo.Culture != null)
            {
                setCulture.Do<Action<CultureInfo>>(x => x(maskInfo.Culture));
            }
            if (maskInfo.AutomaticallyAdvanceCaret)
            {
                Action<Action> action = <>c.<>9__1_3;
                if (<>c.<>9__1_3 == null)
                {
                    Action<Action> local2 = <>c.<>9__1_3;
                    action = <>c.<>9__1_3 = x => x();
                }
                setAutomaticallyAdvanceCaret.Do<Action>(action);
            }
            if (maskInfo.PlaceHolder != '_')
            {
                setPlaceHolder.Do<Action<char>>(x => x(maskInfo.PlaceHolder));
            }
            if (!maskInfo.SaveLiteral)
            {
                Action<Action> action = <>c.<>9__1_5;
                if (<>c.<>9__1_5 == null)
                {
                    Action<Action> local3 = <>c.<>9__1_5;
                    action = <>c.<>9__1_5 = x => x();
                }
                setNotSaveLiteral.Do<Action>(action);
            }
            if (!maskInfo.ShowPlaceHolders)
            {
                Action<Action> action = <>c.<>9__1_6;
                if (<>c.<>9__1_6 == null)
                {
                    Action<Action> local4 = <>c.<>9__1_6;
                    action = <>c.<>9__1_6 = x => x();
                }
                setNotShowPlaceHolders.Do<Action>(action);
            }
            if (!maskInfo.IgnoreBlank)
            {
                Action<Action> action = <>c.<>9__1_7;
                if (<>c.<>9__1_7 == null)
                {
                    Action<Action> local5 = <>c.<>9__1_7;
                    action = <>c.<>9__1_7 = x => x();
                }
                setNotIgnoreBlank.Do<Action>(action);
            }
        }

        public static void ApplyMaskAttributesForEditor(MaskInfo maskInfo, Func<IModelItem> getOrCreateEditor, Action<IModelItem, DependencyProperty, object> setValueMethod = null)
        {
            Action<IModelItem, DependencyProperty, object> setValue = GetMethodForSettingValue(setValueMethod);
            ApplyMaskAttributes(maskInfo, x => setValue(getOrCreateEditor(), TextEdit.MaskProperty, x), () => setValue(getOrCreateEditor(), TextEdit.MaskUseAsDisplayFormatProperty, true), x => setValue(getOrCreateEditor(), TextEdit.MaskCultureProperty, x), () => setValue(getOrCreateEditor(), TextEdit.MaskTypeProperty, MaskType.DateTimeAdvancingCaret), x => setValue(getOrCreateEditor(), TextEdit.MaskPlaceHolderProperty, x), () => setValue(getOrCreateEditor(), TextEdit.MaskSaveLiteralProperty, false), () => setValue(getOrCreateEditor(), TextEdit.MaskShowPlaceHoldersProperty, false), () => setValue(getOrCreateEditor(), TextEdit.MaskIgnoreBlankProperty, false));
        }

        public static void ApplyMaskAttributesForEditSettings(MaskInfo maskInfo, Func<IModelItem> getOrCreateEditSettings, Action<IModelItem, DependencyProperty, object> setValueMethod = null)
        {
            Action<IModelItem, DependencyProperty, object> setValue = GetMethodForSettingValue(setValueMethod);
            ApplyMaskAttributes(maskInfo, x => setValue(getOrCreateEditSettings(), TextEditSettings.MaskProperty, x), () => setValue(getOrCreateEditSettings(), TextEditSettings.MaskUseAsDisplayFormatProperty, true), x => setValue(getOrCreateEditSettings(), TextEditSettings.MaskCultureProperty, x), () => setValue(getOrCreateEditSettings(), TextEditSettings.MaskTypeProperty, MaskType.DateTimeAdvancingCaret), x => setValue(getOrCreateEditSettings(), TextEditSettings.MaskPlaceHolderProperty, x), () => setValue(getOrCreateEditSettings(), TextEditSettings.MaskSaveLiteralProperty, false), () => setValue(getOrCreateEditSettings(), TextEditSettings.MaskShowPlaceHoldersProperty, false), () => setValue(getOrCreateEditSettings(), TextEditSettings.MaskIgnoreBlankProperty, false));
        }

        private static Action<IModelItem, DependencyProperty, object> GetMethodForSettingValue(Action<IModelItem, DependencyProperty, object> customSetValueMethod)
        {
            Action<IModelItem, DependencyProperty, object> action1 = customSetValueMethod;
            if (customSetValueMethod == null)
            {
                Action<IModelItem, DependencyProperty, object> local1 = customSetValueMethod;
                action1 = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Action<IModelItem, DependencyProperty, object> local2 = <>c.<>9__9_0;
                    action1 = <>c.<>9__9_0 = delegate (IModelItem modelItem, DependencyProperty dp, object v) {
                        if (modelItem != null)
                        {
                            modelItem.SetValueIfNotSet(dp, v, false);
                        }
                    };
                }
            }
            return action1;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AttributesApplier.<>c <>9 = new AttributesApplier.<>c();
            public static Action<Action> <>9__0_1;
            public static Action<Action> <>9__1_1;
            public static Action<Action> <>9__1_3;
            public static Action<Action> <>9__1_5;
            public static Action<Action> <>9__1_6;
            public static Action<Action> <>9__1_7;
            public static Action<Action> <>9__2_4;
            public static Action<Action> <>9__2_6;
            public static Action<Action> <>9__2_7;
            public static Action<Action> <>9__2_8;
            public static Func<string, string> <>9__7_0;
            public static Action<IModelItem, DependencyProperty, object> <>9__7_1;
            public static Action<IModelItem, DependencyProperty, object> <>9__9_0;

            internal void <ApplyBaseAttributes>b__2_4(Action x)
            {
                x();
            }

            internal void <ApplyBaseAttributes>b__2_6(Action x)
            {
                x();
            }

            internal void <ApplyBaseAttributes>b__2_7(Action x)
            {
                x();
            }

            internal void <ApplyBaseAttributes>b__2_8(Action x)
            {
                x();
            }

            internal string <ApplyBaseAttributesForLayoutItem>b__7_0(string x) => 
                x;

            internal void <ApplyBaseAttributesForLayoutItem>b__7_1(IModelItem modelItem, DependencyProperty dp, object v)
            {
                if (modelItem != null)
                {
                    modelItem.SetValue(dp, v, false);
                }
            }

            internal void <ApplyDisplayFormatAttributes>b__0_1(Action x)
            {
                x();
            }

            internal void <ApplyMaskAttributes>b__1_1(Action x)
            {
                x();
            }

            internal void <ApplyMaskAttributes>b__1_3(Action x)
            {
                x();
            }

            internal void <ApplyMaskAttributes>b__1_5(Action x)
            {
                x();
            }

            internal void <ApplyMaskAttributes>b__1_6(Action x)
            {
                x();
            }

            internal void <ApplyMaskAttributes>b__1_7(Action x)
            {
                x();
            }

            internal void <GetMethodForSettingValue>b__9_0(IModelItem modelItem, DependencyProperty dp, object v)
            {
                if (modelItem != null)
                {
                    modelItem.SetValueIfNotSet(dp, v, false);
                }
            }
        }
    }
}

