namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DependencyPropertyRegistrator<T>
    {
        protected DependencyPropertyRegistrator()
        {
        }

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, null);

        private DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, PropertyMetadata metadata)
        {
            propertyField = sourceProperty.AddOwner(typeof(T), metadata);
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T> changedCallback = null) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, FrameworkPropertyMetadataOptions frameworkOptions) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, (Action<T>) null, null, new FrameworkPropertyMetadataOptions?(frameworkOptions)));

        public DependencyPropertyRegistrator<T> AddOwner<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions frameworkOptions) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, new FrameworkPropertyMetadataOptions?(frameworkOptions)));

        public DependencyPropertyRegistrator<T> AddOwnerFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T> changedCallback = null) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, getStorage, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwnerFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, getStorage, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwnerFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateFrameworkMetadata<T, TProperty>(defaultValue, getStorage, changedCallback));

        public DependencyPropertyRegistrator<T> AddOwnerFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, FrameworkPropertyMetadataOptions frameworkOptions) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, (Action<T>) null, null, new FrameworkPropertyMetadataOptions?(frameworkOptions)));

        public DependencyPropertyRegistrator<T> AddOwnerFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, DependencyProperty sourceProperty, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions frameworkOptions) => 
            this.AddOwner<TProperty>(property, out propertyField, sourceProperty, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, new FrameworkPropertyMetadataOptions?(frameworkOptions)));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner>(changedCallback));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner>(changedCallback));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(changedCallback));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback));

        private static PropertyMetadata CreateFrameworkMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, TProperty, TProperty> changedCallback) => 
            new FrameworkPropertyMetadata(defaultValue, DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback));

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner>(changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner>(changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadata<TOwner, TProperty>(TProperty defaultValue, Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions)
        {
            PropertyChangedCallback handler = DependencyPropertyRegistrator<T>.ToHandler<TOwner, TProperty>(getStorage, changedCallback);
            return DependencyPropertyRegistrator<T>.CreateMetadataCore<TOwner, TProperty>(defaultValue, handler, DependencyPropertyRegistrator<T>.ToCoerce<TOwner, TProperty>(coerceCallback), frameworkOptions);
        }

        private static PropertyMetadata CreateMetadataCore<TOwner, TProperty>(TProperty defaultValue, PropertyChangedCallback handler, CoerceValueCallback coerce, FrameworkPropertyMetadataOptions? frameworkOptions) => 
            (frameworkOptions == null) ? new PropertyMetadata(defaultValue, handler, coerce) : new FrameworkPropertyMetadata(defaultValue, frameworkOptions.Value, handler, coerce);

        public DependencyPropertyRegistrator<T> FixPropertyValue(DependencyProperty propertyField, object value)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(value, null, (d, o) => value));
            return (DependencyPropertyRegistrator<T>) this;
        }

        internal static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> property) => 
            (property.Body as MemberExpression).Member.Name;

        private static string GetPropertyNameFromMethod<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> method)
        {
            string name = (method.Body as MethodCallExpression).Method.Name;
            if (!name.StartsWith("Get"))
            {
                throw new ArgumentException();
            }
            return name.Substring(3);
        }

        public static DependencyPropertyRegistrator<T> New() => 
            new DependencyPropertyRegistrator<T>();

        public DependencyPropertyRegistrator<T> OverrideDefaultStyleKey()
        {
            DefaultStyleKeyHelper<T>.DefaultStyleKeyProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(typeof(T)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata(DependencyProperty propertyField, Action<T> changedCallback = null)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T>(changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata<TProperty>(DependencyProperty propertyField, Action<T, TProperty, TProperty> changedCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata<TProperty>(Expression<Func<T, TProperty>> getMethodName, DependencyProperty propertyField, Action<T, TProperty, TProperty> changedCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata<TProperty>(DependencyProperty propertyField, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T>(changedCallback), DependencyPropertyRegistrator<T>.ToCoerce<T, TProperty>(coerceCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata<TProperty>(DependencyProperty propertyField, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(changedCallback), DependencyPropertyRegistrator<T>.ToCoerce<T, TProperty>(coerceCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata(DependencyProperty propertyField, object defaultValue, Action<T> changedCallback = null, FrameworkPropertyMetadataOptions frameworkOptions = 0)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(defaultValue, frameworkOptions, DependencyPropertyRegistrator<T>.ToHandler<T>(changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadata<TProperty>(DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions frameworkOptions = 0)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(defaultValue, frameworkOptions, DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, Action<T> changedCallback = null)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, Action<T, TProperty, TProperty> changedCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(Expression<Func<T, TProperty>> getMethodName, Func<T, DPValueStorage<TProperty>> getStorage, DependencyProperty propertyField, Action<T, TProperty, TProperty> changedCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback), DependencyPropertyRegistrator<T>.ToCoerce<T, TProperty>(coerceCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback), DependencyPropertyRegistrator<T>.ToCoerce<T, TProperty>(coerceCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, object defaultValue, Action<T> changedCallback = null, FrameworkPropertyMetadataOptions frameworkOptions = 0)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(defaultValue, frameworkOptions, DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> OverrideMetadataFast<TProperty>(DependencyProperty propertyField, Func<T, DPValueStorage<TProperty>> getStorage, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions frameworkOptions = 0)
        {
            propertyField.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(defaultValue, frameworkOptions, DependencyPropertyRegistrator<T>.ToHandler<T, TProperty>(getStorage, changedCallback)));
            return (DependencyPropertyRegistrator<T>) this;
        }

        private DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, PropertyMetadata metadata)
        {
            propertyField = DependencyProperty.Register(DependencyPropertyRegistrator<T>.GetPropertyName<TProperty>(property), typeof(TProperty), typeof(T), metadata);
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, (Action<T>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, (Action<T>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> Register<TProperty>(Expression<Func<T, TProperty>> property, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        private DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, PropertyMetadata metadata) where TOwner: DependencyObject
        {
            propertyField = DependencyProperty.RegisterAttached(DependencyPropertyRegistrator<T>.GetPropertyNameFromMethod<TOwner, TProperty>(getMethodName), typeof(TProperty), typeof(T), metadata);
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, (Action<TOwner>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, (Action<TOwner>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        private DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, PropertyMetadata metadata) where TOwner: DependencyObject
        {
            propertyFieldKey = DependencyProperty.RegisterAttachedReadOnly(DependencyPropertyRegistrator<T>.GetPropertyNameFromMethod<TOwner, TProperty>(getMethodName), typeof(TProperty), typeof(T), metadata);
            propertyField = propertyFieldKey.DependencyProperty;
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, (Action<TOwner>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, (Action<TOwner>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, (Action<T>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, (Action<T>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFast<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.Register<TProperty>(property, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, (Action<TOwner>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, (Action<TOwner>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttached<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttached<TOwner, TProperty>(getMethodName, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, (Action<TOwner>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, (Action<TOwner>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastAttachedReadOnly<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> getMethodName, Func<TOwner, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<TOwner, TProperty, TProperty> changedCallback, Func<TOwner, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) where TOwner: DependencyObject => 
            this.RegisterAttachedReadOnly<TOwner, TProperty>(getMethodName, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<TOwner, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, (Action<T>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, (Action<T>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterFastReadOnly<TProperty>(Expression<Func<T, TProperty>> property, Func<T, DPValueStorage<TProperty>> getStorage, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, getStorage, changedCallback, coerceCallback, frameworkOptions));

        private DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, PropertyMetadata metadata)
        {
            propertyFieldKey = DependencyProperty.RegisterReadOnly(DependencyPropertyRegistrator<T>.GetPropertyName<TProperty>(property), typeof(TProperty), typeof(T), metadata);
            propertyField = propertyFieldKey.DependencyProperty;
            return (DependencyPropertyRegistrator<T>) this;
        }

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, (Action<T>) null, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, null, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, (Action<T>) null, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, DependencyPropertyChangedEventArgs> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        public DependencyPropertyRegistrator<T> RegisterReadOnly<TProperty>(Expression<Func<T, TProperty>> property, out DependencyPropertyKey propertyFieldKey, out DependencyProperty propertyField, TProperty defaultValue, Action<T, TProperty, TProperty> changedCallback, Func<T, TProperty, TProperty> coerceCallback, FrameworkPropertyMetadataOptions? frameworkOptions = new FrameworkPropertyMetadataOptions?()) => 
            this.RegisterReadOnly<TProperty>(property, out propertyFieldKey, out propertyField, DependencyPropertyRegistrator<T>.CreateMetadata<T, TProperty>(defaultValue, changedCallback, coerceCallback, frameworkOptions));

        private static void SetStorageValue<TOwner, TProperty>(DependencyObject d, object newValue, Func<TOwner, DPValueStorage<TProperty>> getStorage)
        {
            DPValueStorage<TProperty> storage = getStorage((TOwner) d);
            if (storage != null)
            {
                storage.SetValue(newValue);
            }
        }

        private static CoerceValueCallback ToCoerce<TOwner, TProperty>(Func<TOwner, TProperty, TProperty> coerceCallback) => 
            (coerceCallback != null) ? ((CoerceValueCallback) ((d, o) => coerceCallback((TOwner) d, (TProperty) o))) : null;

        private static PropertyChangedCallback ToHandler<TOwner>(Action<TOwner> changedCallback) => 
            delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((Action<TOwner>) changedCallback).Do<Action<TOwner>>(x => x((TOwner) d));
            };

        private static PropertyChangedCallback ToHandler<TOwner>(Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback) => 
            delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((Action<TOwner, DependencyPropertyChangedEventArgs>) changedCallback).Do<Action<TOwner, DependencyPropertyChangedEventArgs>>(x => x((TOwner) d, e));
            };

        private static PropertyChangedCallback ToHandler<TOwner, TProperty>(Action<TOwner, TProperty, TProperty> changedCallback) => 
            delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((Action<TOwner, TProperty, TProperty>) changedCallback).Do<Action<TOwner, TProperty, TProperty>>(x => x((TOwner) d, (TProperty) e.OldValue, (TProperty) e.NewValue));
            };

        private static PropertyChangedCallback ToHandler<TOwner, TProperty>(Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner> changedCallback) => 
            (changedCallback != null) ? delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
                changedCallback((TOwner) d);
            } : delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
            };

        private static PropertyChangedCallback ToHandler<TOwner, TProperty>(Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, DependencyPropertyChangedEventArgs> changedCallback) => 
            (changedCallback != null) ? delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
                changedCallback((TOwner) d, e);
            } : delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
            };

        private static PropertyChangedCallback ToHandler<TOwner, TProperty>(Func<TOwner, DPValueStorage<TProperty>> getStorage, Action<TOwner, TProperty, TProperty> changedCallback) => 
            (changedCallback != null) ? delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
                changedCallback((TOwner) d, (TProperty) e.OldValue, (TProperty) e.NewValue);
            } : delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                DependencyPropertyRegistrator<T>.SetStorageValue<TOwner, TProperty>(d, e.NewValue, getStorage);
            };

        private class DefaultStyleKeyHelper : FrameworkElement
        {
            public static DependencyProperty DefaultStyleKeyProperty =>
                FrameworkElement.DefaultStyleKeyProperty;
        }
    }
}

