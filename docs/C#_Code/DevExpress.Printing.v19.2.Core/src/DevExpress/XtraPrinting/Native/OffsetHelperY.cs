namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class OffsetHelperY
    {
        private double maxOffsetY;
        private double minNegativeOffsetY;

        public OffsetHelperY();
        public void Update(PageRowBuilderBase builder);
        public void Update(double offsetY, double negativeOffsetY);
        public virtual void UpdateBuilder(PageRowBuilderBase builder);

        protected virtual bool ShouldUpdateNegativeOffsetY { get; }
    }
}

