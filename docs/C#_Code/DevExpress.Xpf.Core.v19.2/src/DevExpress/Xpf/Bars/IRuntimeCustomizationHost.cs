namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public interface IRuntimeCustomizationHost
    {
        void CustomizationApplyingSkipped(RuntimeCustomization customization);
        DependencyObject FindTarget(string targetName);

        RuntimeCustomizationCollection RuntimeCustomizations { get; }
    }
}

