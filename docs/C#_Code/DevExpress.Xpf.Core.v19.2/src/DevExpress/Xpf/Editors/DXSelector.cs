namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class DXSelector : DXItemsControl
    {
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty SelectedValueProperty;
        public static readonly DependencyProperty SelectedValuePathProperty;
        private readonly Locker coercionLocker = new Locker();

        public event EventHandler SelectionChanged;

        static DXSelector()
        {
            Type ownerType = typeof(DXSelector);
            SelectedIndexProperty = DependencyPropertyManager.Register("SelectedIndex", typeof(int), ownerType, new FrameworkPropertyMetadata(-2147483648, (d, e) => ((DXSelector) d).SelectedIndexChanged((int) e.NewValue), (CoerceValueCallback) ((d, value) => ((DXSelector) d).CoerceSelectedIndex((int) value))));
            SelectedItemProperty = DependencyPropertyManager.Register("SelectedItem", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DXSelector) d).SelectedItemChanged(e.NewValue), (d, value) => ((DXSelector) d).CoerceSelectedItem(value)));
            SelectedValueProperty = DependencyPropertyManager.Register("SelectedValue", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DXSelector) d).SelectedValueChanged(e.NewValue), (d, value) => ((DXSelector) d).CoerceSelectedValue(value)));
            SelectedValuePathProperty = DependencyPropertyManager.Register("SelectedValuePath", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((DXSelector) d).SelectedValuePathChanged((string) e.NewValue), (CoerceValueCallback) ((d, value) => ((DXSelector) d).CoerceSelectedValuePath((string) value))));
        }

        public DXSelector()
        {
            this.DataAccessDescriptor = new SelectorPropertyDescriptor(string.Empty);
        }

        protected virtual void BringToView()
        {
        }

        protected virtual int CoerceSelectedIndex(int value)
        {
            if (!base.IsInitialized || this.coercionLocker.IsLocked)
            {
                return value;
            }
            int index = base.CanGenerateItem(value) ? value : -2147483648;
            this.coercionLocker.DoLockedActionIfNotLocked(delegate {
                object item = this.GetItem(index);
                this.SelectedItem = item;
                this.SelectedValue = this.DataAccessDescriptor.GetValue(item);
            });
            return index;
        }

        protected virtual object CoerceSelectedItem(object value)
        {
            if (this.coercionLocker.IsLocked)
            {
                return value;
            }
            int index = base.GetIndex(value);
            object item = base.GetItem(index);
            this.coercionLocker.DoLockedActionIfNotLocked(delegate {
                this.SelectedIndex = index;
                this.SelectedValue = this.DataAccessDescriptor.GetValue(item);
            });
            return item;
        }

        protected virtual object CoerceSelectedValue(object value)
        {
            if (this.coercionLocker.IsLocked)
            {
                return value;
            }
            int index = base.GetIndex(value, x => Equals(this.DataAccessDescriptor.GetValue(x), value));
            this.coercionLocker.DoLockedActionIfNotLocked(delegate {
                this.SelectedIndex = index;
                this.SelectedItem = this.GetItem(index);
            });
            return ((index >= 0) ? value : null);
        }

        protected virtual string CoerceSelectedValuePath(string path)
        {
            this.DataAccessDescriptor = new SelectorPropertyDescriptor(path);
            if (base.IsInitialized)
            {
                this.coercionLocker.DoLockedActionIfNotLocked(delegate {
                    int selectedIndex = this.SelectedIndex;
                    object item = base.GetItem(selectedIndex);
                    this.SelectedItem = item;
                    this.SelectedValue = this.DataAccessDescriptor.GetValue(item);
                });
            }
            return path;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            base.CoerceValue(SelectedIndexProperty);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.BringToView();
        }

        protected override void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            base.OnScrollChanged(sender, e);
        }

        protected override void OnViewChanged(object sender, ViewChangedEventArgs e)
        {
            base.OnViewChanged(sender, e);
            if (base.IsLoaded && !e.IsIntermediate)
            {
                double offset = base.Panel.Offset;
                this.SelectedIndex = base.Panel.IndexCalculator.LogicalOffsetToIndex(offset, base.GetItemsCount(), base.Panel.IsLooped);
            }
        }

        private void RaiseSelectionChanged()
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void SelectedIndexChanged(int newValue)
        {
        }

        protected virtual void SelectedItemChanged(object newValue)
        {
        }

        protected virtual void SelectedValueChanged(object value)
        {
            this.RaiseSelectionChanged();
        }

        protected virtual void SelectedValuePathChanged(string path)
        {
        }

        public int SelectedIndex
        {
            get => 
                (int) base.GetValue(SelectedIndexProperty);
            set => 
                base.SetValue(SelectedIndexProperty, value);
        }

        public object SelectedItem
        {
            get => 
                base.GetValue(SelectedItemProperty);
            set => 
                base.SetValue(SelectedItemProperty, value);
        }

        public object SelectedValue
        {
            get => 
                base.GetValue(SelectedValueProperty);
            set => 
                base.SetValue(SelectedValueProperty, value);
        }

        public string SelectedValuePath
        {
            get => 
                (string) base.GetValue(SelectedValuePathProperty);
            set => 
                base.SetValue(SelectedValuePathProperty, value);
        }

        private PropertyDescriptor DataAccessDescriptor { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXSelector.<>c <>9 = new DXSelector.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXSelector) d).SelectedIndexChanged((int) e.NewValue);
            }

            internal object <.cctor>b__4_1(DependencyObject d, object value) => 
                ((DXSelector) d).CoerceSelectedIndex((int) value);

            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXSelector) d).SelectedItemChanged(e.NewValue);
            }

            internal object <.cctor>b__4_3(DependencyObject d, object value) => 
                ((DXSelector) d).CoerceSelectedItem(value);

            internal void <.cctor>b__4_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXSelector) d).SelectedValueChanged(e.NewValue);
            }

            internal object <.cctor>b__4_5(DependencyObject d, object value) => 
                ((DXSelector) d).CoerceSelectedValue(value);

            internal void <.cctor>b__4_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXSelector) d).SelectedValuePathChanged((string) e.NewValue);
            }

            internal object <.cctor>b__4_7(DependencyObject d, object value) => 
                ((DXSelector) d).CoerceSelectedValuePath((string) value);
        }
    }
}

