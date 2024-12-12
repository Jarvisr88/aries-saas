namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomItem : DependencyObject, ICloneable, ITemplatedCustomItem, ICustomItem
    {
        public static readonly DependencyProperty DisplayTextProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly DependencyProperty EditValueProperty;
        private WeakReference ownerEditReference;

        static CustomItem()
        {
            Type ownerType = typeof(CustomItem);
            DisplayTextProperty = DependencyPropertyManager.Register("DisplayText", typeof(string), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CustomItem) d).UpdateProperties()));
            EditValueProperty = DependencyPropertyManager.Register("EditValue", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CustomItem) d).UpdateProperties()));
            ItemTemplateProperty = DependencyPropertyManager.Register("ItemTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CustomItem) d).UpdateProperties()));
            ItemContainerStyleProperty = DependencyPropertyManager.Register("ItemContainerStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CustomItem) d).UpdateProperties()));
        }

        public CustomItem()
        {
            this.UpdatePropertiesLocker = new Locker();
        }

        protected virtual void Assign(CustomItem item)
        {
            item.DisplayText = this.DisplayText;
            item.EditValue = this.EditValue;
            item.ItemContainerStyle = this.ItemContainerStyle;
            item.ItemTemplate = this.ItemTemplate;
        }

        void ITemplatedCustomItem.UpdateOwner(ISelectorEdit ownerEdit)
        {
            this.UpdateOwner(ownerEdit);
        }

        public static object FilterCustomItem(object item)
        {
            CustomItem item2 = item as CustomItem;
            return (((item2 == null) || !item2.ShouldFilter) ? item : null);
        }

        public static IEnumerable<object> FilterCustomItems(IEnumerable<object> items)
        {
            Func<IEnumerable<object>, IEnumerable<object>> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IEnumerable<object>, IEnumerable<object>> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = delegate (IEnumerable<object> x) {
                    Func<object, bool> predicate = <>c.<>9__0_1;
                    if (<>c.<>9__0_1 == null)
                    {
                        Func<object, bool> local1 = <>c.<>9__0_1;
                        predicate = <>c.<>9__0_1 = item => FilterCustomItem(item) != null;
                    }
                    return x.Where<object>(predicate);
                };
            }
            return items.With<IEnumerable<object>, IEnumerable<object>>(evaluator);
        }

        protected virtual ICustomItem GetCustomItem()
        {
            if (!this.IsPropertyAssigned(EditValueProperty))
            {
                return null;
            }
            EditorCustomItem item1 = new EditorCustomItem();
            item1.EditValue = this.EditValue;
            item1.DisplayValue = this.DisplayText;
            return item1;
        }

        protected string GetDisplayText() => 
            this.DisplayText ?? this.GetDisplayTextInternal();

        protected virtual string GetDisplayTextInternal() => 
            string.Empty;

        protected object GetEditValue() => 
            this.EditValue ?? this.GetEditValueInternal();

        protected virtual object GetEditValueInternal() => 
            null;

        protected virtual Style GetItemStyle() => 
            (this.ItemContainerStyle == null) ? this.GetItemStyleInternal() : this.ItemContainerStyle;

        protected virtual Style GetItemStyleInternal()
        {
            ISelectorEdit ownerEdit = this.OwnerEdit;
            return ((ownerEdit != null) ? ((ISelectorEditPropertyProvider) ((BaseEdit) ownerEdit).PropertyProvider).GetItemContainerStyle() : null);
        }

        protected virtual DataTemplate GetTemplate()
        {
            if (this.ItemTemplate != null)
            {
                return this.ItemTemplate;
            }
            FrameworkElement ownerEdit = (FrameworkElement) this.OwnerEdit;
            if (ownerEdit == null)
            {
                return null;
            }
            CustomItemThemeKeyExtension resourceKey = new CustomItemThemeKeyExtension();
            resourceKey.ResourceKey = CustomItemThemeKeys.DefaultTemplate;
            return (DataTemplate) ownerEdit.FindResource(resourceKey);
        }

        object ICloneable.Clone()
        {
            CustomItem item = (CustomItem) Activator.CreateInstance(base.GetType());
            this.Assign(item);
            return item;
        }

        protected internal virtual void UpdateOwner(ISelectorEdit ownerEdit)
        {
            if (!ReferenceEquals(this.OwnerEdit, ownerEdit))
            {
                this.OwnerEdit = ownerEdit;
                this.UpdateProperties();
            }
        }

        protected virtual void UpdateProperties()
        {
            if (this.OwnerEdit != null)
            {
                this.UpdatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                    this.DisplayText = this.GetDisplayText();
                    this.EditValue = this.GetEditValue();
                    this.ItemTemplate = this.GetTemplate();
                    this.ItemContainerStyle = this.GetItemStyle();
                });
            }
        }

        protected internal virtual bool ShouldFilter =>
            false;

        public string DisplayText
        {
            get => 
                (string) base.GetValue(DisplayTextProperty);
            set => 
                base.SetValue(DisplayTextProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        protected internal ISelectorEdit OwnerEdit
        {
            get => 
                (this.ownerEditReference != null) ? ((ISelectorEdit) this.ownerEditReference.Target) : null;
            set => 
                this.ownerEditReference = new WeakReference(value);
        }

        private Locker UpdatePropertiesLocker { get; set; }

        object ICustomItem.DisplayValue
        {
            get => 
                this.DisplayText;
            set => 
                this.DisplayText = Convert.ToString(value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomItem.<>c <>9 = new CustomItem.<>c();
            public static Func<object, bool> <>9__0_1;
            public static Func<IEnumerable<object>, IEnumerable<object>> <>9__0_0;

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CustomItem) d).UpdateProperties();
            }

            internal void <.cctor>b__6_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CustomItem) d).UpdateProperties();
            }

            internal void <.cctor>b__6_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CustomItem) d).UpdateProperties();
            }

            internal void <.cctor>b__6_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CustomItem) d).UpdateProperties();
            }

            internal IEnumerable<object> <FilterCustomItems>b__0_0(IEnumerable<object> x)
            {
                Func<object, bool> predicate = <>9__0_1;
                if (<>9__0_1 == null)
                {
                    Func<object, bool> local1 = <>9__0_1;
                    predicate = <>9__0_1 = item => CustomItem.FilterCustomItem(item) != null;
                }
                return x.Where<object>(predicate);
            }

            internal bool <FilterCustomItems>b__0_1(object item) => 
                CustomItem.FilterCustomItem(item) != null;
        }
    }
}

