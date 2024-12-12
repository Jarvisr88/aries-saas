namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class EffectiveTrackBarExportSettings : EffectiveTextExportSettings, ITrackBarExportSettings, ITextExportSettings, IExportSettings
    {
        public EffectiveTrackBarExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectiveTrackBarExportValue<T>(DependencyProperty property, Func<ITrackBarExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, ITrackBarExportSettings>(property, getValue);

        public int Maximum
        {
            get
            {
                Func<ITrackBarExportSettings, int> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<ITrackBarExportSettings, int> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = d => d.Maximum;
                }
                return this.GetEffectiveTrackBarExportValue<int>(TrackBarExportSettings.MaximumProperty, getValue);
            }
        }

        public int Minimum
        {
            get
            {
                Func<ITrackBarExportSettings, int> getValue = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<ITrackBarExportSettings, int> local1 = <>c.<>9__4_0;
                    getValue = <>c.<>9__4_0 = d => d.Minimum;
                }
                return this.GetEffectiveTrackBarExportValue<int>(TrackBarExportSettings.MinimumProperty, getValue);
            }
        }

        public int Position
        {
            get
            {
                Func<ITrackBarExportSettings, int> getValue = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<ITrackBarExportSettings, int> local1 = <>c.<>9__6_0;
                    getValue = <>c.<>9__6_0 = d => d.Position;
                }
                return this.GetEffectiveTrackBarExportValue<int>(TrackBarExportSettings.PositionProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveTrackBarExportSettings.<>c <>9 = new EffectiveTrackBarExportSettings.<>c();
            public static Func<ITrackBarExportSettings, int> <>9__2_0;
            public static Func<ITrackBarExportSettings, int> <>9__4_0;
            public static Func<ITrackBarExportSettings, int> <>9__6_0;

            internal int <get_Maximum>b__2_0(ITrackBarExportSettings d) => 
                d.Maximum;

            internal int <get_Minimum>b__4_0(ITrackBarExportSettings d) => 
                d.Minimum;

            internal int <get_Position>b__6_0(ITrackBarExportSettings d) => 
                d.Position;
        }
    }
}

