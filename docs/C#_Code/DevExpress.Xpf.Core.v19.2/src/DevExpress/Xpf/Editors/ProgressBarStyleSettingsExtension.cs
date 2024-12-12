namespace DevExpress.Xpf.Editors
{
    public class ProgressBarStyleSettingsExtension : BaseProgressBarStyleSettingsExtension
    {
        protected override BaseProgressBarStyleSettings CreateStyleSettings() => 
            new ProgressBarStyleSettings();
    }
}

