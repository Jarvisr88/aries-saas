namespace DevExpress.XtraPrinting.InternalAccess
{
    using DevExpress.XtraPrinting;
    using System;

    public static class BrickAccessor
    {
        public static Brick GetRealBrick(Brick brick) => 
            brick.GetRealBrick();
    }
}

