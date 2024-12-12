namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlPtgOffsetWalker : IXlPtgVisitor
    {
        private XlExpression result;

        public XlExpression ProcessExpression(XlExpression expression)
        {
            if ((this.ColumnOffset == 0) && (this.RowOffset == 0))
            {
                return expression;
            }
            this.result = new XlExpression();
            foreach (XlPtgBase base2 in expression)
            {
                base2.Visit(this);
            }
            return this.result;
        }

        public void Visit(XlPtgArea ptg)
        {
            this.result.Add(new XlPtgArea(ptg.TopLeft.Offset(this.ColumnOffset, this.RowOffset), ptg.BottomRight.Offset(this.ColumnOffset, this.RowOffset), ptg.DataType));
        }

        public void Visit(XlPtgArea3d ptg)
        {
            this.result.Add(new XlPtgArea3d(ptg.TopLeft.Offset(this.ColumnOffset, this.RowOffset), ptg.BottomRight.Offset(this.ColumnOffset, this.RowOffset), ptg.SheetName, ptg.DataType));
        }

        public void Visit(XlPtgAreaErr ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAreaErr3d ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAreaN ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAreaN3d ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgArray ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrChoose ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrGoto ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrIf ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrIfError ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrSemi ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgAttrSpace ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgBinaryOperator ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgBool ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgErr ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgExp ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgFunc ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgFuncVar ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgInt ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgMemArea ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgMemErr ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgMemFunc ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgMissArg ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgName ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgNameX ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgNum ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgParen ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgRef ptg)
        {
            this.result.Add(new XlPtgRef(ptg.CellPosition.Offset(this.ColumnOffset, this.RowOffset), ptg.DataType));
        }

        public void Visit(XlPtgRef3d ptg)
        {
            this.result.Add(new XlPtgRef3d(ptg.CellPosition.Offset(this.ColumnOffset, this.RowOffset), ptg.SheetName, ptg.DataType));
        }

        public void Visit(XlPtgRefErr ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgRefErr3d ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgRefN ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgRefN3d ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgStr ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgTableRef ptg)
        {
            this.result.Add(ptg);
        }

        public void Visit(XlPtgUnaryOperator ptg)
        {
            this.result.Add(ptg);
        }

        public int ColumnOffset { get; set; }

        public int RowOffset { get; set; }
    }
}

