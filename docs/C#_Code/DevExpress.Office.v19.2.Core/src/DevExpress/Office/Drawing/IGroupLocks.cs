namespace DevExpress.Office.Drawing
{
    using System;

    public interface IGroupLocks : ICommonDrawingLocks
    {
        bool NoUngroup { get; set; }

        bool NoRotate { get; set; }

        bool NoResize { get; set; }
    }
}

