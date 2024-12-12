namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal static class BindingHelper
    {
        public static void ClearBinding(DependencyObject dObj, DependencyProperty property)
        {
            if (dObj != null)
            {
                BindingOperations.ClearBinding(dObj, property);
            }
        }

        public static BindingBase CloneBinding(BindingBase bindingBase, object source)
        {
            Binding binding = bindingBase as Binding;
            if (binding == null)
            {
                return null;
            }
            Binding binding1 = new Binding();
            binding1.Source = source;
            binding1.AsyncState = binding.AsyncState;
            binding1.BindingGroupName = binding.BindingGroupName;
            binding1.XPath = binding.XPath;
            binding1.IsAsync = binding.IsAsync;
            binding1.NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated;
            binding1.NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated;
            binding1.UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter;
            binding1.BindsDirectlyToSource = binding.BindsDirectlyToSource;
            binding1.Converter = binding.Converter;
            binding1.ConverterCulture = binding.ConverterCulture;
            binding1.ConverterParameter = binding.ConverterCulture;
            binding1.FallbackValue = binding.FallbackValue;
            binding1.Mode = binding.Mode;
            binding1.NotifyOnValidationError = binding.NotifyOnValidationError;
            binding1.Path = binding.Path;
            binding1.StringFormat = binding.StringFormat;
            binding1.TargetNullValue = binding.TargetNullValue;
            binding1.UpdateSourceTrigger = binding.UpdateSourceTrigger;
            binding1.ValidatesOnDataErrors = binding.ValidatesOnDataErrors;
            binding1.ValidatesOnExceptions = binding.ValidatesOnExceptions;
            Binding binding2 = binding1;
            foreach (ValidationRule rule in binding.ValidationRules)
            {
                binding2.ValidationRules.Add(rule);
            }
            return binding2;
        }

        public static bool IsDataBound(DependencyObject dObj, DependencyProperty property) => 
            BindingOperations.IsDataBound(dObj, property);

        public static BindingExpressionBase SetBinding(DependencyObject target, DependencyProperty property, object source, string path)
        {
            if (target == null)
            {
                return null;
            }
            Binding binding = new Binding(path);
            binding.Source = source;
            return BindingOperations.SetBinding(target, property, binding);
        }

        public static void SetBinding(DependencyObject target, DependencyProperty property, object source, DependencyProperty path = null, BindingMode mode = 1)
        {
            if (target != null)
            {
                path ??= property;
                Binding binding = new Binding();
                binding.Path = new PropertyPath(path);
                binding.Source = source;
                binding.Mode = mode;
                BindingOperations.SetBinding(target, property, binding);
            }
        }

        public static void SetBinding(DependencyObject target, DependencyProperty property, object source, DependencyProperty path, IValueConverter converter)
        {
            if (target != null)
            {
                path ??= property;
                Binding binding = new Binding();
                binding.Path = new PropertyPath(path);
                binding.Source = source;
                binding.Converter = converter;
                BindingOperations.SetBinding(target, property, binding);
            }
        }
    }
}

