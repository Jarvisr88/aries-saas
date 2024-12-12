namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal class EffectiveExportSettings : IExportSettings
    {
        protected readonly DependencyObject source;

        public EffectiveExportSettings(DependencyObject source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.source = source;
        }

        private T GetEffectiveExportValue<T>(DependencyProperty property, Func<IExportSettings, T> getValue) => 
            this.GetEffectiveValue<T, IExportSettings>(property, getValue);

        protected T GetEffectiveValue<T, TInterface>(DependencyProperty property, Func<TInterface, T> getValue) where TInterface: class, IExportSettings
        {
            if (DependencyPropertyHelper.GetValueSource(this.source, property).BaseValueSource > BaseValueSource.Default)
            {
                return (T) this.source.GetValue(property);
            }
            TInterface source = this.source as TInterface;
            return ((source == null) ? ((T) property.GetDefaultValue()) : getValue(source));
        }

        public Color Background
        {
            get
            {
                Func<IExportSettings, Color> getValue = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<IExportSettings, Color> local1 = <>c.<>9__3_0;
                    getValue = <>c.<>9__3_0 = o => o.Background;
                }
                return this.GetEffectiveExportValue<Color>(ExportSettings.BackgroundProperty, getValue);
            }
        }

        public Color BorderColor
        {
            get
            {
                Func<IExportSettings, Color> getValue = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<IExportSettings, Color> local1 = <>c.<>9__5_0;
                    getValue = <>c.<>9__5_0 = o => o.BorderColor;
                }
                return this.GetEffectiveExportValue<Color>(ExportSettings.BorderColorProperty, getValue);
            }
        }

        public Thickness BorderThickness
        {
            get
            {
                Func<IExportSettings, Thickness> getValue = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<IExportSettings, Thickness> local1 = <>c.<>9__7_0;
                    getValue = <>c.<>9__7_0 = o => o.BorderThickness;
                }
                return this.GetEffectiveExportValue<Thickness>(ExportSettings.BorderThicknessProperty, getValue);
            }
        }

        public Color Foreground
        {
            get
            {
                Func<IExportSettings, Color> getValue = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<IExportSettings, Color> local1 = <>c.<>9__9_0;
                    getValue = <>c.<>9__9_0 = o => o.Foreground;
                }
                return this.GetEffectiveExportValue<Color>(ExportSettings.ForegroundProperty, getValue);
            }
        }

        public string Url
        {
            get
            {
                Func<IExportSettings, string> getValue = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<IExportSettings, string> local1 = <>c.<>9__11_0;
                    getValue = <>c.<>9__11_0 = o => o.Url;
                }
                return this.GetEffectiveExportValue<string>(ExportSettings.UrlProperty, getValue);
            }
        }

        public IOnPageUpdater OnPageUpdater
        {
            get
            {
                Func<IExportSettings, IOnPageUpdater> getValue = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<IExportSettings, IOnPageUpdater> local1 = <>c.<>9__13_0;
                    getValue = <>c.<>9__13_0 = o => o.OnPageUpdater;
                }
                return this.GetEffectiveExportValue<IOnPageUpdater>(ExportSettings.OnPageUpdaterProperty, getValue);
            }
        }

        public DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle
        {
            get
            {
                Func<IExportSettings, DevExpress.XtraPrinting.BorderDashStyle> getValue = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<IExportSettings, DevExpress.XtraPrinting.BorderDashStyle> local1 = <>c.<>9__15_0;
                    getValue = <>c.<>9__15_0 = o => o.BorderDashStyle;
                }
                return this.GetEffectiveExportValue<DevExpress.XtraPrinting.BorderDashStyle>(ExportSettings.BorderDashStyleProperty, getValue);
            }
        }

        public object MergeValue
        {
            get
            {
                Func<IExportSettings, object> getValue = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Func<IExportSettings, object> local1 = <>c.<>9__17_0;
                    getValue = <>c.<>9__17_0 = o => o.MergeValue;
                }
                return this.GetEffectiveExportValue<object>(ExportSettings.MergeValueProperty, getValue);
            }
        }

        public System.Windows.FlowDirection FlowDirection
        {
            get
            {
                Func<IExportSettings, System.Windows.FlowDirection> getValue = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<IExportSettings, System.Windows.FlowDirection> local1 = <>c.<>9__19_0;
                    getValue = <>c.<>9__19_0 = o => o.FlowDirection;
                }
                return this.GetEffectiveExportValue<System.Windows.FlowDirection>(ExportSettings.FlowDirectionProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveExportSettings.<>c <>9 = new EffectiveExportSettings.<>c();
            public static Func<IExportSettings, Color> <>9__3_0;
            public static Func<IExportSettings, Color> <>9__5_0;
            public static Func<IExportSettings, Thickness> <>9__7_0;
            public static Func<IExportSettings, Color> <>9__9_0;
            public static Func<IExportSettings, string> <>9__11_0;
            public static Func<IExportSettings, IOnPageUpdater> <>9__13_0;
            public static Func<IExportSettings, BorderDashStyle> <>9__15_0;
            public static Func<IExportSettings, object> <>9__17_0;
            public static Func<IExportSettings, FlowDirection> <>9__19_0;

            internal Color <get_Background>b__3_0(IExportSettings o) => 
                o.Background;

            internal Color <get_BorderColor>b__5_0(IExportSettings o) => 
                o.BorderColor;

            internal BorderDashStyle <get_BorderDashStyle>b__15_0(IExportSettings o) => 
                o.BorderDashStyle;

            internal Thickness <get_BorderThickness>b__7_0(IExportSettings o) => 
                o.BorderThickness;

            internal FlowDirection <get_FlowDirection>b__19_0(IExportSettings o) => 
                o.FlowDirection;

            internal Color <get_Foreground>b__9_0(IExportSettings o) => 
                o.Foreground;

            internal object <get_MergeValue>b__17_0(IExportSettings o) => 
                o.MergeValue;

            internal IOnPageUpdater <get_OnPageUpdater>b__13_0(IExportSettings o) => 
                o.OnPageUpdater;

            internal string <get_Url>b__11_0(IExportSettings o) => 
                o.Url;
        }
    }
}

