namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    [DXToolboxBrowsable(false)]
    public class PopupBaseEdit : ButtonEdit, IPopupContentOwner
    {
        private readonly DevExpress.Xpf.Editors.PopupSettings popupSettings;
        public static readonly DependencyProperty ShowPopupIfReadOnlyProperty;
        public static readonly DependencyProperty StaysPopupOpenProperty;
        public static readonly DependencyProperty IgnorePopupSizeConstraintsProperty;
        public static readonly DependencyProperty AllowRecreatePopupContentProperty;
        public static readonly DependencyProperty IsPopupOpenProperty;
        public static readonly DependencyProperty PopupTemplateProperty;
        public static readonly DependencyProperty PopupContentTemplateProperty;
        public static readonly DependencyProperty PopupContentContainerTemplateProperty;
        public static readonly DependencyProperty PopupTopAreaTemplateProperty;
        public static readonly DependencyProperty PopupBottomAreaTemplateProperty;
        public static readonly DependencyProperty PopupWidthProperty;
        protected static readonly DependencyPropertyKey ActualPopupWidthPropertyKey;
        public static readonly DependencyProperty ActualPopupWidthProperty;
        public static readonly DependencyProperty PopupMaxWidthProperty;
        public static readonly DependencyProperty PopupMinWidthProperty;
        protected static readonly DependencyPropertyKey ActualPopupMinWidthPropertyKey;
        public static readonly DependencyProperty ActualPopupMinWidthProperty;
        public static readonly DependencyProperty PopupHeightProperty;
        protected static readonly DependencyPropertyKey ActualPopupHeightPropertyKey;
        public static readonly DependencyProperty ActualPopupHeightProperty;
        public static readonly DependencyProperty PopupMaxHeightProperty;
        public static readonly DependencyProperty PopupMinHeightProperty;
        public static readonly DependencyProperty PopupFooterButtonsProperty;
        public static readonly DependencyProperty ShowSizeGripProperty;
        public static readonly RoutedEvent PopupOpeningEvent;
        public static readonly RoutedEvent PopupOpenedEvent;
        public static readonly RoutedEvent PopupClosingEvent;
        public static readonly RoutedEvent PopupClosedEvent;
        protected static readonly DependencyPropertyKey PopupOwnerEditPropertyKey;
        public static readonly DependencyProperty PopupOwnerEditProperty;
        public static readonly DependencyProperty FocusPopupOnOpenProperty;
        public static readonly DependencyProperty ClosePopupOnClickModeProperty;
        private readonly Locker closingPopupLocker = new Locker();
        private static bool openPopupSyncFlag;
        private bool closePopupOnLostFocus = true;
        private FrameworkElement child;

        public event ClosePopupEventHandler PopupClosed
        {
            add
            {
                base.AddHandler(PopupClosedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupClosedEvent, value);
            }
        }

        public event ClosingPopupEventHandler PopupClosing
        {
            add
            {
                base.AddHandler(PopupClosingEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupClosingEvent, value);
            }
        }

        public event RoutedEventHandler PopupOpened
        {
            add
            {
                base.AddHandler(PopupOpenedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupOpenedEvent, value);
            }
        }

        public event OpenPopupEventHandler PopupOpening
        {
            add
            {
                base.AddHandler(PopupOpeningEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupOpeningEvent, value);
            }
        }

        static PopupBaseEdit()
        {
            Type ownerType = typeof(PopupBaseEdit);
            ShowPopupIfReadOnlyProperty = DependencyProperty.Register("ShowPopupIfReadOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            StaysPopupOpenProperty = DependencyPropertyManager.Register("StaysPopupOpen", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            IgnorePopupSizeConstraintsProperty = DependencyPropertyManager.Register("IgnorePopupSizeConstraints", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            FocusPopupOnOpenProperty = DependencyPropertyManager.Register("FocusPopupOnOpen", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            AllowRecreatePopupContentProperty = DependencyPropertyManager.Register("AllowRecreatePopupContent", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PopupTemplateProperty = DependencyPropertyManager.Register("PopupTemplate", typeof(ControlTemplate), ownerType);
            IsPopupOpenProperty = DependencyPropertyManager.Register("IsPopupOpen", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(PopupBaseEdit.OnIsPopupOpenChanged), new CoerceValueCallback(PopupBaseEdit.OnIsPopupOpenCoerce)));
            PopupContentTemplateProperty = DependencyPropertyManager.Register("PopupContentTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(PopupBaseEdit.OnPopupContentTemplateChanged)));
            PopupContentContainerTemplateProperty = DependencyPropertyManager.Register("PopupContentContainerTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            PopupTopAreaTemplateProperty = DependencyPropertyManager.Register("PopupTopAreaTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((PopupBaseEdit) d).PopupTopAreaTemplateChanged()));
            PopupBottomAreaTemplateProperty = DependencyPropertyManager.Register("PopupBottomAreaTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((PopupBaseEdit) d).PopupBottomAreaTemplateChanged()));
            PopupOpeningEvent = EventManager.RegisterRoutedEvent("PopupOpening", RoutingStrategy.Tunnel, typeof(OpenPopupEventHandler), ownerType);
            PopupOpenedEvent = EventManager.RegisterRoutedEvent("PopupOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            PopupClosingEvent = EventManager.RegisterRoutedEvent("PopupClosing", RoutingStrategy.Tunnel, typeof(ClosingPopupEventHandler), ownerType);
            PopupClosedEvent = EventManager.RegisterRoutedEvent("PopupClosed", RoutingStrategy.Bubble, typeof(ClosePopupEventHandler), ownerType);
            PopupMinWidthProperty = DependencyPropertyManager.Register("PopupMinWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(17.0, new PropertyChangedCallback(PopupBaseEdit.OnPopupMinWidthChanged), new CoerceValueCallback(PopupBaseEdit.CoercePopupMinWidth)));
            ActualPopupMinWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPopupMinWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(PopupBaseEdit.OnActualPopupMinWidthChanged)));
            ActualPopupMinWidthProperty = ActualPopupMinWidthPropertyKey.DependencyProperty;
            PopupMaxWidthProperty = DependencyPropertyManager.Register("PopupMaxWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(PopupBaseEdit.OnPopupMaxWidthChanged), new CoerceValueCallback(PopupBaseEdit.CoercePopupMaxWidth)));
            PopupWidthProperty = DependencyPropertyManager.Register("PopupWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((PopupBaseEdit) d).OnPopupWidthChanged((double) e.NewValue), new CoerceValueCallback(PopupBaseEdit.CoercePopupWidth)));
            ActualPopupWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPopupWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(PopupBaseEdit.OnActualPopupWidthChanged), new CoerceValueCallback(PopupBaseEdit.CoerceActualPopupWidth)));
            ActualPopupWidthProperty = ActualPopupWidthPropertyKey.DependencyProperty;
            PopupMinHeightProperty = DependencyPropertyManager.Register("PopupMinHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(35.0, new PropertyChangedCallback(PopupBaseEdit.OnPopupMinHeightChanged), new CoerceValueCallback(PopupBaseEdit.CoercePopupMinHeight)));
            PopupMaxHeightProperty = DependencyPropertyManager.Register("PopupMaxHeight", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(PopupBaseEdit.OnPopupMaxHeightChanged), new CoerceValueCallback(PopupBaseEdit.CoercePopupMaxHeight)));
            PopupHeightProperty = DependencyPropertyManager.Register("PopupHeight", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((PopupBaseEdit) d).OnPopupHeightChanged((double) e.NewValue), new CoerceValueCallback(PopupBaseEdit.CoercePopupHeight)));
            ActualPopupHeightPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPopupHeight", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(PopupBaseEdit.OnActualPopupHeightChanged), new CoerceValueCallback(PopupBaseEdit.CoerceActualPopupHeight)));
            ActualPopupHeightProperty = ActualPopupHeightPropertyKey.DependencyProperty;
            PopupFooterButtonsProperty = DependencyPropertyManager.Register("PopupFooterButtons", typeof(DevExpress.Xpf.Editors.PopupFooterButtons?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(PopupBaseEdit.OnPopupFooterButtonsChanged), new CoerceValueCallback(PopupBaseEdit.CoercePopupFooterButtons)));
            ShowSizeGripProperty = DependencyPropertyManager.Register("ShowSizeGrip", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((PopupBaseEdit) d).ShowSizeGripChanged((bool?) e.NewValue), new CoerceValueCallback(PopupBaseEdit.CoerceShowSizeGrip)));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PopupBaseEdit), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DevExpress.Xpf.Editors.PopupCloseMode? defaultValue = null;
            ClosePopupOnClickModeProperty = DependencyPropertyRegistrator.Register<PopupBaseEdit, DevExpress.Xpf.Editors.PopupCloseMode?>(System.Linq.Expressions.Expression.Lambda<Func<PopupBaseEdit, DevExpress.Xpf.Editors.PopupCloseMode?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PopupBaseEdit.get_ClosePopupOnClickMode)), parameters), defaultValue);
            PopupOwnerEditPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PopupOwnerEdit", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            PopupOwnerEditProperty = PopupOwnerEditPropertyKey.DependencyProperty;
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(typeof(PopupBaseEdit)));
        }

        public PopupBaseEdit()
        {
            SetPopupOwnerEdit(this, this);
            this.<VisualClient>k__BackingField = this.CreateVisualClient();
            this.popupSettings = this.CreatePopupSettings();
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            this.ClosePopupCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.ClosePopupInternal), false);
            this.OpenPopupCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.OpenPopupInternal), false);
            this.CreatePopupLocker = new Locker();
        }

        protected virtual void AcceptPopupValue()
        {
            PopupBaseEditHelper.SetIsValueChangedViaPopup(this.Settings, true);
            this.PopupSettings.AcceptPopupValue();
        }

        protected virtual void AfterPreviewKeyDown(KeyEventArgs e)
        {
            this.ProcessPopupPreviewKeyDown(e);
        }

        protected internal virtual void BeforePopupOpened()
        {
            if (base.IsLoaded)
            {
                this.UpdatePopupTemplates();
                this.UpdatePopupElements();
                this.VisualClient.BeforePopupOpened();
            }
        }

        protected virtual void BeforePreviewKeyDown(KeyEventArgs e)
        {
        }

        internal static double CalcRestrictedValue(double currentValue, double minValue, double maxValue)
        {
            double num = currentValue;
            if (!double.IsNaN(minValue))
            {
                num = Math.Max(currentValue, minValue);
            }
            double num2 = num;
            if (!double.IsNaN(maxValue))
            {
                num2 = Math.Min(num, maxValue);
            }
            return num2;
        }

        public void CancelPopup()
        {
            this.ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode.Cancel);
        }

        protected virtual bool CanShowPopupIfReadOnly() => 
            !base.IsReadOnly || this.ShowPopupIfReadOnly;

        public void ClosePopup()
        {
            this.ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode.Normal);
        }

        protected void ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode closeMode)
        {
            if (this.IsPopupOpen && !this.PropertyProvider.StaysPopupOpen)
            {
                this.popupSettings.SetPopupCloseMode(closeMode);
                this.closingPopupLocker.DoLockedAction<bool>(delegate {
                    bool flag;
                    this.IsPopupOpen = flag = false;
                    return flag;
                });
            }
        }

        internal void ClosePopupInternal()
        {
            this.OnPopupClosed();
            this.PopupSettings.ClosePopup();
            this.RaisePopupClosed(this.popupSettings.PopupCloseMode, base.EditValue);
            PopupBaseEditHelper.SetIsValueChangedViaPopup(this.Settings, false);
            BarsAutomationHelper.RaiseAutomationStructureChanged(this);
        }

        protected virtual void ClosePopupInternal(object parameter)
        {
            DevExpress.Xpf.Editors.PopupCloseMode closeMode = (DevExpress.Xpf.Editors.PopupCloseMode) parameter;
            this.ClosePopup(closeMode);
        }

        protected internal virtual void ClosePopupOnClick()
        {
            DevExpress.Xpf.Editors.PopupCloseMode closePopupOnClickMode = this.PropertyProvider.GetClosePopupOnClickMode();
            this.ClosePopup(closePopupOnClickMode);
        }

        private double CoerceActualPopupHeight(double value) => 
            Math.Max(value, 0.0);

        protected static object CoerceActualPopupHeight(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoerceActualPopupHeight((double) baseValue);

        private double CoerceActualPopupWidth(double value) => 
            Math.Max(value, 0.0);

        protected static object CoerceActualPopupWidth(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoerceActualPopupWidth((double) baseValue);

        private object CoercePopupClosing(object baseValue) => 
            !this.PropertyProvider.StaysPopupOpen ? baseValue : true;

        protected virtual DevExpress.Xpf.Editors.PopupFooterButtons? CoercePopupFooterButtons(DevExpress.Xpf.Editors.PopupFooterButtons? buttons) => 
            buttons;

        protected static object CoercePopupFooterButtons(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoercePopupFooterButtons((DevExpress.Xpf.Editors.PopupFooterButtons?) baseValue);

        private static object CoercePopupHeight(DependencyObject obj, object baseValue)
        {
            PopupBaseEdit edit = (PopupBaseEdit) obj;
            return CoerceSize((double) baseValue, edit.PopupMinHeight, edit.PopupMaxHeight);
        }

        private double CoercePopupMaxHeight(double value) => 
            Math.Max(value, 0.0);

        protected static object CoercePopupMaxHeight(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoercePopupMaxHeight((double) baseValue);

        private double CoercePopupMaxWidth(double value) => 
            Math.Max(value, 0.0);

        private static object CoercePopupMaxWidth(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoercePopupMaxWidth((double) baseValue);

        private double CoercePopupMinHeight(double value) => 
            Math.Max(value, 0.0);

        protected static object CoercePopupMinHeight(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoercePopupMinHeight((double) baseValue);

        private double CoercePopupMinWidth(double value) => 
            Math.Max(value, 0.0);

        private static object CoercePopupMinWidth(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoercePopupMinWidth((double) baseValue);

        private static object CoercePopupWidth(DependencyObject obj, object baseValue)
        {
            PopupBaseEdit edit = (PopupBaseEdit) obj;
            return CoerceSize((double) baseValue, edit.ActualPopupMinWidth, edit.PopupMaxWidth);
        }

        protected virtual bool? CoerceShowSizeGrip(bool? show) => 
            show;

        protected static object CoerceShowSizeGrip(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).CoerceShowSizeGrip((bool?) baseValue);

        private static object CoerceSize(double value, double minLen, double maxLen)
        {
            double naN = double.NaN;
            if (!double.IsNaN(value))
            {
                naN = double.Epsilon;
                double num2 = CalcRestrictedValue(value, minLen, maxLen);
                if (num2 > 0.0)
                {
                    naN = num2;
                }
            }
            return naN;
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new PopupBaseEditPropertyProvider(this);

        protected virtual DevExpress.Xpf.Editors.PopupSettings CreatePopupSettings() => 
            new DevExpress.Xpf.Editors.PopupSettings(this);

        protected virtual VisualClientOwner CreateVisualClient() => 
            new DummyVisualClient(this);

        protected internal virtual void DestroyPopupContent(EditorPopupBase popup)
        {
        }

        protected internal virtual void FindPopupContent()
        {
            this.VisualClient.PopupContentLoaded();
        }

        private void FocusPopup()
        {
            if ((this.PopupFocusElement != null) && this.PropertyProvider.FocusPopupOnOpen)
            {
                FocusHelper.SetFocusable(this.PopupFocusElement, this.PropertyProvider.FocusPopupOnOpen);
                this.PopupFocusElement.Focus();
            }
        }

        public static PopupBaseEdit GetPopupOwnerEdit(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (PopupBaseEdit) DependencyObjectHelper.GetValueWithInheritance(element, PopupOwnerEditProperty);
        }

        protected internal override MaskType[] GetSupportedMaskTypes() => 
            null;

        protected internal override bool IsChildElement(DependencyObject element, DependencyObject root = null) => 
            base.IsChildElement(element, root) || ((this.Popup != null) && ((this.Popup.Child != null) && LayoutHelper.IsChildElementEx(this.Popup.Child, element, false)));

        protected virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            false;

        protected virtual bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            ((key != Key.Escape) || !ModifierKeysHelper.NoModifiers(modifiers)) ? (key == Key.Apps) : true;

        protected virtual bool IsTogglePopupOpenGesture(Key key, ModifierKeys modifiers) => 
            this.Settings.IsTogglePopupOpenGesture(key, modifiers);

        protected internal virtual bool LeavePopupGesture(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            Key key = BaseEditHelper.GetKey(e);
            return (this.IsPopupOpen && this.IsClosePopupWithCancelGesture(key, keyboardModifiers));
        }

        private void MakeFocused()
        {
            if (this.PropertyProvider.GetService<BaseEditingSettingsService>().AllowKeyHandling)
            {
                base.Focus();
            }
        }

        protected internal override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            !this.IsTogglePopupOpenGesture(key, modifiers) ? (this.IsPopupOpen || base.NeedsKey(key, modifiers)) : true;

        protected override void NullValueButtonPlacementChanged(EditorPlacement? newValue)
        {
            this.UpdatePopupElements();
        }

        protected virtual void OnActualPopupHeightChanged()
        {
            this.PopupSettings.SetActualHeightToPopup();
        }

        private static void OnActualPopupHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnActualPopupHeightChanged();
        }

        protected virtual void OnActualPopupMinWidthChanged()
        {
            this.popupSettings.SetMinWidthToPopup();
            this.UpdatePopupWidth();
        }

        private static void OnActualPopupMinWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnActualPopupMinWidthChanged();
        }

        protected virtual void OnActualPopupWidthChanged()
        {
            this.PopupSettings.SetActualWidthToPopup();
        }

        private static void OnActualPopupWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnActualPopupWidthChanged();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new PopupBaseEditAutomationPeer(this);

        protected internal override void OnDefaultButtonClick(object sender, RoutedEventArgs e)
        {
            this.PopupSettings.SetPopupSource(null);
            base.OnDefaultButtonClick(sender, e);
            this.MakeFocused();
            if (this.CanShowByClicking)
            {
                this.IsPopupOpen = true;
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (!this.CreatePopupLocker.IsLocked && (!this.IsEditorKeyboardFocused && this.ClosePopupOnLostFocus))
            {
                this.CancelPopup();
            }
        }

        protected virtual void OnIsPopupOpenChanged()
        {
            if (base.IsLoaded && this.IsInVisualTree())
            {
                this.ProcessIsPopupOpenChanged();
            }
            else if (this.AllowRecreatePopupContent)
            {
                this.PopupContentOwner.Child = null;
            }
        }

        private static void OnIsPopupOpenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnIsPopupOpenChanged();
        }

        protected virtual object OnIsPopupOpenCoerce(object baseValue)
        {
            if (this.IsPopupOpen)
            {
                baseValue = this.CoercePopupClosing(baseValue);
            }
            return (this.ShouldProcessIsPopupOpenedChanging((bool) baseValue) ? baseValue : this.IsPopupOpen);
        }

        private static object OnIsPopupOpenCoerce(DependencyObject obj, object baseValue) => 
            ((PopupBaseEdit) obj).OnIsPopupOpenCoerce(baseValue);

        protected override void OnLoadedInternal()
        {
            base.OnLoadedInternal();
            if (this.IsPopupOpen)
            {
                this.OpenPopupInternal(OpenPopupSyncFlagInternal);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            this.PopupSettings.LostMouseCapture();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            this.MakeFocused();
            if ((e.ChangedButton == MouseButton.Left) && (!this.PropertyProvider.IsTextEditable && this.CanShowByClicking))
            {
                this.ShowPopup();
                e.Handled = this.IsPopupOpen;
            }
        }

        protected virtual void OnPopupClosed()
        {
            if (this.PopupCloseMode != DevExpress.Xpf.Editors.PopupCloseMode.Cancel)
            {
                this.AcceptPopupValue();
            }
            if (this.IsEditorKeyboardFocused)
            {
                base.Focus();
            }
        }

        protected virtual void OnPopupContentChanged()
        {
        }

        protected virtual void OnPopupContentTemplateChanged()
        {
        }

        private static void OnPopupContentTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupContentTemplateChanged();
        }

        protected virtual void OnPopupFooterButtonsChanged()
        {
            this.UpdatePopupButtons();
        }

        private static void OnPopupFooterButtonsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupFooterButtonsChanged();
        }

        protected virtual void OnPopupHeightChanged(double height)
        {
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupHeightProperty, height);
            this.popupSettings.UpdateActualPopupHeight(height);
        }

        protected internal virtual void OnPopupIsKeyboardFocusWithinChanged(EditorPopupBase popupBase)
        {
            if (!this.IsEditorKeyboardFocused && this.ClosePopupOnLostFocus)
            {
                this.CancelPopup();
            }
        }

        protected virtual void OnPopupMaxHeightChanged()
        {
            this.popupSettings.SetMaxHeightToPopup();
            this.UpdatePopupHeight();
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupMaxHeightProperty, this.PopupMaxHeight);
        }

        private static void OnPopupMaxHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupMaxHeightChanged();
        }

        protected virtual void OnPopupMaxWidthChanged()
        {
            this.popupSettings.SetMaxWidthToPopup();
            this.UpdatePopupWidth();
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupMaxWidthProperty, this.PopupMaxWidth);
        }

        private static void OnPopupMaxWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupMaxWidthChanged();
        }

        protected virtual void OnPopupMinHeightChanged()
        {
            this.popupSettings.SetMinHeightToPopup();
            this.UpdatePopupHeight();
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupMinHeightProperty, this.PopupMinHeight);
        }

        private static void OnPopupMinHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupMinHeightChanged();
        }

        protected virtual void OnPopupMinWidthChanged()
        {
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupMinWidthProperty, this.PopupMinWidth);
            this.PopupSettings.UpdateActualPopupMinWidth();
        }

        private static void OnPopupMinWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBaseEdit) obj).OnPopupMinWidthChanged();
        }

        protected virtual void OnPopupOpened()
        {
            this.OnPopupOpenedInternal();
            this.VisualClient.PopupOpened();
            PopupBaseEditHelper.SetIsValueChangedViaPopup(this.Settings, false);
        }

        protected virtual void OnPopupOpenedInternal()
        {
            this.FocusPopup();
        }

        protected virtual void OnPopupWidthChanged(double value)
        {
            this.UpdateSettingsPopupSize(PopupBaseEditSettings.PopupWidthProperty, value);
            this.popupSettings.UpdateActualPopupWidth(value);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            this.BeforePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
            this.AfterPreviewKeyDown(e);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.PopupSettings.UpdateActualPopupMinWidth();
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if ((VisualTreeHelper.GetParent(this) == null) && this.IsPopupOpen)
            {
                this.ClosePopupInternal();
            }
        }

        internal void OpenPopupInternal()
        {
            if (this.IsPopupOpen)
            {
                this.CreatePopupLocker.DoLockedAction(new Action(this.OpenPopupInternalImpl));
            }
        }

        private void OpenPopupInternal(bool sync)
        {
            if (sync)
            {
                this.OpenPopupInternal();
            }
            else
            {
                base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(this.OpenPopupInternal));
            }
        }

        private void OpenPopupInternal(object parameter)
        {
            this.ShowPopup();
        }

        private void OpenPopupInternalImpl()
        {
            if (this.IsInVisualTree())
            {
                this.popupSettings.OpenPopup();
                this.OnPopupOpened();
                this.RaisePopupOpened();
            }
        }

        protected virtual void PopupBottomAreaTemplateChanged()
        {
            this.UpdatePopupTemplates();
        }

        protected virtual void PopupTopAreaTemplateChanged()
        {
            this.UpdatePopupTemplates();
        }

        protected override void ProcessActivatingKeyCore(Key key, ModifierKeys modifiers)
        {
            if (this.IsTogglePopupOpenGesture(key, modifiers))
            {
                this.ShowPopup();
            }
            else
            {
                base.ProcessActivatingKeyCore(key, modifiers);
            }
        }

        private void ProcessIsPopupOpenChanged()
        {
            if (this.IsPopupOpen)
            {
                this.OpenPopupInternal(OpenPopupSyncFlagInternal);
            }
            else
            {
                this.ClosePopupInternal();
            }
        }

        protected internal virtual bool ProcessPopupKeyDown(KeyEventArgs e) => 
            e.Handled;

        protected internal virtual bool ProcessPopupPreviewKeyDown(KeyEventArgs e)
        {
            bool flag;
            if (e.Handled)
            {
                return true;
            }
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            Key key = BaseEditHelper.GetKey(e);
            if (this.IsClosePopupWithCancelGesture(key, keyboardModifiers) && this.IsPopupOpen)
            {
                this.ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode.Cancel);
                e.Handled = flag = true;
                return flag;
            }
            if (this.IsClosePopupWithAcceptGesture(key, keyboardModifiers) && this.IsPopupOpen)
            {
                this.ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode.Normal);
                e.Handled = flag = true;
                return flag;
            }
            if (!this.IsTogglePopupOpenGesture(key, keyboardModifiers))
            {
                return false;
            }
            if (this.IsPopupOpen)
            {
                this.ClosePopup(DevExpress.Xpf.Editors.PopupCloseMode.Cancel);
            }
            else
            {
                this.IsPopupOpen = true;
            }
            e.Handled = flag = true;
            return flag;
        }

        protected internal virtual bool ProcessVisualClientKeyDown(KeyEventArgs e) => 
            this.VisualClient.ProcessKeyDown(e);

        protected internal virtual bool ProcessVisualClientPreviewKeyDown(KeyEventArgs e) => 
            this.VisualClient.ProcessPreviewKeyDown(e);

        protected virtual void RaisePopupClosed(DevExpress.Xpf.Editors.PopupCloseMode mode, object value)
        {
            base.RaiseEvent(new ClosePopupEventArgs(PopupClosedEvent, mode, value));
        }

        protected virtual bool RaisePopupClosing(DevExpress.Xpf.Editors.PopupCloseMode mode, object value)
        {
            ClosingPopupEventArgs e = new ClosingPopupEventArgs(PopupClosingEvent, mode, value);
            base.RaiseEvent(e);
            return (!e.Handled || !e.Cancel);
        }

        protected virtual void RaisePopupOpened()
        {
            base.RaiseEvent(new RoutedEventArgs(PopupOpenedEvent));
        }

        protected virtual bool RaisePopupOpening()
        {
            OpenPopupEventArgs e = new OpenPopupEventArgs(PopupOpeningEvent);
            base.RaiseEvent(e);
            return !e.Cancel;
        }

        protected internal override void RenderCheckChanged(bool isChecked)
        {
            if (isChecked)
            {
                this.ShowPopup();
            }
            else
            {
                this.ClosePopupOnClick();
            }
        }

        internal static void SetPopupOwnerEdit(DependencyObject element, PopupBaseEdit value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(PopupOwnerEditPropertyKey, value);
        }

        private bool ShouldProcessIsPopupOpenedChanging(bool isOpening) => 
            isOpening ? (this.RaisePopupOpening() && this.CanShowPopup) : this.RaisePopupClosing(this.PopupCloseMode, base.EditValue);

        public void ShowPopup()
        {
            this.IsPopupOpen = true;
        }

        protected virtual void ShowSizeGripChanged(bool? value)
        {
            this.UpdatePopupElements();
        }

        protected internal virtual void UpdatePopupButtons()
        {
            if (!base.IsInSupportInitializing)
            {
                base.UpdateButtonInfoCollections();
                if (this.IsPopupOpen)
                {
                    this.PropertyProvider.PopupViewModel.Update();
                }
            }
        }

        protected internal void UpdatePopupElements()
        {
            this.UpdatePopupButtons();
            this.UpdateSizeGrip();
        }

        protected void UpdatePopupHeight()
        {
            base.CoerceValue(PopupHeightProperty);
        }

        protected internal virtual void UpdatePopupTemplates()
        {
            if (!base.IsInSupportInitializing && this.IsPopupOpen)
            {
                this.PropertyProvider.UpdatePopupTemplates();
            }
        }

        protected void UpdatePopupWidth()
        {
            base.CoerceValue(PopupWidthProperty);
        }

        private void UpdateSettingsPopupSize(DependencyProperty property, double value)
        {
            if (this.Settings.IsSharedPopupSize)
            {
                this.Settings.SetValue(property, value);
            }
        }

        protected internal void UpdateSizeGrip()
        {
            if (this.IsPopupOpen)
            {
                this.PropertyProvider.ResizeGripViewModel.Update();
            }
        }

        private Locker CreatePopupLocker { get; set; }

        internal bool IsPopupCloseInProgress =>
            (bool) this.closingPopupLocker;

        protected internal override Type StyleSettingsType =>
            typeof(PopupBaseEditStyleSettings);

        protected PopupBaseEditPropertyProvider PropertyProvider =>
            (PopupBaseEditPropertyProvider) base.PropertyProvider;

        protected DevExpress.Xpf.Editors.PopupCloseMode PopupCloseMode =>
            this.popupSettings.PopupCloseMode;

        private bool CreatedFromSettings =>
            !this.AllowRecreatePopupContent && this.PropertyProvider.CreatedFromSettings;

        protected internal IPopupContentOwner PopupContentOwner =>
            this.CreatedFromSettings ? ((IPopupContentOwner) this.Settings) : ((IPopupContentOwner) this);

        protected internal VisualClientOwner VisualClient { get; }

        public bool ShowPopupIfReadOnly
        {
            get => 
                (bool) base.GetValue(ShowPopupIfReadOnlyProperty);
            set => 
                base.SetValue(ShowPopupIfReadOnlyProperty, value);
        }

        public DevExpress.Xpf.Editors.PopupCloseMode? ClosePopupOnClickMode
        {
            get => 
                (DevExpress.Xpf.Editors.PopupCloseMode?) base.GetValue(ClosePopupOnClickModeProperty);
            set => 
                base.SetValue(ClosePopupOnClickModeProperty, value);
        }

        public bool? IgnorePopupSizeConstraints
        {
            get => 
                (bool?) base.GetValue(IgnorePopupSizeConstraintsProperty);
            set => 
                base.SetValue(IgnorePopupSizeConstraintsProperty, value);
        }

        public bool? StaysPopupOpen
        {
            get => 
                (bool?) base.GetValue(StaysPopupOpenProperty);
            set => 
                base.SetValue(StaysPopupOpenProperty, value);
        }

        [Description("Gets or sets whether the drop-down's content is recreated before it is displayed onscreen. This is a dependency property.")]
        public bool AllowRecreatePopupContent
        {
            get => 
                (bool) base.GetValue(AllowRecreatePopupContentProperty);
            set => 
                base.SetValue(AllowRecreatePopupContentProperty, value);
        }

        [Category("Appearance"), Description("Gets or sets the dropdown's width.")]
        public double PopupWidth
        {
            get => 
                (double) base.GetValue(PopupWidthProperty);
            set => 
                base.SetValue(PopupWidthProperty, value);
        }

        [Category("Appearance"), Description("Gets or sets the dropdown's height.")]
        public double PopupHeight
        {
            get => 
                (double) base.GetValue(PopupHeightProperty);
            set => 
                base.SetValue(PopupHeightProperty, value);
        }

        [Category("Appearance"), Description("Gets a popup window's actual width. This is a dependency property.")]
        public double ActualPopupWidth
        {
            get => 
                (double) base.GetValue(ActualPopupWidthProperty);
            protected internal set => 
                base.SetValue(ActualPopupWidthPropertyKey, value);
        }

        [Category("Appearance"), Description("Gets a popup window's actual height. This is a dependency property.")]
        public double ActualPopupHeight
        {
            get => 
                (double) base.GetValue(ActualPopupHeightProperty);
            protected internal set => 
                base.SetValue(ActualPopupHeightPropertyKey, value);
        }

        [Description("Gets or sets the dropdown's maximum width."), Category("Appearance")]
        public double PopupMaxWidth
        {
            get => 
                (double) base.GetValue(PopupMaxWidthProperty);
            set => 
                base.SetValue(PopupMaxWidthProperty, value);
        }

        [Description("Gets or sets the dropdown's minimum width."), Category("Appearance")]
        public double PopupMinWidth
        {
            get => 
                (double) base.GetValue(PopupMinWidthProperty);
            set => 
                base.SetValue(PopupMinWidthProperty, value);
        }

        [Browsable(false), Description("Gets the popup window's actual minimum width. This is a dependency property.")]
        public double ActualPopupMinWidth
        {
            get => 
                (double) base.GetValue(ActualPopupMinWidthProperty);
            protected internal set => 
                base.SetValue(ActualPopupMinWidthPropertyKey, value);
        }

        [Description("Gets or sets the dropdown's maximum height. This is a dependency property."), Category("Appearance")]
        public double PopupMaxHeight
        {
            get => 
                (double) base.GetValue(PopupMaxHeightProperty);
            set => 
                base.SetValue(PopupMaxHeightProperty, value);
        }

        [Description("Gets or sets the dropdown's minimum height. This is a dependency property."), Category("Appearance")]
        public double PopupMinHeight
        {
            get => 
                (double) base.GetValue(PopupMinHeightProperty);
            set => 
                base.SetValue(PopupMinHeightProperty, value);
        }

        [Category("Behavior")]
        public ICommand ClosePopupCommand { get; private set; }

        [Category("Behavior")]
        public ICommand OpenPopupCommand { get; private set; }

        public bool? FocusPopupOnOpen
        {
            get => 
                (bool?) base.GetValue(FocusPopupOnOpenProperty);
            set => 
                base.SetValue(FocusPopupOnOpenProperty, value);
        }

        protected internal DevExpress.Xpf.Editors.PopupSettings PopupSettings =>
            this.popupSettings;

        public virtual FrameworkElement PopupRoot
        {
            get
            {
                Func<EditorPopupBase, FrameworkElement> evaluator = <>c.<>9__142_0;
                if (<>c.<>9__142_0 == null)
                {
                    Func<EditorPopupBase, FrameworkElement> local1 = <>c.<>9__142_0;
                    evaluator = <>c.<>9__142_0 = x => x.Child as FrameworkElement;
                }
                return this.Popup.With<EditorPopupBase, FrameworkElement>(evaluator);
            }
        }

        public virtual FrameworkElement PopupElement
        {
            get
            {
                Func<FrameworkElement, FrameworkElement> evaluator = <>c.<>9__144_0;
                if (<>c.<>9__144_0 == null)
                {
                    Func<FrameworkElement, FrameworkElement> local1 = <>c.<>9__144_0;
                    evaluator = <>c.<>9__144_0 = x => LayoutHelper.FindElementByName(x, "PART_PopupContent");
                }
                return this.PopupRoot.With<FrameworkElement, FrameworkElement>(evaluator);
            }
        }

        internal FrameworkElement PopupFocusElement =>
            this.PopupElement ?? ((PopupContentControl) this.PopupContentOwner.Child);

        internal static bool OpenPopupSyncFlagInternal
        {
            get => 
                false;
            set => 
                openPopupSyncFlag = value;
        }

        protected internal PopupBaseEditSettings Settings =>
            (PopupBaseEditSettings) base.Settings;

        [Description("Gets or sets the template that defines the presentation of the popup window's content. This is a dependency property.")]
        public ControlTemplate PopupContentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupContentTemplateProperty);
            set => 
                base.SetValue(PopupContentTemplateProperty, value);
        }

        [Browsable(false), Description("Gets or sets the template that defines the presentation of the popup window's content container. This is a dependency property.")]
        public ControlTemplate PopupContentContainerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupContentContainerTemplateProperty);
            set => 
                base.SetValue(PopupContentContainerTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the top area within an editor's dropdown."), Browsable(false)]
        public ControlTemplate PopupTopAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupTopAreaTemplateProperty);
            set => 
                base.SetValue(PopupTopAreaTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the popup window's bottom area. This is a dependency property."), Browsable(false)]
        public ControlTemplate PopupBottomAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupBottomAreaTemplateProperty);
            set => 
                base.SetValue(PopupBottomAreaTemplateProperty, value);
        }

        [Description("Gets or sets which buttons are displayed within the editor's dropdown. This is a dependency property."), Category("Behavior")]
        public DevExpress.Xpf.Editors.PopupFooterButtons? PopupFooterButtons
        {
            get => 
                (DevExpress.Xpf.Editors.PopupFooterButtons?) base.GetValue(PopupFooterButtonsProperty);
            set => 
                base.SetValue(PopupFooterButtonsProperty, value);
        }

        [Description("Gets or sets whether to show the size grip within the editor's drop-down."), Category("Behavior"), TypeConverter(typeof(NullableBoolConverter))]
        public bool? ShowSizeGrip
        {
            get => 
                (bool?) base.GetValue(ShowSizeGripProperty);
            set => 
                base.SetValue(ShowSizeGripProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of an editor's popup window. This is a dependency property."), Browsable(false)]
        public ControlTemplate PopupTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupTemplateProperty);
            set => 
                base.SetValue(PopupTemplateProperty, value);
        }

        [Category("Common Properties"), Description("Gets or sets whether the editor's dropdown (popup window) is opened. This is a dependency property.")]
        public bool IsPopupOpen
        {
            get => 
                (bool) base.GetValue(IsPopupOpenProperty);
            set => 
                base.SetValue(IsPopupOpenProperty, value);
        }

        protected internal override bool IsEditorKeyboardFocused
        {
            get
            {
                if (!base.IsEditorKeyboardFocused && ((this.Popup == null) || ((this.Popup.Child == null) || !FocusHelper.IsKeyboardFocusWithin(this.Popup))))
                {
                    Func<ContextMenu, UIElement> evaluator = <>c.<>9__225_0;
                    if (<>c.<>9__225_0 == null)
                    {
                        Func<ContextMenu, UIElement> local1 = <>c.<>9__225_0;
                        evaluator = <>c.<>9__225_0 = x => x.PlacementTarget;
                    }
                    if ((Keyboard.FocusedElement as FrameworkElement).With<FrameworkElement, ContextMenu>(new Func<FrameworkElement, ContextMenu>(LayoutHelper.FindParentObject<ContextMenu>)).With<ContextMenu, UIElement>(evaluator).With<UIElement, PopupBaseEdit>(new Func<UIElement, PopupBaseEdit>(PopupBaseEdit.GetPopupOwnerEdit)) != this)
                    {
                        Func<FrameworkElement, BarPopupBase> func2 = <>c.<>9__225_1;
                        if (<>c.<>9__225_1 == null)
                        {
                            Func<FrameworkElement, BarPopupBase> local2 = <>c.<>9__225_1;
                            func2 = <>c.<>9__225_1 = x => BarManagerHelper.GetPopup(x);
                        }
                        Func<BarPopupBase, UIElement> func3 = <>c.<>9__225_2;
                        if (<>c.<>9__225_2 == null)
                        {
                            Func<BarPopupBase, UIElement> local3 = <>c.<>9__225_2;
                            func3 = <>c.<>9__225_2 = x => x.PlacementTarget;
                        }
                        return ((Keyboard.FocusedElement as FrameworkElement).With<FrameworkElement, BarPopupBase>(func2).With<BarPopupBase, UIElement>(func3).With<UIElement, PopupBaseEdit>(new Func<UIElement, PopupBaseEdit>(PopupBaseEdit.GetPopupOwnerEdit)) == this);
                    }
                }
                return true;
            }
        }

        protected override bool HandlesScrolling =>
            false;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ClosePopupOnLostFocus
        {
            get => 
                this.closePopupOnLostFocus;
            set => 
                this.closePopupOnLostFocus = value;
        }

        [Description(""), Browsable(false)]
        protected internal virtual bool CanShowPopup =>
            this.IsEditingMode() && (this.IsInVisualTree() && this.CanShowPopupIfReadOnly());

        protected internal EditorPopupBase Popup =>
            this.popupSettings.Popup;

        protected internal virtual bool ShouldApplyPopupSize =>
            true;

        private bool CanShowByClicking =>
            base.IsEnabled;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                IEnumerator logicalChildren = base.LogicalChildren;
                if (logicalChildren != null)
                {
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                if (this.child != null)
                {
                    list.Add(this.child);
                }
                return list.GetEnumerator();
            }
        }

        FrameworkElement IPopupContentOwner.Child
        {
            get => 
                this.child;
            set
            {
                if (!ReferenceEquals(value, this.child))
                {
                    base.RemoveLogicalChild(this.child);
                    this.child = value;
                    base.AddLogicalChild(this.child);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupBaseEdit.<>c <>9 = new PopupBaseEdit.<>c();
            public static Func<EditorPopupBase, FrameworkElement> <>9__142_0;
            public static Func<FrameworkElement, FrameworkElement> <>9__144_0;
            public static Func<ContextMenu, UIElement> <>9__225_0;
            public static Func<FrameworkElement, BarPopupBase> <>9__225_1;
            public static Func<BarPopupBase, UIElement> <>9__225_2;

            internal void <.cctor>b__37_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupBaseEdit) d).PopupTopAreaTemplateChanged();
            }

            internal void <.cctor>b__37_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupBaseEdit) d).PopupBottomAreaTemplateChanged();
            }

            internal void <.cctor>b__37_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupBaseEdit) d).OnPopupWidthChanged((double) e.NewValue);
            }

            internal void <.cctor>b__37_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupBaseEdit) d).OnPopupHeightChanged((double) e.NewValue);
            }

            internal void <.cctor>b__37_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupBaseEdit) d).ShowSizeGripChanged((bool?) e.NewValue);
            }

            internal UIElement <get_IsEditorKeyboardFocused>b__225_0(ContextMenu x) => 
                x.PlacementTarget;

            internal BarPopupBase <get_IsEditorKeyboardFocused>b__225_1(FrameworkElement x) => 
                BarManagerHelper.GetPopup(x);

            internal UIElement <get_IsEditorKeyboardFocused>b__225_2(BarPopupBase x) => 
                x.PlacementTarget;

            internal FrameworkElement <get_PopupElement>b__144_0(FrameworkElement x) => 
                LayoutHelper.FindElementByName(x, "PART_PopupContent");

            internal FrameworkElement <get_PopupRoot>b__142_0(EditorPopupBase x) => 
                x.Child as FrameworkElement;
        }
    }
}

