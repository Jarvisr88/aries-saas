namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Data;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;

    public class UnboundDataSource : SimpleDataSourceBase
    {
        private static readonly DependencyPropertyKey DataPropertyKey;
        public static readonly DependencyProperty DataProperty;
        public static readonly DependencyProperty PropertiesProperty;
        public static readonly DependencyProperty CountProperty;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanged;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanging;

        public event EventHandler<UnboundSourceValueNeededEventArgs> ValueNeeded;

        public event EventHandler<UnboundSourceValuePushedEventArgs> ValuePushed;

        static UnboundDataSource()
        {
            Type ownerType = typeof(UnboundDataSource);
            DataPropertyKey = DependencyProperty.RegisterReadOnly("Data", typeof(IListSource), ownerType, new PropertyMetadata(null, (d, e) => ((UnboundDataSource) d).OnDataChanged((IListSource) e.OldValue, (IListSource) e.NewValue)));
            DataProperty = DataPropertyKey.DependencyProperty;
            PropertiesProperty = DependencyProperty.Register("Properties", typeof(ObservableCollection<UnboundDataSourceProperty>), ownerType, new PropertyMetadata(null, (d, e) => ((UnboundDataSource) d).OnPropertiesCollectionChanged((ObservableCollection<UnboundDataSourceProperty>) e.OldValue, (ObservableCollection<UnboundDataSourceProperty>) e.NewValue)));
            CountProperty = DependencyProperty.Register("Count", typeof(int), ownerType, new PropertyMetadata(0, (d, e) => ((UnboundDataSource) d).OnCountChanged((int) e.OldValue, (int) e.NewValue)));
        }

        public UnboundDataSource()
        {
            this.Properties = new ObservableCollection<UnboundDataSourceProperty>();
            base.UpdateData();
        }

        public int Add() => 
            (this.Source != null) ? this.Source.Add() : -1;

        public void Change(int row, string propertyName = null)
        {
            if (this.Source != null)
            {
                this.Source.Change(row, propertyName);
            }
        }

        public void Clear()
        {
            if (this.Source != null)
            {
                this.Source.Clear();
            }
        }

        protected override object CreateDesignTimeDataSourceCore()
        {
            UnboundSource source = new UnboundSource();
            source.SetRowCount(base.DesignData.RowCount);
            return source;
        }

        private void data_UnboundSourceListChanged(object sender, UnboundSourceListChangedEventArgs e)
        {
            if (this.UnboundSourceListChanged != null)
            {
                this.UnboundSourceListChanged(sender, e);
            }
        }

        private void data_UnboundSourceListChanging(object sender, UnboundSourceListChangedEventArgs e)
        {
            if (this.UnboundSourceListChanging != null)
            {
                this.UnboundSourceListChanging(sender, e);
            }
        }

        private void data_ValueNeeded(object sender, UnboundSourceValueNeededEventArgs e)
        {
            if (this.ValueNeeded != null)
            {
                this.ValueNeeded(sender, e);
            }
        }

        private void data_ValuePushed(object sender, UnboundSourceValuePushedEventArgs e)
        {
            if (this.ValuePushed != null)
            {
                this.ValuePushed(sender, e);
            }
        }

        public void InsertAt(int position)
        {
            if (this.Source != null)
            {
                this.Source.InsertAt(position);
            }
        }

        public void Move(int from, int to)
        {
            if (this.Source != null)
            {
                this.Source.Move(from, to);
            }
        }

        private void OnCountChanged(int oldRowCount, int newRowCount)
        {
            this.SetRowCount(newRowCount);
        }

        private void OnDataChanged(IListSource oldData, IListSource newData)
        {
            UnboundSource source = oldData as UnboundSource;
            if (source != null)
            {
                source.ValueNeeded -= new EventHandler<UnboundSourceValueNeededEventArgs>(this.data_ValueNeeded);
                source.ValuePushed -= new EventHandler<UnboundSourceValuePushedEventArgs>(this.data_ValuePushed);
                source.UnboundSourceListChanged -= new EventHandler<UnboundSourceListChangedEventArgs>(this.data_UnboundSourceListChanged);
                source.UnboundSourceListChanging -= new EventHandler<UnboundSourceListChangedEventArgs>(this.data_UnboundSourceListChanging);
            }
            UnboundSource source2 = newData as UnboundSource;
            if (source2 != null)
            {
                source2.ValueNeeded += new EventHandler<UnboundSourceValueNeededEventArgs>(this.data_ValueNeeded);
                source2.ValuePushed += new EventHandler<UnboundSourceValuePushedEventArgs>(this.data_ValuePushed);
                source2.UnboundSourceListChanged += new EventHandler<UnboundSourceListChangedEventArgs>(this.data_UnboundSourceListChanged);
                source2.UnboundSourceListChanging += new EventHandler<UnboundSourceListChangedEventArgs>(this.data_UnboundSourceListChanging);
            }
            this.RefreshData();
            this.SetRowCount(this.Count);
        }

        private void OnPropertiesCollectionChanged(ObservableCollection<UnboundDataSourceProperty> oldProperties, ObservableCollection<UnboundDataSourceProperty> newProperties)
        {
            if (oldProperties != null)
            {
                oldProperties.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.properties_CollectionChanged);
            }
            if (newProperties != null)
            {
                newProperties.CollectionChanged += new NotifyCollectionChangedEventHandler(this.properties_CollectionChanged);
            }
            this.RefreshData();
        }

        private void properties_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            if (this.Source != null)
            {
                Func<UnboundDataSourceProperty, UnboundSourceProperty> selector = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<UnboundDataSourceProperty, UnboundSourceProperty> local1 = <>c.<>9__14_0;
                    selector = <>c.<>9__14_0 = wrapper => wrapper.Property;
                }
                this.Source.Properties.ClearAndAddRange(this.Properties.Select<UnboundDataSourceProperty, UnboundSourceProperty>(selector));
                this.SetRowCount(this.Count);
            }
        }

        public void RemoveAt(int index)
        {
            if (this.Source != null)
            {
                this.Source.RemoveAt(index);
            }
        }

        public void Reset(int countAfterReset = 0)
        {
            if (this.Source != null)
            {
                this.Source.Reset(countAfterReset);
            }
        }

        public void SetRowCount(int count)
        {
            if (this.Count != count)
            {
                this.Count = count;
            }
            if (this.Source != null)
            {
                this.Source.SetRowCount(count);
            }
        }

        protected override object UpdateDataCore() => 
            new UnboundSource();

        protected internal UnboundSource Source =>
            this.Data as UnboundSource;

        public object this[int rowIndex, string propertyName] =>
            this.Source[rowIndex, propertyName];

        [Category("Data"), Description("Gets the data represented by the UnboundDataSource. This is a dependency property.")]
        public IListSource Data
        {
            get => 
                (IListSource) base.GetValue(DataProperty);
            protected internal set => 
                base.SetValue(DataPropertyKey, value);
        }

        [Description("Gets or sets a collection of properties displayed by the control. This is a dependency property."), Category("Data")]
        public ObservableCollection<UnboundDataSourceProperty> Properties
        {
            get => 
                (ObservableCollection<UnboundDataSourceProperty>) base.GetValue(PropertiesProperty);
            set => 
                base.SetValue(PropertiesProperty, value);
        }

        [Description("Gets or sets the number of items in the UnboundDataSource's list. This is a dependency property."), Category("Data")]
        public int Count
        {
            get => 
                (int) base.GetValue(CountProperty);
            set => 
                base.SetValue(CountProperty, value);
        }

        protected internal override object DataCore
        {
            get => 
                this.Data;
            set => 
                this.Data = (UnboundSource) value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UnboundDataSource.<>c <>9 = new UnboundDataSource.<>c();
            public static Func<UnboundDataSourceProperty, UnboundSourceProperty> <>9__14_0;

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((UnboundDataSource) d).OnDataChanged((IListSource) e.OldValue, (IListSource) e.NewValue);
            }

            internal void <.cctor>b__4_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((UnboundDataSource) d).OnPropertiesCollectionChanged((ObservableCollection<UnboundDataSourceProperty>) e.OldValue, (ObservableCollection<UnboundDataSourceProperty>) e.NewValue);
            }

            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((UnboundDataSource) d).OnCountChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal UnboundSourceProperty <RefreshData>b__14_0(UnboundDataSourceProperty wrapper) => 
                wrapper.Property;
        }
    }
}

