namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IBrickPagePairFactory
    {
        BrickPagePair CreateBrickPagePair(int[] brickIndexes, int pageIndex);
    }
}

