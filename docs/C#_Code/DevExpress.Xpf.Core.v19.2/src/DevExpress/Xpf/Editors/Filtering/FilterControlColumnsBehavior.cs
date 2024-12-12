namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FilterControlColumnsBehavior : Behavior<FilterControl>
    {
        public static readonly DependencyProperty HiddenPropertiesProperty;
        public static readonly DependencyProperty AdditionalPropertiesProperty;
        public static readonly DependencyProperty ObjectTypeProperty;
        public static readonly DependencyProperty UpperCasePropertyNamesProperty;

        static FilterControlColumnsBehavior()
        {
            HiddenPropertiesProperty = DependencyProperty.Register("HiddenProperties", typeof(IEnumerable<string>), typeof(FilterControlColumnsBehavior), new PropertyMetadata(null, (d, e) => ((FilterControlColumnsBehavior) d).UpdateColumns()));
            AdditionalPropertiesProperty = DependencyProperty.Register("AdditionalProperties", typeof(PropertyInfoCollection), typeof(FilterControlColumnsBehavior), new PropertyMetadata(null, (d, e) => ((FilterControlColumnsBehavior) d).UpdateColumns()));
            ObjectTypeProperty = DependencyProperty.Register("ObjectType", typeof(Type), typeof(FilterControlColumnsBehavior), new PropertyMetadata(null, (d, e) => ((FilterControlColumnsBehavior) d).UpdateColumns()));
            UpperCasePropertyNamesProperty = DependencyProperty.Register("UpperCasePropertyNames", typeof(bool), typeof(FilterControlColumnsBehavior), new PropertyMetadata(false, (d, e) => ((FilterControlColumnsBehavior) d).UpdateColumns()));
        }

        public FilterControlColumnsBehavior()
        {
            this.HiddenProperties = new List<string>();
            this.AdditionalProperties = new PropertyInfoCollection();
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

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateColumns();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.FilterColumns = null;
        }

        private string UpdateColumnCaption(string caption) => 
            !this.UpperCasePropertyNames ? caption : caption.ToUpper();

        protected void UpdateColumns()
        {
            if ((base.AssociatedObject != null) && (this.ObjectType != null))
            {
                StandardColumnsProvider provider = new StandardColumnsProvider(base.AssociatedObject);
                IEnumerable<FilterColumn> first = (from x in TypeDescriptor.GetProperties(this.ObjectType).Cast<PropertyDescriptor>()
                    where ((this.HiddenProperties == null) || !this.HiddenProperties.Contains<string>(x.Name)) ? (!(x.PropertyType == typeof(string)) ? ((IEnumerable<PropertyDescriptor>) !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)) : ((IEnumerable<PropertyDescriptor>) true)) : ((IEnumerable<PropertyDescriptor>) false)
                    select x).Select<PropertyDescriptor, FilterColumn>(delegate (PropertyDescriptor x) {
                    PropertyDescription standardColumn = provider.GetStandardColumn(x);
                    string caption = standardColumn.ColumnCaption ?? standardColumn.FieldName;
                    return this.CreateFilterColumn(this.UpdateColumnCaption(caption), standardColumn.EditSettings, x.PropertyType, standardColumn.FieldName);
                });
                if (this.AdditionalProperties != null)
                {
                    first = first.Concat<FilterColumn>(this.AdditionalProperties.Select<PropertyInfo, FilterColumn>(delegate (PropertyInfo x) {
                        Type type = x.Type ?? typeof(string);
                        PropertyDescription standardColumn = provider.GetStandardColumn(new CustomPropertyDescriptor(type));
                        string caption = x.Caption;
                        FilterControlColumnsBehavior behavior1 = this;
                        if (caption == null)
                        {
                            behavior1 = (FilterControlColumnsBehavior) x.Name;
                        }
                        string name = x.Name;
                        string fieldName = name;
                        if (name == null)
                        {
                            string local3 = name;
                            fieldName = string.Empty;
                        }
                        return caption.CreateFilterColumn(this.UpdateColumnCaption((string) behavior1), standardColumn.EditSettings, type, fieldName);
                    }));
                }
                base.AssociatedObject.FilterColumns = first;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IEnumerable<string> HiddenProperties
        {
            get => 
                (IEnumerable<string>) base.GetValue(HiddenPropertiesProperty);
            set => 
                base.SetValue(HiddenPropertiesProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PropertyInfoCollection AdditionalProperties
        {
            get => 
                (PropertyInfoCollection) base.GetValue(AdditionalPropertiesProperty);
            set => 
                base.SetValue(AdditionalPropertiesProperty, value);
        }

        public Type ObjectType
        {
            get => 
                (Type) base.GetValue(ObjectTypeProperty);
            set => 
                base.SetValue(ObjectTypeProperty, value);
        }

        public bool UpperCasePropertyNames
        {
            get => 
                (bool) base.GetValue(UpperCasePropertyNamesProperty);
            set => 
                base.SetValue(UpperCasePropertyNamesProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlColumnsBehavior.<>c <>9 = new FilterControlColumnsBehavior.<>c();

            internal void <.cctor>b__23_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnsBehavior) d).UpdateColumns();
            }

            internal void <.cctor>b__23_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnsBehavior) d).UpdateColumns();
            }

            internal void <.cctor>b__23_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnsBehavior) d).UpdateColumns();
            }

            internal void <.cctor>b__23_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlColumnsBehavior) d).UpdateColumns();
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

