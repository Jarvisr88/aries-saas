namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SelectorEditColumnProvider : Decorator, ISearchPanelColumnProvider, ISearchPanelColumnProviderBase
    {
        public static readonly DependencyProperty OwnerEditProperty;
        internal static readonly DependencyPropertyKey ColumnsPropertyKey;
        public static readonly DependencyProperty ColumnsProperty;
        public static readonly DependencyProperty AllowFilterProperty;
        internal static readonly DependencyPropertyKey AvailableColumnsPropertyKey;
        public static readonly DependencyProperty AvailableColumnsProperty;
        public static readonly DependencyProperty CustomColumnsProperty;
        public static readonly DependencyProperty ItemsSourceTypeProperty;

        static SelectorEditColumnProvider()
        {
            Type ownerType = typeof(SelectorEditColumnProvider);
            OwnerEditProperty = DependencyPropertyManager.Register("OwnerEdit", typeof(ISelectorEdit), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((SelectorEditColumnProvider) d).OwnerEditChanged((ISelectorEdit) e.NewValue)));
            AllowFilterProperty = DependencyPropertyManager.Register("AllowFilter", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((SelectorEditColumnProvider) d).AllowFilterChanged((bool) e.NewValue)));
            ColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("Columns", typeof(ReadOnlyObservableCollection<string>), ownerType, new FrameworkPropertyMetadata(null));
            ColumnsProperty = ColumnsPropertyKey.DependencyProperty;
            AvailableColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("AvailableColumns", typeof(ReadOnlyObservableCollection<string>), ownerType, new FrameworkPropertyMetadata(null));
            AvailableColumnsProperty = AvailableColumnsPropertyKey.DependencyProperty;
            CustomColumnsProperty = DependencyPropertyManager.Register("CustomColumns", typeof(ObservableCollection<string>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((SelectorEditColumnProvider) d).CustomColumnsChanged((ObservableCollection<string>) e.OldValue, (ObservableCollection<string>) e.NewValue)));
            ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), ownerType, new PropertyMetadata(null, (d, e) => ((SelectorEditColumnProvider) d).ItemsSourceTypeChanged()));
        }

        public SelectorEditColumnProvider()
        {
            this.Columns = new ReadOnlyObservableCollection<string>(new ObservableCollection<string>());
            this.CustomColumns = new ObservableCollection<string>();
        }

        protected virtual void AllowFilterChanged(bool value)
        {
            if (value)
            {
                this.PerformUpdate();
            }
            else
            {
                this.OwnerEdit.FilterCriteria = null;
            }
        }

        protected virtual void CustomColumnsChanged(ObservableCollection<string> oldCollection, ObservableCollection<string> newCollection)
        {
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.CustomColumnsCollectionChanged);
            }
            if (newCollection != null)
            {
                newCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CustomColumnsCollectionChanged);
            }
            this.PerformUpdate();
        }

        protected virtual void CustomColumnsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.PerformUpdate();
        }

        string ISearchPanelColumnProviderBase.GetSearchText() => 
            this.SearchText;

        bool ISearchPanelColumnProviderBase.UpdateFilter(string searchText, DevExpress.Data.Filtering.FilterCondition filterCondition, CriteriaOperator filterCriteria)
        {
            this.FilterCriteria = filterCriteria;
            this.FilterCondition = filterCondition;
            this.SearchText = searchText;
            if (this.AllowFilter)
            {
                this.PerformUpdate();
            }
            return true;
        }

        private ObservableCollection<string> GetAvailableColumns() => 
            (this.OwnerEdit != null) ? new ObservableCollection<string>(this.OwnerEdit.ItemsProvider.AvailableColumns) : new ObservableCollection<string>();

        private ObservableCollection<string> GetColumns(DevExpress.Xpf.Editors.FilterByColumnsMode mode)
        {
            if (mode != DevExpress.Xpf.Editors.FilterByColumnsMode.Default)
            {
                return ((mode == DevExpress.Xpf.Editors.FilterByColumnsMode.Custom) ? new ObservableCollection<string>(this.GetCustomColumns()) : new ObservableCollection<string>());
            }
            ObservableCollection<string> collection1 = new ObservableCollection<string>();
            collection1.Add(this.GetDisplayColumn());
            return collection1;
        }

        private ObservableCollection<string> GetCustomColumns() => 
            (this.CustomColumns != null) ? this.CustomColumns : new ObservableCollection<string>();

        private string GetDisplayColumn() => 
            (this.OwnerEdit != null) ? (string.IsNullOrEmpty(this.OwnerEdit.DisplayMember) ? "DisplayColumn" : this.OwnerEdit.DisplayMember) : string.Empty;

        private void ItemsSourceTypeChanged()
        {
            ObservableCollection<string> observables = new ObservableCollection<string>();
            if (this.ItemsSourceType == null)
            {
                this.CustomColumns = observables;
            }
            else
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this.ItemsSourceType))
                {
                    observables.Add(descriptor.Name);
                }
                this.CustomColumns = observables;
            }
        }

        protected virtual void OwnerEditChanged(ISelectorEdit editor)
        {
            if (editor != null)
            {
                this.PerformUpdate();
            }
        }

        protected virtual void PerformUpdate()
        {
            if ((this.OwnerEdit != null) && this.AllowFilter)
            {
                this.OwnerEdit.FilterCriteria = this.FilterCriteria;
            }
            this.UpdateColumns(this.FilterByColumnsMode);
        }

        public void UpdateColumns(DevExpress.Xpf.Editors.FilterByColumnsMode mode)
        {
            this.FilterByColumnsMode = mode;
            this.AvailableColumns = new ReadOnlyObservableCollection<string>(this.GetAvailableColumns());
            this.Columns = new ReadOnlyObservableCollection<string>(this.GetColumns(mode));
        }

        public ObservableCollection<string> CustomColumns
        {
            get => 
                (ObservableCollection<string>) base.GetValue(CustomColumnsProperty);
            set => 
                base.SetValue(CustomColumnsProperty, value);
        }

        public ReadOnlyObservableCollection<string> AvailableColumns
        {
            get => 
                (ReadOnlyObservableCollection<string>) base.GetValue(AvailableColumnsProperty);
            internal set => 
                base.SetValue(AvailableColumnsPropertyKey, value);
        }

        public ReadOnlyObservableCollection<string> Columns
        {
            get => 
                (ReadOnlyObservableCollection<string>) base.GetValue(ColumnsProperty);
            internal set => 
                base.SetValue(ColumnsPropertyKey, value);
        }

        public ISelectorEdit OwnerEdit
        {
            get => 
                (ISelectorEdit) base.GetValue(OwnerEditProperty);
            set => 
                base.SetValue(OwnerEditProperty, value);
        }

        public bool AllowFilter
        {
            get => 
                (bool) base.GetValue(AllowFilterProperty);
            set => 
                base.SetValue(AllowFilterProperty, value);
        }

        public Type ItemsSourceType
        {
            get => 
                (Type) base.GetValue(ItemsSourceTypeProperty);
            set => 
                base.SetValue(ItemsSourceTypeProperty, value);
        }

        private CriteriaOperator FilterCriteria { get; set; }

        private DevExpress.Data.Filtering.FilterCondition FilterCondition { get; set; }

        private string SearchText { get; set; }

        private DevExpress.Xpf.Editors.FilterByColumnsMode FilterByColumnsMode { get; set; }

        IEnumerable<string> ISearchPanelColumnProvider.Columns =>
            this.Columns;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorEditColumnProvider.<>c <>9 = new SelectorEditColumnProvider.<>c();

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectorEditColumnProvider) d).OwnerEditChanged((ISelectorEdit) e.NewValue);
            }

            internal void <.cctor>b__8_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectorEditColumnProvider) d).AllowFilterChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__8_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectorEditColumnProvider) d).CustomColumnsChanged((ObservableCollection<string>) e.OldValue, (ObservableCollection<string>) e.NewValue);
            }

            internal void <.cctor>b__8_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectorEditColumnProvider) d).ItemsSourceTypeChanged();
            }
        }
    }
}

