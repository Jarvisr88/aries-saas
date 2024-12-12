namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Linq.Expressions;

    public class MetadataBuilder<T> : ClassMetadataBuilder<T>
    {
        public CommandMetadataBuilder<T> Command(Expression<Func<T, ICommand>> propertyExpression) => 
            base.CommandCore(propertyExpression);

        public CommandMethodMetadataBuilder<T> CommandFromMethod(Expression<Action<T>> methodExpression) => 
            base.CommandFromMethodInternal(methodExpression).AddOrModifyAttribute<CommandAttribute>(null);

        public AsyncCommandMethodMetadataBuilder<T> CommandFromMethod(Expression<Func<T, Task>> asyncMethodExpression) => 
            base.CommandFromAsyncMethodInternal(asyncMethodExpression).AddOrModifyAttribute<CommandAttribute>(null);

        public DataFormLayoutBuilder<T, MetadataBuilder<T>> DataFormLayout() => 
            new DataFormLayoutBuilder<T, MetadataBuilder<T>>(this);

        public MetadataBuilder<T> DisplayName(string name) => 
            DisplayNameCore<MetadataBuilder<T>>((MetadataBuilder<T>) this, name);

        public GroupBuilder<T, MetadataBuilder<T>> Group(string groupName) => 
            new GroupBuilder<T, MetadataBuilder<T>>((MetadataBuilder<T>) this, groupName);

        public PropertyMetadataBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            base.PropertyCore<TProperty>(propertyExpression);

        public PropertyMetadataBuilder<T, TProperty> Property<TProperty>(string propertyName) => 
            base.PropertyCore<TProperty>(propertyName);

        public TableGroupContainerLayoutBuilder<T> TableLayout() => 
            base.TableLayoutCore();

        public ToolBarLayoutBuilder<T> ToolBarLayout() => 
            base.ToolBarLayoutCore();
    }
}

