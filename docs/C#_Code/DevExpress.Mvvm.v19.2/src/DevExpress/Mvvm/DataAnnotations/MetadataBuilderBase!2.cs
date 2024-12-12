namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class MetadataBuilderBase<T, TMetadataBuilder> : IAttributesProvider, IAttributeBuilderInternal, IAttributeBuilderInternal<TMetadataBuilder> where TMetadataBuilder: MetadataBuilderBase<T, TMetadataBuilder>
    {
        private Dictionary<string, MemberMetadataStorage> storages;

        protected MetadataBuilderBase()
        {
            this.storages = new Dictionary<string, MemberMetadataStorage>();
        }

        protected internal TMetadataBuilder AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue) where TAttribute: Attribute, new()
        {
            this.GetBuilder<IPropertyMetadataBuilder>(null, delegate (MemberMetadataStorage x) {
                x.AddOrModifyAttribute<TAttribute>(setAttributeValue);
                return null;
            });
            return (TMetadataBuilder) this;
        }

        protected internal TMetadataBuilder AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute
        {
            this.GetBuilder<IPropertyMetadataBuilder>(null, delegate (MemberMetadataStorage x) {
                x.AddOrReplaceAttribute<TAttribute>(attribute);
                return null;
            });
            return (TMetadataBuilder) this;
        }

        public TMetadataBuilder DefaultEditor(object templateKey) => 
            ((IAttributeBuilderInternal<TMetadataBuilder>) this).AddOrModifyAttribute<DefaultEditorAttribute>(delegate (DefaultEditorAttribute a) {
                a.TemplateKey = templateKey;
            });

        void IAttributeBuilderInternal.AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue) where TAttribute: Attribute, new()
        {
            this.AddOrModifyAttribute<TAttribute>(setAttributeValue);
        }

        void IAttributeBuilderInternal.AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute
        {
            this.AddOrReplaceAttribute<TAttribute>(attribute);
        }

        TMetadataBuilder IAttributeBuilderInternal<TMetadataBuilder>.AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue) where TAttribute: Attribute, new() => 
            this.AddOrModifyAttribute<TAttribute>(setAttributeValue);

        TMetadataBuilder IAttributeBuilderInternal<TMetadataBuilder>.AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute => 
            this.AddOrReplaceAttribute<TAttribute>(attribute);

        IEnumerable<Attribute> IAttributesProvider.GetAttributes(string propertyName)
        {
            MemberMetadataStorage storage;
            string key = propertyName;
            if (propertyName == null)
            {
                string local1 = propertyName;
                key = string.Empty;
            }
            this.storages.TryGetValue(key, out storage);
            return storage?.GetAttributes();
        }

        internal TBuilder GetBuilder<TBuilder>(string memberName, Func<MemberMetadataStorage, TBuilder> createBuilderCallBack) where TBuilder: IPropertyMetadataBuilder
        {
            string key = memberName;
            if (memberName == null)
            {
                string local1 = memberName;
                key = string.Empty;
            }
            MemberMetadataStorage arg = this.storages.GetOrAdd<string, MemberMetadataStorage>(key, <>c__2<T, TMetadataBuilder, TBuilder>.<>9__2_0 ??= () => new MemberMetadataStorage());
            return createBuilderCallBack(arg);
        }

        internal static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression) => 
            ExpressionHelper.GetArgumentPropertyStrict<T, TProperty>(expression).Name;

        public TMetadataBuilder GridEditor(object templateKey) => 
            ((IAttributeBuilderInternal<TMetadataBuilder>) this).AddOrModifyAttribute<GridEditorAttribute>(delegate (GridEditorAttribute a) {
                a.TemplateKey = templateKey;
            });

        public TMetadataBuilder LayoutControlEditor(object templateKey) => 
            ((IAttributeBuilderInternal<TMetadataBuilder>) this).AddOrModifyAttribute<LayoutControlEditorAttribute>(delegate (LayoutControlEditorAttribute a) {
                a.TemplateKey = templateKey;
            });

        public TMetadataBuilder PropertyGridEditor(object templateKey) => 
            ((IAttributeBuilderInternal<TMetadataBuilder>) this).AddOrModifyAttribute<PropertyGridEditorAttribute>(delegate (PropertyGridEditorAttribute a) {
                a.TemplateKey = templateKey;
            });

        public ClassTypeConverterBuilder<T, TMetadataBuilder> TypeConverter() => 
            new ClassTypeConverterBuilder<T, TMetadataBuilder>((TMetadataBuilder) this);

        public TMetadataBuilder TypeConverter<TConverter>() where TConverter: System.ComponentModel.TypeConverter, new()
        {
            Action<TypeConverterWrapperAttribute> setAttributeValue = <>c__11<T, TMetadataBuilder, TConverter>.<>9__11_0;
            if (<>c__11<T, TMetadataBuilder, TConverter>.<>9__11_0 == null)
            {
                Action<TypeConverterWrapperAttribute> local1 = <>c__11<T, TMetadataBuilder, TConverter>.<>9__11_0;
                setAttributeValue = <>c__11<T, TMetadataBuilder, TConverter>.<>9__11_0 = delegate (TypeConverterWrapperAttribute x) {
                    x.BaseConverterType = typeof(TConverter);
                };
            }
            return this.AddOrModifyAttribute<TypeConverterWrapperAttribute>(setAttributeValue);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__11<TConverter> where TConverter: TypeConverter, new()
        {
            public static readonly MetadataBuilderBase<T, TMetadataBuilder>.<>c__11<TConverter> <>9;
            public static Action<TypeConverterWrapperAttribute> <>9__11_0;

            static <>c__11()
            {
                MetadataBuilderBase<T, TMetadataBuilder>.<>c__11<TConverter>.<>9 = new MetadataBuilderBase<T, TMetadataBuilder>.<>c__11<TConverter>();
            }

            internal void <TypeConverter>b__11_0(TypeConverterWrapperAttribute x)
            {
                x.BaseConverterType = typeof(TConverter);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<TBuilder> where TBuilder: IPropertyMetadataBuilder
        {
            public static readonly MetadataBuilderBase<T, TMetadataBuilder>.<>c__2<TBuilder> <>9;
            public static Func<MemberMetadataStorage> <>9__2_0;

            static <>c__2()
            {
                MetadataBuilderBase<T, TMetadataBuilder>.<>c__2<TBuilder>.<>9 = new MetadataBuilderBase<T, TMetadataBuilder>.<>c__2<TBuilder>();
            }

            internal MemberMetadataStorage <GetBuilder>b__2_0() => 
                new MemberMetadataStorage();
        }
    }
}

