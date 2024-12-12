namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class EffectiveProgressBarExportSettings : EffectiveTextExportSettings, IProgressBarExportSettings, ITextExportSettings, IExportSettings
    {
        public EffectiveProgressBarExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectiveProgressBarExportValue<T>(DependencyProperty property, Func<IProgressBarExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, IProgressBarExportSettings>(property, getValue);

        public int Position
        {
            get
            {
                Func<IProgressBarExportSettings, int> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<IProgressBarExportSettings, int> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = d => d.Position;
                }
                return this.GetEffectiveProgressBarExportValue<int>(ProgressBarExportSettings.PositionProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveProgressBarExportSettings.<>c <>9 = new EffectiveProgressBarExportSettings.<>c();
            public static Func<IProgressBarExportSettings, int> <>9__2_0;

            internal int <get_Position>b__2_0(IProgressBarExportSettings d) => 
                d.Position;
        }
    }
}

