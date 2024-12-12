namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;

    public abstract class BaseEditSettings : DXFrameworkContentElement, ISupportRaiseChanged
    {
        private static readonly DependencyPropertyKey ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty;
        public static readonly DependencyProperty DisableExcessiveUpdatesInInplaceInactiveModeProperty;
        public static readonly DependencyProperty AllowNullInputProperty;
        public static readonly DependencyProperty NullTextProperty;
        public static readonly DependencyProperty NullValueProperty;
        public static readonly DependencyProperty DisplayTextConverterProperty;
        public static readonly DependencyProperty DisplayFormatProperty;
        public static readonly DependencyProperty HorizontalContentAlignmentProperty;
        public static readonly DependencyProperty VerticalContentAlignmentProperty;
        public static readonly DependencyProperty StyleSettingsProperty;
        public static readonly DependencyProperty ValidationErrorTemplateProperty;
        public static readonly DependencyProperty ShowNullTextProperty;
        public static readonly DependencyProperty ShowNullTextForEmptyValueProperty;
        public static readonly DependencyProperty FlowDirectionProperty;
        public static readonly DependencyProperty MaxWidthProperty;
        public static readonly DependencyProperty ErrorToolTipContentTemplateProperty;
        private static readonly object getIsActivatingKey = new object();
        private static readonly object processActivatingKey = new object();
        private readonly Locker beginEndInitLocker = new Locker();
        private readonly EndInitPostponedAction endInitPostponedAction;
        private IDefaultEditorViewInfo defaultViewInfo;
        private IBaseEdit editor;
        private bool? isInDesignTime;
        private readonly Locker createEditorLocker = new Locker();
        private readonly Dictionary<object, Delegate> events;
        private readonly Locker forceAssignLocker = new Locker();

        internal event EventHandler EditSettingsChanged;

        public event GetIsActivatingKeyEventHandler GetIsActivatingKey
        {
            add
            {
                this.AddHandler(getIsActivatingKey, value);
            }
            remove
            {
                this.RemoveHandler(getIsActivatingKey, value);
            }
        }

        public event ProcessActivatingKeyEventHandler ProcessActivatingKey
        {
            add
            {
                this.AddHandler(processActivatingKey, value);
            }
            remove
            {
                this.RemoveHandler(processActivatingKey, value);
            }
        }

        static BaseEditSettings()
        {
            Type ownerType = typeof(BaseEditSettings);
            ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey = DependencyPropertyManager.RegisterReadOnly("ShouldDisableExcessiveUpdatesInInplaceInactiveMode", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty = ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey.DependencyProperty;
            DisableExcessiveUpdatesInInplaceInactiveModeProperty = DependencyPropertyManager.Register("DisableExcessiveUpdatesInInplaceInactiveMode", typeof(bool?), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            AllowNullInputProperty = DependencyPropertyManager.Register("AllowNullInput", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowNullTextProperty = DependencyPropertyManager.Register("ShowNullText", typeof(bool), ownerType, new PropertyMetadata(true));
            ShowNullTextForEmptyValueProperty = DependencyPropertyManager.Register("ShowNullTextForEmptyValue", typeof(bool), ownerType, new PropertyMetadata(true));
            NullTextProperty = DependencyPropertyManager.Register("NullText", typeof(string), ownerType, new PropertyMetadata("", new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            NullValueProperty = DependencyPropertyManager.Register("NullValue", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            DisplayTextConverterProperty = DependencyPropertyManager.Register("DisplayTextConverter", typeof(IValueConverter), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            DisplayFormatProperty = DependencyPropertyManager.Register("DisplayFormat", typeof(string), ownerType, new PropertyMetadata("", new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged), new CoerceValueCallback(BaseEditSettings.OnDisplayFormatCoerce)));
            HorizontalContentAlignmentProperty = DependencyPropertyManager.Register("HorizontalContentAlignment", typeof(EditSettingsHorizontalAlignment), ownerType, new PropertyMetadata(EditSettingsHorizontalAlignment.Default, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            VerticalContentAlignmentProperty = DependencyPropertyManager.Register("VerticalContentAlignment", typeof(VerticalAlignment), ownerType, new PropertyMetadata(VerticalAlignment.Center, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            MaxWidthProperty = DependencyPropertyManager.Register("MaxWidth", typeof(double), ownerType, new PropertyMetadata((double) 1.0 / (double) 0.0));
            StyleSettingsProperty = BaseEdit.StyleSettingsProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            FlowDirectionProperty = DependencyPropertyManager.Register("FlowDirection", typeof(System.Windows.FlowDirection), ownerType, new FrameworkPropertyMetadata(System.Windows.FlowDirection.LeftToRight));
            ValidationErrorTemplateProperty = DependencyPropertyManager.Register("ValidationErrorTemplate", typeof(DataTemplate), ownerType);
            ErrorToolTipContentTemplateProperty = DependencyPropertyManager.Register("ErrorToolTipContentTemplate", typeof(DataTemplate), ownerType);
        }

        protected BaseEditSettings()
        {
            this.AssignToEditCoreLocker = new Locker();
            this.events = new Dictionary<object, Delegate>();
            this.endInitPostponedAction = new EndInitPostponedAction(() => this.beginEndInitLocker.IsLocked);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected void AddHandler(object obj, Delegate handler)
        {
            if (this.Events.ContainsKey(obj))
            {
                this.Events[obj] += handler;
            }
            else
            {
                this.Events[obj] = handler;
            }
        }

        internal void ApplyToEdit(IBaseEdit edit, bool assignEditorSettings)
        {
            IDefaultEditorViewInfo defaultViewInfo = this.defaultViewInfo;
            if (this.defaultViewInfo == null)
            {
                IDefaultEditorViewInfo local1 = this.defaultViewInfo;
                defaultViewInfo = EmptyDefaultEditorViewInfo.Instance;
            }
            this.ApplyToEdit(edit, assignEditorSettings, defaultViewInfo);
        }

        public void ApplyToEdit(IBaseEdit edit, bool assignEditorSettings, IDefaultEditorViewInfo defaultViewInfo)
        {
            this.ApplyToEdit(edit, assignEditorSettings, defaultViewInfo, false);
        }

        public void ApplyToEdit(IBaseEdit edit, bool assignEditorSettings, IDefaultEditorViewInfo defaultViewInfo, bool force)
        {
            if (edit != null)
            {
                this.AssignToEdit(edit, defaultViewInfo, force, assignEditorSettings);
            }
        }

        internal void AssignToEdit(IBaseEdit edit)
        {
            IDefaultEditorViewInfo defaultViewInfo = this.defaultViewInfo;
            if (this.defaultViewInfo == null)
            {
                IDefaultEditorViewInfo local1 = this.defaultViewInfo;
                defaultViewInfo = EmptyDefaultEditorViewInfo.Instance;
            }
            this.AssignToEdit(edit, defaultViewInfo);
        }

        protected internal void AssignToEdit(IBaseEdit edit, IDefaultEditorViewInfo defaultViewInfo)
        {
            this.AssignToEdit(edit, defaultViewInfo, false, false);
        }

        protected internal void AssignToEdit(IBaseEdit edit, IDefaultEditorViewInfo defaultViewInfo, bool isForce, bool assignEditSettings = false)
        {
            if (edit == null)
            {
                throw new ArgumentNullException("edit");
            }
            edit.BeginInit(false);
            if (isForce)
            {
                this.forceAssignLocker.Lock();
            }
            try
            {
                this.AssignViewInfoProperties(edit, defaultViewInfo);
                this.AssignToEditCoreLocker.DoLockedAction(delegate {
                    if (assignEditSettings)
                    {
                        edit.SetSettings(this);
                    }
                    this.AssignToEditCore(edit);
                });
            }
            finally
            {
                if (isForce)
                {
                    this.forceAssignLocker.Unlock();
                }
                edit.EndInit(false, true);
            }
        }

        protected virtual void AssignToEditCore(IBaseEdit edit)
        {
            edit.VerticalContentAlignment = this.VerticalContentAlignment;
            if (edit is IInplaceBaseEdit)
            {
                edit.NullText = this.NullText;
                edit.NullValue = this.NullValue;
                edit.ShowNullText = this.ShowNullText;
                edit.ShowNullTextForEmptyValue = this.ShowNullTextForEmptyValue;
                edit.ErrorToolTipContentTemplate = this.ErrorToolTipContentTemplate;
            }
            else
            {
                BaseEdit baseEdit = edit as BaseEdit;
                if (baseEdit != null)
                {
                    this.SetValueFromSettings(AllowNullInputProperty, () => baseEdit.AllowNullInput = this.AllowNullInput);
                    this.SetValueFromSettings(MaxWidthProperty, () => edit.MaxWidth = this.MaxWidth);
                    this.SetValueFromSettings(NullTextProperty, () => edit.NullText = this.NullText);
                    this.SetValueFromSettings(ShowNullTextProperty, () => baseEdit.ShowNullText = this.ShowNullText);
                    this.SetValueFromSettings(ShowNullTextForEmptyValueProperty, () => baseEdit.ShowNullTextForEmptyValue = this.ShowNullTextForEmptyValue);
                    this.SetValueFromSettings(NullValueProperty, () => edit.NullValue = this.NullValue);
                    this.SetValueFromSettings(DisplayFormatProperty, () => edit.DisplayFormatString = this.DisplayFormat);
                    this.SetValueFromSettings(DisplayTextConverterProperty, () => edit.DisplayTextConverter = this.DisplayTextConverter);
                    this.SetValueFromSettings(DisableExcessiveUpdatesInInplaceInactiveModeProperty, () => edit.DisableExcessiveUpdatesInInplaceInactiveMode = this.DisableExcessiveUpdatesInInplaceInactiveMode);
                    this.SetValueFromSettings(ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty, () => edit.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = this.ShouldDisableExcessiveUpdatesInInplaceInactiveMode);
                    this.SetValueFromSettings(StyleSettingsProperty, () => baseEdit.StyleSettings = this.StyleSettings);
                    this.SetValueFromSettings(FlowDirectionProperty, () => baseEdit.FlowDirection = this.FlowDirection);
                    this.SetValueFromSettings(ValidationErrorTemplateProperty, () => baseEdit.ValidationErrorTemplate = this.ValidationErrorTemplate, () => this.ClearEditorPropertyIfNeeded(baseEdit, BaseEdit.ValidationErrorTemplateProperty, ValidationErrorTemplateProperty));
                    this.SetValueFromSettings(ErrorToolTipContentTemplateProperty, () => baseEdit.ErrorToolTipContentTemplate = this.ErrorToolTipContentTemplate, () => this.ClearEditorPropertyIfNeeded(baseEdit, BaseEdit.ErrorToolTipContentTemplateProperty, ErrorToolTipContentTemplateProperty));
                }
            }
        }

        protected internal virtual void AssignViewInfoProperties(IBaseEdit edit, IDefaultEditorViewInfo defaultViewInfo)
        {
            this.defaultViewInfo = defaultViewInfo;
            edit.HorizontalContentAlignment = EditSettingsHorizontalAlignmentHelper.GetHorizontalAlignment(this.GetActualHorizontalContentAlignment(), defaultViewInfo.DefaultHorizontalAlignment);
        }

        public override void BeginInit()
        {
            base.BeginInit();
            this.beginEndInitLocker.Lock();
        }

        protected bool ClearEditorPropertyIfNeeded(BaseEdit edit, DependencyProperty editorProperty, DependencyProperty settingsProperty)
        {
            if (this.IsPropertyAssigned(settingsProperty) || !this.IsDefaultValue(settingsProperty))
            {
                return false;
            }
            edit.ClearValue(editorProperty);
            return true;
        }

        public IBaseEdit CreateEditor(EditorOptimizationMode optimizationMode = 0)
        {
            IDefaultEditorViewInfo defaultViewInfo = this.defaultViewInfo;
            if (this.defaultViewInfo == null)
            {
                IDefaultEditorViewInfo local1 = this.defaultViewInfo;
                defaultViewInfo = EmptyDefaultEditorViewInfo.Instance;
            }
            return this.CreateEditor(defaultViewInfo, optimizationMode);
        }

        public IBaseEdit CreateEditor(IDefaultEditorViewInfo defaultViewInfo, EditorOptimizationMode optimizationMode = 0) => 
            this.CreateEditor(true, defaultViewInfo, optimizationMode);

        public virtual IBaseEdit CreateEditor(bool assignEditorSettings, IDefaultEditorViewInfo defaultViewInfo, EditorOptimizationMode optimizationMode)
        {
            IBaseEdit edit = EditorSettingsProvider.Default.CreateEditor(base.GetType(), optimizationMode);
            this.ApplyToEdit(edit, assignEditorSettings, defaultViewInfo);
            edit.ForceInitialize(false);
            return edit;
        }

        void ISupportRaiseChanged.RaiseChanged()
        {
            this.RaiseChangedEventIfNotLoading();
        }

        public override void EndInit()
        {
            this.beginEndInitLocker.Unlock();
            this.endInitPostponedAction.PerformActionOnEndInitIfNeeded(new Action(this.RaiseChangedEvent));
            base.EndInit();
        }

        protected virtual EditSettingsHorizontalAlignment GetActualHorizontalContentAlignment() => 
            this.HorizontalContentAlignment;

        public virtual string GetDisplayText(object editValue, bool applyFormatting) => 
            this.Editor.GetDisplayText(editValue, applyFormatting);

        public virtual string GetDisplayTextFromEditor(object editValue) => 
            this.GetDisplayText(editValue, true);

        protected internal virtual bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            false;

        internal static bool IsBaseTypeProperty(string propertyName) => 
            typeof(BaseEditSettings).BaseType.GetProperty(propertyName) != null;

        protected internal virtual bool IsCompatibleWith(BaseEditSettings settings) => 
            Equals(base.GetType(), settings.GetType());

        private bool IsDefaultValue(DependencyProperty property)
        {
            object defaultValue = property.GetMetadata(base.GetType()).DefaultValue;
            defaultValue = (defaultValue == DependencyProperty.UnsetValue) ? null : defaultValue;
            return (base.GetValue(property) == defaultValue);
        }

        protected internal static bool IsNativeNullValue(object value) => 
            (value == null) || (value == DBNull.Value);

        protected internal virtual bool IsPasteGesture(Key key, ModifierKeys modifiers) => 
            !BrowserInteropHelper.IsBrowserHosted ? ((key == Key.V) && (modifiers == ModifierKeys.Control)) : false;

        protected internal static bool IsStringEmpty(object value) => 
            (value is string) && string.IsNullOrEmpty((string) value);

        protected virtual string OnDisplayFormatCoerce(string baseValue) => 
            FormatStringConverter.GetDisplayFormat(baseValue);

        private static object OnDisplayFormatCoerce(DependencyObject obj, object baseValue) => 
            ((BaseEditSettings) obj).OnDisplayFormatCoerce((string) baseValue);

        protected virtual void OnLoaded()
        {
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (!IsBaseTypeProperty(e.Property.GetName()))
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(e.Property, base.GetType());
                if ((descriptor != null) && !descriptor.IsAttached)
                {
                    this.RaiseChangedEventIfNotLoading();
                }
            }
        }

        protected virtual void OnSettingsChanged()
        {
        }

        protected void OnSettingsChanged(DependencyPropertyChangedEventArgs e)
        {
            this.OnSettingsChanged();
        }

        protected static void OnSettingsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEditSettings) obj).OnSettingsChanged(e);
        }

        protected virtual void OnUnloaded()
        {
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        private void RaiseChangedEvent()
        {
            this.createEditorLocker.DoIfNotLocked(new Action(this.ResetEditor));
            if (this.EditSettingsChanged != null)
            {
                this.EditSettingsChanged(this, EventArgs.Empty);
            }
        }

        protected void RaiseChangedEventIfNotLoading()
        {
            if (!this.AssignToEditCoreLocker.IsLocked)
            {
                this.endInitPostponedAction.PerformIfNotLoading(new Action(this.RaiseChangedEvent), null);
            }
        }

        protected internal virtual void RaiseGetIsActivatingKey(object sender, GetIsActivatingKeyEventArgs e)
        {
            Delegate delegate2;
            if (this.Events.TryGetValue(getIsActivatingKey, out delegate2))
            {
                ((GetIsActivatingKeyEventHandler) delegate2)(sender, e);
            }
        }

        protected internal virtual void RaiseProcessActivatingKey(object sender, ProcessActivatingKeyEventArgs e)
        {
            Delegate delegate2;
            if (this.Events.TryGetValue(processActivatingKey, out delegate2))
            {
                ((ProcessActivatingKeyEventHandler) delegate2)(sender, e);
            }
        }

        protected void RemoveHandler(object obj, Delegate handler)
        {
            if (this.Events.ContainsKey(obj))
            {
                this.Events[obj] -= handler;
            }
        }

        private void ResetEditor()
        {
            this.editor = null;
        }

        protected void SetValueFromSettings(DependencyProperty property, Action setAction)
        {
            if (this.forceAssignLocker || (this.IsInDesignTime || (!this.IsDefaultValue(property) || this.IsPropertyAssigned(property))))
            {
                setAction();
            }
        }

        protected void SetValueFromSettings(DependencyProperty property, Action setAction, Func<bool> clearAction)
        {
            if (clearAction == null)
            {
                this.SetValueFromSettings(property, setAction);
            }
            else if (!clearAction())
            {
                this.SetValueFromSettings(property, setAction);
            }
        }

        protected internal virtual bool UsesFlatBorderTemplate() => 
            true;

        protected internal Locker AssignToEditCoreLocker { get; private set; }

        private bool IsInDesignTime
        {
            get
            {
                if (this.isInDesignTime == null)
                {
                    this.isInDesignTime = new bool?(this.IsInDesignTool());
                }
                return this.isInDesignTime.Value;
            }
        }

        protected IBaseEdit Editor
        {
            get
            {
                if (this.editor == null)
                {
                    this.createEditorLocker.DoLockedAction(delegate {
                        IDefaultEditorViewInfo defaultViewInfo = this.defaultViewInfo;
                        if (this.defaultViewInfo == null)
                        {
                            IDefaultEditorViewInfo local1 = this.defaultViewInfo;
                            defaultViewInfo = EmptyDefaultEditorViewInfo.Instance;
                        }
                        this.editor = this.CreateEditor(defaultViewInfo, EditorOptimizationMode.Disabled);
                        this.editor.DisableExcessiveUpdatesInInplaceInactiveMode = true;
                        this.editor.EditMode = EditMode.InplaceInactive;
                        this.editor.ForceInitialize(true);
                    });
                }
                return this.editor;
            }
        }

        protected internal virtual bool RequireDisplayTextSorting =>
            false;

        [SkipPropertyAssertion, Description("Gets or sets whether end-users can set the editor's value to a null reference by pressing the CTRL+DEL or CTRL+0 key combinations. This is a dependency property."), Category("Behavior")]
        public bool AllowNullInput
        {
            get => 
                (bool) base.GetValue(AllowNullInputProperty);
            set => 
                base.SetValue(AllowNullInputProperty, value);
        }

        protected internal Dictionary<object, Delegate> Events =>
            this.events;

        [Category("Appearance "), SkipPropertyAssertion]
        public DataTemplate ErrorToolTipContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ErrorToolTipContentTemplateProperty);
            set => 
                base.SetValue(ErrorToolTipContentTemplateProperty, value);
        }

        public bool ShowNullText
        {
            get => 
                (bool) base.GetValue(ShowNullTextProperty);
            set => 
                base.SetValue(ShowNullTextProperty, value);
        }

        public bool ShowNullTextForEmptyValue
        {
            get => 
                (bool) base.GetValue(ShowNullTextForEmptyValueProperty);
            set => 
                base.SetValue(ShowNullTextForEmptyValueProperty, value);
        }

        [SkipPropertyAssertion, Category("Format"), Description("Gets or sets the pattern used to format the editor's display value. This is a dependency property.")]
        public string DisplayFormat
        {
            get => 
                (string) base.GetValue(DisplayFormatProperty);
            set => 
                base.SetValue(DisplayFormatProperty, value);
        }

        [Category("Format"), Description("Gets or sets a converter used to provide the editor's display value. This is a dependency property.")]
        public IValueConverter DisplayTextConverter
        {
            get => 
                (IValueConverter) base.GetValue(DisplayTextConverterProperty);
            set => 
                base.SetValue(DisplayTextConverterProperty, value);
        }

        [Description("Gets or sets the horizontal alignment of an editor's contents."), Category("Layout"), SkipPropertyAssertion]
        public EditSettingsHorizontalAlignment HorizontalContentAlignment
        {
            get => 
                (EditSettingsHorizontalAlignment) base.GetValue(HorizontalContentAlignmentProperty);
            set => 
                base.SetValue(HorizontalContentAlignmentProperty, value);
        }

        [Category("Behavior"), SkipPropertyAssertion, Description("Gets or sets an object that defines an editor's appearance and behavior. This is a dependency property.")]
        public BaseEditStyleSettings StyleSettings
        {
            get => 
                (BaseEditStyleSettings) base.GetValue(StyleSettingsProperty);
            set => 
                base.SetValue(StyleSettingsProperty, value);
        }

        [Category("Layout"), SkipPropertyAssertion, Description("Gets or sets the vertical alignment of the editor's contents.")]
        public VerticalAlignment VerticalContentAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(VerticalContentAlignmentProperty);
            set => 
                base.SetValue(VerticalContentAlignmentProperty, value);
        }

        public bool? DisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                (bool?) base.GetValue(DisableExcessiveUpdatesInInplaceInactiveModeProperty);
            set => 
                base.SetValue(DisableExcessiveUpdatesInInplaceInactiveModeProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldDisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                (bool) base.GetValue(ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty);
            internal set => 
                base.SetValue(ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey, value);
        }

        public DataTemplate ValidationErrorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ValidationErrorTemplateProperty);
            set => 
                base.SetValue(ValidationErrorTemplateProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the editor's maximum width. This is a dependency property."), SkipPropertyAssertion]
        public double MaxWidth
        {
            get => 
                (double) base.GetValue(MaxWidthProperty);
            set => 
                base.SetValue(MaxWidthProperty, value);
        }

        [Description("Gets or sets the text displayed within the edit box when the editor's value is null. This is a dependency property.")]
        public string NullText
        {
            get => 
                (string) base.GetValue(NullTextProperty);
            set => 
                base.SetValue(NullTextProperty, value);
        }

        [Description("Gets or sets a null value for the editor. This is a dependency property.")]
        public object NullValue
        {
            get => 
                base.GetValue(NullValueProperty);
            set => 
                base.SetValue(NullValueProperty, value);
        }

        public System.Windows.FlowDirection FlowDirection
        {
            get => 
                (System.Windows.FlowDirection) base.GetValue(FlowDirectionProperty);
            set => 
                base.SetValue(FlowDirectionProperty, value);
        }
    }
}

