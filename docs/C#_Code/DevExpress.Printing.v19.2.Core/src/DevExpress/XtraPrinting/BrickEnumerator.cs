namespace DevExpress.XtraPrinting
{
    using System;

    public class BrickEnumerator : PageBrickEnumerator
    {
        public BrickEnumerator(BrickBase brick) : base(brick)
        {
        }

        public override object Current =>
            ((Brick) base.Current).GetRealBrick();
    }
}

