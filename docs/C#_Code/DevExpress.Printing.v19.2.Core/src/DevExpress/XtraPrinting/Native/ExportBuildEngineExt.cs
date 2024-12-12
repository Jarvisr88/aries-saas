namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal static class ExportBuildEngineExt
    {
        [IteratorStateMachine(typeof(ExportBuildEngineExt.<ToRectangleDFEnumerable>d__0))]
        public static IEnumerable<KeyValuePair<Brick, RectangleDF>> ToRectangleDFEnumerable(this IEnumerable<KeyValuePair<Brick, RectangleF>> enumerable);

        [CompilerGenerated]
        private sealed class <ToRectangleDFEnumerable>d__0 : IEnumerable<KeyValuePair<Brick, RectangleDF>>, IEnumerable, IEnumerator<KeyValuePair<Brick, RectangleDF>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<Brick, RectangleDF> <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<KeyValuePair<Brick, RectangleF>> enumerable;
            public IEnumerable<KeyValuePair<Brick, RectangleF>> <>3__enumerable;
            private IEnumerator<KeyValuePair<Brick, RectangleF>> <>7__wrap1;

            [DebuggerHidden]
            public <ToRectangleDFEnumerable>d__0(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<KeyValuePair<Brick, RectangleDF>> IEnumerable<KeyValuePair<Brick, RectangleDF>>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            KeyValuePair<Brick, RectangleDF> IEnumerator<KeyValuePair<Brick, RectangleDF>>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

