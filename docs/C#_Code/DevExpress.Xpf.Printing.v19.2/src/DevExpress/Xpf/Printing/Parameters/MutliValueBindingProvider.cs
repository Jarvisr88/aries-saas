namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class MutliValueBindingProvider : Behavior<ComboBoxEdit>, IValueConverter
    {
        public static readonly DependencyProperty SourceTypeProperty = DependencyProperty.Register("SourceType", typeof(Type), typeof(MutliValueBindingProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof(bool), typeof(MutliValueBindingProvider), new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ArrayList array = new ArrayList();
            (value as IEnumerable<object>).Do<IEnumerable<object>>(delegate (IEnumerable<object> e) {
                Action<object> <>9__1;
                Action<object> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<object> local1 = <>9__1;
                    action = <>9__1 = delegate (object x) {
                        if (x.GetType() == this.SourceType)
                        {
                            array.Add(x);
                        }
                        else
                        {
                            object obj2 = ParameterHelper.ConvertFrom(x, this.SourceType, null);
                            if (obj2 != null)
                            {
                                array.Add(obj2);
                            }
                        }
                    };
                }
                e.ForEach<object>(action);
            });
            Array array = array.ToArray(this.SourceType);
            return (((array.Length != 0) || !this.AllowNull) ? array : null);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Binding binding1 = new Binding("Value");
            binding1.Mode = BindingMode.TwoWay;
            binding1.Converter = this;
            binding1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding1.ValidatesOnDataErrors = true;
            Binding binding = binding1;
            BindingOperations.SetBinding(base.AssociatedObject, BaseEdit.EditValueProperty, binding);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            BindingOperations.ClearBinding(base.AssociatedObject, BaseEdit.EditValueProperty);
        }

        public Type SourceType
        {
            get => 
                (Type) base.GetValue(SourceTypeProperty);
            set => 
                base.SetValue(SourceTypeProperty, value);
        }

        public bool AllowNull
        {
            get => 
                (bool) base.GetValue(AllowNullProperty);
            set => 
                base.SetValue(AllowNullProperty, value);
        }
    }
}

