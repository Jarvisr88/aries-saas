namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Filtering.Internal;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class EditorsGeneratorSelector
    {
        internal static void GenerateEditor(IEdmPropertyInfo property, EditorsGeneratorBase generator, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty, IEnumerable<TypeNamePropertyPair> collectionProperties = null, bool guessImageProperties = false, bool guessDisplayMembers = false, bool skipIEnumerableProperties = false)
        {
            if ((!GenerateEditorBasedEditorTemplates(property, generator, guessImageProperties, skipIEnumerableProperties) && !GenerateFilterEditor(property, generator as EditorsGeneratorFilteringBase, guessImageProperties, skipIEnumerableProperties)) && !GenerateLookUp(property, generator, getForegnKeyProperty, collectionProperties, guessDisplayMembers))
            {
                SelectMethod(generator, true, property, property.PropertyType, guessImageProperties, skipIEnumerableProperties);
            }
        }

        private static bool GenerateEditorBasedEditorTemplates(IEdmPropertyInfo property, EditorsGeneratorBase generator, bool guessImageProperties, bool skipIEnumerableProperties)
        {
            object resourceKey = null;
            resourceKey = GetEditorAttributeTemplateKey(generator.Target, delegate (Type x) {
                Func<CommonEditorAttributeBase, CommonEditorAttributeBase> reader = <>c.<>9__2_1;
                if (<>c.<>9__2_1 == null)
                {
                    Func<CommonEditorAttributeBase, CommonEditorAttributeBase> local1 = <>c.<>9__2_1;
                    reader = <>c.<>9__2_1 = y => y;
                }
                return property.Attributes.ReadAttributeProperty<CommonEditorAttributeBase, CommonEditorAttributeBase>(x, reader, null);
            });
            if (resourceKey == null)
            {
                Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                Type type = underlyingType;
                if (underlyingType == null)
                {
                    Type local1 = underlyingType;
                    type = property.PropertyType;
                }
                IEnumerable<CommonEditorAttributeBase> typeEditorAttributes = AttributesHelper.GetAttributes(type).OfType<CommonEditorAttributeBase>();
                resourceKey = GetEditorAttributeTemplateKey(generator.Target, attrType => new Lazy<CommonEditorAttributeBase>(delegate {
                    Func<CommonEditorAttributeBase, bool> <>9__4;
                    Func<CommonEditorAttributeBase, bool> predicate = <>9__4;
                    if (<>9__4 == null)
                    {
                        Func<CommonEditorAttributeBase, bool> local1 = <>9__4;
                        predicate = <>9__4 = delegate (CommonEditorAttributeBase attr) {
                            Type type = attr.GetType();
                            return (type == attrType) || type.IsSubclassOf(attrType);
                        };
                    }
                    return typeEditorAttributes.FirstOrDefault<CommonEditorAttributeBase>(predicate);
                }));
            }
            if (resourceKey == null)
            {
                return false;
            }
            generator.GenerateEditorFromResources(property, resourceKey, SelectMethod(generator, false, property, property.PropertyType, guessImageProperties, skipIEnumerableProperties).Value);
            return true;
        }

        private static bool GenerateFilterEditor(IEdmPropertyInfo property, EditorsGeneratorFilteringBase generator, bool guessImageProperties, bool skipIEnumerableProperties)
        {
            if (generator == null)
            {
                return false;
            }
            Func<EditorsGeneratorBase.Initializer> func = () => SelectMethod(generator, false, property, property.PropertyType, guessImageProperties, skipIEnumerableProperties).Value;
            Func<FilterEditorAttribute, FilterEditorAttribute> reader = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Func<FilterEditorAttribute, FilterEditorAttribute> local1 = <>c.<>9__4_1;
                reader = <>c.<>9__4_1 = x => x;
            }
            FilterEditorAttribute attribute = property.Attributes.ReadAttributeProperty<FilterEditorAttribute, FilterEditorAttribute>(reader, null).Value;
            if ((generator != null) && property.PropertyType.GetInterfaces().Contains<Type>(typeof(IValueViewModel)))
            {
                Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                Type nullableType = property.PropertyType.GetGenericArguments().First<Type>();
                nullableType = Nullable.GetUnderlyingType(nullableType) ?? nullableType;
                if (genericTypeDefinition == typeof(IRangeValueViewModel<>))
                {
                    generator.FilterRange(property, attribute.RangeEditorSettings, func());
                }
                else if (genericTypeDefinition == typeof(ICollectionValueViewModel<>))
                {
                    generator.FilterLookup(property, attribute.LookupEditorSettings, func());
                }
                else if (genericTypeDefinition == typeof(ISimpleValueViewModel<>))
                {
                    generator.FilterBooleanChoice(property, attribute.BooleanEditorSettings, func());
                }
                else
                {
                    if (!(genericTypeDefinition == typeof(IEnumValueViewModel<>)))
                    {
                        throw new NotImplementedException();
                    }
                    generator.FilterEnumChoice(property, attribute.EnumEditorSettings, func());
                }
                return true;
            }
            if (attribute == null)
            {
                return false;
            }
            if (attribute.RangeEditorSettings != null)
            {
                generator.FilterRangeProperty(property, attribute.RangeEditorSettings, func());
            }
            else if (attribute.BooleanEditorSettings != null)
            {
                generator.FilterBooleanChoiceProperty(property, attribute.BooleanEditorSettings, func());
            }
            else if (attribute.LookupEditorSettings != null)
            {
                generator.FilterLookupProperty(property, attribute.LookupEditorSettings, func());
            }
            else if (attribute.EnumEditorSettings != null)
            {
                generator.FilterEnumChoiceProperty(property, attribute.EnumEditorSettings, func());
            }
            return true;
        }

        private static bool GenerateLookUp(IEdmPropertyInfo property, EditorsGeneratorBase generator, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty, IEnumerable<TypeNamePropertyPair> collectionProperties, bool guessDisplayMembers)
        {
            Func<LookupBindingPropertiesAttribute, LookupBindingPropertiesAttribute> reader = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<LookupBindingPropertiesAttribute, LookupBindingPropertiesAttribute> local1 = <>c.<>9__5_0;
                reader = <>c.<>9__5_0 = x => x;
            }
            LookupBindingPropertiesAttribute attribute = property.Attributes.ReadAttributeProperty<LookupBindingPropertiesAttribute, LookupBindingPropertiesAttribute>(reader, null).Value;
            if (!guessDisplayMembers || !property.IsNavigationProperty)
            {
                return false;
            }
            string displayMemberPropertyName = EditorsSource.GetDisplayMemberPropertyName(property.PropertyType);
            string propertyTypeName = property.GetUnderlyingClrType().FullName;
            TypeNamePropertyPair pair = (collectionProperties != null) ? collectionProperties.FirstOrDefault<TypeNamePropertyPair>(x => (x.TypeFullName == propertyTypeName)) : null;
            ForeignKeyInfo foreignKeyInfo = getForegnKeyProperty.With<Func<IEdmPropertyInfo, ForeignKeyInfo>, ForeignKeyInfo>(x => x(property));
            generator.LookUp(property, pair?.PropertyName, displayMemberPropertyName, foreignKeyInfo);
            return true;
        }

        private static object GetEditorAttributeTemplateKey(EditorsGeneratorBase.EditorsGeneratorTarget target, Func<Type, Lazy<CommonEditorAttributeBase>> readEditorAttribute)
        {
            Lazy<CommonEditorAttributeBase> lazy = new Lazy<CommonEditorAttributeBase>(<>c.<>9__3_0 ??= ((Func<CommonEditorAttributeBase>) (() => null)));
            Lazy<CommonEditorAttributeBase> lazy2 = readEditorAttribute(typeof(DefaultEditorAttribute));
            switch (target)
            {
                case EditorsGeneratorBase.EditorsGeneratorTarget.GridControl:
                    lazy = readEditorAttribute(typeof(GridEditorAttribute));
                    break;

                case EditorsGeneratorBase.EditorsGeneratorTarget.LayoutControl:
                    lazy = readEditorAttribute(typeof(LayoutControlEditorAttribute));
                    break;

                case EditorsGeneratorBase.EditorsGeneratorTarget.PropertyGrid:
                    lazy = readEditorAttribute(typeof(PropertyGridEditorAttribute));
                    break;

                default:
                    break;
            }
            Func<CommonEditorAttributeBase, object> evaluator = <>c.<>9__3_1;
            if (<>c.<>9__3_1 == null)
            {
                Func<CommonEditorAttributeBase, object> local2 = <>c.<>9__3_1;
                evaluator = <>c.<>9__3_1 = x => x.TemplateKey;
            }
            object local3 = lazy.Value.With<CommonEditorAttributeBase, object>(evaluator);
            object local6 = local3;
            if (local3 == null)
            {
                object local4 = local3;
                local6 = lazy2.Value.With<CommonEditorAttributeBase, object>(<>c.<>9__3_2 ??= x => x.TemplateKey);
            }
            return local6;
        }

        private static MaskInfo GetMaskInfo(IEdmPropertyInfo property, string defaultMask, bool defaultNotUseMaskAsDisplayFormat, RegExMaskType? regExMaskType) => 
            MaskInfo.GetMaskIfo(property.Attributes.Mask(), defaultMask, defaultNotUseMaskAsDisplayFormat, regExMaskType, string.IsNullOrEmpty(property.Attributes.DataFormatString));

        private static MaskInfo GetNumericMask(IEdmPropertyInfo property)
        {
            RegExMaskType? defaultMaskType = null;
            defaultMaskType = MaskInfo.GetRegExMaskType(property.Attributes.Mask(), defaultMaskType);
            if (defaultMaskType != null)
            {
                return null;
            }
            bool flag = EditorsSource.NumericFloatTypes.Contains<Type>(property.GetUnderlyingClrType());
            PropertyDataType type = property.Attributes.PropertyDataType();
            string actualMask = property.GetActualMask((type == PropertyDataType.Currency) ? (flag ? "C" : "C0") : (flag ? string.Empty : "d"));
            defaultMaskType = null;
            return GetMaskInfo(property, actualMask, string.IsNullOrEmpty(actualMask), defaultMaskType);
        }

        private static bool IsNumericType(Type type) => 
            EditorsSource.NumericFloatTypes.Contains<Type>(type) || EditorsSource.NumericIntegerTypes.Contains<Type>(type);

        private static Lazy<EditorsGeneratorBase.Initializer> SelectMethod(EditorsGeneratorBase generator, bool callGenerateMethod, IEdmPropertyInfo property, Type propertyType, bool guessImageProperties, bool skipIEnumerableProperties)
        {
            RegExMaskType? nullable2;
            RegExMaskType? nullable1;
            Lazy<EditorsGeneratorBase.Initializer> lazy = new Lazy<EditorsGeneratorBase.Initializer>(<>c.<>9__1_0 ??= () => EditorsGeneratorBase.Initializer.Default);
            if ((property == null) || (propertyType == null))
            {
                return lazy;
            }
            propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            DataColumnAttributes attributes = property.Attributes;
            PropertyDataType type = attributes.PropertyDataType();
            if (type == PropertyDataType.PhoneNumber)
            {
                nullable1 = 0;
            }
            else
            {
                nullable2 = null;
                nullable1 = nullable2;
            }
            RegExMaskType? regExMaskType = MaskInfo.GetRegExMaskType(attributes.Mask(), nullable1);
            if (IsNumericType(propertyType))
            {
                nullable2 = null;
                if (MaskInfo.GetRegExMaskType(attributes.Mask(), nullable2) != null)
                {
                    return generator.RegExMaskText(callGenerateMethod, property, GetMaskInfo(property, null, true, regExMaskType));
                }
                if (Net45Detector.IsNet45())
                {
                    Func<System.ComponentModel.DataAnnotations.RangeAttribute, System.ComponentModel.DataAnnotations.RangeAttribute> reader = <>c.<>9__1_1;
                    if (<>c.<>9__1_1 == null)
                    {
                        Func<System.ComponentModel.DataAnnotations.RangeAttribute, System.ComponentModel.DataAnnotations.RangeAttribute> local3 = <>c.<>9__1_1;
                        reader = <>c.<>9__1_1 = x => x;
                    }
                    System.ComponentModel.DataAnnotations.RangeAttribute attribute = attributes.ReadAttributeProperty<System.ComponentModel.DataAnnotations.RangeAttribute, System.ComponentModel.DataAnnotations.RangeAttribute>(reader, null).Value;
                    if (attribute != null)
                    {
                        return generator.Range(callGenerateMethod, property, GetNumericMask(property), attribute.Minimum, attribute.Maximum);
                    }
                }
                return generator.Numeric(callGenerateMethod, property, GetNumericMask(property));
            }
            if (propertyType == typeof(bool))
            {
                return generator.Check(callGenerateMethod, property);
            }
            if (type == PropertyDataType.Url)
            {
                return generator.Hyperlink(callGenerateMethod, property);
            }
            if (!(propertyType == typeof(string)))
            {
                return (!(propertyType == typeof(char)) ? (!(propertyType == typeof(System.DateTime)) ? ((!((propertyType == typeof(byte[])) & guessImageProperties) || !EditorsSource.IsImagePropertyName(property.Name)) ? (!propertyType.IsEnum ? (((!skipIEnumerableProperties || (string.IsNullOrEmpty(property.Name) || !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))) || property.Attributes.AutoGenerateField.GetValueOrDefault(false)) ? generator.Object(callGenerateMethod, property) : lazy) : generator.Enum(callGenerateMethod, property, propertyType)) : generator.Image(callGenerateMethod, property, false)) : generator.DateTime(callGenerateMethod, property)) : generator.Char(callGenerateMethod, property));
            }
            if (type == PropertyDataType.Password)
            {
                return generator.Password(callGenerateMethod, property);
            }
            if (type == PropertyDataType.MultilineText)
            {
                return generator.Text(callGenerateMethod, property, true);
            }
            if (type == PropertyDataType.ImageUrl)
            {
                return generator.Image(callGenerateMethod, property, true);
            }
            if (regExMaskType == null)
            {
                return generator.Text(callGenerateMethod, property, false);
            }
            string defaultMask = (type == PropertyDataType.PhoneNumber) ? EditorsSource.PhoneNumberMask : null;
            return generator.RegExMaskText(callGenerateMethod, property, GetMaskInfo(property, defaultMask, string.IsNullOrEmpty(defaultMask), regExMaskType));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorsGeneratorSelector.<>c <>9 = new EditorsGeneratorSelector.<>c();
            public static Func<EditorsGeneratorBase.Initializer> <>9__1_0;
            public static Func<System.ComponentModel.DataAnnotations.RangeAttribute, System.ComponentModel.DataAnnotations.RangeAttribute> <>9__1_1;
            public static Func<CommonEditorAttributeBase, CommonEditorAttributeBase> <>9__2_1;
            public static Func<CommonEditorAttributeBase> <>9__3_0;
            public static Func<CommonEditorAttributeBase, object> <>9__3_1;
            public static Func<CommonEditorAttributeBase, object> <>9__3_2;
            public static Func<FilterEditorAttribute, FilterEditorAttribute> <>9__4_1;
            public static Func<LookupBindingPropertiesAttribute, LookupBindingPropertiesAttribute> <>9__5_0;

            internal CommonEditorAttributeBase <GenerateEditorBasedEditorTemplates>b__2_1(CommonEditorAttributeBase y) => 
                y;

            internal FilterEditorAttribute <GenerateFilterEditor>b__4_1(FilterEditorAttribute x) => 
                x;

            internal LookupBindingPropertiesAttribute <GenerateLookUp>b__5_0(LookupBindingPropertiesAttribute x) => 
                x;

            internal CommonEditorAttributeBase <GetEditorAttributeTemplateKey>b__3_0() => 
                null;

            internal object <GetEditorAttributeTemplateKey>b__3_1(CommonEditorAttributeBase x) => 
                x.TemplateKey;

            internal object <GetEditorAttributeTemplateKey>b__3_2(CommonEditorAttributeBase x) => 
                x.TemplateKey;

            internal EditorsGeneratorBase.Initializer <SelectMethod>b__1_0() => 
                EditorsGeneratorBase.Initializer.Default;

            internal System.ComponentModel.DataAnnotations.RangeAttribute <SelectMethod>b__1_1(System.ComponentModel.DataAnnotations.RangeAttribute x) => 
                x;
        }
    }
}

