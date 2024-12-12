namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutViewFloatingDragListener : FloatingListener
    {
        protected override IFloatingHelper CreateFloatingHelper() => 
            new FloatingHelper(this.View);

        protected override ILayoutElement EnsureElementForFloating(ILayoutElement element)
        {
            ILayoutElement element2 = base.EnsureElementForFloating(element);
            if (!element2.IsDisposing)
            {
                return element2;
            }
            IDockLayoutElement element3 = (IDockLayoutElement) element;
            return (this.View.Container.GetViewElement((element3.Element as IUIElement)) ?? element2);
        }

        protected override void EnsureFloatingView(IView floatingView)
        {
            base.EnsureFloatingView(floatingView);
            Action<FloatingView> action = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Action<FloatingView> local1 = <>c.<>9__4_0;
                action = <>c.<>9__4_0 = x => x.BeginFloating();
            }
            (floatingView as FloatingView).Do<FloatingView>(action);
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutViewFloatingDragListener.<>c <>9 = new LayoutViewFloatingDragListener.<>c();
            public static Action<FloatingView> <>9__4_0;

            internal void <EnsureFloatingView>b__4_0(FloatingView x)
            {
                x.BeginFloating();
            }
        }
    }
}

