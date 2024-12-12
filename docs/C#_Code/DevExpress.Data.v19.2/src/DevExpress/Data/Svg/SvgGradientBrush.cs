namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class SvgGradientBrush : SvgBrush
    {
        private readonly SvgGradientDefinition gradientDefionition;

        public SvgGradientBrush(Color color1, Color color2);
        public SvgGradientBrush(Color color1, Color color2, SvgGradientUnits units);
        protected abstract SvgGradientDefinition CreateDefinition();
        [IteratorStateMachine(typeof(SvgGradientBrush.<ExportData>d__9))]
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator, string colorKey, string opacityKey);

        public SvgGradientDefinition GradientDefinition { get; }

        public SvgTransformCollection TransformCollection { get; set; }

        [CompilerGenerated]
        private sealed class <ExportData>d__9 : IEnumerable<SvgDefinition>, IEnumerable, IEnumerator<SvgDefinition>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private SvgDefinition <>2__current;
            private int <>l__initialThreadId;
            private SvgElementDataExportAgent dataAgent;
            public SvgElementDataExportAgent <>3__dataAgent;
            private string colorKey;
            public string <>3__colorKey;
            public SvgGradientBrush <>4__this;
            private IDefinitionKeysGenerator keysGenerator;
            public IDefinitionKeysGenerator <>3__keysGenerator;

            [DebuggerHidden]
            public <ExportData>d__9(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<SvgDefinition> IEnumerable<SvgDefinition>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            SvgDefinition IEnumerator<SvgDefinition>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

