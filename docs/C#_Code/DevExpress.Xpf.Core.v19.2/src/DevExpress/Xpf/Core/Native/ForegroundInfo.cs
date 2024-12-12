namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Elements")]
    public class ForegroundInfo
    {
        public ForegroundInfo();
        public void Apply(DependencyObject target, string stateName);
        public virtual bool Contains(string stateName);

        public Dictionary<string, Brush> Elements { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ForegroundInfo.<>c <>9;
            public static Func<Brush, Brush> <>9__5_0;

            static <>c();
            internal Brush <Apply>b__5_0(Brush x);
        }
    }
}

