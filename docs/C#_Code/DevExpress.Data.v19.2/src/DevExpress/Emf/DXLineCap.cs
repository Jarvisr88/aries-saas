namespace DevExpress.Emf
{
    using System;

    public enum DXLineCap
    {
        Flat = 0,
        Square = 1,
        Round = 2,
        Triangle = 3,
        NoAnchor = 0x10,
        SquareAnchor = 0x11,
        RoundAnchor = 0x12,
        DiamondAnchor = 0x13,
        ArrowAnchor = 20,
        AnchorMask = 240,
        Custom = 0xff
    }
}

