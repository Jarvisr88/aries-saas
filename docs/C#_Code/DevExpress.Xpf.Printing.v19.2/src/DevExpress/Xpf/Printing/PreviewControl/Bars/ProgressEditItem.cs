namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class ProgressEditItem : BarEditItem
    {
        public static readonly DependencyProperty ProgressSettingsProperty;

        static ProgressEditItem()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ProgressEditItem), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ProgressSettingsProperty = DependencyPropertyRegistrator.Register<ProgressEditItem, IProgressSettings>(System.Linq.Expressions.Expression.Lambda<Func<ProgressEditItem, IProgressSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ProgressEditItem.get_ProgressSettings)), parameters), null, (d, o, n) => d.OnProgressSettingsChanged(n));
        }

        public ProgressEditItem()
        {
            base.DefaultStyleKey = typeof(ProgressEditItem);
            base.EditSettings = new ProgressBarEditSettings();
        }

        private void OnProgressSettingsChanged(IProgressSettings settings)
        {
            BindingOperations.ClearBinding(base.EditSettings, BaseEditSettings.StyleSettingsProperty);
            Binding binding1 = new Binding("ProgressSettings.ProgressType");
            binding1.Source = this;
            binding1.Converter = new ProgressTypeToEditSettingsConverter();
            binding1.FallbackValue = new ProgressBarStyleSettings();
            Binding binding = binding1;
            BindingOperations.SetBinding(base.EditSettings, BaseEditSettings.StyleSettingsProperty, binding);
        }

        public IProgressSettings ProgressSettings
        {
            get => 
                (IProgressSettings) base.GetValue(ProgressSettingsProperty);
            set => 
                base.SetValue(ProgressSettingsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProgressEditItem.<>c <>9 = new ProgressEditItem.<>c();

            internal void <.cctor>b__4_0(ProgressEditItem d, IProgressSettings o, IProgressSettings n)
            {
                d.OnProgressSettingsChanged(n);
            }
        }
    }
}

