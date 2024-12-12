namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class SelectorWrapper : DependencyObject
    {
        private static readonly DependencyPropertyKey SelectorWrapperPropertyKey;
        public static readonly DependencyProperty SelectorWrapperProperty;
        private static readonly Dictionary<Type, Func<object, SelectorWrapper>> Factory = new Dictionary<Type, Func<object, SelectorWrapper>>();

        static SelectorWrapper()
        {
            Type ownerType = typeof(SelectorWrapper);
            SelectorWrapperPropertyKey = DependencyProperty.RegisterAttachedReadOnly("SelectorWrapper", typeof(SelectorWrapper), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            SelectorWrapperProperty = SelectorWrapperPropertyKey.DependencyProperty;
        }

        protected SelectorWrapper(FrameworkElement element)
        {
            this.Selector = element;
        }

        public abstract void ClearEditor();
        public static SelectorWrapper Create(FrameworkElement element)
        {
            Type c = element.GetType();
            if (typeof(FrameworkElement).IsAssignableFrom(c))
            {
                for (Type type2 = c; (type2 != null) && (type2 != typeof(FrameworkElement)); type2 = type2.BaseType)
                {
                    Func<object, SelectorWrapper> func;
                    if (Factory.TryGetValue(type2, out func))
                    {
                        return func(element);
                    }
                }
            }
            return null;
        }

        public abstract IEnumerable GetSelectedItems();
        public static SelectorWrapper GetSelectorWrapper(DependencyObject d) => 
            (SelectorWrapper) d.GetValue(SelectorWrapperProperty);

        public void InvalidateMeasure()
        {
            this.Selector.InvalidateMeasure();
        }

        public abstract void NotifyMouseDown(object sender, MouseButtonEventArgs e);
        public abstract void NotifyMouseUp(object sender, MouseButtonEventArgs e);
        public abstract bool ProcessKeyDown(KeyEventArgs e);
        public abstract bool ProcessPreviewKeyDown(KeyEventArgs e);
        public static void Register(Type type, Func<object, SelectorWrapper> createHandler)
        {
            Factory[type] = createHandler;
        }

        protected internal static void SetSelectorWrapper(DependencyObject d, SelectorWrapper value)
        {
            d.SetValue(SelectorWrapperPropertyKey, value);
        }

        public abstract void SetupEditor();
        public abstract void SyncValuesWithOwnerEdit(bool resetTotals);
        public abstract void SyncWithOwnerEdit(bool syncDataSource);

        public FrameworkElement Selector { get; private set; }

        public abstract object SelectedItem { get; }

        public bool PostPopupValue { get; protected set; }
    }
}

