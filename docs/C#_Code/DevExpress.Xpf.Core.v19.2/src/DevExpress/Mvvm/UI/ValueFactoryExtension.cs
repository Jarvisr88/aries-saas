namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("ValueTemplate")]
    public class ValueFactoryExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            object targetObject = service?.TargetObject;
            object targetProperty = service?.TargetProperty;
            Binding binding1 = new Binding();
            binding1.RelativeSource = RelativeSource.Self;
            binding1.Converter = new ValueFactoryConverter(this.ValueTemplate);
            Binding binding = binding1;
            if (targetObject is Setter)
            {
                return binding;
            }
            if (targetProperty is DependencyProperty)
            {
                return binding.ProvideValue(serviceProvider);
            }
            Type dataType = this.ValueTemplate.DataType as Type;
            Type local8 = dataType;
            if (dataType == null)
            {
                Type local1 = dataType;
                Type local3 = (targetProperty as PropertyInfo).With<PropertyInfo, Type>(<>c.<>9__4_0 ??= x => x.PropertyType);
                local8 = local3;
                if (local3 == null)
                {
                    Type local4 = local3;
                    Type local6 = (targetProperty as PropertyDescriptor).With<PropertyDescriptor, Type>(<>c.<>9__4_1 ??= x => x.PropertyType);
                    local8 = local6;
                    if (local6 == null)
                    {
                        Type local7 = local6;
                        local8 = typeof(object);
                    }
                }
            }
            Type valueType = local8;
            return TemplateHelper.LoadFromTemplate(this.ValueTemplate, valueType, null);
        }

        public DataTemplate ValueTemplate { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ValueFactoryExtension.<>c <>9 = new ValueFactoryExtension.<>c();
            public static Func<PropertyInfo, Type> <>9__4_0;
            public static Func<PropertyDescriptor, Type> <>9__4_1;

            internal Type <ProvideValue>b__4_0(PropertyInfo x) => 
                x.PropertyType;

            internal Type <ProvideValue>b__4_1(PropertyDescriptor x) => 
                x.PropertyType;
        }

        private class ValueFactoryConverter : IValueConverter
        {
            private readonly DataTemplate valueTemplate;

            public ValueFactoryConverter(DataTemplate valueTemplate)
            {
                this.valueTemplate = valueTemplate;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                Type dataType = this.valueTemplate.DataType as Type;
                Type valueType = dataType;
                if (dataType == null)
                {
                    Type local1 = dataType;
                    valueType = targetType;
                }
                return TemplateHelper.LoadFromTemplate(this.valueTemplate, valueType, null);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}

