namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class LayoutAssigner
    {
        public static readonly LayoutAssigner Default = new LayoutAssigner();

        public virtual void CreateLayout(ColumnsLayoutCalculator calculator)
        {
            calculator.CreateLayout();
        }

        public virtual ColumnPosition GetColumnPosition(BaseColumn column) => 
            column.ColumnPosition;

        public virtual bool GetHasLeftSibling(BaseColumn column) => 
            column.HasLeftSibling;

        public virtual bool GetHasRightSibling(BaseColumn column) => 
            column.HasRightSibling;

        public virtual double GetWidth(BaseColumn column) => 
            column.ActualHeaderWidth;

        public virtual void SetColumnPosition(BaseColumn column, ColumnPosition position)
        {
            column.ColumnPosition = position;
        }

        public virtual void SetHasLeftSibling(BaseColumn column, bool value)
        {
            column.HasLeftSibling = value;
        }

        public virtual void SetHasRightSibling(BaseColumn column, bool value)
        {
            column.HasRightSibling = value;
        }

        public virtual void SetWidth(BaseColumn column, double value)
        {
            column.ActualHeaderWidth = value;
        }

        public virtual bool UseDataAreaIndent =>
            true;

        public virtual bool UseFixedColumnIndents =>
            true;

        public virtual bool UseDetailButtonsIndents =>
            true;
    }
}

