namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlCellError
    {
        XlCellErrorType Type { get; }

        string Name { get; }

        string Description { get; }

        XlVariantValue Value { get; }
    }
}

