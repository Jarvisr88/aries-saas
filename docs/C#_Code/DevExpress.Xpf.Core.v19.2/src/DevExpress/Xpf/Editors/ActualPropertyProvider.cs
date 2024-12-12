namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ActualPropertyProvider : FrameworkElement, IServiceContainer, IServiceProvider
    {
        internal const bool ShouldDisableExcessiveUpdatesInInplaceInactiveModeDefaultValue = false;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PropertiesProperty;
        private readonly WeakReference weakEditor;
        private bool isNullTextVisible = true;
        private object editValue;
        private bool showNullText = true;
        private bool showNullTextForEmptyValue = true;
        private object nullValue;
        private DevExpress.Xpf.Editors.EditMode editMode;
        private bool? disableExcessiveUpdatesInInplaceInactiveMode;
        private bool shouldDisableExcessiveUpdatesInInplaceInactiveMode;
        private string displayFormatString;
        private BaseEditStyleSettings defaultStyleSettings;
        private BaseEditStyleSettings styleSettings;
        private bool hasDisplayTextProviderText;
        private bool hasValidationErrorTemplate;
        private ValueContainerService valueContainerService;
        private TextInputServiceBase textInputService;
        private ItemsProviderService itemsProviderService;

        static ActualPropertyProvider()
        {
            Type ownerType = typeof(ActualPropertyProvider);
            PropertiesProperty = DependencyPropertyManager.RegisterAttached("Properties", typeof(ActualPropertyProvider), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ActualPropertyProvider.PropertiesPropertyChanged)));
        }

        public ActualPropertyProvider(BaseEdit editor)
        {
            this.weakEditor = new WeakReference(editor);
            this.ValueTypeConverter = new DevExpress.Xpf.Editors.Internal.ValueTypeConverter();
            this.Services = new Dictionary<Type, object>();
            this.SetDisplayFormatString(editor.DisplayFormatString);
        }

        public virtual bool CalcSuppressFeatures() => 
            ((this.DisableExcessiveUpdatesInInplaceInactiveMode != null) || !this.ShouldDisableExcessiveUpdatesInInplaceInactiveMode) ? ((this.DisableExcessiveUpdatesInInplaceInactiveMode == null) ? false : this.DisableExcessiveUpdatesInInplaceInactiveMode.Value) : true;

        public virtual EditorPlacement GetAddNewButtonPlacement() => 
            EditorPlacement.None;

        public virtual EditorPlacement GetFindButtonPlacement() => 
            EditorPlacement.None;

        public virtual EditorPlacement GetNullValueButtonPlacement() => 
            EditorPlacement.None;

        protected virtual ControlTemplate GetPopupBottomAreaTemplate() => 
            null;

        public virtual PopupFooterButtons GetPopupFooterButtons() => 
            PopupFooterButtons.None;

        protected virtual ControlTemplate GetPopupTopAreaTemplate() => 
            null;

        internal static ActualPropertyProvider GetProperties<T>(IBaseEdit obj) => 
            GetProperties((DependencyObject) obj);

        public static ActualPropertyProvider GetProperties(DependencyObject obj) => 
            (ActualPropertyProvider) obj.GetValue(PropertiesProperty);

        public T GetService<T>() where T: class => 
            (T) ((IServiceProvider) this).GetService(typeof(T));

        private static void PropertiesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseEdit)
            {
                ((BaseEdit) d).SetActualPropertyProvider((ActualPropertyProvider) e.NewValue);
            }
        }

        public void RegisterService<T>(T service)
        {
            ((IServiceContainer) this).AddService(typeof(T), service);
        }

        public void SetCharacterCasing(System.Windows.Controls.CharacterCasing characterCasing)
        {
            this.CharacterCasing = characterCasing;
        }

        public void SetDisableExcessiveUpdatesInInplaceInactiveMode(bool? optimizePerformance)
        {
            this.disableExcessiveUpdatesInInplaceInactiveMode = optimizePerformance;
        }

        public void SetDisplayFormatString(string displayFormatString)
        {
            this.displayFormatString = displayFormatString;
        }

        public void SetDisplayText(string displayText)
        {
            this.DisplayText = displayText;
        }

        public void SetDisplayTextConverter(IValueConverter converter)
        {
            this.DisplayTextConverter = converter;
        }

        public void SetEditMode(DevExpress.Xpf.Editors.EditMode editMode)
        {
            this.editMode = editMode;
        }

        public void SetEditValue(object editValue)
        {
            this.editValue = this.ValueTypeConverter.Convert(editValue);
        }

        public void SetHasDisplayTextProviderText(bool hasDisplayTextProviderText)
        {
            this.hasDisplayTextProviderText = hasDisplayTextProviderText;
        }

        public void SetHasValidationErrorTemplate(bool hasValidationErrorTemplate)
        {
            this.hasValidationErrorTemplate = hasValidationErrorTemplate;
        }

        public void SetHiddenValidationError(BaseValidationError error)
        {
            this.HiddenValidationError = error;
        }

        public void SetIsNullTextVisible(bool isNullTextVisible)
        {
            this.isNullTextVisible = isNullTextVisible;
        }

        public void SetNullValue(object nullValue)
        {
            this.nullValue = nullValue;
        }

        public static void SetProperties(DependencyObject obj, ActualPropertyProvider value)
        {
            obj.SetValue(PropertiesProperty, value);
        }

        public void SetShouldDisableExcessiveUpdatesInInplaceInactiveMode(bool shouldBeOptimized)
        {
            this.shouldDisableExcessiveUpdatesInInplaceInactiveMode = shouldBeOptimized;
        }

        public void SetShowNullText(bool showNullText)
        {
            this.showNullText = showNullText;
        }

        public void SetShowNullTextForEmptyValue(bool showNullTextForEmptyValue)
        {
            this.showNullTextForEmptyValue = showNullTextForEmptyValue;
        }

        public void SetStyleSettings(BaseEditStyleSettings settings)
        {
            this.styleSettings = settings;
        }

        public void SetValueTypeConverter(DevExpress.Xpf.Editors.Internal.ValueTypeConverter valueConverter)
        {
            this.ValueTypeConverter = valueConverter;
            this.SetEditValue(this.EditValue);
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            IServiceContainer container = this;
            container.AddService(serviceType, callback(container, serviceType));
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance)
        {
            if (serviceType == typeof(ValueContainerService))
            {
                this.valueContainerService = (ValueContainerService) serviceInstance;
            }
            if (serviceType == typeof(TextInputServiceBase))
            {
                this.textInputService = (TextInputServiceBase) serviceInstance;
            }
            if (serviceType == typeof(ItemsProviderService))
            {
                this.itemsProviderService = (ItemsProviderService) serviceInstance;
            }
            this.Services[serviceType] = serviceInstance;
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            ((IServiceContainer) this).AddService(serviceType, callback);
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
        {
            ((IServiceContainer) this).AddService(serviceType, serviceInstance);
        }

        void IServiceContainer.RemoveService(Type serviceType)
        {
            this.Services.Remove(serviceType);
        }

        void IServiceContainer.RemoveService(Type serviceType, bool promote)
        {
            this.Services.Remove(serviceType);
        }

        object IServiceProvider.GetService(Type serviceType) => 
            !(serviceType == typeof(ValueContainerService)) ? (!(serviceType == typeof(TextInputServiceBase)) ? (!(serviceType == typeof(ItemsProviderService)) ? this.Services[serviceType] : this.itemsProviderService) : this.textInputService) : this.valueContainerService;

        public System.Windows.Controls.CharacterCasing CharacterCasing { get; private set; }

        public bool CreatedFromSettings { get; protected internal set; }

        public string DisplayText { get; private set; }

        public IValueConverter DisplayTextConverter { get; private set; }

        public DevExpress.Xpf.Editors.Internal.ValueTypeConverter ValueTypeConverter { get; private set; }

        public bool IsNullTextVisible =>
            this.isNullTextVisible;

        public object EditValue =>
            this.editValue;

        public bool ShowNullText =>
            this.showNullText;

        public object NullValue =>
            this.nullValue;

        public bool ShowNullTextForEmptyValue =>
            this.showNullTextForEmptyValue;

        public bool? DisableExcessiveUpdatesInInplaceInactiveMode =>
            this.disableExcessiveUpdatesInInplaceInactiveMode;

        public bool ShouldDisableExcessiveUpdatesInInplaceInactiveMode =>
            this.shouldDisableExcessiveUpdatesInInplaceInactiveMode;

        public DevExpress.Xpf.Editors.EditMode EditMode =>
            this.editMode;

        public bool SuppressFeatures =>
            (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive) && this.CalcSuppressFeatures();

        public string DisplayFormatString =>
            this.displayFormatString;

        public BaseEdit Editor =>
            (BaseEdit) this.weakEditor.Target;

        public BaseEditStyleSettings StyleSettings
        {
            get
            {
                BaseEditStyleSettings styleSettings = this.styleSettings;
                if (this.styleSettings == null)
                {
                    BaseEditStyleSettings local1 = this.styleSettings;
                    styleSettings = this.defaultStyleSettings;
                    if (this.defaultStyleSettings == null)
                    {
                        BaseEditStyleSettings defaultStyleSettings = this.defaultStyleSettings;
                        styleSettings = this.defaultStyleSettings = this.Editor.CreateStyleSettings();
                    }
                }
                return styleSettings;
            }
        }

        public bool HasDisplayTextProviderText =>
            this.hasDisplayTextProviderText;

        public bool HasValidationErrorTemplate =>
            this.hasValidationErrorTemplate;

        public BaseValidationError HiddenValidationError { get; private set; }

        public bool HasHiddenValidationError =>
            this.HiddenValidationError != null;

        private Dictionary<Type, object> Services { get; set; }
    }
}

