namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class LayoutControlBaseScrollBehavior : ScrollBehaviorBase
    {
        public override bool CanScrollDown(DependencyObject source)
        {
            Func<LayoutControllerBase, bool> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<LayoutControllerBase, bool> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.CanScrollDown;
            }
            return this.GetController(source).Return<LayoutControllerBase, bool>(evaluator, (<>c.<>9__1_1 ??= () => false));
        }

        public override bool CanScrollLeft(DependencyObject source)
        {
            Func<LayoutControllerBase, bool> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<LayoutControllerBase, bool> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.CanScrollLeft;
            }
            return this.GetController(source).Return<LayoutControllerBase, bool>(evaluator, (<>c.<>9__2_1 ??= () => false));
        }

        public override bool CanScrollRight(DependencyObject source)
        {
            Func<LayoutControllerBase, bool> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<LayoutControllerBase, bool> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.CanScrollRight;
            }
            return this.GetController(source).Return<LayoutControllerBase, bool>(evaluator, (<>c.<>9__3_1 ??= () => false));
        }

        public override bool CanScrollUp(DependencyObject source)
        {
            Func<LayoutControllerBase, bool> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<LayoutControllerBase, bool> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.CanScrollUp;
            }
            return this.GetController(source).Return<LayoutControllerBase, bool>(evaluator, (<>c.<>9__4_1 ??= () => false));
        }

        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            true;

        private LayoutControllerBase GetController(DependencyObject source)
        {
            Func<LayoutControlBase, LayoutControllerBase> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<LayoutControlBase, LayoutControllerBase> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Controller;
            }
            return (source as LayoutControlBase).With<LayoutControlBase, LayoutControllerBase>(evaluator);
        }

        public override void MouseWheelDown(DependencyObject source, int lineCount)
        {
            this.ProcessMouseWheelScrolling(source, -lineCount, false);
        }

        public override void MouseWheelLeft(DependencyObject source, int lineCount)
        {
            this.ProcessMouseWheelScrolling(source, lineCount, true);
        }

        public override void MouseWheelRight(DependencyObject source, int lineCount)
        {
            this.ProcessMouseWheelScrolling(source, -lineCount, true);
        }

        public override void MouseWheelUp(DependencyObject source, int lineCount)
        {
            this.ProcessMouseWheelScrolling(source, lineCount, false);
        }

        public override bool PreventMouseScrolling(DependencyObject source) => 
            false;

        private void ProcessMouseWheelScrolling(DependencyObject source, int lineCount, bool horz)
        {
            this.GetController(source).Do<LayoutControllerBase>(x => x.ProcessMouseWheelScrolling(horz, 120 * lineCount));
        }

        public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
        {
            this.GetController(source).Do<LayoutControllerBase>(x => x.ProcessMouseWheelScrolling(true, -((int) offset)));
        }

        public override void ScrollToVerticalOffset(DependencyObject source, double offset)
        {
            this.GetController(source).Do<LayoutControllerBase>(x => x.ProcessMouseWheelScrolling(false, (int) offset));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlBaseScrollBehavior.<>c <>9 = new LayoutControlBaseScrollBehavior.<>c();
            public static Func<LayoutControlBase, LayoutControllerBase> <>9__0_0;
            public static Func<LayoutControllerBase, bool> <>9__1_0;
            public static Func<bool> <>9__1_1;
            public static Func<LayoutControllerBase, bool> <>9__2_0;
            public static Func<bool> <>9__2_1;
            public static Func<LayoutControllerBase, bool> <>9__3_0;
            public static Func<bool> <>9__3_1;
            public static Func<LayoutControllerBase, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;

            internal bool <CanScrollDown>b__1_0(LayoutControllerBase x) => 
                x.CanScrollDown;

            internal bool <CanScrollDown>b__1_1() => 
                false;

            internal bool <CanScrollLeft>b__2_0(LayoutControllerBase x) => 
                x.CanScrollLeft;

            internal bool <CanScrollLeft>b__2_1() => 
                false;

            internal bool <CanScrollRight>b__3_0(LayoutControllerBase x) => 
                x.CanScrollRight;

            internal bool <CanScrollRight>b__3_1() => 
                false;

            internal bool <CanScrollUp>b__4_0(LayoutControllerBase x) => 
                x.CanScrollUp;

            internal bool <CanScrollUp>b__4_1() => 
                false;

            internal LayoutControllerBase <GetController>b__0_0(LayoutControlBase x) => 
                x.Controller;
        }
    }
}

