namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Elements")]
    public class BrushSet
    {
        public BrushSet();
        public void ApplyForeground(DependencyObject target, string brushName);
        public Brush GetBrush(string brushName);

        public Dictionary<string, BrushInfo> Elements { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrushSet.<>c <>9;
            public static Func<BrushInfo, Brush> <>9__5_0;

            static <>c();
            internal Brush <ApplyForeground>b__5_0(BrushInfo x);
        }
    }
}

