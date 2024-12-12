namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class EffectiveImageExportSettings : EffectiveExportSettings, IImageExportSettings, IExportSettings
    {
        public EffectiveImageExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectiveImageExportValue<T>(DependencyProperty property, Func<IImageExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, IImageExportSettings>(property, getValue);

        public FrameworkElement SourceElement
        {
            get
            {
                Func<IImageExportSettings, FrameworkElement> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<IImageExportSettings, FrameworkElement> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = o => o.SourceElement;
                }
                return this.GetEffectiveImageExportValue<FrameworkElement>(ImageExportSettings.ImageSourceProperty, getValue);
            }
        }

        public DevExpress.Xpf.Printing.ImageRenderMode ImageRenderMode
        {
            get
            {
                Func<IImageExportSettings, DevExpress.Xpf.Printing.ImageRenderMode> getValue = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<IImageExportSettings, DevExpress.Xpf.Printing.ImageRenderMode> local1 = <>c.<>9__4_0;
                    getValue = <>c.<>9__4_0 = o => o.ImageRenderMode;
                }
                return this.GetEffectiveImageExportValue<DevExpress.Xpf.Printing.ImageRenderMode>(ImageExportSettings.ImageRenderModeProperty, getValue);
            }
        }

        public object ImageKey
        {
            get
            {
                Func<IImageExportSettings, object> getValue = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<IImageExportSettings, object> local1 = <>c.<>9__6_0;
                    getValue = <>c.<>9__6_0 = o => o.ImageKey;
                }
                return this.GetEffectiveImageExportValue<object>(ImageExportSettings.ImageKeyProperty, getValue);
            }
        }

        public bool ForceCenterImageMode
        {
            get
            {
                Func<IImageExportSettings, bool> getValue = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<IImageExportSettings, bool> local1 = <>c.<>9__8_0;
                    getValue = <>c.<>9__8_0 = o => o.ForceCenterImageMode;
                }
                return this.GetEffectiveImageExportValue<bool>(ImageExportSettings.ForceCenterImageModeProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveImageExportSettings.<>c <>9 = new EffectiveImageExportSettings.<>c();
            public static Func<IImageExportSettings, FrameworkElement> <>9__2_0;
            public static Func<IImageExportSettings, ImageRenderMode> <>9__4_0;
            public static Func<IImageExportSettings, object> <>9__6_0;
            public static Func<IImageExportSettings, bool> <>9__8_0;

            internal bool <get_ForceCenterImageMode>b__8_0(IImageExportSettings o) => 
                o.ForceCenterImageMode;

            internal object <get_ImageKey>b__6_0(IImageExportSettings o) => 
                o.ImageKey;

            internal ImageRenderMode <get_ImageRenderMode>b__4_0(IImageExportSettings o) => 
                o.ImageRenderMode;

            internal FrameworkElement <get_SourceElement>b__2_0(IImageExportSettings o) => 
                o.SourceElement;
        }
    }
}

