namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class FloatingViewNativeInteractionListener : FloatingViewUIInteractionListener
    {
        protected override bool MaximizeElementOnDoubleClick(ILayoutElement element) => 
            this.MaximizeView();

        protected override bool MaximizeItemCore(BaseLayoutItem item) => 
            this.MaximizeView();

        private bool MaximizeView()
        {
            Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.Window;
            }
            Func<FloatingPaneWindow, bool> func2 = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<FloatingPaneWindow, bool> local2 = <>c.<>9__5_1;
                func2 = <>c.<>9__5_1 = x => x.Maximize();
            }
            return this.View.FloatGroup.UIElements.GetElement<FloatingWindowPresenter>().With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator).Return<FloatingPaneWindow, bool>(func2, null);
        }

        protected override bool MinimizeItemCore(BaseLayoutItem item) => 
            this.MinimizeView();

        private bool MinimizeView()
        {
            Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.Window;
            }
            Func<FloatingPaneWindow, bool> func2 = <>c.<>9__6_1;
            if (<>c.<>9__6_1 == null)
            {
                Func<FloatingPaneWindow, bool> local2 = <>c.<>9__6_1;
                func2 = <>c.<>9__6_1 = x => x.Minimize();
            }
            return this.View.FloatGroup.UIElements.GetElement<FloatingWindowPresenter>().With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator).Return<FloatingPaneWindow, bool>(func2, null);
        }

        protected override bool RestoreElementOnDoubleClick(ILayoutElement element) => 
            this.RestoreView();

        protected override bool RestoreItemCore(BaseLayoutItem item) => 
            this.RestoreView();

        private bool RestoreView()
        {
            Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.Window;
            }
            Func<FloatingPaneWindow, bool> func2 = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<FloatingPaneWindow, bool> local2 = <>c.<>9__9_1;
                func2 = <>c.<>9__9_1 = x => x.Restore();
            }
            return this.View.FloatGroup.UIElements.GetElement<FloatingWindowPresenter>().With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator).Return<FloatingPaneWindow, bool>(func2, null);
        }

        public FloatingView View =>
            base.ServiceProvider as FloatingView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingViewNativeInteractionListener.<>c <>9 = new FloatingViewNativeInteractionListener.<>c();
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__5_0;
            public static Func<FloatingPaneWindow, bool> <>9__5_1;
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__6_0;
            public static Func<FloatingPaneWindow, bool> <>9__6_1;
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__9_0;
            public static Func<FloatingPaneWindow, bool> <>9__9_1;

            internal FloatingPaneWindow <MaximizeView>b__5_0(FloatingWindowPresenter x) => 
                x.Window;

            internal bool <MaximizeView>b__5_1(FloatingPaneWindow x) => 
                x.Maximize();

            internal FloatingPaneWindow <MinimizeView>b__6_0(FloatingWindowPresenter x) => 
                x.Window;

            internal bool <MinimizeView>b__6_1(FloatingPaneWindow x) => 
                x.Minimize();

            internal FloatingPaneWindow <RestoreView>b__9_0(FloatingWindowPresenter x) => 
                x.Window;

            internal bool <RestoreView>b__9_1(FloatingPaneWindow x) => 
                x.Restore();
        }
    }
}

