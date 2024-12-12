namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class SvgTransformCollection : List<SvgTransformBase>
    {
        public SvgTransformCollection();
        public SvgTransformCollection(IList<SvgTransformBase> svgTransforms);
        public Matrix GetMatrix();
        public static SvgTransformCollection Parse(string transformString);
        private static SvgTransformCollection ParseTransforms(IEnumerable<string> transformList);
        [IteratorStateMachine(typeof(SvgTransformCollection.<SplitTransforms>d__2))]
        private static IEnumerable<string> SplitTransforms(string transforms);
        public override string ToString();
        public SvgPoint Transform(SvgPoint point);

        [CompilerGenerated]
        private sealed class <SplitTransforms>d__2 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;
            private string transforms;
            public string <>3__transforms;
            private int <i>5__1;

            [DebuggerHidden]
            public <SplitTransforms>d__2(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            string IEnumerator<string>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

