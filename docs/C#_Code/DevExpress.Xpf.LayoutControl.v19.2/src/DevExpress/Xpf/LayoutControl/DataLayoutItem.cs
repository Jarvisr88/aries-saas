namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Mvvm.UI.ViewGenerator.Metadata;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class DataLayoutItem : LayoutItem
    {
        public static readonly DependencyProperty UnderlyingObjectProperty = DependencyProperty.RegisterAttached("UnderlyingObject", typeof(object), typeof(DataLayoutItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        [DXHelpExclude(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty AttributeSettingsProperty = DependencyProperty.RegisterAttached("AttributeSettings", typeof(DataLayoutItemAttributeSettings), typeof(DataLayoutItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty ApplySettingsForCustomContentProperty;
        private System.Windows.Data.Binding _Binding;
        private DevExpress.Xpf.LayoutControl.DataLayoutControl _DataLayoutControl;
        private bool _IsActuallyReadOnly;
        private WeakReference _DataLayoutControlReference;
        internal DataTemplate editorTemplate;
        private BindingExpressionBase dataContextBindingExpression;
        private BindingExpressionBase addColonToItemLabelsBindingExpression;
        private DevExpress.Mvvm.Native.PropertyValidator PropertyValidator;
        private DispatcherOperation editorLostKeyboardOperation;
        private BindingExpression _contentBinding;
        private readonly TextBlock emptyItemDesignTimeMessage;
        private readonly Locker labelLocker;
        private readonly Locker contentLocker;

        static DataLayoutItem()
        {
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DataLayoutItem), new PropertyMetadata((o, e) => ((DataLayoutItem) o).OnIsReadOnlyChanged()));
            ApplySettingsForCustomContentProperty = DependencyProperty.Register("ApplySettingsForCustomContent", typeof(bool), typeof(DataLayoutItem), new PropertyMetadata(false));
        }

        public DataLayoutItem()
        {
            TextBlock block1 = new TextBlock();
            block1.Text = "No data source: set Binding";
            this.emptyItemDesignTimeMessage = block1;
            this.labelLocker = new Locker();
            this.contentLocker = new Locker();
            this.UpdateIsActuallyReadOnly();
            base.UpdateIsActuallyRequired();
            this.ShowEmptyItemDesignTimeMessage();
            base.DataContextChanged += (sender, e) => this.OnDataContextChanged(e.OldValue, e.NewValue);
        }

        protected void CheckIsActuallyReadOnlyChanged()
        {
            if (this.IsActuallyReadOnly != this._IsActuallyReadOnly)
            {
                this._IsActuallyReadOnly = this.IsActuallyReadOnly;
                this.OnIsActuallyReadOnlyChanged();
            }
        }

        private void ClearDataLayoutControlReference()
        {
            if (this._DataLayoutControlReference != null)
            {
                this._DataLayoutControlReference = null;
                if (ReferenceEquals(this.dataContextBindingExpression, base.ReadLocalValue(FrameworkElement.DataContextProperty)))
                {
                    base.ClearValue(FrameworkElement.DataContextProperty);
                }
                if (ReferenceEquals(this.addColonToItemLabelsBindingExpression, base.ReadLocalValue(LayoutItem.AddColonToLabelProperty)))
                {
                    base.ClearValue(LayoutItem.AddColonToLabelProperty);
                }
            }
        }

        protected void ClearUI()
        {
            this.PropertyInfo = null;
            this.UpdateIsActuallyReadOnly();
            base.UpdateIsActuallyRequired();
            using (this.labelLocker.Lock())
            {
                if (IsPropertyHasDefaultValue(this, LayoutItem.LabelProperty))
                {
                    base.ClearValue(LayoutItem.LabelProperty);
                }
            }
            if (IsPropertyHasDefaultValue(this, FrameworkElement.ToolTipProperty))
            {
                base.ClearValue(FrameworkElement.ToolTipProperty);
            }
            using (this.contentLocker.Lock())
            {
                if (IsPropertyHasDefaultValue(this, LayoutItem.ContentProperty))
                {
                    this.UnsubscribeEditorEvents(base.Content);
                    base.ClearValue(LayoutItem.ContentProperty);
                    this._contentBinding = null;
                }
                this.FinalizeUI();
            }
            if (base.Content == null)
            {
                this.ShowEmptyItemDesignTimeMessage();
            }
        }

        protected virtual FrameworkElement CreateContentAndInitializeUI()
        {
            DataLayoutItemGenerator generator = new DataLayoutItemGenerator(this, true);
            EditorsSource.GenerateEditor(this.PropertyInfo, generator, null, null, false, false, false);
            return generator.Content;
        }

        protected virtual System.Windows.Data.Binding CreateContentValueBinding(bool isReadOnly) => 
            this.CreateContentValueBindingCore(isReadOnly, true);

        private System.Windows.Data.Binding CreateContentValueBindingCore(bool isReadOnly, bool allowValidation)
        {
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
            if (this.Binding.Source != null)
            {
                binding.Source = this.Binding.Source;
            }
            if (this.Binding.Path != null)
            {
                binding.Path = this.Binding.Path;
            }
            binding.Mode = isReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            binding.Converter = this.Binding.Converter;
            binding.ConverterCulture = this.Binding.ConverterCulture;
            binding.ConverterParameter = this.Binding.ConverterParameter;
            binding.FallbackValue = this.Binding.FallbackValue;
            binding.StringFormat = this.Binding.StringFormat;
            binding.TargetNullValue = this.Binding.TargetNullValue;
            binding.UpdateSourceTrigger = this.Binding.UpdateSourceTrigger;
            if (allowValidation)
            {
                binding.ValidatesOnDataErrors = true;
                binding.ValidatesOnExceptions = true;
                binding.NotifyOnValidationError = true;
                binding.NotifyOnSourceUpdated = true;
            }
            return binding;
        }

        private void DoApplySettingsForCustomContent()
        {
            if (this.ApplySettingsForCustomContent)
            {
                DataLayoutItemGenerator generator = new DataLayoutItemGenerator(this, false);
                EditorsSource.GenerateEditor(this.PropertyInfo, generator, null, null, false, false, false);
            }
        }

        internal void DoValidate()
        {
            BaseEdit content = base.Content as BaseEdit;
            if ((content != null) && (content.IsLoaded && (this.PropertyValidator != null)))
            {
                Func<DevExpress.Xpf.LayoutControl.DataLayoutControl, object> evaluator = <>c.<>9__94_0;
                if (<>c.<>9__94_0 == null)
                {
                    Func<DevExpress.Xpf.LayoutControl.DataLayoutControl, object> local1 = <>c.<>9__94_0;
                    evaluator = <>c.<>9__94_0 = x => x.CurrentItem;
                }
                string errorText = this.PropertyValidator.GetErrorText(content.EditValue, this.GetDataLayoutControl().Return<DevExpress.Xpf.LayoutControl.DataLayoutControl, object>(evaluator, () => base.DataContext));
                if (string.IsNullOrEmpty(errorText))
                {
                    BaseEditHelper.SetValidationError(content, null);
                }
                else
                {
                    BaseEditHelper.SetValidationError(content, new BaseValidationError(errorText));
                }
            }
        }

        private void EnsureDataLayoutControlReference()
        {
            if (this.GetDataLayoutControl() == null)
            {
                DevExpress.Xpf.LayoutControl.DataLayoutControl layoutControl = this.GetLayoutControl() as DevExpress.Xpf.LayoutControl.DataLayoutControl;
                if (layoutControl != null)
                {
                    this._DataLayoutControlReference = new WeakReference(layoutControl);
                    if (IsPropertyHasDefaultOrInheritedValue(this, FrameworkElement.DataContextProperty))
                    {
                        System.Windows.Data.Binding binding = new System.Windows.Data.Binding("ValueProvider.Value");
                        binding.Source = layoutControl;
                        binding.Mode = BindingMode.OneWay;
                        this.dataContextBindingExpression = base.SetBinding(FrameworkElement.DataContextProperty, binding);
                    }
                    if (IsPropertyHasDefaultValue(this, LayoutItem.AddColonToLabelProperty))
                    {
                        System.Windows.Data.Binding binding = new System.Windows.Data.Binding("AddColonToItemLabels");
                        binding.Source = layoutControl;
                        binding.Mode = BindingMode.OneWay;
                        this.addColonToItemLabelsBindingExpression = base.SetBinding(LayoutItem.AddColonToLabelProperty, binding);
                    }
                }
            }
        }

        protected virtual void FinalizeUI()
        {
            base.ClearValue(FrameworkElement.MinHeightProperty);
            base.ClearValue(FrameworkElement.VerticalAlignmentProperty);
        }

        protected void GenerateUI()
        {
            if ((this.Binding != null) && (this.BindingSourceType != null))
            {
                PropertyDescriptor bindingProperty = this.GetBindingProperty();
                IEdmPropertyInfo info1 = (bindingProperty != null) ? ((IEdmPropertyInfo) new EdmPropertyInfo(bindingProperty, DataColumnAttributesProvider.GetAttributes(bindingProperty, this.BindingSourceType, null), false, false)) : ((IEdmPropertyInfo) new EmptyEdmPropertyInfo(this.BindingSourceType));
                this.PropertyInfo = info1;
                SetAttributeSettings(this, DataLayoutItemAttributeSettings.Create(this.PropertyInfo));
                this.UpdateIsActuallyReadOnly();
                base.UpdateIsActuallyRequired();
                if (IsPropertyHasDefaultValue(this, LayoutItem.LabelProperty))
                {
                    base.SetCurrentValue(LayoutItem.LabelProperty, this.GetLabel());
                }
                if (IsPropertyHasDefaultValue(this, FrameworkElement.ToolTipProperty))
                {
                    base.SetCurrentValue(FrameworkElement.ToolTipProperty, this.GetToolTip());
                }
                if (ReferenceEquals(base.Content, this.emptyItemDesignTimeMessage))
                {
                    base.ClearValue(LayoutItem.ContentProperty);
                }
                if (IsPropertyHasDefaultValue(this, LayoutItem.ContentProperty))
                {
                    FrameworkElement content = this.CreateContentAndInitializeUI();
                    this.UpdateContentValueBinding(content);
                    this.UpdateContentIsReadOnly(content);
                    base.SetCurrentValue(LayoutItem.ContentProperty, content);
                }
                else
                {
                    this.UpdateUnderlyingObject(base.Content as FrameworkElement);
                    this.DoApplySettingsForCustomContent();
                    this.UnsubscribeEditorEvents(base.Content);
                    this.SubscribeEditorEvents(base.Content);
                }
            }
        }

        [DXHelpExclude(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static DataLayoutItemAttributeSettings GetAttributeSettings(DependencyObject obj) => 
            ((DataLayoutItemAttributeSettings) obj.GetValue(AttributeSettingsProperty)) ?? DataLayoutItemAttributeSettings.Empty;

        protected virtual PropertyDescriptor GetBindingProperty()
        {
            string bindingPropertyPath = this.GetBindingPropertyPath();
            if (string.IsNullOrEmpty(bindingPropertyPath))
            {
                return null;
            }
            object bindingSource = this.GetBindingSource(base.DataContext);
            if (this.Binding.Path.PathParameters.Count <= 0)
            {
                return (!bindingPropertyPath.Contains(".") ? TypeDescriptor.GetProperties(bindingSource)[bindingPropertyPath] : ComplexPropertyDescriptorEx.GetIsReady(bindingSource, bindingPropertyPath));
            }
            PropertyDescriptor descriptor = this.Binding.Path.PathParameters[0] as PropertyDescriptor;
            return TypeDescriptor.GetProperties(bindingSource)[descriptor.Name];
        }

        protected string GetBindingPropertyPath() => 
            this.Binding.Path?.Path;

        protected object GetBindingSource(object dataContext)
        {
            object source = this.Binding?.Source;
            object obj1 = source;
            if (source == null)
            {
                object local2 = source;
                obj1 = dataContext;
            }
            return obj1;
        }

        protected Type GetBindingSourceType(object dataContext) => 
            this.GetBindingSource(dataContext)?.GetType();

        protected virtual DependencyProperty GetContentIsReadOnlyProperty(FrameworkElement content) => 
            (content is BaseEdit) ? BaseEdit.IsReadOnlyProperty : null;

        protected virtual DependencyProperty GetContentValueProperty(FrameworkElement content) => 
            (content is BaseEdit) ? BaseEdit.EditValueProperty : null;

        internal DevExpress.Xpf.LayoutControl.DataLayoutControl GetDataLayoutControl() => 
            this.DataLayoutControl ?? (this._DataLayoutControlReference.With<WeakReference, object>((<>c.<>9__46_0 ??= x => x.Target)) as DevExpress.Xpf.LayoutControl.DataLayoutControl);

        protected virtual bool GetIsActuallyReadOnly()
        {
            if ((this.PropertyInfo == null) || (!this.PropertyInfo.PropertyType.IsEditable() && !this.HasCustomEditorAttribute()))
            {
                return true;
            }
            Func<DevExpress.Xpf.LayoutControl.DataLayoutControl, bool> evaluator = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Func<DevExpress.Xpf.LayoutControl.DataLayoutControl, bool> local1 = <>c.<>9__48_0;
                evaluator = <>c.<>9__48_0 = x => x.IsReadOnly;
            }
            if (this.GetDataLayoutControl().Return<DevExpress.Xpf.LayoutControl.DataLayoutControl, bool>(evaluator, <>c.<>9__48_1 ??= () => false))
            {
                return true;
            }
            if (this.IsReadOnly)
            {
                return true;
            }
            ReadOnlyAttribute attribute = (this.BindingSourceType != null) ? this.BindingSourceType.GetAttribute<ReadOnlyAttribute>() : null;
            return (GetAttributeSettings(this).IsReadOnly || ((attribute != null) && attribute.IsReadOnly));
        }

        protected override bool GetIsActuallyRequired() => 
            !this.IsActuallyReadOnly ? (((this.PropertyInfo == null) || !GetAttributeSettings(this).IsRequired) ? base.GetIsActuallyRequired() : true) : false;

        protected virtual object GetLabel() => 
            GetAttributeSettings(this).Label;

        protected internal override FrameworkElement GetSelectableElement(Point p) => 
            !IsPropertyHasDefaultValue(this, LayoutItem.ContentProperty) ? base.GetSelectableElement(p) : this;

        protected virtual object GetToolTip() => 
            GetAttributeSettings(this).ToolTip;

        public static object GetUnderlyingObject(DependencyObject obj) => 
            obj.GetValue(UnderlyingObjectProperty);

        private bool HasCustomEditorAttribute()
        {
            Func<CommonEditorAttributeBase, CommonEditorAttributeBase> reader = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Func<CommonEditorAttributeBase, CommonEditorAttributeBase> local1 = <>c.<>9__47_0;
                reader = <>c.<>9__47_0 = y => y;
            }
            Func<CommonEditorAttributeBase, object> evaluator = <>c.<>9__47_1;
            if (<>c.<>9__47_1 == null)
            {
                Func<CommonEditorAttributeBase, object> local2 = <>c.<>9__47_1;
                evaluator = <>c.<>9__47_1 = x => x.TemplateKey;
            }
            return (this.PropertyInfo.Attributes.ReadAttributeProperty<CommonEditorAttributeBase, CommonEditorAttributeBase>(typeof(LayoutControlEditorAttribute), reader, null).Value.With<CommonEditorAttributeBase, object>(evaluator) != null);
        }

        private static bool IsPropertyHasDefaultOrInheritedValue(DependencyObject dObj, DependencyProperty property) => 
            System.Windows.DependencyPropertyHelper.GetValueSource(dObj, property).BaseValueSource <= BaseValueSource.Inherited;

        internal static bool IsPropertyHasDefaultValue(DependencyObject dObj, DependencyProperty property) => 
            System.Windows.DependencyPropertyHelper.GetValueSource(dObj, property).BaseValueSource < BaseValueSource.Inherited;

        protected virtual void OnBindingChanged()
        {
            this.UpdateUI();
        }

        protected override void OnContentChanged(UIElement oldValue, UIElement newValue)
        {
            base.OnContentChanged(oldValue, newValue);
            if (!this.IsInDesignTool())
            {
                this.UnsubscribeEditorEvents(oldValue);
                this.SubscribeEditorEvents(newValue);
            }
            else if (!this.contentLocker)
            {
                using (this.contentLocker.Lock())
                {
                    if (base.Content == null)
                    {
                        this.GenerateUI();
                    }
                    if (base.Content == null)
                    {
                        this.ShowEmptyItemDesignTimeMessage();
                    }
                }
            }
        }

        protected virtual void OnDataContextChanged(object oldValue, object newValue)
        {
            if (this.GetBindingSourceType(newValue) != this.GetBindingSourceType(oldValue))
            {
                this.UpdateUI();
            }
        }

        protected virtual void OnDataLayoutControlChanged()
        {
            this.CheckIsActuallyReadOnlyChanged();
        }

        protected internal virtual void OnDataLayoutControlIsReadOnlyChanged()
        {
            this.CheckIsActuallyReadOnlyChanged();
        }

        private void OnEditorEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            BindingExpression bindingExpression;
            BaseEdit edit1 = sender as BaseEdit;
            if (edit1 != null)
            {
                bindingExpression = edit1.GetBindingExpression(BaseEdit.EditValueProperty);
            }
            else
            {
                BaseEdit local1 = edit1;
                bindingExpression = null;
            }
            BindingExpression expression = bindingExpression;
            if ((expression == null) || ((expression.ParentBinding == null) || (expression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.PropertyChanged)))
            {
                ILayoutControl layoutControl = this.GetLayoutControl();
                if (layoutControl == null)
                {
                    ILayoutControl local2 = layoutControl;
                }
                else
                {
                    layoutControl.RequestEditorValidation();
                }
            }
        }

        private void OnEditorLoaded(object sender, RoutedEventArgs e)
        {
            this.DoValidate();
        }

        private void OnEditorLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (this.editorLostKeyboardOperation == null)
            {
                DispatcherOperation editorLostKeyboardOperation = this.editorLostKeyboardOperation;
            }
            else
            {
                this.editorLostKeyboardOperation.Abort();
            }
            this.editorLostKeyboardOperation = base.Dispatcher.BeginInvoke(delegate {
                ILayoutControl layoutControl = this.GetLayoutControl();
                if (layoutControl == null)
                {
                    ILayoutControl local1 = layoutControl;
                }
                else
                {
                    layoutControl.RequestEditorValidation();
                }
            }, new object[0]);
        }

        internal override void OnEndDeserializing()
        {
            base.OnEndDeserializing();
            if (IsPropertyHasDefaultValue(this, LayoutItem.LabelProperty))
            {
                base.SetCurrentValue(LayoutItem.LabelProperty, this.GetLabel());
            }
        }

        protected virtual void OnIsActuallyReadOnlyChanged()
        {
            base.UpdateIsActuallyRequired();
            if (base.Content != null)
            {
                this.UpdateContentValueBinding((FrameworkElement) base.Content);
                this.UpdateContentIsReadOnly((FrameworkElement) base.Content);
            }
        }

        protected virtual void OnIsReadOnlyChanged()
        {
            this.CheckIsActuallyReadOnlyChanged();
        }

        protected override void OnLabelChanged(object oldValue)
        {
            base.OnLabelChanged(oldValue);
            if (this.IsInDesignTool() && !this.labelLocker)
            {
                using (this.labelLocker.Lock())
                {
                    if (base.Label == null)
                    {
                        base.SetCurrentValue(LayoutItem.LabelProperty, this.GetLabel());
                    }
                }
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if (base.VisualParent != null)
            {
                this.EnsureDataLayoutControlReference();
            }
            else
            {
                this.ClearDataLayoutControlReference();
            }
        }

        internal static void SetAttributeSettings(DependencyObject obj, DataLayoutItemAttributeSettings value)
        {
            obj.SetValue(AttributeSettingsProperty, value);
        }

        internal static void SetUnderlyingObject(DependencyObject obj, object value)
        {
            obj.SetValue(UnderlyingObjectProperty, value);
        }

        private void ShowEmptyItemDesignTimeMessage()
        {
            if (this.IsInDesignTool())
            {
                base.SetCurrentValue(LayoutItem.ContentProperty, this.emptyItemDesignTimeMessage);
            }
        }

        private void SubscribeEditorEvents(UIElement element)
        {
            if ((this.Binding != null) && (this.BindingSourceType != null))
            {
                PropertyDescriptor bindingProperty = this.GetBindingProperty();
                if (bindingProperty != null)
                {
                    this.PropertyValidator = DataColumnAttributesExtensions.CreatePropertyValidator(bindingProperty, this.BindingSourceType);
                    BaseEdit edit = element as BaseEdit;
                    if (edit != null)
                    {
                        edit.EditValueChanged -= new EditValueChangedEventHandler(this.OnEditorEditValueChanged);
                        edit.EditValueChanged += new EditValueChangedEventHandler(this.OnEditorEditValueChanged);
                        edit.LostKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditorLostKeyboardFocus);
                        edit.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditorLostKeyboardFocus);
                        edit.Loaded -= new RoutedEventHandler(this.OnEditorLoaded);
                        edit.Loaded += new RoutedEventHandler(this.OnEditorLoaded);
                    }
                }
            }
        }

        private void UnsubscribeEditorEvents(UIElement element)
        {
            BaseEdit edit = element as BaseEdit;
            if ((this.PropertyValidator != null) && (edit != null))
            {
                edit.EditValueChanged -= new EditValueChangedEventHandler(this.OnEditorEditValueChanged);
                edit.LostKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditorLostKeyboardFocus);
                edit.Loaded -= new RoutedEventHandler(this.OnEditorLoaded);
            }
            if (this.editorLostKeyboardOperation == null)
            {
                DispatcherOperation editorLostKeyboardOperation = this.editorLostKeyboardOperation;
            }
            else
            {
                this.editorLostKeyboardOperation.Abort();
            }
        }

        protected void UpdateContentIsReadOnly(FrameworkElement content)
        {
            if (this.GetContentIsReadOnlyProperty(content) != null)
            {
                content.SetValue(this.GetContentIsReadOnlyProperty(content), this.IsActuallyReadOnly);
            }
        }

        protected virtual void UpdateContentValueBinding(FrameworkElement content)
        {
            this.UpdateUnderlyingObject(content);
            DependencyProperty contentValueProperty = this.GetContentValueProperty(content);
            if (contentValueProperty != null)
            {
                BindingExpression bindingExpression = content.GetBindingExpression(contentValueProperty);
                if ((bindingExpression == null) || ReferenceEquals(bindingExpression, this._contentBinding))
                {
                    content.SetBinding(contentValueProperty, this.CreateContentValueBinding(this.IsActuallyReadOnly));
                    this._contentBinding = content.GetBindingExpression(contentValueProperty);
                }
            }
        }

        protected void UpdateIsActuallyReadOnly()
        {
            this._IsActuallyReadOnly = this.IsActuallyReadOnly;
        }

        protected void UpdateUI()
        {
            if ((this.Binding != null) && (this.BindingSourceType != null))
            {
                this.GenerateUI();
            }
            else
            {
                this.ClearUI();
            }
        }

        private void UpdateUnderlyingObject(FrameworkElement content)
        {
            if (content != null)
            {
                content.SetBinding(UnderlyingObjectProperty, this.CreateContentValueBindingCore(true, false));
            }
        }

        public static HorizontalAlignment CurrencyValueAlignment
        {
            get => 
                EditorsSource.CurrencyValueAlignment;
            set => 
                EditorsSource.CurrencyValueAlignment = value;
        }

        public static double MultilineTextMinHeight
        {
            get => 
                EditorsSource.MultilineTextMinHeight;
            set => 
                EditorsSource.MultilineTextMinHeight = value;
        }

        public static string PhoneNumberMask
        {
            get => 
                EditorsSource.PhoneNumberMask;
            set => 
                EditorsSource.PhoneNumberMask = value;
        }

        [Description("Gets or sets whether the layout item's editor is read-only.This is a dependency property.")]
        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        [Description("Gets whether the layout item's editor is actually read-only.")]
        public bool IsActuallyReadOnly =>
            this.GetIsActuallyReadOnly();

        public bool ApplySettingsForCustomContent
        {
            get => 
                (bool) base.GetValue(ApplySettingsForCustomContentProperty);
            set => 
                base.SetValue(ApplySettingsForCustomContentProperty, value);
        }

        [Description("Gets or sets the Binding object for the current DataLayoutItem.")]
        public System.Windows.Data.Binding Binding
        {
            get => 
                this._Binding;
            set
            {
                if (!ReferenceEquals(this.Binding, value))
                {
                    if (value != null)
                    {
                        if (!string.IsNullOrEmpty(value.ElementName))
                        {
                            throw new NotSupportedException("Binding.ElementName is not supported.");
                        }
                        if (value.RelativeSource != null)
                        {
                            throw new NotSupportedException("Binding.RelativeSource is not supported.");
                        }
                    }
                    this._Binding = value;
                    this.OnBindingChanged();
                }
            }
        }

        protected Type BindingSourceType
        {
            get
            {
                string bindingPropertyPath = this.GetBindingPropertyPath();
                if (string.IsNullOrEmpty(bindingPropertyPath) || (!bindingPropertyPath.Contains(".") || (this.Binding.Path.PathParameters.Count > 0)))
                {
                    return this.GetBindingSourceType(base.DataContext);
                }
                object bindingSource = this.GetBindingSource(base.DataContext);
                if (bindingSource == null)
                {
                    return this.GetBindingSourceType(base.DataContext);
                }
                PropertyDescriptor isReady = ComplexPropertyDescriptorEx.GetIsReady(bindingSource, bindingPropertyPath);
                return ((isReady != null) ? isReady.ComponentType : this.GetBindingSourceType(base.DataContext));
            }
        }

        protected internal DevExpress.Xpf.LayoutControl.DataLayoutControl DataLayoutControl
        {
            get => 
                this._DataLayoutControl;
            set
            {
                if (!ReferenceEquals(this.DataLayoutControl, value))
                {
                    this._DataLayoutControl = value;
                    this.OnDataLayoutControlChanged();
                }
            }
        }

        protected IEdmPropertyInfo PropertyInfo { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataLayoutItem.<>c <>9 = new DataLayoutItem.<>c();
            public static Func<WeakReference, object> <>9__46_0;
            public static Func<CommonEditorAttributeBase, CommonEditorAttributeBase> <>9__47_0;
            public static Func<CommonEditorAttributeBase, object> <>9__47_1;
            public static Func<DataLayoutControl, bool> <>9__48_0;
            public static Func<bool> <>9__48_1;
            public static Func<DataLayoutControl, object> <>9__94_0;

            internal void <.cctor>b__98_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutItem) o).OnIsReadOnlyChanged();
            }

            internal object <DoValidate>b__94_0(DataLayoutControl x) => 
                x.CurrentItem;

            internal object <GetDataLayoutControl>b__46_0(WeakReference x) => 
                x.Target;

            internal bool <GetIsActuallyReadOnly>b__48_0(DataLayoutControl x) => 
                x.IsReadOnly;

            internal bool <GetIsActuallyReadOnly>b__48_1() => 
                false;

            internal CommonEditorAttributeBase <HasCustomEditorAttribute>b__47_0(CommonEditorAttributeBase y) => 
                y;

            internal object <HasCustomEditorAttribute>b__47_1(CommonEditorAttributeBase x) => 
                x.TemplateKey;
        }
    }
}

