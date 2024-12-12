namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class SvgVisualElement : SvgElement
    {
        protected SvgVisualElement();
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        protected abstract void ExportFields(SvgElementDataExportAgent dataAgent);
        public override bool FillData(SvgElementDataImportAgent dataAgent);
        protected abstract bool FillFields(SvgElementDataImportAgent dataAgent);
        private void FillTitle(SvgElementDataImportAgent dataAgent);

        public string Title { get; private set; }
    }
}

