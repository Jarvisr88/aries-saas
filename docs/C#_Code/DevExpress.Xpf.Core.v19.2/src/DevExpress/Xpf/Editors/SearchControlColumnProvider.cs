namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SearchControlColumnProvider : Decorator, IFilteredComponent, IFilteredComponentBase, ISearchPanelColumnProvider, ISearchPanelColumnProviderBase
    {
        public static readonly DependencyProperty HiddenPropertiesProperty;
        public static readonly DependencyProperty UpperCasePropertyNamesProperty;
        public static readonly DependencyProperty AdditionalPropertiesProperty;
        public static readonly DependencyProperty ItemsSourceTypeProperty;
        public static readonly DependencyProperty SourceControlProperty;
        private StandardColumnsProvider provider;
        private EventHandler filteredComponentPropertiesChanged;
        private EventHandler rowFilterChanged;

        event EventHandler IFilteredComponentBase.PropertiesChanged
        {
            add
            {
                this.filteredComponentPropertiesChanged += value;
            }
            remove
            {
                this.filteredComponentPropertiesChanged -= value;
            }
        }

        event EventHandler IFilteredComponentBase.RowFilterChanged
        {
            add
            {
                this.rowFilterChanged += value;
            }
            remove
            {
                this.rowFilterChanged -= value;
            }
        }

        static SearchControlColumnProvider()
        {
            Type ownerType = typeof(SearchControlColumnProvider);
            ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), ownerType, new PropertyMetadata(null, (d, e) => ((SearchControlColumnProvider) d).ItemsSourceTypeChanged()));
            AdditionalPropertiesProperty = DependencyProperty.Register("AdditionalProperties", typeof(PropertyInfoCollection), typeof(SearchControlColumnProvider), new PropertyMetadata(null, new PropertyChangedCallback(SearchControlColumnProvider.Update)));
            HiddenPropertiesProperty = DependencyProperty.Register("HiddenProperties", typeof(PropertyNameCollection), typeof(SearchControlColumnProvider), new PropertyMetadata(null, new PropertyChangedCallback(SearchControlColumnProvider.Update)));
            UpperCasePropertyNamesProperty = DependencyProperty.Register("UpperCasePropertyNames", typeof(bool), typeof(SearchControlColumnProvider), new PropertyMetadata(false, new PropertyChangedCallback(SearchControlColumnProvider.Update)));
            SourceControlProperty = DependencyProperty.Register("SourceControl", typeof(IFilteredComponent), typeof(SearchControlColumnProvider), new PropertyMetadata(null, (d, e) => ((SearchControlColumnProvider) d).OnSourceControlChanged(e)));
        }

        public SearchControlColumnProvider()
        {
            this.provider = new StandardColumnsProvider(this);
            this.AdditionalProperties = new PropertyInfoCollection();
            this.HiddenProperties = new PropertyNameCollection();
        }

        protected virtual FilterColumn CreateFilterColumn(string columnCaption, BaseEditSettings editSettings, Type columnType, string fieldName)
        {
            FilterColumn column1 = new FilterColumn();
            column1.ColumnCaption = columnCaption;
            column1.EditSettings = editSettings;
            column1.ColumnType = columnType;
            column1.FieldName = fieldName;
            return column1;
        }

        IEnumerable<FilterColumn> IFilteredComponent.CreateFilterColumnCollection() => 
            this.GetAllColumns();

        string ISearchPanelColumnProviderBase.GetSearchText()
        {
            throw new NotImplementedException();
        }

        void ISearchPanelColumnProviderBase.UpdateColumns(FilterByColumnsMode mode)
        {
            throw new NotImplementedException();
        }

        bool ISearchPanelColumnProviderBase.UpdateFilter(string searchText, DevExpress.Data.Filtering.FilterCondition filterCondition, CriteriaOperator filterCriteria)
        {
            this.SourceControl.RowCriteria = filterCriteria;
            return true;
        }

        private IEnumerable<FilterColumn> GetAllColumns()
        {
            IEnumerable<FilterColumn> sourceColums = this.GetSourceColums();
            return this.UpdatedColumns(sourceColums);
        }

        internal IEnumerable<FilterColumn> GetSourceColums()
        {
            switch (this.ColumnSourceMode)
            {
                case DevExpress.Xpf.Editors.Native.ColumnSourceMode.None:
                {
                    FilterColumn item = new FilterColumn();
                    item.FieldName = "";
                    List<FilterColumn> list1 = new List<FilterColumn>();
                    list1.Add(item);
                    return list1;
                }
                case DevExpress.Xpf.Editors.Native.ColumnSourceMode.SourceControl:
                    return this.SourceControl.CreateFilterColumnCollection();

                case DevExpress.Xpf.Editors.Native.ColumnSourceMode.ItemSourceType:
                    return FilteredComponentHelper.GetColumnsByType(this, this.ItemsSourceType, this.UpperCasePropertyNames);
            }
            throw new InvalidOperationException();
        }

        protected void ItemsSourceTypeChanged()
        {
            this.filteredComponentPropertiesChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void OnSourceControlChanged(DependencyPropertyChangedEventArgs e)
        {
            (e.OldValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.PropertiesChanged -= new EventHandler(this.SearchControlColumnProvider_PropertiesChanged);
            });
            (e.OldValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.RowFilterChanged -= new EventHandler(this.SearchControlColumnProvider_RowFilterChanged);
            });
            (e.NewValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.PropertiesChanged += new EventHandler(this.SearchControlColumnProvider_PropertiesChanged);
            });
            (e.NewValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.RowFilterChanged += new EventHandler(this.SearchControlColumnProvider_RowFilterChanged);
            });
            this.Update();
        }

        private void SearchControlColumnProvider_PropertiesChanged(object sender, EventArgs e)
        {
            this.filteredComponentPropertiesChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void SearchControlColumnProvider_RowFilterChanged(object sender, EventArgs e)
        {
            this.rowFilterChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void Update()
        {
            this.ItemsSourceTypeChanged();
        }

        private static void Update(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SearchControlColumnProvider) d).Update();
        }

        private string UpdateColumnCaption(string caption) => 
            !this.UpperCasePropertyNames ? caption : caption?.ToUpper();

        private IEnumerable<FilterColumn> UpdatedColumns(IEnumerable<FilterColumn> sourceColumns) => 
            (from x in sourceColumns
                where !this.HiddenProperties.Contains(x.FieldName)
                select x).Concat<FilterColumn>(this.AdditionalProperties.Select<PropertyInfo, FilterColumn>(delegate (PropertyInfo x) {
                Type type = x.Type ?? typeof(string);
                PropertyDescription standardColumn = this.provider.GetStandardColumn(new CustomPropertyDescriptor(type));
                string caption = x.Caption;
                SearchControlColumnProvider provider1 = this;
                if (caption == null)
                {
                    provider1 = (SearchControlColumnProvider) x.Name;
                }
                string name = x.Name;
                string fieldName = name;
                if (name == null)
                {
                    string local3 = name;
                    fieldName = string.Empty;
                }
                return caption.CreateFilterColumn(this.UpdateColumnCaption((string) provider1), standardColumn.EditSettings, type, fieldName);
            }));

        private CriteriaOperator FilterCriteria { get; set; }

        private DevExpress.Data.Filtering.FilterCondition FilterCondition { get; set; }

        private string SearchText { get; set; }

        private FilterByColumnsMode OverridedFilterByColumnsMode { get; set; }

        private DevExpress.Xpf.Editors.Native.ColumnSourceMode ColumnSourceMode =>
            (this.SourceControl == null) ? ((this.ItemsSourceType == null) ? DevExpress.Xpf.Editors.Native.ColumnSourceMode.None : DevExpress.Xpf.Editors.Native.ColumnSourceMode.ItemSourceType) : DevExpress.Xpf.Editors.Native.ColumnSourceMode.SourceControl;

        public IFilteredComponent SourceControl
        {
            get => 
                (IFilteredComponent) base.GetValue(SourceControlProperty);
            set => 
                base.SetValue(SourceControlProperty, value);
        }

        public PropertyNameCollection HiddenProperties
        {
            get => 
                (PropertyNameCollection) base.GetValue(HiddenPropertiesProperty);
            set => 
                base.SetValue(HiddenPropertiesProperty, value);
        }

        public bool UpperCasePropertyNames
        {
            get => 
                (bool) base.GetValue(UpperCasePropertyNamesProperty);
            set => 
                base.SetValue(UpperCasePropertyNamesProperty, value);
        }

        public PropertyInfoCollection AdditionalProperties
        {
            get => 
                (PropertyInfoCollection) base.GetValue(AdditionalPropertiesProperty);
            set => 
                base.SetValue(AdditionalPropertiesProperty, value);
        }

        public Type ItemsSourceType
        {
            get => 
                (Type) base.GetValue(ItemsSourceTypeProperty);
            set => 
                base.SetValue(ItemsSourceTypeProperty, value);
        }

        CriteriaOperator IFilteredComponentBase.RowCriteria
        {
            get => 
                this.SourceControl.RowCriteria;
            set => 
                this.SourceControl.RowCriteria = value;
        }

        IEnumerable<string> ISearchPanelColumnProvider.Columns
        {
            get
            {
                Func<FilterColumn, string> selector = <>c.<>9__66_0;
                if (<>c.<>9__66_0 == null)
                {
                    Func<FilterColumn, string> local1 = <>c.<>9__66_0;
                    selector = <>c.<>9__66_0 = x => x.FieldName;
                }
                return this.GetAllColumns().Select<FilterColumn, string>(selector);
            }
        }

        ObservableCollection<string> ISearchPanelColumnProviderBase.CustomColumns
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchControlColumnProvider.<>c <>9 = new SearchControlColumnProvider.<>c();
            public static Func<FilterColumn, string> <>9__66_0;

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlColumnProvider) d).ItemsSourceTypeChanged();
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlColumnProvider) d).OnSourceControlChanged(e);
            }

            internal string <DevExpress.Xpf.Editors.ISearchPanelColumnProvider.get_Columns>b__66_0(FilterColumn x) => 
                x.FieldName;
        }

        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private Type type;

            public CustomPropertyDescriptor(Type type) : base("name", new Attribute[0])
            {
                this.type = type;
            }

            public override bool CanResetValue(object component) => 
                false;

            public override object GetValue(object component) => 
                null;

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
            }

            public override bool ShouldSerializeValue(object component) => 
                false;

            public override Type ComponentType =>
                typeof(object);

            public override bool IsReadOnly =>
                true;

            public override Type PropertyType =>
                this.type;
        }
    }
}

