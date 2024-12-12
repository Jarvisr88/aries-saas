namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ToggleSwitchEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty IsThreeStateProperty;
        public static readonly DependencyProperty CheckedStateContentProperty;
        public static readonly DependencyProperty UncheckedStateContentProperty;
        public static readonly DependencyProperty EnableAnimationProperty;
        public static readonly DependencyProperty ContentPlacementProperty;
        public static readonly DependencyProperty CheckedStateContentTemplateProperty;
        public static readonly DependencyProperty UncheckedStateContentTemplateProperty;
        public static readonly DependencyProperty ClickModeProperty;
        public static readonly DependencyProperty SwitchThumbTemplateProperty;
        public static readonly DependencyProperty SwitchBorderTemplateProperty;

        static ToggleSwitchEditSettings()
        {
            Type ownerType = typeof(ToggleSwitchEditSettings);
            CheckedStateContentProperty = ToggleSwitchEdit.CheckedStateContentProperty.AddOwner(ownerType);
            UncheckedStateContentProperty = ToggleSwitchEdit.UncheckedStateContentProperty.AddOwner(ownerType);
            EnableAnimationProperty = ToggleSwitchEdit.EnableAnimationProperty.AddOwner(ownerType);
            ContentPlacementProperty = ToggleSwitchEdit.ContentPlacementProperty.AddOwner(ownerType);
            CheckedStateContentTemplateProperty = ToggleSwitchEdit.CheckedStateContentTemplateProperty.AddOwner(ownerType);
            UncheckedStateContentTemplateProperty = ToggleSwitchEdit.UncheckedStateContentTemplateProperty.AddOwner(ownerType);
            IsThreeStateProperty = ToggleSwitchEdit.IsThreeStateProperty.AddOwner(ownerType);
            ClickModeProperty = ToggleSwitchEdit.ClickModeProperty.AddOwner(ownerType);
            SwitchThumbTemplateProperty = DependencyProperty.Register("SwitchThumbTemplate", typeof(DataTemplate), ownerType);
            SwitchBorderTemplateProperty = DependencyProperty.Register("SwitchBorderTemplate", typeof(DataTemplate), ownerType);
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            ToggleSwitchEdit ts = edit as ToggleSwitchEdit;
            if (ts != null)
            {
                base.SetValueFromSettings(ClickModeProperty, () => ts.ClickMode = this.ClickMode);
                base.SetValueFromSettings(IsThreeStateProperty, () => ts.IsThreeState = this.IsThreeState);
                base.SetValueFromSettings(CheckedStateContentTemplateProperty, () => ts.CheckedStateContentTemplate = this.CheckedStateContentTemplate);
                base.SetValueFromSettings(UncheckedStateContentTemplateProperty, () => ts.UncheckedStateContentTemplate = this.UncheckedStateContentTemplate);
                base.SetValueFromSettings(ContentPlacementProperty, () => ts.ContentPlacement = this.ContentPlacement);
                base.SetValueFromSettings(CheckedStateContentProperty, () => ts.CheckedStateContent = this.CheckedStateContent);
                base.SetValueFromSettings(UncheckedStateContentProperty, () => ts.UncheckedStateContent = this.UncheckedStateContent);
                base.SetValueFromSettings(EnableAnimationProperty, () => ts.EnableAnimation = this.EnableAnimation);
                base.SetValueFromSettings(SwitchBorderTemplateProperty, () => ts.SwitchBorderTemplate = this.SwitchBorderTemplate);
                base.SetValueFromSettings(SwitchThumbTemplateProperty, () => ts.SwitchThumbTemplate = this.SwitchThumbTemplate);
            }
        }

        protected internal override bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            !this.IsToggleCheckGesture(key) ? base.IsActivatingKey(key, modifiers) : true;

        public bool IsToggleCheckGesture(Key key) => 
            key == Key.Space;

        public DataTemplate SwitchThumbTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchThumbTemplateProperty);
            set => 
                base.SetValue(SwitchThumbTemplateProperty, value);
        }

        public DataTemplate SwitchBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchBorderTemplateProperty);
            set => 
                base.SetValue(SwitchBorderTemplateProperty, value);
        }

        public System.Windows.Controls.ClickMode ClickMode
        {
            get => 
                (System.Windows.Controls.ClickMode) base.GetValue(ClickModeProperty);
            set => 
                base.SetValue(ClickModeProperty, value);
        }

        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        public DataTemplate CheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CheckedStateContentTemplateProperty);
            set => 
                base.SetValue(CheckedStateContentTemplateProperty, value);
        }

        public DataTemplate UncheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(UncheckedStateContentTemplateProperty);
            set => 
                base.SetValue(UncheckedStateContentTemplateProperty, value);
        }

        public ToggleSwitchContentPlacement ContentPlacement
        {
            get => 
                (ToggleSwitchContentPlacement) base.GetValue(ContentPlacementProperty);
            set => 
                base.SetValue(ContentPlacementProperty, value);
        }

        public object CheckedStateContent
        {
            get => 
                base.GetValue(CheckedStateContentProperty);
            set => 
                base.SetValue(CheckedStateContentProperty, value);
        }

        public object UncheckedStateContent
        {
            get => 
                base.GetValue(UncheckedStateContentProperty);
            set => 
                base.SetValue(UncheckedStateContentProperty, value);
        }

        public bool EnableAnimation
        {
            get => 
                (bool) base.GetValue(EnableAnimationProperty);
            set => 
                base.SetValue(EnableAnimationProperty, value);
        }
    }
}

