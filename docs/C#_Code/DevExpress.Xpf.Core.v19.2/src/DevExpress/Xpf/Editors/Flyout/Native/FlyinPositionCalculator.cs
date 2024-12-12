namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using System;
    using System.Windows;

    public class FlyinPositionCalculator : FlyoutPositionCalculator
    {
        public override void CalcLocation()
        {
            base.ContentSize = this.CalcSize();
            Point location = new Point(base.GetX(base.TargetBounds, base.ContentSize, base.HorizontalAlignment), base.GetY(base.TargetBounds, base.ContentSize, base.VerticalAlignment));
            Rect bounds = new Rect(location, base.ContentSize);
            base.Result.Size = base.ContentSize;
            base.Result.Location = this.CorrectPostionByRect(bounds, base.NormalizedScreenRect);
            base.Result.State = CalculationState.Finished;
        }
    }
}

