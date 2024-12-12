namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    internal abstract class FilterUIElement<TUIElementID> : LocalizableUIElement<TUIElementID>, INotifyPropertyChanged, IServiceProvider where TUIElementID: struct
    {
        protected readonly Func<IServiceProvider> getServiceProvider;
        [CompilerGenerated]
        private PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            [CompilerGenerated] add
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand + value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand - value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        protected FilterUIElement(TUIElementID id, Func<IServiceProvider> getServiceProvider = null) : base(id)
        {
            this.getServiceProvider = getServiceProvider;
        }

        protected TService GetService<TService>() where TService: class
        {
            Func<IServiceProvider, TService> get = <>c__2<TUIElementID, TService>.<>9__2_0;
            if (<>c__2<TUIElementID, TService>.<>9__2_0 == null)
            {
                Func<IServiceProvider, TService> local1 = <>c__2<TUIElementID, TService>.<>9__2_0;
                get = <>c__2<TUIElementID, TService>.<>9__2_0 = x => x.GetService(typeof(TService)) as TService;
            }
            TService defaultValue = default(TService);
            return this.getServiceProvider().Get<IServiceProvider, TService>(get, defaultValue);
        }

        protected virtual object GetServiceCore(Type serviceType) => 
            this.getServiceProvider().Get<IServiceProvider, object>(x => x.GetService(serviceType), null);

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        object IServiceProvider.GetService(Type serviceType) => 
            this.GetServiceCore(serviceType);

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<TService> where TService: class
        {
            public static readonly FilterUIElement<TUIElementID>.<>c__2<TService> <>9;
            public static Func<IServiceProvider, TService> <>9__2_0;

            static <>c__2()
            {
                FilterUIElement<TUIElementID>.<>c__2<TService>.<>9 = new FilterUIElement<TUIElementID>.<>c__2<TService>();
            }

            internal TService <GetService>b__2_0(IServiceProvider x) => 
                x.GetService(typeof(TService)) as TService;
        }
    }
}

