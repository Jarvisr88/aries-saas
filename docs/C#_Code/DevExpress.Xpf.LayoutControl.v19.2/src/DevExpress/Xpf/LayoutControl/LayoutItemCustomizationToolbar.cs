namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    [TemplatePart(Name="AddNewItemElement", Type=typeof(Button)), TemplatePart(Name="HorizontalAlignmentElement", Type=typeof(LayoutItemAlignmentControl)), TemplatePart(Name="VerticalAlignmentElement", Type=typeof(LayoutItemAlignmentControl)), TemplatePart(Name="MoveItemBackwardElement", Type=typeof(Button)), TemplatePart(Name="MoveItemForwardElement", Type=typeof(Button)), TemplatePart(Name="MoveToAvailableItemsElement", Type=typeof(Button)), TemplatePart(Name="RenameElement", Type=typeof(Button)), TemplatePart(Name="RenamingEditElement", Type=typeof(TextBox)), TemplatePart(Name="SelectParentElement", Type=typeof(Button))]
    public class LayoutItemCustomizationToolbar : ControlBase
    {
        public static readonly DependencyProperty AvailableItemsUIVisibilityProperty = DependencyProperty.Register("AvailableItemsUIVisibility", typeof(Visibility), typeof(LayoutItemCustomizationToolbar), null);
        public static readonly DependencyProperty CanMoveItemBackwardProperty = DependencyProperty.Register("CanMoveItemBackward", typeof(bool), typeof(LayoutItemCustomizationToolbar), null);
        public static readonly DependencyProperty CanMoveItemForwardProperty = DependencyProperty.Register("CanMoveItemForward", typeof(bool), typeof(LayoutItemCustomizationToolbar), null);
        public static readonly DependencyProperty ItemHeaderProperty;
        public static readonly DependencyProperty ItemMovingDirectionProperty;
        public static readonly DependencyProperty ItemMovingBackwardDirectionProperty;
        public static readonly DependencyProperty ItemMovingForwardDirectionProperty;
        public static readonly DependencyProperty ItemMovingUIVisibilityProperty;
        public static readonly DependencyProperty ItemRenamingUIVisibilityProperty;
        public static readonly DependencyProperty NewItemsUIVisibilityProperty;
        private HorizontalAlignment? _ItemHorizontalAlignment;
        private VerticalAlignment? _ItemVerticalAlignment;
        private bool buttonWasPressed;
        private const string AddNewItemElementName = "AddNewItemElement";
        private const string HorizontalAlignmentElementName = "HorizontalAlignmentElement";
        private const string VerticalAlignmentElementName = "VerticalAlignmentElement";
        private const string MoveItemBackwardElementName = "MoveItemBackwardElement";
        private const string MoveItemForwardElementName = "MoveItemForwardElement";
        private const string MoveToAvailableItemsElementName = "MoveToAvailableItemsElement";
        private const string RenameElementName = "RenameElement";
        private const string RenamingEditElementName = "RenamingEditElement";
        private const string SelectParentElementName = "SelectParentElement";
        private object _OriginalItemHeader;
        private object _StoredRenameElementVisibility;

        public event Action AddNewItem;

        public event Action HideItemParentIndicator;

        public event Action ItemHeaderChanged;

        public event Action ItemHorizontalAlignmentChanged;

        public event Action ItemVerticalAlignmentChanged;

        public event Action<bool> MoveItem;

        public event Action MoveItemToAvailableItems;

        public event Action ReturnFocus;

        public event Action SelectItemParent;

        public event Action ShowItemParentIndicator;

        static LayoutItemCustomizationToolbar()
        {
            ItemHeaderProperty = DependencyProperty.Register("ItemHeader", typeof(object), typeof(LayoutItemCustomizationToolbar), new PropertyMetadata((o, e) => ((LayoutItemCustomizationToolbar) o).OnItemHeaderChanged()));
            ItemMovingDirectionProperty = DependencyProperty.Register("ItemMovingDirection", typeof(Orientation), typeof(LayoutItemCustomizationToolbar), new PropertyMetadata((o, e) => ((LayoutItemCustomizationToolbar) o).OnItemMovingDirectionChanged()));
            ItemMovingBackwardDirectionProperty = DependencyProperty.Register("ItemMovingBackwardDirection", typeof(DevExpress.Xpf.Core.Side), typeof(LayoutItemCustomizationToolbar), null);
            ItemMovingForwardDirectionProperty = DependencyProperty.Register("ItemMovingForwardDirection", typeof(DevExpress.Xpf.Core.Side), typeof(LayoutItemCustomizationToolbar), null);
            ItemMovingUIVisibilityProperty = DependencyProperty.Register("ItemMovingUIVisibility", typeof(Visibility), typeof(LayoutItemCustomizationToolbar), null);
            ItemRenamingUIVisibilityProperty = DependencyProperty.Register("ItemRenamingUIVisibility", typeof(Visibility), typeof(LayoutItemCustomizationToolbar), null);
            NewItemsUIVisibilityProperty = DependencyProperty.Register("NewItemsUIVisibility", typeof(Visibility), typeof(LayoutItemCustomizationToolbar), null);
        }

        public LayoutItemCustomizationToolbar()
        {
            base.DefaultStyleKey = typeof(LayoutItemCustomizationToolbar);
            this.OnItemMovingDirectionChanged();
        }

        private void HideRenamingEdit(bool accept)
        {
            if ((this.RenamingEditElement != null) && (this.RenamingEditElement.Visibility != Visibility.Collapsed))
            {
                this.RenamingEditElement.Visibility = Visibility.Collapsed;
                this.RenamingEditElement.ClearValue(TextBox.TextProperty);
                if ((this.RenameElement != null) && (this._StoredRenameElementVisibility != null))
                {
                    this.RenameElement.RestorePropertyValue(UIElement.VisibilityProperty, this._StoredRenameElementVisibility);
                    this._StoredRenameElementVisibility = null;
                }
                if (!accept)
                {
                    this.ItemHeader = this._OriginalItemHeader;
                }
            }
        }

        protected virtual void OnAddNewItem()
        {
            if (this.AddNewItem != null)
            {
                this.AddNewItem();
            }
        }

        private void OnAddNewItemElementClick(object sender, RoutedEventArgs e)
        {
            this.OnAddNewItem();
        }

        public override void OnApplyTemplate()
        {
            if (this.AddNewItemElement != null)
            {
                this.AddNewItemElement.Click -= new RoutedEventHandler(this.OnAddNewItemElementClick);
            }
            if (this.HorizontalAlignmentElement != null)
            {
                this.HorizontalAlignmentElement.AlignmentChanged -= new Action(this.OnHorizontalAlignmentElementAlignmentChanged);
            }
            if (this.VerticalAlignmentElement != null)
            {
                this.VerticalAlignmentElement.AlignmentChanged -= new Action(this.OnVerticalAlignmentElementAlignmentChanged);
            }
            if (this.MoveItemBackwardElement != null)
            {
                this.MoveItemBackwardElement.Click -= new RoutedEventHandler(this.OnMoveItemBackwardElementClick);
            }
            if (this.MoveItemForwardElement != null)
            {
                this.MoveItemForwardElement.Click -= new RoutedEventHandler(this.OnMoveItemForwardElementClick);
            }
            if (this.MoveToAvailableItemsElement != null)
            {
                this.MoveToAvailableItemsElement.Click -= new RoutedEventHandler(this.OnMoveToAvailableItemsElementClick);
            }
            if (this.RenameElement != null)
            {
                this.RenameElement.Click -= new RoutedEventHandler(this.OnRenameElementClick);
            }
            if (this.RenamingEditElement != null)
            {
                this.RenamingEditElement.KeyDown -= new KeyEventHandler(this.OnRenamingEditElementKeyDown);
                this.RenamingEditElement.LostFocus -= new RoutedEventHandler(this.OnRenamingEditElementLostFocus);
                this.RenamingEditElement.TextChanged -= new TextChangedEventHandler(this.OnRenamingEditElementTextChanged);
            }
            if (this.SelectParentElement != null)
            {
                this.SelectParentElement.Click -= new RoutedEventHandler(this.OnSelectParentElementClick);
                this.SelectParentElement.MouseEnter -= new MouseEventHandler(this.OnSelectParentElementMouseEnter);
                this.SelectParentElement.MouseLeave -= new MouseEventHandler(this.OnSelectParentElementMouseLeave);
            }
            base.OnApplyTemplate();
            this.AddNewItemElement = base.GetTemplateChild("AddNewItemElement") as Button;
            this.HorizontalAlignmentElement = base.GetTemplateChild("HorizontalAlignmentElement") as LayoutItemAlignmentControl;
            this.VerticalAlignmentElement = base.GetTemplateChild("VerticalAlignmentElement") as LayoutItemAlignmentControl;
            this.MoveItemBackwardElement = base.GetTemplateChild("MoveItemBackwardElement") as Button;
            this.MoveItemForwardElement = base.GetTemplateChild("MoveItemForwardElement") as Button;
            this.MoveToAvailableItemsElement = base.GetTemplateChild("MoveToAvailableItemsElement") as Button;
            this.RenameElement = base.GetTemplateChild("RenameElement") as Button;
            this.RenamingEditElement = base.GetTemplateChild("RenamingEditElement") as TextBox;
            this.SelectParentElement = base.GetTemplateChild("SelectParentElement") as Button;
            if (this.AddNewItemElement != null)
            {
                this.AddNewItemElement.Click += new RoutedEventHandler(this.OnAddNewItemElementClick);
            }
            if (this.HorizontalAlignmentElement != null)
            {
                this.HorizontalAlignmentElement.AlignmentChanged += new Action(this.OnHorizontalAlignmentElementAlignmentChanged);
            }
            if (this.VerticalAlignmentElement != null)
            {
                this.VerticalAlignmentElement.AlignmentChanged += new Action(this.OnVerticalAlignmentElementAlignmentChanged);
            }
            if (this.MoveItemBackwardElement != null)
            {
                this.MoveItemBackwardElement.Click += new RoutedEventHandler(this.OnMoveItemBackwardElementClick);
            }
            if (this.MoveItemForwardElement != null)
            {
                this.MoveItemForwardElement.Click += new RoutedEventHandler(this.OnMoveItemForwardElementClick);
            }
            if (this.MoveToAvailableItemsElement != null)
            {
                this.MoveToAvailableItemsElement.Click += new RoutedEventHandler(this.OnMoveToAvailableItemsElementClick);
            }
            if (this.RenameElement != null)
            {
                this.RenameElement.Click += new RoutedEventHandler(this.OnRenameElementClick);
            }
            if (this.RenamingEditElement != null)
            {
                this.RenamingEditElement.KeyDown += new KeyEventHandler(this.OnRenamingEditElementKeyDown);
                this.RenamingEditElement.LostFocus += new RoutedEventHandler(this.OnRenamingEditElementLostFocus);
                this.RenamingEditElement.TextChanged += new TextChangedEventHandler(this.OnRenamingEditElementTextChanged);
            }
            if (this.SelectParentElement != null)
            {
                this.SelectParentElement.Click += new RoutedEventHandler(this.OnSelectParentElementClick);
                this.SelectParentElement.MouseEnter += new MouseEventHandler(this.OnSelectParentElementMouseEnter);
                this.SelectParentElement.MouseLeave += new MouseEventHandler(this.OnSelectParentElementMouseLeave);
            }
            this.UpdateTemplate();
        }

        internal void OnButtonPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.buttonWasPressed = true;
        }

        internal void OnButtonPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.buttonWasPressed = false;
            this.HideRenamingEdit(true);
        }

        protected internal virtual void OnHide()
        {
            this.OnHideItemParentIndicator();
        }

        protected virtual void OnHideItemParentIndicator()
        {
            if (this.HideItemParentIndicator != null)
            {
                this.HideItemParentIndicator();
            }
        }

        private void OnHorizontalAlignmentElementAlignmentChanged()
        {
            if (this.HorizontalAlignmentElement.Alignment != null)
            {
                this.ItemHorizontalAlignment = new HorizontalAlignment?(this.HorizontalAlignmentElement.Alignment.Value.GetHorizontalAlignment());
            }
            else
            {
                this.ItemHorizontalAlignment = null;
            }
        }

        protected virtual void OnItemHeaderChanged()
        {
            if (!this.IsInitializing && (this.ItemHeaderChanged != null))
            {
                this.ItemHeaderChanged();
            }
        }

        protected virtual void OnItemHorizontalAlignmentChanged()
        {
            if (!this.IsInitializing && (this.ItemHorizontalAlignmentChanged != null))
            {
                this.ItemHorizontalAlignmentChanged();
            }
        }

        protected virtual void OnItemMovingDirectionChanged()
        {
            this.ItemMovingBackwardDirection = (this.ItemMovingDirection == Orientation.Horizontal) ? DevExpress.Xpf.Core.Side.Left : DevExpress.Xpf.Core.Side.Top;
            this.ItemMovingForwardDirection = (this.ItemMovingDirection == Orientation.Horizontal) ? DevExpress.Xpf.Core.Side.LeftRight : DevExpress.Xpf.Core.Side.Bottom;
        }

        protected virtual void OnItemVerticalAlignmentChanged()
        {
            if (!this.IsInitializing && (this.ItemVerticalAlignmentChanged != null))
            {
                this.ItemVerticalAlignmentChanged();
            }
        }

        protected virtual void OnMoveItem(bool forward)
        {
            this.OnReturnFocus();
            if (this.MoveItem != null)
            {
                this.MoveItem(forward);
            }
        }

        private void OnMoveItemBackwardElementClick(object sender, RoutedEventArgs e)
        {
            this.OnMoveItem(false);
        }

        private void OnMoveItemForwardElementClick(object sender, RoutedEventArgs e)
        {
            this.OnMoveItem(true);
        }

        protected virtual void OnMoveItemToAvailableItems()
        {
            if (this.MoveItemToAvailableItems != null)
            {
                this.MoveItemToAvailableItems();
            }
        }

        private void OnMoveToAvailableItemsElementClick(object sender, RoutedEventArgs e)
        {
            this.OnMoveItemToAvailableItems();
        }

        private void OnRenameElementClick(object sender, RoutedEventArgs e)
        {
            this.OnRenameItem();
        }

        protected virtual void OnRenameItem()
        {
            this.ShowRenamingEdit();
        }

        private void OnRenamingEditElementKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Return) || (e.Key == Key.Escape))
            {
                this.HideRenamingEdit(e.Key == Key.Return);
                this.OnReturnFocus();
            }
        }

        private void OnRenamingEditElementLostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.buttonWasPressed)
            {
                this.HideRenamingEdit(true);
            }
        }

        private void OnRenamingEditElementTextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bindingExpression = this.RenamingEditElement.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }

        protected virtual void OnReturnFocus()
        {
            if (this.ReturnFocus != null)
            {
                this.ReturnFocus();
            }
        }

        protected virtual void OnSelectItemParent()
        {
            if (this.SelectItemParent != null)
            {
                this.SelectItemParent();
            }
        }

        private void OnSelectParentElementClick(object sender, RoutedEventArgs e)
        {
            this.OnHideItemParentIndicator();
            this.OnSelectItemParent();
        }

        private void OnSelectParentElementMouseEnter(object sender, MouseEventArgs e)
        {
            this.OnShowItemParentIndicator();
        }

        private void OnSelectParentElementMouseLeave(object sender, MouseEventArgs e)
        {
            this.OnHideItemParentIndicator();
        }

        protected virtual void OnShowItemParentIndicator()
        {
            if (this.ShowItemParentIndicator != null)
            {
                this.ShowItemParentIndicator();
            }
        }

        private void OnVerticalAlignmentElementAlignmentChanged()
        {
            if (this.VerticalAlignmentElement.Alignment != null)
            {
                this.ItemVerticalAlignment = new VerticalAlignment?(this.VerticalAlignmentElement.Alignment.Value.GetVerticalAlignment());
            }
            else
            {
                this.ItemVerticalAlignment = null;
            }
        }

        private void ShowRenamingEdit()
        {
            if ((this.RenamingEditElement != null) && (this.RenamingEditElement.Visibility != Visibility.Visible))
            {
                this._OriginalItemHeader = this.ItemHeader;
                Binding binding = new Binding("ItemHeader");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                this.RenamingEditElement.SetBinding(TextBox.TextProperty, binding);
                this.RenamingEditElement.SelectionStart = this.RenamingEditElement.Text.Length;
                if (this.RenameElement != null)
                {
                    this._StoredRenameElementVisibility = this.RenameElement.StorePropertyValue(UIElement.VisibilityProperty);
                    this.RenameElement.Visibility = Visibility.Collapsed;
                }
                this.RenamingEditElement.Visibility = Visibility.Visible;
                this.RenamingEditElement.Focus();
            }
        }

        protected virtual void UpdateTemplate()
        {
            ItemAlignment? nullable2;
            if (this.HorizontalAlignmentElement != null)
            {
                if (this.ItemHorizontalAlignment != null)
                {
                    this.HorizontalAlignmentElement.Alignment = new ItemAlignment?(this.ItemHorizontalAlignment.Value.GetItemAlignment());
                }
                else
                {
                    nullable2 = null;
                    this.HorizontalAlignmentElement.Alignment = nullable2;
                }
            }
            if (this.VerticalAlignmentElement != null)
            {
                if (this.ItemVerticalAlignment == null)
                {
                    nullable2 = null;
                    this.VerticalAlignmentElement.Alignment = nullable2;
                }
                else
                {
                    this.VerticalAlignmentElement.Alignment = new ItemAlignment?(this.ItemVerticalAlignment.Value.GetItemAlignment());
                }
            }
        }

        public Visibility AvailableItemsUIVisibility
        {
            get => 
                (Visibility) base.GetValue(AvailableItemsUIVisibilityProperty);
            set => 
                base.SetValue(AvailableItemsUIVisibilityProperty, value);
        }

        public bool CanMoveItemBackward
        {
            get => 
                (bool) base.GetValue(CanMoveItemBackwardProperty);
            set => 
                base.SetValue(CanMoveItemBackwardProperty, value);
        }

        public bool CanMoveItemForward
        {
            get => 
                (bool) base.GetValue(CanMoveItemForwardProperty);
            set => 
                base.SetValue(CanMoveItemForwardProperty, value);
        }

        public bool IsInitializing { get; set; }

        public object ItemHeader
        {
            get => 
                base.GetValue(ItemHeaderProperty);
            set => 
                base.SetValue(ItemHeaderProperty, value);
        }

        public HorizontalAlignment? ItemHorizontalAlignment
        {
            get => 
                this._ItemHorizontalAlignment;
            set
            {
                HorizontalAlignment? itemHorizontalAlignment = this.ItemHorizontalAlignment;
                HorizontalAlignment? nullable2 = value;
                if (!((itemHorizontalAlignment.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((itemHorizontalAlignment != null) == (nullable2 != null)) : false))
                {
                    this._ItemHorizontalAlignment = value;
                    this.UpdateTemplate();
                    this.OnItemHorizontalAlignmentChanged();
                }
            }
        }

        public VerticalAlignment? ItemVerticalAlignment
        {
            get => 
                this._ItemVerticalAlignment;
            set
            {
                VerticalAlignment? itemVerticalAlignment = this.ItemVerticalAlignment;
                VerticalAlignment? nullable2 = value;
                if (!((itemVerticalAlignment.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((itemVerticalAlignment != null) == (nullable2 != null)) : false))
                {
                    this._ItemVerticalAlignment = value;
                    this.UpdateTemplate();
                    this.OnItemVerticalAlignmentChanged();
                }
            }
        }

        public Orientation ItemMovingDirection
        {
            get => 
                (Orientation) base.GetValue(ItemMovingDirectionProperty);
            set => 
                base.SetValue(ItemMovingDirectionProperty, value);
        }

        public DevExpress.Xpf.Core.Side ItemMovingBackwardDirection
        {
            get => 
                (DevExpress.Xpf.Core.Side) base.GetValue(ItemMovingBackwardDirectionProperty);
            set => 
                base.SetValue(ItemMovingBackwardDirectionProperty, value);
        }

        public DevExpress.Xpf.Core.Side ItemMovingForwardDirection
        {
            get => 
                (DevExpress.Xpf.Core.Side) base.GetValue(ItemMovingForwardDirectionProperty);
            set => 
                base.SetValue(ItemMovingForwardDirectionProperty, value);
        }

        public Visibility ItemMovingUIVisibility
        {
            get => 
                (Visibility) base.GetValue(ItemMovingUIVisibilityProperty);
            set => 
                base.SetValue(ItemMovingUIVisibilityProperty, value);
        }

        public Visibility ItemRenamingUIVisibility
        {
            get => 
                (Visibility) base.GetValue(ItemRenamingUIVisibilityProperty);
            set => 
                base.SetValue(ItemRenamingUIVisibilityProperty, value);
        }

        public Visibility NewItemsUIVisibility
        {
            get => 
                (Visibility) base.GetValue(NewItemsUIVisibilityProperty);
            set => 
                base.SetValue(NewItemsUIVisibilityProperty, value);
        }

        protected Button AddNewItemElement { get; private set; }

        protected LayoutItemAlignmentControl HorizontalAlignmentElement { get; private set; }

        protected LayoutItemAlignmentControl VerticalAlignmentElement { get; private set; }

        protected Button MoveItemBackwardElement { get; private set; }

        protected Button MoveItemForwardElement { get; private set; }

        protected Button MoveToAvailableItemsElement { get; private set; }

        protected Button RenameElement { get; private set; }

        protected TextBox RenamingEditElement { get; private set; }

        protected Button SelectParentElement { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemCustomizationToolbar.<>c <>9 = new LayoutItemCustomizationToolbar.<>c();

            internal void <.cctor>b__163_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemCustomizationToolbar) o).OnItemHeaderChanged();
            }

            internal void <.cctor>b__163_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemCustomizationToolbar) o).OnItemMovingDirectionChanged();
            }
        }
    }
}

