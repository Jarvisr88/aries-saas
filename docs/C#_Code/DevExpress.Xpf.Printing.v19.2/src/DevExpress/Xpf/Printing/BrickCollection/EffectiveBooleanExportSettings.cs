namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class EffectiveBooleanExportSettings : EffectiveExportSettings, IBooleanExportSettings, IExportSettings
    {
        public EffectiveBooleanExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectiveBooleanExportValue<T>(DependencyProperty property, Func<IBooleanExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, IBooleanExportSettings>(property, getValue);

        public bool? BooleanValue
        {
            get
            {
                Func<IBooleanExportSettings, bool?> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<IBooleanExportSettings, bool?> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = o => o.BooleanValue;
                }
                return this.GetEffectiveBooleanExportValue<bool?>(BooleanExportSettings.BooleanValueProperty, getValue);
            }
        }

        public string CheckText
        {
            get
            {
                Func<IBooleanExportSettings, string> getValue = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<IBooleanExportSettings, string> local1 = <>c.<>9__4_0;
                    getValue = <>c.<>9__4_0 = o => o.CheckText;
                }
                return this.GetEffectiveBooleanExportValue<string>(BooleanExportSettings.CheckTextProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveBooleanExportSettings.<>c <>9 = new EffectiveBooleanExportSettings.<>c();
            public static Func<IBooleanExportSettings, bool?> <>9__2_0;
            public static Func<IBooleanExportSettings, string> <>9__4_0;

            internal bool? <get_BooleanValue>b__2_0(IBooleanExportSettings o) => 
                o.BooleanValue;

            internal string <get_CheckText>b__4_0(IBooleanExportSettings o) => 
                o.CheckText;
        }
    }
}

