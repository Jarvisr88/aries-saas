namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;

    public class DocumentBrickEnumerator : PageElementsEnumerator, IEnumerable
    {
        public DocumentBrickEnumerator(Document doc);
        protected override IEnumerator GetPageElementsEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public DevExpress.XtraPrinting.Brick Brick { get; }
    }
}

