namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class TouchPaddingInfoExtension : MarkupExtension
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty dp;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            TouchInfo info1 = new TouchInfo();
            info1.Value = this.Value;
            info1.TouchValue = this.TouchValue;
            info1.Scale = this.TouchScale;
            info1.TargetProperty = this.TargetProperty;
            return info1;
        }

        public DependencyProperty TargetProperty
        {
            get => 
                this.dp;
            set => 
                this.dp = value;
        }

        public Thickness Value { get; set; }

        public Thickness TouchValue { get; set; }

        public double? TouchScale { get; set; }
    }
}

