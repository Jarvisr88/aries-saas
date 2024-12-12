namespace DevExpress.Office.Drawing
{
    using System;

    public interface IPictureLocks : IDrawingLocks, ICommonDrawingLocks
    {
        bool NoCrop { get; set; }
    }
}

