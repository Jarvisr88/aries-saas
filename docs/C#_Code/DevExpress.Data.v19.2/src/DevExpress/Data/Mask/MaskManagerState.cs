namespace DevExpress.Data.Mask
{
    using System;

    public abstract class MaskManagerState
    {
        protected MaskManagerState();
        public abstract bool IsSame(MaskManagerState comparedState);
    }
}

