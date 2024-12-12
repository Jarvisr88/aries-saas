namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;

    internal class ResolutionComparer : IComparer<ResolutionItem>
    {
        public int Compare(ResolutionItem x, ResolutionItem y) => 
            x.IsGreater(y) ? 1 : -1;
    }
}

