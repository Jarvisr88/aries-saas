namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;

    public class NestedBricksEnumerator : NestedObjectEnumeratorBase
    {
        public NestedBricksEnumerator(Brick brick);
        protected override IEnumerator GetNestedObjects();

        public Brick Current { get; }
    }
}

