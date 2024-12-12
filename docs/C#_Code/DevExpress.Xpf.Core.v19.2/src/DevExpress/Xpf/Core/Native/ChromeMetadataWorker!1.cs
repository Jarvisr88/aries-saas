namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public sealed class ChromeMetadataWorker<TChrome> : IDisposable where TChrome: FrameworkElement, IChromeSlaveMaster
    {
        private bool font;
        private bool svg;
        private bool common;

        private static void AddOwnerOrOverrideMetadata(DependencyProperty source, ref DependencyProperty target, PropertyMetadata metadata);
        private void Dispose();
        protected override void Finalize();
        public ChromeMetadataWorker<TChrome> OverrideCommonProperties();
        public ChromeMetadataWorker<TChrome> OverrideFontProperties();
        public ChromeMetadataWorker<TChrome> OverrideFontProperties(ref DependencyProperty ForegroundProperty, ref DependencyProperty FontSizeProperty, ref DependencyProperty FontFamilyProperty, ref DependencyProperty FontStretchProperty, ref DependencyProperty FontStyleProperty, ref DependencyProperty FontWeightProperty);
        public void OverrideMetadata(ref DependencyProperty PaletteProperty, ref DependencyProperty StateProperty, ref DependencyProperty ForegroundProperty, ref DependencyProperty FontSizeProperty, ref DependencyProperty FontFamilyProperty, ref DependencyProperty FontStretchProperty, ref DependencyProperty FontStyleProperty, ref DependencyProperty FontWeightProperty);
        public ChromeMetadataWorker<TChrome> OverrideSvgProperties();
        public ChromeMetadataWorker<TChrome> OverrideSvgProperties(ref DependencyProperty PaletteProperty, ref DependencyProperty StateProperty);
        public ChromeMetadataWorker<TChrome> SkipCommonProperties();
        public ChromeMetadataWorker<TChrome> SkipFontProperties();
        public ChromeMetadataWorker<TChrome> SkipSvgProperties();
        void IDisposable.Dispose();
        private static void ThrowIf(bool flag, string message = null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChromeMetadataWorker<TChrome>.<>c <>9;
            public static PropertyChangedCallback <>9__5_0;
            public static PropertyChangedCallback <>9__5_1;
            public static PropertyChangedCallback <>9__5_2;
            public static PropertyChangedCallback <>9__8_0;
            public static PropertyChangedCallback <>9__8_1;
            public static PropertyChangedCallback <>9__11_0;
            public static PropertyChangedCallback <>9__11_1;
            public static PropertyChangedCallback <>9__11_2;
            public static PropertyChangedCallback <>9__11_3;
            public static PropertyChangedCallback <>9__11_4;
            public static PropertyChangedCallback <>9__11_5;

            static <>c();
            internal void <OverrideCommonProperties>b__5_0(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideCommonProperties>b__5_1(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideCommonProperties>b__5_2(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_0(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_1(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_2(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_3(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_4(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideFontProperties>b__11_5(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideSvgProperties>b__8_0(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <OverrideSvgProperties>b__8_1(DependencyObject o, DependencyPropertyChangedEventArgs args);
        }
    }
}

