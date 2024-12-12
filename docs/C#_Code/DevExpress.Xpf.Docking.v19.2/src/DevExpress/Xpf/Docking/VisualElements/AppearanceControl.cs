namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    [DXToolboxBrowsable(false)]
    public class AppearanceControl : psvControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ForegroundInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BackgroundInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FontFamilyInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FontSizeInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FontStretchInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FontStyleInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FontWeightInternalProperty;

        static AppearanceControl()
        {
            DependencyPropertyRegistrator<AppearanceControl> registrator = new DependencyPropertyRegistrator<AppearanceControl>();
            registrator.Register<Brush>("ForegroundInternal", ref ForegroundInternalProperty, null, (dObj, ea) => ((AppearanceControl) dObj).OnForegroundChanged(ea.NewValue), null);
            registrator.Register<Brush>("BackgroundInternal", ref BackgroundInternalProperty, null, (dObj, ea) => ((AppearanceControl) dObj).OnBackgroundChanged(ea.NewValue), null);
            registrator.Register<FontFamily>("FontFamilyInternal", ref FontFamilyInternalProperty, null, (dObj, ea) => ((AppearanceControl) dObj).OnFontFamilyChanged(ea.NewValue), null);
            registrator.Register<double>("FontSizeInternal", ref FontSizeInternalProperty, double.NaN, (dObj, ea) => ((AppearanceControl) dObj).OnFontSizeChanged(ea.NewValue), null);
            registrator.Register<FontStretch>("FontStretchInternal", ref FontStretchInternalProperty, FontStretches.Normal, (dObj, ea) => ((AppearanceControl) dObj).OnFontStretchChanged(ea.NewValue), null);
            registrator.Register<FontStyle>("FontStyleInternal", ref FontStyleInternalProperty, FontStyles.Normal, (dObj, ea) => ((AppearanceControl) dObj).OnFontStyleChanged(ea.NewValue), null);
            registrator.Register<FontWeight>("FontWeightInternal", ref FontWeightInternalProperty, FontWeights.Normal, (dObj, ea) => ((AppearanceControl) dObj).OnFontWeightChanged(ea.NewValue), null);
        }

        public AppearanceControl()
        {
            this.StartListen(ForegroundInternalProperty, "Foreground", BindingMode.OneWay);
            this.StartListen(BackgroundInternalProperty, "Background", BindingMode.OneWay);
            this.StartListen(FontFamilyInternalProperty, "FontFamily", BindingMode.OneWay);
            this.StartListen(FontSizeInternalProperty, "FontSize", BindingMode.OneWay);
            this.StartListen(FontStretchInternalProperty, "FontStretch", BindingMode.OneWay);
            this.StartListen(FontStyleInternalProperty, "FontStyle", BindingMode.OneWay);
            this.StartListen(FontWeightInternalProperty, "FontWeight", BindingMode.OneWay);
        }

        protected virtual void OnBackgroundChanged(object newValue)
        {
            this.OnVisualChanged();
        }

        protected virtual void OnFontFamilyChanged(object newValue)
        {
            this.OnGeometryChanged();
        }

        protected virtual void OnFontSizeChanged(object newValue)
        {
            this.OnGeometryChanged();
        }

        protected virtual void OnFontStretchChanged(object newValue)
        {
            this.OnGeometryChanged();
        }

        protected virtual void OnFontStyleChanged(object newValue)
        {
            this.OnGeometryChanged();
        }

        protected virtual void OnFontWeightChanged(object newValue)
        {
            this.OnGeometryChanged();
        }

        protected virtual void OnForegroundChanged(object newValue)
        {
            this.OnVisualChanged();
        }

        protected virtual void OnGeometryChanged()
        {
            this.OnVisualChanged();
        }

        protected virtual void OnVisualChanged()
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AppearanceControl.<>c <>9 = new AppearanceControl.<>c();

            internal void <.cctor>b__7_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnForegroundChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnBackgroundChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_2(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnFontFamilyChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_3(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnFontSizeChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_4(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnFontStretchChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_5(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnFontStyleChanged(ea.NewValue);
            }

            internal void <.cctor>b__7_6(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AppearanceControl) dObj).OnFontWeightChanged(ea.NewValue);
            }
        }
    }
}

