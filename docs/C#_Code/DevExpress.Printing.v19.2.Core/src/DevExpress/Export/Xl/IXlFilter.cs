namespace DevExpress.Export.Xl
{
    using System;

    internal interface IXlFilter
    {
        bool MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter);
    }
}

