namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public abstract class SvgElement : SvgItem
    {
        private readonly List<SvgAttribute> attributeCollection;
        private readonly Dictionary<string, string> styles;

        public SvgElement();
        public abstract T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        [IteratorStateMachine(typeof(SvgElement.<ExportClipping>d__26))]
        private IEnumerable<SvgDefinition> ExportClipping(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        private void ExportStyle(SvgElementDataExportAgent dataAgent);
        private void ExportTransform(SvgElementDataExportAgent dataAgent);
        private IEnumerable<SvgDefinition> ExportVisual(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        private void FillAttributes(SvgElementDataImportAgent dataAgent);
        public override bool FillData(SvgElementDataImportAgent dataAgent);
        private void FillTransform(SvgElementDataImportAgent dataAgent);
        private void FillVisual(SvgElementDataImportAgent dataAgent);
        public abstract SvgRect GetBoundaryPoints();
        private void PopulateId(SvgElementDataImportAgent dataAgent);

        public Dictionary<string, string> Styles { get; }

        public List<SvgAttribute> AttributeCollection { get; }

        public SvgShapeVisual ElementVisual { get; private set; }

        public SvgTransformCollection TransformCollection { get; set; }

        public SvgClippingDefinition ClippingDefinition { get; set; }

        [CompilerGenerated]
        private sealed class <ExportClipping>d__26 : IEnumerable<SvgDefinition>, IEnumerable, IEnumerator<SvgDefinition>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private SvgDefinition <>2__current;
            private int <>l__initialThreadId;
            public SvgElement <>4__this;
            private SvgElementDataExportAgent dataAgent;
            public SvgElementDataExportAgent <>3__dataAgent;
            private IDefinitionKeysGenerator keysGenerator;
            public IDefinitionKeysGenerator <>3__keysGenerator;

            [DebuggerHidden]
            public <ExportClipping>d__26(int <>1__state);
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

