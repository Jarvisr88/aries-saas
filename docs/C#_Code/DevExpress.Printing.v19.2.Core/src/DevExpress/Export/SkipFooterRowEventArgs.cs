namespace DevExpress.Export
{
    using System;
    using System.Runtime.CompilerServices;

    public class SkipFooterRowEventArgs
    {
        public FooterAreaType AreaType { get; internal set; }

        public int MultiLineSummaryFooterIndex { get; internal set; }

        public string GroupFieldName { get; internal set; }

        public int SummaryFooterHandle { get; internal set; }

        public int GroupHierarchyLevel { get; internal set; }

        public bool SkipFooterRow { get; set; }
    }
}

