namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class CompatibilityOptions
    {
        private static readonly DependencyPropertyKey UseLightweightTemplatesInStandardButtonsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("UseLightweightTemplatesInStandardButtons", typeof(bool), typeof(CompatibilityOptions), new PropertyMetadata(CompatibilitySettings.UseLightweightTemplatesInStandardButtons));
        public static readonly DependencyProperty UseLightweightTemplatesInStandardButtonsProperty = UseLightweightTemplatesInStandardButtonsPropertyKey.DependencyProperty;

        public static bool GetUseLightweightTemplatesInStandardButtons(DependencyObject target) => 
            (bool) target.GetValue(UseLightweightTemplatesInStandardButtonsProperty);

        private static void SetUseLightweightTemplatesInStandardButtons(DependencyObject target, bool value)
        {
            target.SetValue(UseLightweightTemplatesInStandardButtonsPropertyKey, value);
        }
    }
}

