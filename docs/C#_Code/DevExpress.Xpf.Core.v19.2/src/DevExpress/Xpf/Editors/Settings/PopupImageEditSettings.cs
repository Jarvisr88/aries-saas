namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class PopupImageEditSettings : PopupBaseEditSettings
    {
        public static readonly DependencyProperty ShowMenuProperty;
        public static readonly DependencyProperty ShowMenuModeProperty;
        public static readonly DependencyProperty StretchProperty;
        public static readonly DependencyProperty ShowLoadDialogOnClickModeProperty;
        public static readonly DependencyProperty ImageEffectProperty;
        private static object convertEditValue = new object();

        public event ConvertEditValueEventHandler ConvertEditValue
        {
            add
            {
                base.AddHandler(convertEditValue, value);
            }
            remove
            {
                base.RemoveHandler(convertEditValue, value);
            }
        }

        static PopupImageEditSettings()
        {
            Type ownerType = typeof(PopupImageEditSettings);
            ShowMenuProperty = ImageEditSettings.ShowMenuProperty.AddOwner(ownerType);
            ShowMenuModeProperty = ImageEditSettings.ShowMenuModeProperty.AddOwner(ownerType);
            StretchProperty = ImageEditSettings.StretchProperty.AddOwner(ownerType);
            ShowLoadDialogOnClickModeProperty = ImageEditSettings.ShowLoadDialogOnClickModeProperty.AddOwner(ownerType);
            ImageEffectProperty = ImageEditSettings.ImageEffectProperty.AddOwner(ownerType);
            ButtonEditSettings.IsTextEditableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
            PopupBaseEditSettings.PopupFooterButtonsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(PopupFooterButtons.None));
            PopupBaseEditSettings.ShowSizeGripProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEditSettings.PopupMinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(200.0));
            PopupBaseEditSettings.PopupMinWidthProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(200.0));
            PopupBaseEditSettings.IsSharedPopupSizeProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PopupImageEdit editor = edit as PopupImageEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(ShowMenuProperty, () => editor.ShowMenu = this.ShowMenu);
                base.SetValueFromSettings(ShowMenuModeProperty, () => editor.ShowMenuMode = this.ShowMenuMode);
                base.SetValueFromSettings(StretchProperty, () => editor.Stretch = this.Stretch);
                base.SetValueFromSettings(ShowLoadDialogOnClickModeProperty, () => editor.ShowLoadDialogOnClickMode = this.ShowLoadDialogOnClickMode);
                base.SetValueFromSettings(ImageEffectProperty, () => editor.ImageEffect = this.ImageEffect);
            }
        }

        protected internal virtual void RaiseConvertEditValue(DependencyObject sender, ConvertEditValueEventArgs e)
        {
            Delegate delegate2;
            if (base.Events.TryGetValue(convertEditValue, out delegate2))
            {
                ((ConvertEditValueEventHandler) delegate2)(sender, e);
            }
        }

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

        public System.Windows.Media.Stretch Stretch
        {
            get => 
                (System.Windows.Media.Stretch) base.GetValue(StretchProperty);
            set => 
                base.SetValue(StretchProperty, value);
        }

        public DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode ShowLoadDialogOnClickMode
        {
            get => 
                (DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode) base.GetValue(ShowLoadDialogOnClickModeProperty);
            set => 
                base.SetValue(ShowLoadDialogOnClickModeProperty, value);
        }

        public Effect ImageEffect
        {
            get => 
                (Effect) base.GetValue(ImageEffectProperty);
            set => 
                base.SetValue(ImageEffectProperty, value);
        }
    }
}

