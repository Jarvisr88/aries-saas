namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("Value")]
    public class UpdateAction : BarManagerControllerActionBase
    {
        private static readonly ReflectionHelper rHelper;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty PropertyProperty;
        public static readonly DependencyProperty PropertyNameProperty;
        public static readonly DependencyProperty ElementNameProperty;
        public static readonly DependencyProperty ElementProperty;

        static UpdateAction();
        protected override void ExecuteCore(DependencyObject associatedObject);
        protected virtual object GetConvertedValue(object obj);
        public override object GetObjectCore();
        private Type GetPropertyType(object obj);
        protected virtual Action<object, object> GetSetter(object element);

        public DependencyProperty Property { get; set; }

        public string PropertyName { get; set; }

        public object Value { get; set; }

        public BindingBase ValueBinding { get; set; }

        public object Element { get; set; }

        public string ElementName { get; set; }
    }
}

