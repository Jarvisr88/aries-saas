namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DropPositionCalculator : IDropPositionCalculator
    {
        public DropPositionCalculationResult CalcPosition(Point dragOverPoint, Size relativeElementSize)
        {
            double num = this.CalcRatio(dragOverPoint, relativeElementSize);
            List<DropPosition> list = new List<DropPosition> {
                (num > 0.5) ? DropPosition.After : DropPosition.Before
            };
            if (this.AllowInsideDrop)
            {
                if ((num > 0.2) && (num < 0.8))
                {
                    list.Insert(0, DropPosition.Inside);
                }
                else
                {
                    list.Add(DropPosition.Inside);
                }
            }
            DropPositionCalculationResult result1 = new DropPositionCalculationResult();
            result1.Positions = list;
            result1.Ratio = num;
            return result1;
        }

        private double CalcRatio(Point dragOverPoint, Size relativeElementSize) => 
            (this.Orientation != System.Windows.Controls.Orientation.Vertical) ? (dragOverPoint.Y / relativeElementSize.Height) : (dragOverPoint.X / relativeElementSize.Width);

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public bool AllowInsideDrop { get; set; }
    }
}

