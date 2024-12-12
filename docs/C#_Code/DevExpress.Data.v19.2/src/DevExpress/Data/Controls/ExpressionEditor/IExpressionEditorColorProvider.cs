namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System.Drawing;

    public interface IExpressionEditorColorProvider
    {
        Color GetColorForElement(ExpressionElementKind elementKind);
    }
}

