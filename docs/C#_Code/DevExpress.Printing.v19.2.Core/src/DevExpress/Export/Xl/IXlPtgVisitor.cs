namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlPtgVisitor
    {
        void Visit(XlPtgArea ptg);
        void Visit(XlPtgArea3d ptg);
        void Visit(XlPtgAreaErr ptg);
        void Visit(XlPtgAreaErr3d ptg);
        void Visit(XlPtgAreaN ptg);
        void Visit(XlPtgAreaN3d ptg);
        void Visit(XlPtgArray ptg);
        void Visit(XlPtgAttrChoose ptg);
        void Visit(XlPtgAttrGoto ptg);
        void Visit(XlPtgAttrIf ptg);
        void Visit(XlPtgAttrIfError ptg);
        void Visit(XlPtgAttrSemi ptg);
        void Visit(XlPtgAttrSpace ptg);
        void Visit(XlPtgBinaryOperator ptg);
        void Visit(XlPtgBool ptg);
        void Visit(XlPtgErr ptg);
        void Visit(XlPtgExp ptg);
        void Visit(XlPtgFunc ptg);
        void Visit(XlPtgFuncVar ptg);
        void Visit(XlPtgInt ptg);
        void Visit(XlPtgMemArea ptg);
        void Visit(XlPtgMemErr ptg);
        void Visit(XlPtgMemFunc ptg);
        void Visit(XlPtgMissArg ptg);
        void Visit(XlPtgName ptg);
        void Visit(XlPtgNameX ptg);
        void Visit(XlPtgNum ptg);
        void Visit(XlPtgParen ptg);
        void Visit(XlPtgRef ptg);
        void Visit(XlPtgRef3d ptg);
        void Visit(XlPtgRefErr ptg);
        void Visit(XlPtgRefErr3d ptg);
        void Visit(XlPtgRefN ptg);
        void Visit(XlPtgRefN3d ptg);
        void Visit(XlPtgStr ptg);
        void Visit(XlPtgTableRef ptg);
        void Visit(XlPtgUnaryOperator ptg);
    }
}

