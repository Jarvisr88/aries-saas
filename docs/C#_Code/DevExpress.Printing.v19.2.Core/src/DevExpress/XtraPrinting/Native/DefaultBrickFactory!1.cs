namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class DefaultBrickFactory<BrickType> : IBrickFactory where BrickType: Brick, new()
    {
        Brick IBrickFactory.CreateBrick();
    }
}

