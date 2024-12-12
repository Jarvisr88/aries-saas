namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false)]
    public class RenderPropertyChangedListener : RenderPropertyBase
    {
        private string property;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private System.Windows.DependencyProperty dependencyProperty;

        public RenderPropertyChangedListener();
        public override RenderPropertyContextBase CreateContext();

        public string Property { get; set; }

        public System.Windows.DependencyProperty DependencyProperty { get; set; }

        public RenderValueSource ValueSource { get; set; }

        public string TargetName { get; set; }

        public string SourceName { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderPropertyChangedListener.<>c <>9;
            public static Func<DependencyProperty, string> <>9__2_0;

            static <>c();
            internal string <get_Property>b__2_0(DependencyProperty x);
        }
    }
}

