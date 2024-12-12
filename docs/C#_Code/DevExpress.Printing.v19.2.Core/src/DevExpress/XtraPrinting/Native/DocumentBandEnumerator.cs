namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;

    public class DocumentBandEnumerator : NestedObjectEnumeratorBase
    {
        public DocumentBandEnumerator(IEnumerator enumerator);
        protected override IEnumerator GetNestedObjects();

        public DocumentBand Current { get; }
    }
}

