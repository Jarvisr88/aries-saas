namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class FilterListEditorBehavior : Behavior<BaseEdit>
    {
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ValueMemberProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty UseFlagsProperty;
        public static readonly DependencyProperty UseSelectAllProperty;
        public static readonly DependencyProperty SelectAllNameProperty;
        public static readonly DependencyProperty UseTokenStyleProperty;
        private bool shouldSetItemsSource;
        private BindingBase radioEditValueBinding;
        private BindingBase checkedEditValueBinding;

        static FilterListEditorBehavior()
        {
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(FilterListEditorBehavior), new PropertyMetadata(null, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorItemsSource()));
            ValueMemberProperty = DependencyProperty.Register("ValueMember", typeof(string), typeof(FilterListEditorBehavior), new PropertyMetadata(null, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorItemsSource()));
            DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(string), typeof(FilterListEditorBehavior), new PropertyMetadata(null, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorItemsSource()));
            UseFlagsProperty = DependencyProperty.Register("UseFlags", typeof(bool), typeof(FilterListEditorBehavior), new PropertyMetadata(false, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorStyleSettings()));
            UseSelectAllProperty = DependencyProperty.Register("UseSelectAll", typeof(bool), typeof(FilterListEditorBehavior), new PropertyMetadata(false, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorUseSelectAll()));
            SelectAllNameProperty = DependencyProperty.Register("SelectAllName", typeof(string), typeof(FilterListEditorBehavior), new PropertyMetadata("All", (d, e) => ((FilterListEditorBehavior) d).UpdateEditorStyleSettings()));
            UseTokenStyleProperty = DependencyProperty.Register("UseTokenStyle", typeof(bool), typeof(FilterListEditorBehavior), new PropertyMetadata(false, (d, e) => ((FilterListEditorBehavior) d).UpdateEditorStyleSettings()));
        }

        private BaseEditStyleSettings GetComboBoxStyleSettings()
        {
            if (this.UseTokenStyle)
            {
                TokenComboBoxStyleSettings settings1 = new TokenComboBoxStyleSettings();
                settings1.NewTokenPosition = 2;
                return settings1;
            }
            if (!this.UseFlags)
            {
                return new ComboBoxStyleSettings();
            }
            SelectAllCheckedComboBoxStyleSettings settings2 = new SelectAllCheckedComboBoxStyleSettings();
            settings2.SelectAllName = this.SelectAllName;
            return settings2;
        }

        private BaseEditStyleSettings GetListBoxStyleSettings()
        {
            if (this.UseFlags)
            {
                SelectAllCheckedListBoxEditStyleSettings settings1 = new SelectAllCheckedListBoxEditStyleSettings();
                settings1.SelectAllName = this.SelectAllName;
                return settings1;
            }
            SelectAllRadioListBoxEditStyleSettings settings2 = new SelectAllRadioListBoxEditStyleSettings();
            settings2.SelectAllName = this.SelectAllName;
            return settings2;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.shouldSetItemsSource)
            {
                this.UpdateEditorItemsSource();
            }
            this.UpdateEditorStyleSettings();
            this.UpdateEditorUseSelectAll();
            this.UpdateEditorEditValue();
        }

        private void UpdateEditorEditValue()
        {
            if ((base.AssociatedObject != null) && ((this.RadioEditValueBinding != null) && (this.CheckedEditValueBinding != null)))
            {
                if (this.UseFlags)
                {
                    base.AssociatedObject.SetBinding(BaseEdit.EditValueProperty, this.CheckedEditValueBinding);
                }
                else
                {
                    base.AssociatedObject.SetBinding(BaseEdit.EditValueProperty, this.RadioEditValueBinding);
                }
            }
        }

        private void UpdateEditorItemsSource()
        {
            this.shouldSetItemsSource = true;
            if (base.AssociatedObject != null)
            {
                TypeDescriptor.GetProperties(base.AssociatedObject)["ItemsSource"].Do<PropertyDescriptor>(x => x.SetValue(base.AssociatedObject, this.ItemsSource));
                TypeDescriptor.GetProperties(base.AssociatedObject)["ValueMember"].Do<PropertyDescriptor>(x => x.SetValue(base.AssociatedObject, this.ValueMember));
                TypeDescriptor.GetProperties(base.AssociatedObject)["DisplayMember"].Do<PropertyDescriptor>(x => x.SetValue(base.AssociatedObject, this.DisplayMember));
            }
        }

        private void UpdateEditorStyleSettings()
        {
            (base.AssociatedObject as ListBoxEdit).Do<ListBoxEdit>(x => x.StyleSettings = this.GetListBoxStyleSettings());
            (base.AssociatedObject as ComboBoxEdit).Do<ComboBoxEdit>(x => x.StyleSettings = this.GetComboBoxStyleSettings());
            this.UpdateEditorEditValue();
        }

        private void UpdateEditorUseSelectAll()
        {
            (base.AssociatedObject as ListBoxEdit).Do<ListBoxEdit>(x => x.ShowCustomItems = new bool?(this.UseSelectAll));
            (base.AssociatedObject as ComboBoxEdit).Do<ComboBoxEdit>(x => x.ShowCustomItems = new bool?(this.UseSelectAll));
        }

        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        public string ValueMember
        {
            get => 
                (string) base.GetValue(ValueMemberProperty);
            set => 
                base.SetValue(ValueMemberProperty, value);
        }

        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        public bool UseFlags
        {
            get => 
                (bool) base.GetValue(UseFlagsProperty);
            set => 
                base.SetValue(UseFlagsProperty, value);
        }

        public bool UseSelectAll
        {
            get => 
                (bool) base.GetValue(UseSelectAllProperty);
            set => 
                base.SetValue(UseSelectAllProperty, value);
        }

        public string SelectAllName
        {
            get => 
                (string) base.GetValue(SelectAllNameProperty);
            set => 
                base.SetValue(SelectAllNameProperty, value);
        }

        public bool UseTokenStyle
        {
            get => 
                (bool) base.GetValue(UseTokenStyleProperty);
            set => 
                base.SetValue(UseTokenStyleProperty, value);
        }

        public BindingBase RadioEditValueBinding
        {
            get => 
                this.radioEditValueBinding;
            set
            {
                this.radioEditValueBinding = value;
                this.UpdateEditorEditValue();
            }
        }

        public BindingBase CheckedEditValueBinding
        {
            get => 
                this.checkedEditValueBinding;
            set
            {
                this.checkedEditValueBinding = value;
                this.UpdateEditorEditValue();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterListEditorBehavior.<>c <>9 = new FilterListEditorBehavior.<>c();

            internal void <.cctor>b__48_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorItemsSource();
            }

            internal void <.cctor>b__48_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorItemsSource();
            }

            internal void <.cctor>b__48_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorItemsSource();
            }

            internal void <.cctor>b__48_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorStyleSettings();
            }

            internal void <.cctor>b__48_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorUseSelectAll();
            }

            internal void <.cctor>b__48_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorStyleSettings();
            }

            internal void <.cctor>b__48_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterListEditorBehavior) d).UpdateEditorStyleSettings();
            }
        }

        private class SelectAllCheckedComboBoxStyleSettings : CheckedComboBoxStyleSettings
        {
            public static readonly DependencyProperty SelectAllNameProperty = FilterListEditorBehavior.SelectAllCheckedListBoxEditStyleSettings.SelectAllNameProperty.AddOwner(typeof(FilterListEditorBehavior.SelectAllCheckedComboBoxStyleSettings));

            protected internal override IEnumerable<CustomItem> GetCustomItems(LookUpEditBase editor)
            {
                SelectAllItem item = new SelectAllItem();
                item.DisplayText = this.SelectAllName;
                List<CustomItem> list1 = new List<CustomItem>();
                list1.Add(item);
                return list1;
            }

            protected override bool ShowCustomItemInternal(LookUpEditBase editor) => 
                true;

            public string SelectAllName
            {
                get => 
                    (string) base.GetValue(SelectAllNameProperty);
                set => 
                    base.SetValue(SelectAllNameProperty, value);
            }
        }

        private class SelectAllCheckedListBoxEditStyleSettings : CheckedListBoxEditStyleSettings
        {
            public static readonly DependencyProperty SelectAllNameProperty = DependencyProperty.Register("SelectAllName", typeof(string), typeof(FilterListEditorBehavior.SelectAllCheckedListBoxEditStyleSettings), new PropertyMetadata("All"));

            protected internal override IEnumerable<CustomItem> GetCustomItems(ListBoxEdit editor)
            {
                SelectAllItem item = new SelectAllItem();
                item.DisplayText = this.SelectAllName;
                List<CustomItem> list1 = new List<CustomItem>();
                list1.Add(item);
                return list1;
            }

            protected override bool ShowCustomItemInternal(ListBoxEdit editor) => 
                true;

            public string SelectAllName
            {
                get => 
                    (string) base.GetValue(SelectAllNameProperty);
                set => 
                    base.SetValue(SelectAllNameProperty, value);
            }
        }

        private class SelectAllRadioListBoxEditStyleSettings : RadioListBoxEditStyleSettings
        {
            public static readonly DependencyProperty SelectAllNameProperty = FilterListEditorBehavior.SelectAllCheckedListBoxEditStyleSettings.SelectAllNameProperty.AddOwner(typeof(RadioListBoxEditStyleSettings));

            protected internal override IEnumerable<CustomItem> GetCustomItems(ListBoxEdit editor)
            {
                string selectAllName = this.SelectAllName;
                RadioEmptyItem item = new RadioEmptyItem();
                List<CustomItem> list1 = new List<CustomItem>();
                string text2 = selectAllName;
                if (selectAllName == null)
                {
                    string local1 = selectAllName;
                    text2 = EditorLocalizer.GetString(EditorStringId.EmptyItem);
                }
                item.DisplayText = text2;
                list1.Add(item);
                return list1;
            }

            protected override bool ShowCustomItemInternal(ListBoxEdit editor) => 
                true;

            public string SelectAllName
            {
                get => 
                    (string) base.GetValue(SelectAllNameProperty);
                set => 
                    base.SetValue(SelectAllNameProperty, value);
            }
        }
    }
}

