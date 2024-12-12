namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlString
    {
        string Text { get; }

        bool IsPlainText { get; }
    }
}

