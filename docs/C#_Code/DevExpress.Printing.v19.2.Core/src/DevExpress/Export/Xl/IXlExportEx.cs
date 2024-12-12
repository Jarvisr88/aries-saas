namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlExportEx : IXlExport
    {
        void SetWorksheetPosition(string name, int position);
    }
}

