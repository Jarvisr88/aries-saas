namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public static class EditorInitializeHelper
    {
        private static MaskType GetMaskType(RegExMaskType maskType)
        {
            switch (maskType)
            {
                case RegExMaskType.Simple:
                    return MaskType.Simple;

                case RegExMaskType.Regular:
                    return MaskType.Regular;

                case RegExMaskType.RegEx:
                    return MaskType.RegEx;
            }
            throw new NotSupportedException();
        }

        public static void InitializeCharEdit(IModelItem textEdit)
        {
            textEdit.SetValueIfNotSet(TextEdit.MaskTypeProperty, MaskType.Simple, false);
            textEdit.SetValueIfNotSet(TextEdit.MaskProperty, "C", false);
            textEdit.SetValueIfNotSet(TextEdit.MaskPlaceHolderProperty, ' ', false);
        }

        public static void InitializeDateTimeMask(IModelItem textEdit, MaskInfo mask)
        {
            InitializeMask(textEdit, mask);
        }

        public static void InitializeEnumItemsSource(IEdmPropertyInfo property, IModelItem comboBox, Type enumType = null)
        {
            if (enumType == null)
            {
                enumType = property.GetUnderlyingClrType();
            }
            IModelItem item = comboBox.Context.CreateItem(typeof(EnumItemsSource));
            item.Properties[BindableBase.GetPropertyName<Type>(Expression.Lambda<Func<Type>>(Expression.Property(Expression.New(typeof(EnumItemsSource)), (MethodInfo) methodof(EnumItemsSource.get_EnumType)), new ParameterExpression[0]))].SetValue(enumType);
            comboBox.SetValue(LookUpEditBase.ItemsSourceProperty, item, false);
            comboBox.SetValueIfNotSet(ButtonEditSettings.IsTextEditableProperty, false, false);
            if (!property.HasNullableType())
            {
                comboBox.SetValueIfNotSet(BaseEditSettings.AllowNullInputProperty, false, true);
            }
            else
            {
                comboBox.SetValueIfNotSet(ButtonEditSettings.NullValueButtonPlacementProperty, EditorPlacement.EditBox, false);
            }
        }

        private static void InitializeMask(IModelItem textEdit, MaskInfo mask)
        {
            AttributesApplier.ApplyMaskAttributesForEditor(mask, () => textEdit, null);
        }

        public static void InitializeMask(IModelItem textEdit, MaskType maskType, MaskInfo mask, int maxLength)
        {
            textEdit.SetValueIfNotSet(TextEdit.MaskTypeProperty, maskType, false);
            InitializeMask(textEdit, mask);
            InitializeMaxLength(textEdit, maxLength);
        }

        public static void InitializeMaxLength(IModelItem textEdit, int maxLength)
        {
            textEdit.SetValueIfNotSet(TextEditSettings.MaxLengthProperty, maxLength, false);
        }

        public static void InitializeRegExMask(IModelItem textEdit, MaskInfo mask, int maxLength)
        {
            InitializeMask(textEdit, GetMaskType(mask.RegExMaskType.Value), mask, maxLength);
        }
    }
}

