namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    internal class CustomEditorPropertyLine : EditorPropertyLineBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CustomEditorPropertyLine(BaseEdit editor, string bindingMember, IValueConverter converter, object converterParameter, PropertyDescriptor property, object obj) : base(editor, property, obj)
        {
            Binding binding = new Binding("Value");
            binding.Source = this;
            binding.Converter = converter;
            binding.ConverterParameter = converterParameter;
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(editor, this.GetDependencyProperty(editor, bindingMember + "Property"), binding);
        }

        private DependencyProperty GetDependencyProperty(DependencyObject obj, string propName)
        {
            FieldInfo field = obj.GetType().GetField(propName, System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            return ((field != null) ? ((DependencyProperty) field.GetValue(obj)) : null);
        }

        protected override void OnValueSet()
        {
            base.OnValueSet();
            this.RaisePropertyChanged<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CustomEditorPropertyLine)), (MethodInfo) methodof(PropertyLine.get_Value)), new ParameterExpression[0]));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            this.RaisePropertyChanged<T>(this.PropertyChanged, property);
        }

        public override void RefreshContent()
        {
            base.RefreshContent();
            this.RaisePropertyChanged<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CustomEditorPropertyLine)), (MethodInfo) methodof(PropertyLine.get_Value)), new ParameterExpression[0]));
        }
    }
}

