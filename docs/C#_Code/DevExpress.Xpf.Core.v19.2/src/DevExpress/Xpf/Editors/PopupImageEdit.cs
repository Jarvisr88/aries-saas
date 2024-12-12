namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class PopupImageEdit : PopupBaseEdit, IImageEdit, IInputElement
    {
        public const double DefaultPopupMinHeight = 200.0;
        public const double DefaultPopupMinWidth = 200.0;
        public static readonly DependencyProperty ShowMenuProperty;
        public static readonly DependencyProperty ShowMenuModeProperty;
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty StretchProperty;
        public static readonly DependencyProperty HasImageProperty;
        private static readonly DependencyPropertyKey HasImagePropertyKey;
        public static readonly DependencyProperty EmptyContentTemplateProperty;
        public static readonly DependencyProperty ShowLoadDialogOnClickModeProperty;
        public static readonly DependencyProperty MenuTemplateProperty;
        public static readonly DependencyProperty MenuContainerTemplateProperty;
        public static readonly DependencyProperty AutoSizePopupProperty;
        public static readonly DependencyProperty ImageEffectProperty;
        public static readonly RoutedEvent ConvertEditValueEvent;
        public static readonly RoutedEvent ImageFailedEvent;
        public static readonly RoutedCommand OKCommand;
        public static readonly RoutedCommand CancelCommand;

        public event ConvertEditValueEventHandler ConvertEditValue
        {
            add
            {
                base.AddHandler(ConvertEditValueEvent, value);
            }
            remove
            {
                base.RemoveHandler(ConvertEditValueEvent, value);
            }
        }

        public event ImageFailedEventHandler ImageFailed;

        static PopupImageEdit()
        {
            Type ownerType = typeof(PopupImageEdit);
            ShowMenuProperty = ImageEdit.ShowMenuProperty.AddOwner(ownerType);
            ShowMenuModeProperty = ImageEdit.ShowMenuModeProperty.AddOwner(ownerType);
            SourceProperty = ImageEdit.SourceProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(PopupImageEdit.OnSourcePropertyChanged), new CoerceValueCallback(PopupImageEdit.CoerceSource)));
            StretchProperty = ImageEdit.StretchProperty.AddOwner(ownerType);
            HasImagePropertyKey = DependencyPropertyManager.RegisterReadOnly("HasImage", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasImageProperty = HasImagePropertyKey.DependencyProperty;
            EmptyContentTemplateProperty = ImageEdit.EmptyContentTemplateProperty.AddOwner(ownerType);
            ShowLoadDialogOnClickModeProperty = ImageEdit.ShowLoadDialogOnClickModeProperty.AddOwner(ownerType);
            MenuTemplateProperty = ImageEdit.MenuTemplateProperty.AddOwner(ownerType);
            MenuContainerTemplateProperty = ImageEdit.MenuContainerTemplateProperty.AddOwner(ownerType);
            ImageEffectProperty = ImageEdit.ImageEffectProperty.AddOwner(ownerType);
            AutoSizePopupProperty = DependencyPropertyManager.Register("AutoSizePopup", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            ConvertEditValueEvent = EventManager.RegisterRoutedEvent("ConvertEditValue", RoutingStrategy.Direct, typeof(ConvertEditValueEventHandler), ownerType);
            OKCommand = new RoutedCommand("OK", ownerType);
            CancelCommand = new RoutedCommand("Cancel", ownerType);
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(OKCommand, (d, e) => ((PopupImageEdit) d).ClosePopup(), null));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(CancelCommand, (d, e) => ((PopupImageEdit) d).CancelPopup(), null));
            ImageFailedEvent = EventManager.RegisterRoutedEvent("ImageFailed", RoutingStrategy.Direct, typeof(ImageFailedEventHandler), ownerType);
            ButtonEdit.IsTextEditableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
            PopupBaseEdit.PopupFooterButtonsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(PopupFooterButtons.None));
            PopupBaseEdit.ShowSizeGripProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEdit.PopupMinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(200.0));
            PopupBaseEdit.PopupMinWidthProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(200.0));
        }

        public PopupImageEdit()
        {
            this.SetDefaultStyleKey(typeof(PopupImageEdit));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptPopupValue();
        }

        protected virtual object CoerceSource(ImageSource value) => 
            this.EditStrategy.CoerceSource(value);

        private static object CoerceSource(DependencyObject d, object value) => 
            ((PopupImageEdit) d).CoerceSource((ImageSource) value);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new PopupImageEditPropertyProvider(this);

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new PopupImageEditSettings();

        protected override EditStrategyBase CreateEditStrategy() => 
            new PopupImageEditStrategy(this);

        object IImageEdit.GetDataFromImage(ImageSource source) => 
            this.GetDataFromImageCore(source);

        void IImageEdit.Load()
        {
            base.CancelPopup();
            ImageSource source = ImageLoader.LoadImage();
            if (source != null)
            {
                this.Source = source;
                this.EditStrategy.UpdateDisplayText();
            }
        }

        void IImageEdit.Save()
        {
            base.CancelPopup();
            if (this.Source != null)
            {
                ImageLoader.SaveImage(ImageLoader.GetSafeBitmapSource((BitmapSource) this.Source, this.ImageEffect));
            }
        }

        protected virtual object GetDataFromImageCore(ImageSource source)
        {
            ConvertEditValueEventArgs args1 = new ConvertEditValueEventArgs(ConvertEditValueEvent);
            args1.ImageSource = source;
            ConvertEditValueEventArgs e = args1;
            base.RaiseEvent(e);
            this.Settings.RaiseConvertEditValue(this, e);
            return (!e.Handled ? source : e.EditValue);
        }

        protected ImageEdit GetImageEditControl()
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__75_0;
            if (<>c.<>9__75_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__75_0;
                predicate = <>c.<>9__75_0 = element => (element is ImageEdit) && (element.Name == "PART_PopupContent");
            }
            return (LayoutHelper.FindElement(base.Popup.Child as FrameworkElement, predicate) as ImageEdit);
        }

        protected override void OnPopupClosed()
        {
            base.OnPopupClosed();
            if (this.ImageEditControl != null)
            {
                this.ImageEditControl.HideMenuPopup();
            }
            this.ImageEditControl = null;
        }

        protected override void OnPopupOpened()
        {
            this.ImageEditControl = this.GetImageEditControl();
            base.OnPopupOpened();
            this.SetInitialPopupSize();
            this.EditStrategy.SyncWithValue();
        }

        protected virtual void OnSourcePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.HasImage = e.NewValue != null;
            this.EditStrategy.SourceChanged((ImageSource) e.OldValue, (ImageSource) e.NewValue);
        }

        private static void OnSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PopupImageEdit) obj).OnSourcePropertyChanged(e);
        }

        private void RaiseImageFailed(Exception exception)
        {
            ImageFailedEventArgs e = new ImageFailedEventArgs(ImageFailedEvent, this, exception);
            base.RaiseEvent(e);
            if (this.ImageFailed != null)
            {
                this.ImageFailed(this, e);
            }
        }

        private void SetInitialPopupSize()
        {
            try
            {
                if (((double.IsNaN(base.ActualPopupWidth) || double.IsNaN(base.ActualPopupHeight)) && (this.AutoSizePopup && ((this.Source != null) && (this.Source.Width > 0.0)))) && (this.Source.Height > 0.0))
                {
                    double num = 0.0;
                    double num2 = 0.0;
                    double num3 = 0.0;
                    num2 = Math.Max(base.PopupMinWidth, base.ActualWidth) / this.Source.Width;
                    num3 = base.PopupMinHeight / this.Source.Height;
                    num = (num3 < num2) ? num3 : num2;
                    base.ActualPopupWidth = Math.Max(base.PopupMinWidth, this.Source.Width * num);
                    base.ActualPopupHeight = Math.Max(base.PopupMinHeight, this.Source.Height * num);
                }
            }
            catch (Exception exception)
            {
                base.SetCurrentValue(SourceProperty, null);
                this.RaiseImageFailed(exception);
            }
        }

        protected internal virtual void UpdateBaseUri()
        {
            this.Source.Do<ImageSource>(x => ImageHelper.UpdateBaseUri(this, x));
        }

        public override FrameworkElement PopupElement =>
            this.ImageEditControl;

        protected internal ImageEdit ImageEditControl { get; private set; }

        protected internal PopupImageEditSettings Settings =>
            base.Settings as PopupImageEditSettings;

        protected PopupImageEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as PopupImageEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        [Description("Gets or sets an image displayed within the editor. This is a dependency property.")]
        public ImageSource Source
        {
            get => 
                (ImageSource) base.GetValue(SourceProperty);
            set => 
                base.SetValue(SourceProperty, value);
        }

        [Description("Gets or sets a value that specifies how an image should be stretched to fill the available space. This is a dependency property.")]
        public System.Windows.Media.Stretch Stretch
        {
            get => 
                (System.Windows.Media.Stretch) base.GetValue(StretchProperty);
            set => 
                base.SetValue(StretchProperty, value);
        }

        [Description("Gets or sets a value indicating whether the menu is displayed when the mouse pointer is hovered over the dropdown. This is a dependency property.")]
        public bool ShowMenu
        {
            get => 
                (bool) base.GetValue(ShowMenuProperty);
            set => 
                base.SetValue(ShowMenuProperty, value);
        }

        public DevExpress.Xpf.Editors.ShowMenuMode ShowMenuMode
        {
            get => 
                (DevExpress.Xpf.Editors.ShowMenuMode) base.GetValue(ShowMenuModeProperty);
            set => 
                base.SetValue(ShowMenuModeProperty, value);
        }

        [Description("Gets whether the editor displays an image. This is a dependency property.")]
        public bool HasImage
        {
            get => 
                (bool) base.GetValue(HasImageProperty);
            protected internal set => 
                base.SetValue(HasImagePropertyKey, value);
        }

        [Description("Gets or sets a template that defines the presentation of an empty editor's dropdown. This is a dependency property.")]
        public ControlTemplate EmptyContentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyContentTemplateProperty);
            set => 
                base.SetValue(EmptyContentTemplateProperty, value);
        }

        [Description("Gets or sets a bitmap effect. This is a dependency property.")]
        public Effect ImageEffect
        {
            get => 
                (Effect) base.GetValue(ImageEffectProperty);
            set => 
                base.SetValue(ImageEffectProperty, value);
        }

        [Description("Gets or sets whether clicking within the editor's dropdown shows the \"Open\" dialog. This is a dependency property.")]
        public DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode ShowLoadDialogOnClickMode
        {
            get => 
                (DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode) base.GetValue(ShowLoadDialogOnClickModeProperty);
            set => 
                base.SetValue(ShowLoadDialogOnClickModeProperty, value);
        }

        [Description("Gets or sets the template used to display the image menu. This is a dependency property.")]
        public ControlTemplate MenuTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MenuTemplateProperty);
            set => 
                base.SetValue(MenuTemplateProperty, value);
        }

        [Browsable(false)]
        public ControlTemplate MenuContainerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MenuContainerTemplateProperty);
            set => 
                base.SetValue(MenuContainerTemplateProperty, value);
        }

        [Description("Gets or sets whether the dropdown is automatically sized to fit the width of the edit box. This is a dependency property.")]
        public bool AutoSizePopup
        {
            get => 
                (bool) base.GetValue(AutoSizePopupProperty);
            set => 
                base.SetValue(AutoSizePopupProperty, value);
        }

        protected internal override bool ShouldApplyPopupSize =>
            !this.AutoSizePopup;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupImageEdit.<>c <>9 = new PopupImageEdit.<>c();
            public static Predicate<FrameworkElement> <>9__75_0;

            internal void <.cctor>b__18_0(object d, ExecutedRoutedEventArgs e)
            {
                ((PopupImageEdit) d).ClosePopup();
            }

            internal void <.cctor>b__18_1(object d, ExecutedRoutedEventArgs e)
            {
                ((PopupImageEdit) d).CancelPopup();
            }

            internal bool <GetImageEditControl>b__75_0(FrameworkElement element) => 
                (element is ImageEdit) && (element.Name == "PART_PopupContent");
        }
    }
}

