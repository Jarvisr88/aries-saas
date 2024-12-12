namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;

    public class ReversedBrickEnumerator : PageBrickEnumerator
    {
        public ReversedBrickEnumerator(CompositeBrick brick);
        protected override IEnumerator GetEnumerator(IList bricks);
    }
}

