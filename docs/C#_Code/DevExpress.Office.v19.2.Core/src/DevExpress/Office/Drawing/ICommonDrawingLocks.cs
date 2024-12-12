namespace DevExpress.Office.Drawing
{
    using System;

    public interface ICommonDrawingLocks
    {
        bool NoGroup { get; set; }

        bool NoSelect { get; set; }

        bool NoChangeAspect { get; set; }

        bool NoMove { get; set; }

        bool IsEmpty { get; }
    }
}

