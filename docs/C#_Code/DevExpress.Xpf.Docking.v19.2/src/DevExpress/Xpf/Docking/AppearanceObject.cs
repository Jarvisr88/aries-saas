namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class AppearanceObject : Freezable
    {
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty FontFamilyProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty FontStretchProperty;
        public static readonly DependencyProperty FontStyleProperty;
        public static readonly DependencyProperty FontWeightProperty;
        public static readonly DependencyProperty TabBackgroundColorProperty;
        private static List<DependencyProperty> _MergedProperties;

        static AppearanceObject()
        {
            DependencyPropertyRegistrator<AppearanceObject> registrator = new DependencyPropertyRegistrator<AppearanceObject>();
            registrator.Register<Brush>("Background", ref BackgroundProperty, null, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            registrator.Register<Brush>("Foreground", ref ForegroundProperty, null, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            registrator.Register<System.Windows.Media.FontFamily>("FontFamily", ref FontFamilyProperty, null, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            registrator.Register<double>("FontSize", ref FontSizeProperty, double.NaN, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            System.Windows.FontStretch? defValue = null;
            registrator.Register<System.Windows.FontStretch?>("FontStretch", ref FontStretchProperty, defValue, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            System.Windows.FontStyle? nullable2 = null;
            registrator.Register<System.Windows.FontStyle?>("FontStyle", ref FontStyleProperty, nullable2, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            System.Windows.FontWeight? nullable3 = null;
            registrator.Register<System.Windows.FontWeight?>("FontWeight", ref FontWeightProperty, nullable3, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
            registrator.Register<Color>("TabBackgroundColor", ref TabBackgroundColorProperty, Colors.Transparent, (dObj, e) => ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e), null);
        }

        protected override Freezable CreateInstanceCore() => 
            new AppearanceObject();

        private static List<DependencyProperty> CreateMergedPropertiesList()
        {
            List<DependencyProperty> list1 = new List<DependencyProperty>();
            list1.Add(BackgroundProperty);
            list1.Add(ForegroundProperty);
            list1.Add(FontFamilyProperty);
            list1.Add(FontSizeProperty);
            list1.Add(FontStretchProperty);
            list1.Add(FontStyleProperty);
            list1.Add(FontWeightProperty);
            list1.Add(TabBackgroundColorProperty);
            return list1;
        }

        protected internal void OnAppearanceObjectPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.Appearance != null)
            {
                this.Appearance.OnAppearanceObjectPropertyChanged(e);
            }
        }

        internal static List<DependencyProperty> MergedProperties
        {
            get
            {
                _MergedProperties ??= CreateMergedPropertiesList();
                return _MergedProperties;
            }
        }

        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        public System.Windows.Media.FontFamily FontFamily
        {
            get => 
                (System.Windows.Media.FontFamily) base.GetValue(FontFamilyProperty);
            set => 
                base.SetValue(FontFamilyProperty, value);
        }

        public double FontSize
        {
            get => 
                (double) base.GetValue(FontSizeProperty);
            set => 
                base.SetValue(FontSizeProperty, value);
        }

        public System.Windows.FontStretch? FontStretch
        {
            get => 
                (System.Windows.FontStretch?) base.GetValue(FontStretchProperty);
            set => 
                base.SetValue(FontStretchProperty, value);
        }

        public System.Windows.FontStyle? FontStyle
        {
            get => 
                (System.Windows.FontStyle?) base.GetValue(FontStyleProperty);
            set => 
                base.SetValue(FontStyleProperty, value);
        }

        public System.Windows.FontWeight? FontWeight
        {
            get => 
                (System.Windows.FontWeight?) base.GetValue(FontWeightProperty);
            set => 
                base.SetValue(FontWeightProperty, value);
        }

        public Color TabBackgroundColor
        {
            get => 
                (Color) base.GetValue(TabBackgroundColorProperty);
            set => 
                base.SetValue(TabBackgroundColorProperty, value);
        }

        protected internal DevExpress.Xpf.Docking.Appearance Appearance { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AppearanceObject.<>c <>9 = new AppearanceObject.<>c();

            internal void <.cctor>b__12_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_5(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }

            internal void <.cctor>b__12_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AppearanceObject) dObj).OnAppearanceObjectPropertyChanged(e);
            }
        }
    }
}

