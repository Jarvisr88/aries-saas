namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class ItemsControlBase : Control, IWeakEventListener
    {
        private static readonly ControlTemplate DefaultItemsPanel = XamlHelper.GetControlTemplate("<StackPanel/>");
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ContainerStyleProperty;
        private static readonly DependencyPropertyKey PanelPropertyKey;
        public static readonly DependencyProperty PanelProperty;
        public static readonly DependencyProperty ItemTemplateProperty;

        static ItemsControlBase()
        {
            Type ownerType = typeof(ItemsControlBase);
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(IEnumerable), ownerType, new PropertyMetadata(null, (d, e) => ((ItemsControlBase) d).OnItemsSourceChanged(e)));
            ItemsPanelProperty = DependencyPropertyManager.Register("ItemsPanel", typeof(ControlTemplate), ownerType, new PropertyMetadata(DefaultItemsPanel, (d, e) => ((ItemsControlBase) d).OnItemsPanelChanged(), (CoerceValueCallback) ((d, baseValue) => (baseValue ?? DefaultItemsPanel))));
            ContainerStyleProperty = DependencyPropertyManager.Register("ContainerStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((ItemsControlBase) d).InvalidateMeasure()));
            PanelPropertyKey = DependencyPropertyManager.RegisterReadOnly("Panel", typeof(System.Windows.Controls.Panel), ownerType, new PropertyMetadata(null));
            PanelProperty = PanelPropertyKey.DependencyProperty;
            ItemTemplateProperty = DependencyPropertyManager.Register("ItemTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((CachedItemsControl) d).OnItemTemplateChanged()));
        }

        public ItemsControlBase()
        {
            this.UpdateTemplate();
        }

        protected virtual FrameworkElement CreateChild(object item)
        {
            ContentPresenter presenter1 = new ContentPresenter();
            presenter1.ContentTemplate = this.ItemTemplate;
            presenter1.Content = item;
            return presenter1;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.ValidateVisualTree();
            return base.MeasureOverride(constraint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Panel = (VisualTreeHelper.GetChildrenCount(this) > 0) ? (VisualTreeHelper.GetChild(this, 0) as System.Windows.Controls.Panel) : null;
        }

        protected virtual void OnCollectionChanged()
        {
            base.InvalidateMeasure();
            if (this.Panel != null)
            {
                this.Panel.InvalidateMeasure();
            }
        }

        private void OnItemsPanelChanged()
        {
            this.UpdateTemplate();
        }

        protected virtual void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            INotifyCollectionChanged oldValue = e.OldValue as INotifyCollectionChanged;
            INotifyCollectionChanged newValue = e.NewValue as INotifyCollectionChanged;
            if (oldValue != null)
            {
                CollectionChangedEventManager.RemoveListener(oldValue, this);
            }
            if (newValue != null)
            {
                CollectionChangedEventManager.AddListener(newValue, this);
            }
            this.OnCollectionChanged();
        }

        protected virtual void OnItemTemplateChanged()
        {
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(CollectionChangedEventManager)))
            {
                return false;
            }
            this.OnCollectionChanged();
            return true;
        }

        private void UpdateTemplate()
        {
            base.Template = this.ItemsPanel;
        }

        protected abstract void ValidateVisualTree();

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        public ControlTemplate ItemsPanel
        {
            get => 
                (ControlTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        public Style ContainerStyle
        {
            get => 
                (Style) base.GetValue(ContainerStyleProperty);
            set => 
                base.SetValue(ContainerStyleProperty, value);
        }

        public System.Windows.Controls.Panel Panel
        {
            get => 
                (System.Windows.Controls.Panel) base.GetValue(PanelProperty);
            internal set => 
                base.SetValue(PanelPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsControlBase.<>c <>9 = new ItemsControlBase.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ItemsControlBase) d).OnItemsSourceChanged(e);
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ItemsControlBase) d).OnItemsPanelChanged();
            }

            internal object <.cctor>b__7_2(DependencyObject d, object baseValue) => 
                baseValue ?? ItemsControlBase.DefaultItemsPanel;

            internal void <.cctor>b__7_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ItemsControlBase) d).InvalidateMeasure();
            }

            internal void <.cctor>b__7_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CachedItemsControl) d).OnItemTemplateChanged();
            }
        }
    }
}

