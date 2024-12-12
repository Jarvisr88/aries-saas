namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum FREInvalidateOptions
    {
        public const FREInvalidateOptions None = FREInvalidateOptions.None;,
        public const FREInvalidateOptions AffectsMeasure = FREInvalidateOptions.AffectsMeasure;,
        public const FREInvalidateOptions AffectsArrange = FREInvalidateOptions.AffectsArrange;,
        public const FREInvalidateOptions AffectsVisual = FREInvalidateOptions.AffectsVisual;,
        public const FREInvalidateOptions AffectsRenderCaches = FREInvalidateOptions.AffectsRenderCaches;,
        public const FREInvalidateOptions AffectsGeneralCaches = FREInvalidateOptions.AffectsGeneralCaches;,
        public const FREInvalidateOptions AffectsChildrenCaches = FREInvalidateOptions.AffectsChildrenCaches;,
        public const FREInvalidateOptions AffectsOpacity = FREInvalidateOptions.AffectsOpacity;,
        public const FREInvalidateOptions AffectsParent = FREInvalidateOptions.AffectsParent;,
        public const FREInvalidateOptions AffectsParentAndSelf = FREInvalidateOptions.AffectsParentAndSelf;,
        public const FREInvalidateOptions AffectsMeasureAndVisual = FREInvalidateOptions.AffectsMeasureAndVisual;,
        public const FREInvalidateOptions UpdateVisual = FREInvalidateOptions.UpdateVisual;,
        public const FREInvalidateOptions UpdateLayout = FREInvalidateOptions.UpdateLayout;,
        public const FREInvalidateOptions UpdateSubTree = FREInvalidateOptions.UpdateSubTree;
    }
}

