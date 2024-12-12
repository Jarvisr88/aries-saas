namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class ClassMetadataBuilder<T> : MetadataBuilderBase<T, ClassMetadataBuilder<T>>
    {
        protected ClassMetadataBuilder()
        {
        }

        protected internal CommandMetadataBuilder<T> CommandCore(Expression<Func<T, ICommand>> propertyExpression) => 
            this.GetBuilder<ICommand, CommandMetadataBuilder<T>>(propertyExpression, x => new CommandMetadataBuilder<T>(x, (ClassMetadataBuilder<T>) this));

        internal AsyncCommandMethodMetadataBuilder<T> CommandFromAsyncMethodInternal(Expression<Func<T, Task>> asyncMethodExpression)
        {
            string methodName = ExpressionHelper.GetArgumentFunctionStrict<T, Task>(asyncMethodExpression).Name;
            return base.GetBuilder<AsyncCommandMethodMetadataBuilder<T>>(methodName, x => new AsyncCommandMethodMetadataBuilder<T>(x, (ClassMetadataBuilder<T>) this, methodName));
        }

        internal CommandMethodMetadataBuilder<T> CommandFromMethodInternal(Expression<Action<T>> methodExpression)
        {
            string methodName = ExpressionHelper.GetArgumentMethodStrict<T>(methodExpression).Name;
            return base.GetBuilder<CommandMethodMetadataBuilder<T>>(methodName, x => new CommandMethodMetadataBuilder<T>(x, (ClassMetadataBuilder<T>) this, methodName));
        }

        protected static TBuilder DisplayNameCore<TBuilder>(TBuilder builder, string name) where TBuilder: ClassMetadataBuilder<T>
        {
            builder.AddOrReplaceAttribute<DisplayNameAttribute>(new DisplayNameAttribute(name));
            return builder;
        }

        protected internal FilteringPropertyMetadataBuilder<T, TProperty> FilteringPropertyCore<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            this.GetBuilder<TProperty, FilteringPropertyMetadataBuilder<T, TProperty>>(propertyExpression, x => new FilteringPropertyMetadataBuilder<T, TProperty>(x, (ClassMetadataBuilder<T>) this));

        private TBuilder GetBuilder<TProperty, TBuilder>(Expression<Func<T, TProperty>> propertyExpression, Func<MemberMetadataStorage, TBuilder> createBuilderCallBack) where TBuilder: IPropertyMetadataBuilder => 
            base.GetBuilder<TBuilder>(GetPropertyName<TProperty>(propertyExpression), createBuilderCallBack);

        protected internal PropertyMetadataBuilder<T, TProperty> PropertyCore<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            this.GetBuilder<TProperty, PropertyMetadataBuilder<T, TProperty>>(propertyExpression, x => new PropertyMetadataBuilder<T, TProperty>(x, (ClassMetadataBuilder<T>) this));

        protected internal PropertyMetadataBuilder<T, TProperty> PropertyCore<TProperty>(string memberName) => 
            base.GetBuilder<PropertyMetadataBuilder<T, TProperty>>(memberName, x => new PropertyMetadataBuilder<T, TProperty>(x, (ClassMetadataBuilder<T>) this));

        protected TableGroupContainerLayoutBuilder<T> TableLayoutCore() => 
            new TableGroupContainerLayoutBuilder<T>((ClassMetadataBuilder<T>) this);

        protected ToolBarLayoutBuilder<T> ToolBarLayoutCore() => 
            new ToolBarLayoutBuilder<T>((ClassMetadataBuilder<T>) this);

        internal int CurrentDisplayAttributeOrder { get; set; }

        internal int CurrentDataFormLayoutOrder { get; set; }

        internal int CurrentTableLayoutOrder { get; set; }

        internal int CurrentToolbarLayoutOrder { get; set; }

        internal int CurrentContextMenuLayoutOrder { get; set; }
    }
}

