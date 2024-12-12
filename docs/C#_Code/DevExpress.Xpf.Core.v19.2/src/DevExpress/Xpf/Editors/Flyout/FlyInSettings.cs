namespace DevExpress.Xpf.Editors.Flyout
{
    using DevExpress.Xpf.Editors.Flyout.Native;

    public class FlyInSettings : FlyoutSettingsBase
    {
        public override FlyoutPositionCalculator CreatePositionCalculator() => 
            new FlyinPositionCalculator();

        public override FlyoutBase.FlyoutStrategy CreateStrategy() => 
            new FlyoutBase.FlyinStrategy();
    }
}

