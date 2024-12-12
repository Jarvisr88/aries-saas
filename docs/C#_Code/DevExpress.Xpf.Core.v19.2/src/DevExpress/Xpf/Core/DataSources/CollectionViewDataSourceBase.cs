namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class CollectionViewDataSourceBase : EnumerableDataSourceBase
    {
        public static readonly DependencyProperty CollectionViewTypeProperty;
        public static readonly DependencyProperty CultureProperty;
        public static readonly DependencyProperty GroupDescriptionsProperty;
        public static readonly DependencyProperty SortDescriptionsProperty;

        static CollectionViewDataSourceBase()
        {
            Type ownerType = typeof(CollectionViewDataSourceBase);
            CollectionViewTypeProperty = DependencyPropertyManager.Register("CollectionViewType", typeof(Type), ownerType, new FrameworkPropertyMetadata(typeof(ListCollectionView), (d, e) => ((CollectionViewDataSourceBase) d).UpdateData()), new ValidateValueCallback(CollectionViewDataSourceBase.ValidateCollectionViewType));
            CultureProperty = DependencyPropertyManager.Register("Culture", typeof(CultureInfo), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CollectionViewDataSourceBase) d).UpdateView()));
            GroupDescriptionsProperty = DependencyPropertyManager.Register("GroupDescriptions", typeof(ObservableCollection<GroupDescription>), ownerType, new FrameworkPropertyMetadata(new ObservableCollection<GroupDescription>(), (d, e) => ((CollectionViewDataSourceBase) d).OnDescriptionsChanged(e.OldValue, e.NewValue)));
            SortDescriptionsProperty = DependencyPropertyManager.Register("SortDescriptions", typeof(SortDescriptionCollection), ownerType, new FrameworkPropertyMetadata(new SortDescriptionCollection(), (d, e) => ((CollectionViewDataSourceBase) d).OnDescriptionsChanged(e.OldValue, e.NewValue)));
        }

        public CollectionViewDataSourceBase()
        {
            this.GroupDescriptions = new ObservableCollection<GroupDescription>();
            this.SortDescriptions = new SortDescriptionCollection();
            this.SortDescriptions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnDescriptionsCollectionChanged);
            this.GroupDescriptions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnDescriptionsCollectionChanged);
        }

        private static void Copy<T>(IEnumerable source, IList<T> target, bool isValid)
        {
            if ((source != null) && ((target != null) && isValid))
            {
                target.Clear();
                foreach (T local in source)
                {
                    target.Add(local);
                }
            }
        }

        protected override object CreateData(object value)
        {
            if (value == null)
            {
                return null;
            }
            IEnumerable target = (IEnumerable) base.Strategy.CreateData(value);
            CollectionViewSource source = new CollectionViewSource();
            ISupportInitialize initialize = source;
            initialize.BeginInit();
            source.CollectionViewType = this.CollectionViewType;
            source.Source = (target is IList) ? target : ToIList(target, base.Strategy.GetDataObjectType());
            initialize.EndInit();
            IEnumerable view = source.View;
            this.UpdateView(view);
            return view;
        }

        protected override object CreateDesignTimeDataSourceCore()
        {
            IEnumerable enumerable = new CollectionViewDesignTimeDataSource(base.Strategy.GetDataObjectType(), base.DesignData.RowCount, base.DesignData.UseDistinctValues, null, null);
            CollectionViewSource source1 = new CollectionViewSource();
            source1.Source = enumerable;
            CollectionViewSource source = source1;
            this.UpdateView(source.View);
            return source.View;
        }

        private void OnDescriptionsChanged(object oldValue, object newValue)
        {
            if (oldValue != null)
            {
                ((INotifyCollectionChanged) oldValue).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnDescriptionsCollectionChanged);
            }
            ((INotifyCollectionChanged) newValue).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnDescriptionsCollectionChanged);
            this.UpdateView();
        }

        private void OnDescriptionsCollectionChanged(object d, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateView();
        }

        private static IList ToIList(IEnumerable target, Type elementType)
        {
            Type[] typeArguments = new Type[] { elementType };
            object[] parameters = new object[] { target };
            return (IList) typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(typeArguments).Invoke(null, parameters);
        }

        private void UpdateView()
        {
            this.UpdateView(base.Data);
        }

        private void UpdateView(object collectionView)
        {
            if ((collectionView != null) && (collectionView is ICollectionView))
            {
                ICollectionView view = (ICollectionView) collectionView;
                Copy<SortDescription>(this.SortDescriptions, view.SortDescriptions, view.CanSort);
                Copy<GroupDescription>(this.GroupDescriptions, view.GroupDescriptions, view.CanGroup);
                if (this.Culture != null)
                {
                    view.Culture = this.Culture;
                }
            }
        }

        private static bool ValidateCollectionViewType(object value)
        {
            Type type = (Type) value;
            return (type.GetInterfaces().Contains<Type>(typeof(ICollectionView)) && (type.GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Count<ConstructorInfo>() > 0));
        }

        public Type CollectionViewType
        {
            get => 
                (Type) base.GetValue(CollectionViewTypeProperty);
            set => 
                base.SetValue(CollectionViewTypeProperty, value);
        }

        [TypeConverter(typeof(CultureConverter))]
        public CultureInfo Culture
        {
            get => 
                (CultureInfo) base.GetValue(CultureProperty);
            set => 
                base.SetValue(CultureProperty, value);
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get => 
                (ObservableCollection<GroupDescription>) base.GetValue(GroupDescriptionsProperty);
            set => 
                base.SetValue(GroupDescriptionsProperty, value);
        }

        public SortDescriptionCollection SortDescriptions
        {
            get => 
                (SortDescriptionCollection) base.GetValue(SortDescriptionsProperty);
            set => 
                base.SetValue(SortDescriptionsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionViewDataSourceBase.<>c <>9 = new CollectionViewDataSourceBase.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CollectionViewDataSourceBase) d).UpdateData();
            }

            internal void <.cctor>b__4_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CollectionViewDataSourceBase) d).UpdateView();
            }

            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CollectionViewDataSourceBase) d).OnDescriptionsChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__4_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CollectionViewDataSourceBase) d).OnDescriptionsChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

