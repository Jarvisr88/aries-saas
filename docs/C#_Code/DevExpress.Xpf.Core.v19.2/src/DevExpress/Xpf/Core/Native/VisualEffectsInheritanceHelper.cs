namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class VisualEffectsInheritanceHelper
    {
        private static void CheckUpdateValue(DependencyObject source, DependencyObject target, ref VisualEffectsInheritanceHelper.VisualEffects allEffects, VisualEffectsInheritanceHelper.VisualEffects currentEffect, DependencyProperty currentProperty, object defaultValue);
        public static void SetTextAndRenderOptions(DependencyObject element, DependencyObject treeRoot = null);

        [Flags]
        private enum VisualEffects
        {
            public const VisualEffectsInheritanceHelper.VisualEffects None = VisualEffectsInheritanceHelper.VisualEffects.None;,
            public const VisualEffectsInheritanceHelper.VisualEffects BitmapScalingMode = VisualEffectsInheritanceHelper.VisualEffects.BitmapScalingMode;,
            public const VisualEffectsInheritanceHelper.VisualEffects CachingHint = VisualEffectsInheritanceHelper.VisualEffects.CachingHint;,
            public const VisualEffectsInheritanceHelper.VisualEffects ClearTypeHint = VisualEffectsInheritanceHelper.VisualEffects.ClearTypeHint;,
            public const VisualEffectsInheritanceHelper.VisualEffects EdgeMode = VisualEffectsInheritanceHelper.VisualEffects.EdgeMode;,
            public const VisualEffectsInheritanceHelper.VisualEffects TextFormattingMode = VisualEffectsInheritanceHelper.VisualEffects.TextFormattingMode;,
            public const VisualEffectsInheritanceHelper.VisualEffects TextHintingMode = VisualEffectsInheritanceHelper.VisualEffects.TextHintingMode;,
            public const VisualEffectsInheritanceHelper.VisualEffects TextRenderingMode = VisualEffectsInheritanceHelper.VisualEffects.TextRenderingMode;,
            public const VisualEffectsInheritanceHelper.VisualEffects All = VisualEffectsInheritanceHelper.VisualEffects.All;
        }
    }
}

