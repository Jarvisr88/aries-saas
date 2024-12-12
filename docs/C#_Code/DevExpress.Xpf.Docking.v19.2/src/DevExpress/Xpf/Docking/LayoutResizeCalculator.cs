namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    internal class LayoutResizeCalculator : RecursiveResizeCalculator
    {
        protected override void ApplyMeasure()
        {
            foreach (RecursiveResizeCalculator.ResizeInfo info in base.Infos)
            {
                if (MathHelper.IsConstraintValid(info.Length))
                {
                    info.Length = Math.Round(info.Length, 2);
                }
            }
            base.ApplyMeasure();
        }

        protected override bool CanMeasureRecursively(LayoutGroup group) => 
            false;
    }
}

