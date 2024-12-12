namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public abstract class PopupBrushEditSettingsBase : PopupBaseEditSettings
    {
        public static readonly DependencyProperty AllowEditBrushTypeProperty;

        static PopupBrushEditSettingsBase()
        {
            Type ownerType = typeof(PopupBrushEditSettingsBase);
            AllowEditBrushTypeProperty = DependencyPropertyManager.Register("AllowEditBrushType", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEditSettings.PopupMinWidthProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(300.0));
        }

        protected PopupBrushEditSettingsBase()
        {
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PopupBrushEditBase editor = edit as PopupBrushEditBase;
            if (editor != null)
            {
                base.SetValueFromSettings(AllowEditBrushTypeProperty, () => editor.AllowEditBrushType = this.AllowEditBrushType);
            }
        }

        public bool AllowEditBrushType
        {
            get => 
                (bool) base.GetValue(AllowEditBrushTypeProperty);
            set => 
                base.SetValue(AllowEditBrushTypeProperty, value);
        }
    }
}

