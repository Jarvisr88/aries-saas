namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingLocks : ICommonDrawingLocks
    {
        bool NoRotate { get; set; }

        bool NoResize { get; set; }

        bool NoEditPoints { get; set; }

        bool NoAdjustHandles { get; set; }

        bool NoChangeArrowheads { get; set; }

        bool NoChangeShapeType { get; set; }
    }
}

