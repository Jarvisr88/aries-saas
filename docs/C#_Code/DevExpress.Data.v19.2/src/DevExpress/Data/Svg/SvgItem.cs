namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;

    public abstract class SvgItem : ISvgInstance
    {
        private readonly List<SvgItem> childs;

        protected SvgItem();
        public abstract IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public abstract bool FillData(SvgElementDataImportAgent dataAgent);

        public IList<SvgItem> Childs { get; }

        public virtual bool IgnoreChildren { get; }
    }
}

