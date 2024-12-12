namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;

    public abstract class SvgDefinition : SvgItem
    {
        private string key;
        private bool isExported;

        protected SvgDefinition();
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public override bool FillData(SvgElementDataImportAgent dataAgent);
        private string GetDefinitionKey(IDefinitionKeysGenerator keysGenerator);
        public string GetUrl(IDefinitionKeysGenerator keysGenerator);

        public bool IsExported { get; }
    }
}

