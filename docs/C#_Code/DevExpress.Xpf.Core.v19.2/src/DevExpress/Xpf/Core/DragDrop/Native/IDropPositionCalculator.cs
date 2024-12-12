namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System.Windows;

    public interface IDropPositionCalculator
    {
        DropPositionCalculationResult CalcPosition(Point dragOverPoint, Size relativeElementSize);
    }
}

