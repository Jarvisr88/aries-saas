namespace DevExpress.XtraPrinting
{
    using System;

    public class BrickEventArgsBase : EventArgs
    {
        private DevExpress.XtraPrinting.Brick brick;

        public BrickEventArgsBase(DevExpress.XtraPrinting.Brick brick)
        {
            this.brick = brick;
        }

        public DevExpress.XtraPrinting.Brick Brick =>
            this.brick;
    }
}

