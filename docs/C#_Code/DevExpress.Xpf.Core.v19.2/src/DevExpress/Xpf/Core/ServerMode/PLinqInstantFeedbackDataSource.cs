namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.PLinq;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.DataSources;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    [DefaultEvent("GetEnumerable")]
    public class PLinqInstantFeedbackDataSource : PLinqDataSourceBase, IDisposable
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty ListSourceProperty;
        public static readonly RoutedEvent GetEnumerableEvent;
        public static readonly RoutedEvent DismissEnumerableEvent;
        private volatile IEnumerable itemsSourceField;
        private volatile IListSource listSourceField;
        private volatile bool isDisposed;

        public event GetEnumerableEventHandler DismissEnumerable
        {
            add
            {
                base.AddHandler(DismissEnumerableEvent, value);
            }
            remove
            {
                base.RemoveHandler(DismissEnumerableEvent, value);
            }
        }

        public event GetEnumerableEventHandler GetEnumerable
        {
            add
            {
                base.AddHandler(GetEnumerableEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetEnumerableEvent, value);
            }
        }

        static PLinqInstantFeedbackDataSource()
        {
            Type ownerType = typeof(PLinqInstantFeedbackDataSource);
            DefaultSortingProperty = DependencyPropertyManager.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(PLinqInstantFeedbackDataSource.OnDefaultSortingChanged)));
            ListSourceProperty = DependencyPropertyManager.Register("ListSource", typeof(IListSource), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(PLinqInstantFeedbackDataSource.OnListSourceChanged)));
            GetEnumerableEvent = EventManager.RegisterRoutedEvent("GetEnumerable", RoutingStrategy.Direct, typeof(GetEnumerableEventHandler), ownerType);
            DismissEnumerableEvent = EventManager.RegisterRoutedEvent("DismissEnumerable", RoutingStrategy.Direct, typeof(GetEnumerableEventHandler), ownerType);
        }

        public PLinqInstantFeedbackDataSource()
        {
            this.ResetPLinqInstantFeedbackSource();
            Func<object, bool> canExecuteMethod = <>c.<>9__18_1;
            if (<>c.<>9__18_1 == null)
            {
                Func<object, bool> local1 = <>c.<>9__18_1;
                canExecuteMethod = <>c.<>9__18_1 = parameter => true;
            }
            this.DisposeCommand = DelegateCommandFactory.Create<object>(parameter => this.Dispose(), canExecuteMethod, false);
        }

        private void Data_DismissEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {
            if (this.isDisposed)
            {
                PLinqInstantFeedbackSource source = (PLinqInstantFeedbackSource) sender;
                source.GetEnumerable -= new EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.Data_GetEnumerable);
                source.DismissEnumerable -= new EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.Data_DismissEnumerable);
            }
            DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs args = new DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs();
            this.RaiseDismissEnumerable(args);
            if (args.Handled)
            {
                e.Source = args.ItemsSource;
                e.Tag = args.Tag;
            }
        }

        private void Data_GetEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs args = new DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs();
            this.RaiseGetEnumerable(args);
            if (args.Handled)
            {
                e.Source = args.ItemsSource;
                e.Tag = args.Tag;
            }
            else
            {
                IEnumerable itemsSourceField = this.itemsSourceField;
                if (itemsSourceField != null)
                {
                    e.Source = itemsSourceField;
                }
                else
                {
                    IListSource listSourceField = this.listSourceField;
                    if (listSourceField != null)
                    {
                        e.Source = listSourceField.GetList();
                    }
                    else
                    {
                        e.Source = Enumerable.Empty<object>();
                    }
                }
            }
        }

        public void Dispose()
        {
            this.isDisposed = true;
            if (base.Data != null)
            {
                ((PLinqInstantFeedbackSource) base.Data).Dispose();
            }
        }

        protected override Type GetDataObjectType()
        {
            if (base.DesignData.DataObjectType != null)
            {
                return base.DesignData.DataObjectType;
            }
            IEnumerable enumerable = (this.ListSource != null) ? this.ListSource.GetList() : base.ItemsSource;
            return ((enumerable != null) ? DataSourceHelper.ExtractEnumerableType(enumerable) : null);
        }

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.PLinqInstantFeedbackDataSource.png";

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PLinqInstantFeedbackSource data = ((PLinqInstantFeedbackDataSource) d).Data as PLinqInstantFeedbackSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnListSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PLinqInstantFeedbackDataSource) d).UpdateData();
        }

        protected virtual void RaiseDismissEnumerable(DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs args)
        {
            args.RoutedEvent = DismissEnumerableEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseGetEnumerable(DevExpress.Xpf.Core.ServerMode.GetEnumerableEventArgs args)
        {
            args.RoutedEvent = GetEnumerableEvent;
            base.RaiseEvent(args);
        }

        public void Refresh()
        {
            if (base.Data != null)
            {
                ((PLinqInstantFeedbackSource) base.Data).Refresh();
            }
        }

        internal void ResetPLinqInstantFeedbackSource()
        {
            if (base.Data != null)
            {
                ((PLinqInstantFeedbackSource) base.Data).Dispose();
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                PLinqInstantFeedbackSource source2 = new PLinqInstantFeedbackSource {
                    DefaultSorting = this.DefaultSorting
                };
                source2.GetEnumerable += new EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.Data_GetEnumerable);
                source2.DismissEnumerable += new EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.Data_DismissEnumerable);
                base.Data = source2;
            }
        }

        protected override object UpdateDataCore()
        {
            this.listSourceField = this.ListSource;
            this.itemsSourceField = base.ItemsSource;
            this.ResetPLinqInstantFeedbackSource();
            return base.Data;
        }

        [Category("Data")]
        public string DefaultSorting
        {
            get => 
                (string) base.GetValue(DefaultSortingProperty);
            set => 
                base.SetValue(DefaultSortingProperty, value);
        }

        [Category("Data")]
        public IListSource ListSource
        {
            get => 
                (IListSource) base.GetValue(ListSourceProperty);
            set => 
                base.SetValue(ListSourceProperty, value);
        }

        public ICommand DisposeCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PLinqInstantFeedbackDataSource.<>c <>9 = new PLinqInstantFeedbackDataSource.<>c();
            public static Func<object, bool> <>9__18_1;

            internal bool <.ctor>b__18_1(object parameter) => 
                true;
        }
    }
}

