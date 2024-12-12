namespace DevExpress.Office.Drawing
{
    using System;

    public interface IShapeLocks : IDrawingLocks, ICommonDrawingLocks
    {
        bool NoTextEdit { get; set; }
    }
}

