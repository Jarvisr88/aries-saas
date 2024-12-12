namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public static class RenderTreeHelper
    {
        public static T FindDescendant<T>(FrameworkRenderElementContext context) where T: FrameworkRenderElementContext;
        public static FrameworkRenderElementContext FindDescendant(FrameworkRenderElementContext context, Func<FrameworkRenderElementContext, bool> predicate);
        public static T FindDescendant<T>(FrameworkRenderElementContext context, Func<T, bool> predicate) where T: FrameworkRenderElementContext;
        public static bool GetIsVisible(FrameworkRenderElementContext context);
        private static bool GetIsVisibleImpl(FrameworkRenderElementContext context);
        private static Matrix GetMatrixTransformToAncestor(FrameworkRenderElementContext element, FrameworkRenderElementContext ancestor);
        private static Matrix GetMatrixTransformToDescendant(FrameworkRenderElementContext element, FrameworkRenderElementContext descendant);
        public static FrameworkRenderElementContext HitTest(FrameworkRenderElementContext context, Point relativePoint);
        public static FrameworkRenderElementContext HitTest(FrameworkRenderElementContext context, Point relativePoint, Func<FrameworkRenderElementContext, bool> predicate);
        public static void HitTest(FrameworkRenderElementContext context, Func<FrameworkRenderElementContext, RenderHitTestFilterBehavior> filterCallback, Func<FrameworkRenderElementContext, RenderHitTestResultBehavior> resultCallback, Point relativePoint);
        [IteratorStateMachine(typeof(RenderTreeHelper.<RenderAncestors>d__11))]
        public static IEnumerable<FrameworkRenderElementContext> RenderAncestors(FrameworkRenderElementContext context);
        [IteratorStateMachine(typeof(RenderTreeHelper.<RenderChildren>d__13))]
        private static IEnumerable<FrameworkRenderElementContext> RenderChildren(FrameworkRenderElementContext context);
        [IteratorStateMachine(typeof(RenderTreeHelper.<RenderDescendants>d__12))]
        public static IEnumerable<FrameworkRenderElementContext> RenderDescendants(FrameworkRenderElementContext context);
        private static Matrix RenderTransformValue(FrameworkRenderElementContext element);
        public static Transform TransformToAncestor(FrameworkRenderElementContext element, FrameworkRenderElementContext ancestor);
        public static Transform TransformToDescendant(FrameworkRenderElementContext element, FrameworkRenderElementContext descendant);
        public static Transform TransformToRoot(FrameworkRenderElementContext element);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderTreeHelper.<>c <>9;
            public static Func<FrameworkRenderElementContext, RenderHitTestFilterBehavior> <>9__0_0;
            public static Func<FrameworkRenderElementContext, RenderHitTestFilterBehavior> <>9__1_0;

            static <>c();
            internal RenderHitTestFilterBehavior <HitTest>b__0_0(FrameworkRenderElementContext frec);
            internal RenderHitTestFilterBehavior <HitTest>b__1_0(FrameworkRenderElementContext frec);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__14<T> where T: FrameworkRenderElementContext
        {
            public static readonly RenderTreeHelper.<>c__14<T> <>9;
            public static Func<FrameworkRenderElementContext, bool> <>9__14_0;

            static <>c__14();
            internal bool <FindDescendant>b__14_0(FrameworkRenderElementContext x);
        }

        [CompilerGenerated]
        private sealed class <RenderAncestors>d__11 : IEnumerable<FrameworkRenderElementContext>, IEnumerable, IEnumerator<FrameworkRenderElementContext>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private FrameworkRenderElementContext <>2__current;
            private int <>l__initialThreadId;
            private FrameworkRenderElementContext context;
            public FrameworkRenderElementContext <>3__context;

            [DebuggerHidden]
            public <RenderAncestors>d__11(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<FrameworkRenderElementContext> IEnumerable<FrameworkRenderElementContext>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            FrameworkRenderElementContext IEnumerator<FrameworkRenderElementContext>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <RenderChildren>d__13 : IEnumerable<FrameworkRenderElementContext>, IEnumerable, IEnumerator<FrameworkRenderElementContext>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private FrameworkRenderElementContext <>2__current;
            private int <>l__initialThreadId;
            private FrameworkRenderElementContext context;
            public FrameworkRenderElementContext <>3__context;
            private IFrameworkRenderElementContext <iContext>5__1;
            private int <i>5__2;

            [DebuggerHidden]
            public <RenderChildren>d__13(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<FrameworkRenderElementContext> IEnumerable<FrameworkRenderElementContext>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            FrameworkRenderElementContext IEnumerator<FrameworkRenderElementContext>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

    }
}

