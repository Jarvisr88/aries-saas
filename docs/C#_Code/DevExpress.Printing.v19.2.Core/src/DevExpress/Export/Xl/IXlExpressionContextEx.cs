namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public interface IXlExpressionContextEx : IXlExpressionContext
    {
        int RowOffset { get; set; }

        IXlTable CurrentTable { get; set; }

        Dictionary<string, XlColumnLookupInfo> LookupColumns { get; }
    }
}

