namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class PropertyMetadataBuilderBase<T, TProperty, TBuilder> : MemberMetadataBuilderBase<T, TBuilder, ClassMetadataBuilder<T>> where TBuilder: PropertyMetadataBuilderBase<T, TProperty, TBuilder>
    {
        internal PropertyMetadataBuilderBase(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }

        protected TBuilder DefaultEditorCore(object templateKey) => 
            base.AddOrModifyAttribute<DefaultEditorAttribute>(delegate (DefaultEditorAttribute x) {
                x.TemplateKey = templateKey;
            });

        protected TBuilder DisplayFormatStringCore(string dataFormatString, bool applyDisplayFormatInEditMode = false) => 
            DataAnnotationsAttributeHelper.SetDataFormatString<TBuilder>((TBuilder) this, dataFormatString, applyDisplayFormatInEditMode);

        protected TBuilder DoNotConvertEmptyStringToNullCore() => 
            DataAnnotationsAttributeHelper.SetConvertEmptyStringToNull<TBuilder>((TBuilder) this, false);

        protected static Func<TProperty, string> GetErrorMessageAccessor(Func<string> errorMessageAccessor) => 
            (errorMessageAccessor != null) ? x => errorMessageAccessor() : null;

        protected TBuilder GridEditorCore(object templateKey) => 
            base.AddOrModifyAttribute<GridEditorAttribute>(delegate (GridEditorAttribute x) {
                x.TemplateKey = templateKey;
            });

        internal TBuilder GroupName(string groupName, LayoutType layoutType)
        {
            int currentTableLayoutOrder;
            if (layoutType == LayoutType.DataForm)
            {
                currentTableLayoutOrder = base.parent.CurrentDataFormLayoutOrder;
                base.parent.CurrentDataFormLayoutOrder = currentTableLayoutOrder + 1;
                return base.AddOrReplaceAttribute<DataFormGroupAttribute>(new DataFormGroupAttribute(groupName, currentTableLayoutOrder));
            }
            if (layoutType != LayoutType.Table)
            {
                throw new NotSupportedException();
            }
            currentTableLayoutOrder = base.parent.CurrentTableLayoutOrder;
            base.parent.CurrentTableLayoutOrder = currentTableLayoutOrder + 1;
            return base.AddOrReplaceAttribute<TableGroupAttribute>(new TableGroupAttribute(groupName, currentTableLayoutOrder));
        }

        protected TBuilder HiddenCore(bool hidden = true) => 
            base.AddOrModifyAttribute<HiddenAttribute>(delegate (HiddenAttribute x) {
                x.Hidden = hidden;
            });

        protected TBuilder InitializerCore<TValue>(Func<TValue> createDelegate, string name = null)
        {
            Func<Type, string, Func<object>, InstanceInitializerAttribute> attributeFactory = <>c__9<T, TProperty, TBuilder, TValue>.<>9__9_0;
            if (<>c__9<T, TProperty, TBuilder, TValue>.<>9__9_0 == null)
            {
                Func<Type, string, Func<object>, InstanceInitializerAttribute> local1 = <>c__9<T, TProperty, TBuilder, TValue>.<>9__9_0;
                attributeFactory = <>c__9<T, TProperty, TBuilder, TValue>.<>9__9_0 = (t, n, c) => new InstanceInitializerAttribute(t, n, c);
            }
            return this.InitializerCore<TValue, InstanceInitializerAttribute>(createDelegate, name, attributeFactory);
        }

        internal TBuilder InitializerCore<TValue, TInstanceInitializerAttribute>(Func<TValue> createDelegate, string name, Func<Type, string, Func<object>, TInstanceInitializerAttribute> attributeFactory) where TInstanceInitializerAttribute: InstanceInitializerAttributeBase
        {
            string text1 = name;
            if (name == null)
            {
                string local1 = name;
                text1 = typeof(TValue).Name;
            }
            return this.AddAttribute(attributeFactory(typeof(TValue), text1, () => createDelegate()));
        }

        internal TBuilder InitializerCore<TKey, TValue, TNewItem, TInstanceInitializerAttribute>(Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TNewItem>> createDelegate, string name, Func<Type, string, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, TInstanceInitializerAttribute> attributeFactory) where TNewItem: TValue where TInstanceInitializerAttribute: InstanceInitializerAttributeBase
        {
            string text1 = name;
            if (name == null)
            {
                string local1 = name;
                text1 = typeof(TNewItem).Name;
            }
            return this.AddAttribute(attributeFactory(typeof(TNewItem), text1, delegate (ITypeDescriptorContext c, IDictionary<TKey, TValue> d) {
                KeyValuePair<TKey, TNewItem> pair = createDelegate(c, d);
                return new KeyValuePair<TKey, TValue>(pair.Key, (TValue) pair.Value);
            }));
        }

        internal TBuilder InitializerCore<TNewItem, TInstanceInitializerAttribute>(Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, TNewItem>> createDelegate, string name, Func<Type, string, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, object>>, TInstanceInitializerAttribute> attributeFactory) where TInstanceInitializerAttribute: InstanceInitializerAttributeBase
        {
            string text1 = name;
            if (name == null)
            {
                string local1 = name;
                text1 = typeof(TNewItem).Name;
            }
            return this.AddAttribute(attributeFactory(typeof(TNewItem), text1, delegate (ITypeDescriptorContext c, IDictionary d) {
                KeyValuePair<object, TNewItem> pair = createDelegate(c, d);
                return new KeyValuePair<object, object>(pair.Key, pair.Value);
            }));
        }

        protected TBuilder LayoutControlEditorCore(object templateKey) => 
            base.AddOrModifyAttribute<LayoutControlEditorAttribute>(delegate (LayoutControlEditorAttribute x) {
                x.TemplateKey = templateKey;
            });

        [Obsolete("Use the MatchesInstanceRule(Func<TProperty, T, bool> isValidFunction, Func<string> errorMessageAccessor = null) method instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected TBuilder MatchesInstanceRuleCore(Func<T, bool> isValidFunction, Func<string> errorMessageAccessor = null) => 
            this.AddOrReplaceAttribute<CustomInstanceValidationAttribute>(new CustomInstanceValidationAttribute(typeof(T), (value, instance) => isValidFunction((T) instance), (errorMessageAccessor == null) ? null : (x, y) => errorMessageAccessor()));

        protected TBuilder MatchesInstanceRuleCore(Func<TProperty, T, bool> isValidFunction, Func<TProperty, T, string> errorMessageAccessor)
        {
            DXValidationAttribute.ErrorMessageAccessorDelegate delegate2 = DXValidationAttribute.ErrorMessageAccessor<TProperty, T>(errorMessageAccessor);
            return base.AddAttribute(new CustomInstanceValidationAttribute(typeof(TProperty), (value, instance) => isValidFunction((TProperty) value, (T) instance), delegate2));
        }

        protected TBuilder MatchesRegularExpressionCore(string pattern, Func<TProperty, string> errorMessageAccessor)
        {
            Func<object, string> func = DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor);
            return base.AddOrReplaceAttribute<RegularExpressionAttribute>(new RegularExpressionAttribute(pattern, func));
        }

        protected TBuilder MatchesRuleCore(Func<TProperty, bool> isValidFunction, Func<TProperty, string> errorMessageAccessor)
        {
            Func<object, string> func = DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor);
            return base.AddAttribute(new CustomValidationAttribute(typeof(TProperty), x => isValidFunction((TProperty) x), func));
        }

        protected TBuilder MaxLengthCore(int maxLength, Func<TProperty, string> errorMessageAccessor)
        {
            Func<object, string> func = DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor);
            return base.AddOrReplaceAttribute<DXMaxLengthAttribute>(new DXMaxLengthAttribute(maxLength, func));
        }

        protected TBuilder MinLengthCore(int minLength, Func<TProperty, string> errorMessageAccessor)
        {
            Func<object, string> func = DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor);
            return base.AddOrReplaceAttribute<DXMinLengthAttribute>(new DXMinLengthAttribute(minLength, func));
        }

        protected TBuilder NotEditableCore() => 
            DataAnnotationsAttributeHelper.SetNotEditable<TBuilder>((TBuilder) this);

        protected TBuilder NullDisplayTextCore(string nullDisplayText) => 
            DataAnnotationsAttributeHelper.SetNullDisplayText<TBuilder>((TBuilder) this, nullDisplayText);

        protected TBuilder PropertyGridEditorCore(object templateKey) => 
            base.AddOrModifyAttribute<PropertyGridEditorAttribute>(delegate (PropertyGridEditorAttribute x) {
                x.TemplateKey = templateKey;
            });

        protected TBuilder ReadOnlyCore() => 
            DataAnnotationsAttributeHelper.SetReadonly<TBuilder>((TBuilder) this);

        protected TBuilder RequiredCore(Func<string> errorMessageAccessor) => 
            this.RequiredCore(false, errorMessageAccessor);

        protected TBuilder RequiredCore(bool allowEmptyStrings = false, Func<string> errorMessageAccessor = null) => 
            base.AddOrReplaceAttribute<DXRequiredAttribute>(new DXRequiredAttribute(allowEmptyStrings, errorMessageAccessor));

        protected TypeConverterBuilder<T, TProperty, TBuilder> TypeConverterCore() => 
            new TypeConverterBuilder<T, TProperty, TBuilder>((TBuilder) this);

        protected TBuilder TypeConverterCore<TConverter>() where TConverter: TypeConverter, new()
        {
            Action<TypeConverterWrapperAttribute> setAttributeValue = <>c__14<T, TProperty, TBuilder, TConverter>.<>9__14_0;
            if (<>c__14<T, TProperty, TBuilder, TConverter>.<>9__14_0 == null)
            {
                Action<TypeConverterWrapperAttribute> local1 = <>c__14<T, TProperty, TBuilder, TConverter>.<>9__14_0;
                setAttributeValue = <>c__14<T, TProperty, TBuilder, TConverter>.<>9__14_0 = delegate (TypeConverterWrapperAttribute x) {
                    x.BaseConverterType = typeof(TConverter);
                };
            }
            return this.AddOrModifyAttribute<TypeConverterWrapperAttribute>(setAttributeValue);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__14<TConverter> where TConverter: TypeConverter, new()
        {
            public static readonly PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__14<TConverter> <>9;
            public static Action<TypeConverterWrapperAttribute> <>9__14_0;

            static <>c__14()
            {
                PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__14<TConverter>.<>9 = new PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__14<TConverter>();
            }

            internal void <TypeConverterCore>b__14_0(TypeConverterWrapperAttribute x)
            {
                x.BaseConverterType = typeof(TConverter);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<TValue>
        {
            public static readonly PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__9<TValue> <>9;
            public static Func<Type, string, Func<object>, InstanceInitializerAttribute> <>9__9_0;

            static <>c__9()
            {
                PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__9<TValue>.<>9 = new PropertyMetadataBuilderBase<T, TProperty, TBuilder>.<>c__9<TValue>();
            }

            internal InstanceInitializerAttribute <InitializerCore>b__9_0(Type t, string n, Func<object> c) => 
                new InstanceInitializerAttribute(t, n, c);
        }
    }
}

