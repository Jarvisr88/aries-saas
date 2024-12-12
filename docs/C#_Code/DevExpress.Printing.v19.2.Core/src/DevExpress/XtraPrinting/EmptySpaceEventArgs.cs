namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EmptySpaceEventArgs : EventArgs
    {
        public EmptySpaceEventArgs(PSPage page, DocumentBand spaceDocumentBand, float emptySpace, float emptySpaceOffset)
        {
            this.Page = page;
            this.EmptySpace = emptySpace;
            this.SpaceDocumentBand = spaceDocumentBand;
            this.EmptySpaceOffset = emptySpaceOffset;
        }

        internal IList<DocumentBand> AddedBands { get; set; }

        public PSPage Page { get; private set; }

        public float EmptySpaceOffset { get; private set; }

        public float EmptySpace { get; private set; }

        public DocumentBand SpaceDocumentBand { get; private set; }
    }
}

