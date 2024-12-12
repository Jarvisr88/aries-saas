namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ThemedWindowDecorator : IBarNameScopeDecorator
    {
        private BarNameScope scope;
        private DevExpress.Xpf.Core.ThemedWindow window;
        private readonly WeakList<FrameworkElement> elements;
        private PostponedAction recalcAction;

        public ThemedWindowDecorator();
        public void Attach(BarNameScope scope);
        public void Detach();
        public FrameworkElement[] GetElements();
        protected internal virtual void OnRegistratorChanged(ElementRegistrator sender, ElementRegistratorChangedArgs e);

        protected DevExpress.Xpf.Core.ThemedWindow ThemedWindow { get; }

        public bool IsAttachedToRoot { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowDecorator.<>c <>9;
            public static Func<bool> <>9__7_0;
            public static Func<BarNameScope, bool> <>9__11_1;
            public static Func<FrameworkElement, bool> <>9__12_0;

            static <>c();
            internal bool <.ctor>b__7_0();
            internal bool <GetElements>b__12_0(FrameworkElement x);
            internal bool <OnRegistratorChanged>b__11_1(BarNameScope x);
        }
    }
}

