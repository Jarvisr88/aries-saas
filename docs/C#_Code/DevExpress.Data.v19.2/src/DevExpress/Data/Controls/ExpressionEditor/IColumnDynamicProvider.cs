namespace DevExpress.Data.Controls.ExpressionEditor
{
    public interface IColumnDynamicProvider
    {
        ColumnInfo GetColumnInfo(ColumnDynamicArguments arguments);
    }
}

