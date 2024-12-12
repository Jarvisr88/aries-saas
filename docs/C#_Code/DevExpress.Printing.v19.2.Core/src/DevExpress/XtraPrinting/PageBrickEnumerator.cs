namespace DevExpress.XtraPrinting
{
    using System;

    public class PageBrickEnumerator : BrickBaseEnumerator
    {
        public PageBrickEnumerator(BrickBase brick) : base(brick)
        {
        }

        public override bool MoveNext()
        {
            while (base.MoveNext())
            {
                if ((base.CurrentBrick is Brick) && (!(base.CurrentBrick is CompositeBrick) || !(base.ParentBrick is Page)))
                {
                    return true;
                }
            }
            return false;
        }

        public Brick CurrentBrick =>
            (Brick) this.Current;
    }
}

