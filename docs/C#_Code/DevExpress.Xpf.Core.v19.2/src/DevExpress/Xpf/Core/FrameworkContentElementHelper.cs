namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class FrameworkContentElementHelper
    {
        private static Action<FrameworkContentElement, DependencyPropertyChangedEventArgs> baseOnPropertyChanged;
        private static Func<FrameworkElement, FrameworkTemplate> getTemplateInternal;
        private static Func<DependencyPropertyChangedEventArgs, bool> getIsAValueChange;
        private static Func<DependencyPropertyChangedEventArgs, bool> getIsASubPropertyChange;
        private static Action<FrameworkContentElement, DependencyObject> setTemplatedParent;
        private static Func<FrameworkContentElement, int> getTemplateChildIndex;
        private static Action<FrameworkContentElement, int> setTemplateChildIndex;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static DependencyProperty dskProperty;

        static FrameworkContentElementHelper()
        {
            int? parametersCount = null;
            baseOnPropertyChanged = ReflectionHelper.CreateInstanceMethodHandler<FrameworkContentElement, Action<FrameworkContentElement, DependencyPropertyChangedEventArgs>>(null, "OnPropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, false);
            DependencyPropertyChangedEventArgs entity = new DependencyPropertyChangedEventArgs();
            parametersCount = null;
            getIsAValueChange = ReflectionHelper.CreateInstanceMethodHandler<DependencyPropertyChangedEventArgs, Func<DependencyPropertyChangedEventArgs, bool>>(entity, "get_IsAValueChange", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            entity = new DependencyPropertyChangedEventArgs();
            parametersCount = null;
            getIsASubPropertyChange = ReflectionHelper.CreateInstanceMethodHandler<DependencyPropertyChangedEventArgs, Func<DependencyPropertyChangedEventArgs, bool>>(entity, "get_IsASubPropertyChange", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            dskProperty = typeof(FrameworkContentElement).GetField("DefaultStyleKeyProperty", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as DependencyProperty;
            parametersCount = null;
            getTemplateInternal = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, Func<FrameworkElement, FrameworkTemplate>>(null, "get_TemplateInternal", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            setTemplatedParent = ReflectionHelper.CreateFieldSetter<FrameworkContentElement, DependencyObject>(typeof(FrameworkContentElement), "_templatedParent", BindingFlags.NonPublic | BindingFlags.Instance);
            parametersCount = null;
            getTemplateChildIndex = ReflectionHelper.CreateInstanceMethodHandler<FrameworkContentElement, Func<FrameworkContentElement, int>>(null, "get_TemplateChildIndex", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            parametersCount = null;
            setTemplateChildIndex = ReflectionHelper.CreateInstanceMethodHandler<FrameworkContentElement, Action<FrameworkContentElement, int>>(null, "set_TemplateChildIndex", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
        }

        public static void SafeOnPropertyChanged(this FrameworkContentElement element, DependencyPropertyChangedEventArgs e)
        {
            bool flag = false;
            int num = -1;
            FrameworkElement arg = null;
            if (getIsAValueChange(e) || getIsASubPropertyChange(e))
            {
                DependencyProperty objA = e.Property;
                if (!ReferenceEquals(objA, FrameworkContentElement.StyleProperty) && !ReferenceEquals(objA, dskProperty))
                {
                    arg = element.TemplatedParent as FrameworkElement;
                    if ((arg != null) && (getTemplateInternal(arg) == null))
                    {
                        flag = true;
                        setTemplatedParent(element, null);
                        num = getTemplateChildIndex(element);
                        setTemplateChildIndex(element, -1);
                    }
                }
            }
            baseOnPropertyChanged(element, e);
            if (flag)
            {
                setTemplateChildIndex(element, num);
                setTemplatedParent(element, arg);
            }
        }
    }
}

