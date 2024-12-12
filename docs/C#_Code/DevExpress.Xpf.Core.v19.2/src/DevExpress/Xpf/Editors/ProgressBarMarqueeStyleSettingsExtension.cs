namespace DevExpress.Xpf.Editors
{
    public class ProgressBarMarqueeStyleSettingsExtension : BaseProgressBarStyleSettingsExtension
    {
        protected override BaseProgressBarStyleSettings CreateStyleSettings() => 
            new ProgressBarMarqueeStyleSettings();
    }
}

