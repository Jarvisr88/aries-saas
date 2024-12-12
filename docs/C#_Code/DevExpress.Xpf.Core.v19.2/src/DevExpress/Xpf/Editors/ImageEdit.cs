namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Flyout;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class ImageEdit : BaseEdit, IImageEdit, IInputElement, IImageExportSettings, IExportSettings
    {
        private const string FlyoutName = "PART_MenuFlyout";
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
        public static readonly DependencyProperty ImageEffectProperty;
        public static readonly RoutedEvent ConvertEditValueEvent;

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

        static ImageEdit()
        {
            Type ownerType = typeof(ImageEdit);
            SourceProperty = DependencyPropertyManager.Register("Source", typeof(ImageSource), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ImageEdit.OnSourceChanged), new CoerceValueCallback(ImageEdit.CoerceSource)));
            ShowMenuProperty = DependencyPropertyManager.Register("ShowMenu", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (obj, baseValue) => ((ImageEdit) obj).ShowMenuChanged(), new CoerceValueCallback(ImageEdit.CoerceShowMenu)));
            ShowMenuModeProperty = DependencyPropertyManager.Register("ShowMenuMode", typeof(DevExpress.Xpf.Editors.ShowMenuMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ShowMenuMode.Hover, (obj, baseValue) => ((ImageEdit) obj).ShowMenuModeChanged()));
            StretchProperty = DependencyPropertyManager.Register("Stretch", typeof(System.Windows.Media.Stretch), ownerType, new FrameworkPropertyMetadata(System.Windows.Media.Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsMeasure));
            HasImagePropertyKey = DependencyPropertyManager.RegisterReadOnly("HasImage", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasImageProperty = HasImagePropertyKey.DependencyProperty;
            EmptyContentTemplateProperty = DependencyPropertyManager.Register("EmptyContentTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ShowLoadDialogOnClickModeProperty = DependencyPropertyManager.Register("ShowLoadDialogOnClickMode", typeof(DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode.Empty));
            MenuTemplateProperty = DependencyPropertyManager.Register("MenuTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            MenuContainerTemplateProperty = DependencyPropertyManager.Register("MenuContainerTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ImageEffectProperty = DependencyPropertyManager.Register("ImageEffect", typeof(Effect), ownerType, new FrameworkPropertyMetadata(null));
            ConvertEditValueEvent = EventManager.RegisterRoutedEvent("ConvertEditValue", RoutingStrategy.Direct, typeof(ConvertEditValueEventHandler), ownerType);
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Cut, (d, e) => ((ImageEdit) d).Cut(), (d, e) => ((ImageEdit) d).CanCut(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Delete, (d, e) => ((ImageEdit) d).Clear(), (d, e) => ((ImageEdit) d).CanDelete(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Copy, (d, e) => ((ImageEdit) d).Copy(), (d, e) => ((ImageEdit) d).CanCopy(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Paste, (d, e) => ((ImageEdit) d).Paste(), (d, e) => ((ImageEdit) d).CanPaste(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Open, (d, e) => ((ImageEdit) d).Load(), (d, e) => ((ImageEdit) d).CanLoad(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Save, (d, e) => ((ImageEdit) d).Save(), (d, e) => ((ImageEdit) d).CanSave(d, e)));
        }

        public ImageEdit()
        {
            this.SetDefaultStyleKey(typeof(ImageEdit));
        }

        protected virtual void CanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.HasImage && !BrowserInteropHelper.IsBrowserHosted;
        }

        protected virtual void CanCut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (base.IsEnabled && (!base.IsReadOnly && this.HasImage)) && !BrowserInteropHelper.IsBrowserHosted;
        }

        protected virtual void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (base.IsEnabled && (!base.IsReadOnly && this.HasImage)) && !BrowserInteropHelper.IsBrowserHosted;
        }

        protected virtual void CanLoad(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanLoadCore() && !BrowserInteropHelper.IsBrowserHosted;
        }

        protected virtual bool CanLoadCore() => 
            true;

        protected virtual void CanPaste(object sender, CanExecuteRoutedEventArgs e)
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = !base.IsReadOnly && this.ClipboardContainsImage();
            }
        }

        protected virtual void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanSaveCore() && !BrowserInteropHelper.IsBrowserHosted;
        }

        protected virtual bool CanSaveCore() => 
            this.HasImage;

        public virtual void Clear()
        {
            base.SetCurrentValue(SourceProperty, null);
        }

        private bool ClipboardContainsImage() => 
            Clipboard.ContainsImage();

        protected virtual object CoerceShowMenu(bool value) => 
            value;

        private static object CoerceShowMenu(DependencyObject d, object value) => 
            ((ImageEdit) d).CoerceShowMenu((bool) value);

        protected virtual object CoerceSource(ImageSource value) => 
            this.EditStrategy.CoerceSource(value);

        private static object CoerceSource(DependencyObject d, object value) => 
            ((ImageEdit) d).CoerceSource((ImageSource) value);

        public virtual void Copy()
        {
            if (this.HasImage && !BrowserInteropHelper.IsBrowserHosted)
            {
                this.CopyCore();
            }
        }

        protected virtual void CopyCore()
        {
            try
            {
                Clipboard.SetImage(ImageLoader.GetSafeBitmapSource((BitmapSource) this.Source, this.ImageEffect));
            }
            catch
            {
            }
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new ImageEditPropertyProvider(this);

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new ImageEditSettings();

        protected override EditStrategyBase CreateEditStrategy() => 
            new ImageEditStrategy(this);

        public virtual void Cut()
        {
            this.Copy();
            this.Clear();
        }

        object IImageEdit.GetDataFromImage(ImageSource source) => 
            (this.PopupOwnerEdit == null) ? this.GetDataFromImageCore(source) : this.PopupOwnerEdit.GetDataFromImage(source);

        protected void DoShowLoadDialogOnClick()
        {
            if (!base.IsReadOnly && ((((this.ShowLoadDialogOnClickMode == DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode.Empty) && !this.HasImage) || (this.ShowLoadDialogOnClickMode == DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode.Always)) && ((Stylus.DirectlyOver == null) || LayoutHelper.IsChildElement(this, Stylus.DirectlyOver as DependencyObject))))
            {
                this.Load();
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

        internal FlyoutControl GetMenuFlyout() => 
            LayoutHelper.FindElementByName(this, "PART_MenuFlyout") as FlyoutControl;

        protected internal void HideMenuPopup()
        {
            Action<FlyoutControl> action = <>c.<>9__92_0;
            if (<>c.<>9__92_0 == null)
            {
                Action<FlyoutControl> local1 = <>c.<>9__92_0;
                action = <>c.<>9__92_0 = x => x.IsOpen = false;
            }
            this.GetMenuFlyout().Do<FlyoutControl>(action);
        }

        public void Load()
        {
            this.HideMenuPopup();
            if (!this.IsInactiveMode)
            {
                if (this.PopupOwnerEdit != null)
                {
                    this.PopupOwnerEdit.Load();
                }
                else
                {
                    this.LoadCore();
                }
            }
        }

        protected virtual void LoadCore()
        {
            if (this.Image != null)
            {
                ImageSource imageSource = ImageLoader.LoadImage();
                if (imageSource != null)
                {
                    this.EditStrategy.SetImage(imageSource);
                }
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (base.EditMode != EditMode.InplaceInactive)
            {
                this.DoShowLoadDialogOnClick();
            }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImageEdit) d).OnSourceChanged((ImageSource) e.OldValue, (ImageSource) e.NewValue);
        }

        protected virtual void OnSourceChanged(ImageSource oldValue, ImageSource newValue)
        {
            this.HasImage = newValue != null;
            this.EditStrategy.OnSourceChanged(oldValue, newValue);
        }

        public virtual void Paste()
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                this.PasteCore();
            }
        }

        protected virtual void PasteCore()
        {
            if (!base.IsReadOnly)
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(ImageLoader.ImageToByteArray(Clipboard.GetImage())))
                    {
                        this.Source = ImageHelper.CreateImageFromStream(stream);
                    }
                }
                catch
                {
                }
            }
        }

        public void Save()
        {
            this.HideMenuPopup();
            if (!this.IsInactiveMode)
            {
                if (this.PopupOwnerEdit != null)
                {
                    this.PopupOwnerEdit.Save();
                }
                else
                {
                    this.SaveCore();
                }
            }
        }

        protected virtual void SaveCore()
        {
            if (this.HasImage)
            {
                ImageLoader.SaveImage(ImageLoader.GetSafeBitmapSource((BitmapSource) this.Source, this.ImageEffect));
            }
        }

        protected virtual void ShowMenuChanged()
        {
            this.UpdateShowMenu();
        }

        protected virtual void ShowMenuModeChanged()
        {
            this.UpdateShowMenu();
        }

        private void UpdateShowMenu()
        {
            this.PropertyProvider.UpdateInplaceMenuVisibility();
            if (!this.CanShowMenuPopup)
            {
                this.HideMenuPopup();
            }
        }

        [Description("Gets or sets a value indicating whether the menu is displayed when the mouse pointer is hovered over the image. This is a dependency property.")]
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

        [Description("Gets whether the editor displays an image. This is a dependency property.")]
        public bool HasImage
        {
            get => 
                (bool) base.GetValue(HasImageProperty);
            private set => 
                base.SetValue(HasImagePropertyKey, value);
        }

        [Description("Gets or sets a template that defines the presentation of an empty editor. This is a dependency property.")]
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

        [Description("Gets or sets whether clicking within the editor shows the 'Open' dialog. This is a dependency property.")]
        public DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode ShowLoadDialogOnClickMode
        {
            get => 
                (DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode) base.GetValue(ShowLoadDialogOnClickModeProperty);
            set => 
                base.SetValue(ShowLoadDialogOnClickModeProperty, value);
        }

        [Description("Gets or sets the template used to display the Image Menu. This is a dependency property.")]
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

        protected internal System.Windows.Controls.Image Image =>
            base.EditCore as System.Windows.Controls.Image;

        protected ImageEditStrategy EditStrategy =>
            base.EditStrategy as ImageEditStrategy;

        protected internal IImageEdit PopupOwnerEdit =>
            PopupBaseEdit.GetPopupOwnerEdit(this) as IImageEdit;

        protected internal ImageEditSettings Settings =>
            base.Settings as ImageEditSettings;

        protected internal ImageEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as ImageEditPropertyProvider;

        protected internal bool CanShowMenuPopup =>
            this.ShowMenu && ((this.ShowMenuMode == DevExpress.Xpf.Editors.ShowMenuMode.Hover) && (!this.IsInactiveMode && !base.IsReadOnly));

        FrameworkElement IImageExportSettings.SourceElement =>
            this.Image;

        ImageRenderMode IImageExportSettings.ImageRenderMode =>
            ImageRenderMode.UseImageSource;

        bool IImageExportSettings.ForceCenterImageMode =>
            false;

        object IImageExportSettings.ImageKey =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageEdit.<>c <>9 = new ImageEdit.<>c();
            public static Action<FlyoutControl> <>9__92_0;

            internal void <.cctor>b__13_0(DependencyObject obj, DependencyPropertyChangedEventArgs baseValue)
            {
                ((ImageEdit) obj).ShowMenuChanged();
            }

            internal void <.cctor>b__13_1(DependencyObject obj, DependencyPropertyChangedEventArgs baseValue)
            {
                ((ImageEdit) obj).ShowMenuModeChanged();
            }

            internal void <.cctor>b__13_10(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Load();
            }

            internal void <.cctor>b__13_11(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanLoad(d, e);
            }

            internal void <.cctor>b__13_12(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Save();
            }

            internal void <.cctor>b__13_13(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanSave(d, e);
            }

            internal void <.cctor>b__13_2(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Cut();
            }

            internal void <.cctor>b__13_3(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanCut(d, e);
            }

            internal void <.cctor>b__13_4(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Clear();
            }

            internal void <.cctor>b__13_5(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanDelete(d, e);
            }

            internal void <.cctor>b__13_6(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Copy();
            }

            internal void <.cctor>b__13_7(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanCopy(d, e);
            }

            internal void <.cctor>b__13_8(object d, ExecutedRoutedEventArgs e)
            {
                ((ImageEdit) d).Paste();
            }

            internal void <.cctor>b__13_9(object d, CanExecuteRoutedEventArgs e)
            {
                ((ImageEdit) d).CanPaste(d, e);
            }

            internal void <HideMenuPopup>b__92_0(FlyoutControl x)
            {
                x.IsOpen = false;
            }
        }
    }
}

