namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class LoadingDecoratorEx : LoadingDecorator
    {
        public static readonly DependencyProperty ArrangeOnStartupOnlyProperty;

        static LoadingDecoratorEx();
        internal override SplashScreenArrangeMode ArrangeMode();

        public bool ArrangeOnStartupOnly { get; set; }
    }
}

