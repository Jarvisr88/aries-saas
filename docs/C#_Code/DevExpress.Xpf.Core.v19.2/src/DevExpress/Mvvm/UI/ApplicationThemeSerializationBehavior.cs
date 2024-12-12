namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using System;

    [Obsolete]
    public class ApplicationThemeSerializationBehavior : Behavior<DependencyObject>
    {
        public ApplicationThemeSerializationBehavior()
        {
            DXSerializer.SetSerializationID(this, "applicationThemeId");
        }

        [XtraSerializableProperty]
        public string ApplicationThemeName
        {
            get => 
                ApplicationThemeHelper.ApplicationThemeName;
            set => 
                ApplicationThemeHelper.ApplicationThemeName = value;
        }
    }
}

