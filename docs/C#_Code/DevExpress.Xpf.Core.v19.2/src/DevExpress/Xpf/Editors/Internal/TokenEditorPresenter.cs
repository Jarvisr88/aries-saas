namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TokenEditorPresenter : Control
    {
        public static readonly DependencyProperty IsTokenFocusedProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        private static readonly DependencyPropertyKey BorderTemplatePropertyKey;
        public static readonly DependencyProperty BorderTemplateProperty;
        public static readonly DependencyProperty ActiveEditorStyleProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        public static readonly DependencyProperty NullTextProperty;
        public static readonly DependencyProperty ShowButtonsProperty;
        public static readonly DependencyProperty ItemProperty;
        public static readonly DependencyProperty OwnerPresenterProperty;
        private static readonly DependencyPropertyKey OwnerPresenterPropertyKey;
        public static readonly DependencyProperty IsEditorActivatedProperty;
        public static readonly DependencyProperty HasNullTextProperty;
        public static readonly DependencyProperty NewTokenTextProperty;
        public static readonly DependencyProperty IsTokenEditorReadOnlyProperty;
        public static readonly DependencyProperty IsNewTokenEditorPresenterProperty;
        public static readonly DependencyProperty IsTextEditableProperty;
        private TokenItemData itemData;

        static TokenEditorPresenter()
        {
            Type ownerType = typeof(TokenEditorPresenter);
            IsTokenFocusedProperty = DependencyProperty.Register("IsTokenFocused", typeof(bool), ownerType, new FrameworkPropertyMetadata((d, e) => ((TokenEditorPresenter) d).OnIsTokenFocusedChanged()));
            BorderTemplatePropertyKey = DependencyProperty.RegisterReadOnly("BorderTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            BorderTemplateProperty = BorderTemplatePropertyKey.DependencyProperty;
            ActiveEditorStyleProperty = DependencyProperty.Register("ActiveEditorStyle", typeof(Style), ownerType);
            ShowBorderProperty = DependencyProperty.Register("ShowBorder", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowButtonsProperty = DependencyProperty.Register("ShowButtons", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            NullTextProperty = DependencyProperty.Register("NullText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((TokenEditorPresenter) d).OnNullTextChanged()));
            ItemProperty = DependencyProperty.Register("Item", typeof(CustomItem), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((TokenEditorPresenter) d).OnItemChanged()));
            OwnerPresenterPropertyKey = DependencyProperty.RegisterAttachedReadOnly("OwnerPresenter", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            OwnerPresenterProperty = OwnerPresenterPropertyKey.DependencyProperty;
            IsEditorActivatedProperty = DependencyProperty.Register("IsEditorActivated", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((TokenEditorPresenter) d).OnIsEditorActivatedChanged()));
            HasNullTextProperty = DependencyProperty.Register("HasNullText", typeof(bool), ownerType);
            NewTokenTextProperty = DependencyProperty.Register("NewTokenText", typeof(string), ownerType);
            IsNewTokenEditorPresenterProperty = DependencyProperty.Register("IsNewTokenEditorPresenter", typeof(bool), ownerType);
            IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), ownerType);
            IsTokenEditorReadOnlyProperty = DependencyProperty.Register("IsTokenEditorReadOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsTextEditableProperty = DependencyProperty.Register("IsTextEditable", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
        }

        public TokenEditorPresenter()
        {
            SetOwnerPresenter(this, this);
            base.DefaultStyleKey = typeof(TokenEditorPresenter);
        }

        private void AssignItemData(CustomItem item)
        {
            this.ItemData.Value = item.EditValue;
            this.ItemData.DisplayText = item.DisplayText;
        }

        public void Clear()
        {
            this.IsTokenFocused = this.IsEditorActivated = false;
        }

        internal void CommitEditor()
        {
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.CommitEditor(true);
            }
        }

        private void CreateEditor()
        {
            this.InplaceEditor = this.GetCellEditor();
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.SetPresenterOwner(this);
            }
        }

        private TokenItemData CreateItemData(CustomItem value)
        {
            TokenItemData data1 = new TokenItemData();
            data1.Value = value.EditValue;
            data1.DisplayText = value.DisplayText;
            data1.Settings = this.Owner.GetEditSettings(this);
            return data1;
        }

        private void CreateOwner()
        {
            this.Owner = LayoutHelper.FindLayoutOrVisualParentObject<TokenEditor>(this, true, null);
            Binding binding1 = new Binding("IsReadOnly");
            binding1.Source = this.Owner;
            binding1.Mode = BindingMode.OneWay;
            Binding binding = binding1;
            base.SetBinding(IsTokenEditorReadOnlyProperty, binding);
        }

        internal void FocusEditCore()
        {
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.FocusEditCore();
            }
        }

        private CellEditor GetCellEditor() => 
            LayoutHelper.FindElementByType(this, typeof(CellEditor)) as CellEditor;

        public static TokenEditorPresenter GetOwnerPresenter(DependencyObject element) => 
            (element != null) ? ((TokenEditorPresenter) element.GetValue(OwnerPresenterProperty)) : null;

        public void HideEditor()
        {
            base.SetCurrentValue(IsEditorActivatedProperty, false);
            this.InplaceEditor.HideEditor(true);
        }

        private void InitializeEditor()
        {
            this.UpdateEditor();
        }

        public bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            (this.InplaceEditor != null) && this.InplaceEditor.IsActivatingKey(key, modifiers);

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.Measure(constraint);
            }
            return base.MeasureOverride(constraint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CreateEditor();
            this.CreateOwner();
            this.UpdateItemData(this.Item);
            this.InitializeEditor();
        }

        private void OnIsEditorActivatedChanged()
        {
            this.UpdateEditor();
            if (this.Owner.CanActivateToken() && this.IsEditorActivated)
            {
                this.InplaceEditor.IsEditorFocused = true;
            }
            else
            {
                this.InplaceEditor.IsEditorFocused = false;
            }
        }

        private void OnIsTokenFocusedChanged()
        {
            if (!this.IsTokenFocused)
            {
                this.IsEditorActivated = false;
            }
            else
            {
                this.Owner.MakeVisibleToken(this);
            }
        }

        protected virtual void OnItemChanged()
        {
            base.DataContext = this.Item;
            this.UpdateItemData(this.Item);
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.InvalidateMeasure();
            }
        }

        private void OnItemDataChanged()
        {
            this.UpdateEditor();
        }

        protected virtual void OnNullTextChanged()
        {
            this.HasNullText = !string.IsNullOrEmpty(this.NullText);
            if (this.HasItemData)
            {
                this.AssignItemData(this.Item);
            }
        }

        public void ProcessActivatingKey(KeyEventArgs e)
        {
            if (this.InplaceEditor != null)
            {
                base.SetCurrentValue(IsEditorActivatedProperty, true);
            }
        }

        internal static void SetOwnerPresenter(DependencyObject element, TokenEditorPresenter value)
        {
            if (element != null)
            {
                element.SetValue(OwnerPresenterPropertyKey, value);
            }
        }

        public void UpdateEditor()
        {
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.ItemData = this.ItemData;
                this.InplaceEditor.OwnerTokenEditor = this.Owner;
                this.InplaceEditor.IsTokenFocused = this.IsTokenFocused;
            }
        }

        public void UpdateEditorEditValue()
        {
            if (this.InplaceEditor != null)
            {
                this.InplaceEditor.UpdateEditCoreEditValue();
            }
        }

        private void UpdateItemData(CustomItem item)
        {
            if ((this.Owner != null) && (item != null))
            {
                if (!this.HasItemData)
                {
                    this.ItemData = this.CreateItemData(item);
                }
                else
                {
                    this.AssignItemData(item);
                }
                this.UpdateEditorEditValue();
            }
        }

        public bool IsTextEditable
        {
            get => 
                (bool) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        public bool IsTokenEditorReadOnly
        {
            get => 
                (bool) base.GetValue(IsTokenEditorReadOnlyProperty);
            set => 
                base.SetValue(IsTokenEditorReadOnlyProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        public bool IsNewTokenEditorPresenter
        {
            get => 
                (bool) base.GetValue(IsNewTokenEditorPresenterProperty);
            set => 
                base.SetValue(IsNewTokenEditorPresenterProperty, value);
        }

        public string NewTokenText
        {
            get => 
                (string) base.GetValue(NewTokenTextProperty);
            set => 
                base.SetValue(NewTokenTextProperty, value);
        }

        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        public bool IsTokenFocused
        {
            get => 
                (bool) base.GetValue(IsTokenFocusedProperty);
            set => 
                base.SetValue(IsTokenFocusedProperty, value);
        }

        public bool ShowButtons
        {
            get => 
                (bool) base.GetValue(ShowButtonsProperty);
            set => 
                base.SetValue(ShowButtonsProperty, value);
        }

        public TokenItemData ItemData
        {
            get => 
                this.itemData;
            private set
            {
                if (!ReferenceEquals(this.itemData, value))
                {
                    this.itemData = value;
                    this.OnItemDataChanged();
                }
            }
        }

        public ControlTemplate BorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(BorderTemplateProperty);
            internal set => 
                base.SetValue(BorderTemplatePropertyKey, value);
        }

        public Style ActiveEditorStyle
        {
            get => 
                (Style) base.GetValue(ActiveEditorStyleProperty);
            set => 
                base.SetValue(ActiveEditorStyleProperty, value);
        }

        public string NullText
        {
            get => 
                (string) base.GetValue(NullTextProperty);
            set => 
                base.SetValue(NullTextProperty, value);
        }

        public CustomItem Item
        {
            get => 
                (CustomItem) base.GetValue(ItemProperty);
            set => 
                base.SetValue(ItemProperty, value);
        }

        public bool IsEditorActivated
        {
            get => 
                (bool) base.GetValue(IsEditorActivatedProperty);
            set => 
                base.SetValue(IsEditorActivatedProperty, value);
        }

        public bool HasNullText
        {
            get => 
                (bool) base.GetValue(HasNullTextProperty);
            set => 
                base.SetValue(HasNullTextProperty, value);
        }

        public Brush NullTextForeground
        {
            get
            {
                Func<TokenEditor, Brush> evaluator = <>c.<>9__66_0;
                if (<>c.<>9__66_0 == null)
                {
                    Func<TokenEditor, Brush> local1 = <>c.<>9__66_0;
                    evaluator = <>c.<>9__66_0 = x => x.NullTextForeground;
                }
                return this.Owner.Return<TokenEditor, Brush>(evaluator, (<>c.<>9__66_1 ??= () => Brushes.Black));
            }
        }

        public CellEditor InplaceEditor { get; private set; }

        private TokenEditor Owner { get; set; }

        private bool HasItemData =>
            this.ItemData != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditorPresenter.<>c <>9 = new TokenEditorPresenter.<>c();
            public static Func<TokenEditor, Brush> <>9__66_0;
            public static Func<Brush> <>9__66_1;

            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPresenter) d).OnIsTokenFocusedChanged();
            }

            internal void <.cctor>b__17_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPresenter) d).OnNullTextChanged();
            }

            internal void <.cctor>b__17_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPresenter) d).OnItemChanged();
            }

            internal void <.cctor>b__17_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPresenter) d).OnIsEditorActivatedChanged();
            }

            internal Brush <get_NullTextForeground>b__66_0(TokenEditor x) => 
                x.NullTextForeground;

            internal Brush <get_NullTextForeground>b__66_1() => 
                Brushes.Black;
        }
    }
}

