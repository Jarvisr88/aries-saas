namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FilterControlColumnProvider : Decorator, IFilteredComponent, IFilteredComponentBase
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

        static FilterControlColumnProvider()
        {
            Type ownerType = typeof(FilterControlColumnProvider);
            ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlColumnProvider) d).ItemsSourceTypeChanged()));
            AdditionalPropertiesProperty = DependencyProperty.Register("AdditionalProperties", typeof(PropertyInfoCollection), typeof(FilterControlColumnProvider), new PropertyMetadata(null, new PropertyChangedCallback(FilterControlColumnProvider.Update)));
            HiddenPropertiesProperty = DependencyProperty.Register("HiddenProperties", typeof(PropertyNameCollection), typeof(FilterControlColumnProvider), new PropertyMetadata(null, new PropertyChangedCallback(FilterControlColumnProvider.Update)));
            UpperCasePropertyNamesProperty = DependencyProperty.Register("UpperCasePropertyNames", typeof(bool), typeof(FilterControlColumnProvider), new PropertyMetadata(false, new PropertyChangedCallback(FilterControlColumnProvider.Update)));
            SourceControlProperty = DependencyProperty.Register("SourceControl", typeof(IFilteredComponent), typeof(FilterControlColumnProvider), new PropertyMetadata(null, (d, e) => ((FilterControlColumnProvider) d).OnSourceControlChanged(e)));
        }

        public FilterControlColumnProvider()
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

        private void FilterControlColumnProvider_PropertiesChanged(object sender, EventArgs e)
        {
            this.filteredComponentPropertiesChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void FilterControlColumnProvider_RowFilterChanged(object sender, EventArgs e)
        {
            this.rowFilterChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
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
                x.PropertiesChanged -= new EventHandler(this.FilterControlColumnProvider_PropertiesChanged);
            });
            (e.OldValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.RowFilterChanged -= new EventHandler(this.FilterControlColumnProvider_RowFilterChanged);
            });
            (e.NewValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.PropertiesChanged += new EventHandler(this.FilterControlColumnProvider_PropertiesChanged);
            });
            (e.NewValue as IFilteredComponent).Do<IFilteredComponent>(delegate (IFilteredComponent x) {
                x.RowFilterChanged += new EventHandler(this.FilterControlColumnProvider_RowFilterChanged);
            });
            this.Update();
        }

        private void Update()
        {
            this.ItemsSourceTypeChanged();
        }

        private static void Update(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FilterControlColumnProvider) d).Update();
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
                FilterControlColumnProvider provider1 = this;
                if (caption == null)
                {
                    provider1 = (FilterControlColumnProvider) x.Name;
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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlColumnProvider.<>c <>9 = new FilterControlColumnProvider.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnProvider) d).ItemsSourceTypeChanged();
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnProvider) d).OnSourceControlChanged(e);
            }
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

