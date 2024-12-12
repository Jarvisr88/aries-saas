namespace DevExpress.Xpf.Editors.ExpressionEditor.Native
{
    using DevExpress.Data;
    using System;

    public interface ISupportExpressionString
    {
        string GetExpressionString(IDataColumnInfo columnInfo);
        void SetExpressionString(IDataColumnInfo columnInfo, string value);
    }
}

