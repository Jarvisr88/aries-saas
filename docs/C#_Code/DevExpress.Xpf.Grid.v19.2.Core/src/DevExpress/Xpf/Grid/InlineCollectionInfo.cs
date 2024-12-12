namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class InlineCollectionInfo
    {
        public InlineCollectionInfo(string textSource, IList<InlineInfo> inlineSource)
        {
            this.TextSource = textSource;
            this.InlineSource = inlineSource;
        }

        public IList<InlineInfo> InlineSource { get; internal set; }

        public string TextSource { get; internal set; }

        internal bool HasStyle { get; set; }
    }
}

