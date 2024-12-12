namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.Collections.Generic;

    internal class XlsPtgConverter : IXlPtgVisitor
    {
        private readonly XlsDataAwareExporter exporter;
        private XlExpression result;
        private List<IOffsetThing> offsetThings = new List<IOffsetThing>();

        public XlsPtgConverter(XlsDataAwareExporter exporter)
        {
            this.exporter = exporter;
        }

        private void AddOffsetToken(XlPtgBase thing)
        {
            XlPtgAttrIf @if = thing as XlPtgAttrIf;
            XlPtgAttrIfError error = thing as XlPtgAttrIfError;
            XlPtgAttrGoto @goto = thing as XlPtgAttrGoto;
            XlPtgAttrChoose choose = thing as XlPtgAttrChoose;
            XlPtgMemBase base2 = thing as XlPtgMemBase;
            if (@if != null)
            {
                this.offsetThings.Add(new AttrIfOffsetThing(@if));
            }
            else if (error != null)
            {
                this.offsetThings.Add(new AttrIfErrorOffsetThing(error));
            }
            else if (@goto != null)
            {
                this.offsetThings.Add(new AttrGotoOffsetThing(@goto));
            }
            else if (base2 != null)
            {
                this.offsetThings.Add(new MemOffsetThing(base2));
            }
            else if (choose != null)
            {
                for (int i = 0; i < choose.Offsets.Count; i++)
                {
                    this.offsetThings.Add(new AttrChoosePartOffsetThing(choose, i));
                }
            }
        }

        public XlExpression Convert(XlExpression expression)
        {
            this.result = new XlExpression();
            this.offsetThings.Clear();
            foreach (XlPtgBase base2 in expression)
            {
                base2.Visit(this);
                this.CountDownOffsetThings();
                if (this.IsControlToken(base2) || this.IsMemToken(base2))
                {
                    this.AddOffsetToken(base2);
                }
            }
            return this.result;
        }

        private void CountDownOffsetThings()
        {
            int index = 0;
            while (index < this.offsetThings.Count)
            {
                if (this.offsetThings[index].CountDown())
                {
                    this.offsetThings.RemoveAt(index);
                    continue;
                }
                index++;
            }
        }

        private int GetCountDelta(XlPtgBase thing) => 
            !this.IsFunction(thing) ? (!this.IsBinaryOperator(thing) ? ((this.IsUnaryOperator(thing) || (this.IsAttribute(thing) || (this.IsMemToken(thing) || (this.IsDisplayToken(thing) || this.IsControlToken(thing))))) ? 0 : -1) : 1) : (this.GetFuncParamCount(thing) - 1);

        private int GetFuncParamCount(XlPtgBase thing)
        {
            XlPtgFuncVar var = thing as XlPtgFuncVar;
            return ((var == null) ? XlFunctionRepository.GetFunctionInfo((thing as XlPtgFunc).FuncCode).MinArgumentsCount : var.ParamCount);
        }

        private bool IsAttribute(XlPtgBase thing) => 
            (thing.TypeCode == 0x219) || ((thing.TypeCode == 0x419) || ((thing.TypeCode == 0x819) || ((thing.TypeCode == 0x119) || (thing.TypeCode == 0x4019))));

        private bool IsBinaryOperator(XlPtgBase thing) => 
            thing is XlPtgBinaryOperator;

        private bool IsControlToken(XlPtgBase thing) => 
            (thing.TypeCode == 0x219) || ((thing.TypeCode == 0x419) || (thing.TypeCode == 0x819));

        private bool IsDisplayToken(XlPtgBase thing) => 
            (thing.TypeCode == 0x4019) || (thing.TypeCode == 0x15);

        private bool IsFunction(XlPtgBase thing) => 
            thing is XlPtgFunc;

        private bool IsMemToken(XlPtgBase thing) => 
            thing is XlPtgMemBase;

        private bool IsUnaryOperator(XlPtgBase thing) => 
            thing is XlPtgUnaryOperator;

        private void MoveOffsetThings(int offset)
        {
            for (int i = 0; i < this.offsetThings.Count; i++)
            {
                this.offsetThings[i].Move(offset);
            }
        }

        private bool OutOfXlsRange(XlCellPosition position) => 
            (position.Column >= 0x100) || (position.Row >= 0x10000);

        private void TakeArguments(XlExpression expression, Stack<XlPtgBase> funcArgs, int paramCount)
        {
            XlPtgBase base2;
            for (int i = paramCount; (i > 0) && (expression.Count > 0); i += this.GetCountDelta(base2))
            {
                int index = expression.Count - 1;
                base2 = expression[index];
                expression.RemoveAt(index);
                funcArgs.Push(base2);
            }
            while (true)
            {
                if (expression.Count > 0)
                {
                    int index = expression.Count - 1;
                    XlPtgBase thing = expression[index];
                    if (!this.IsControlToken(thing) && (this.IsAttribute(thing) || (this.IsMemToken(thing) || this.IsDisplayToken(thing))))
                    {
                        expression.RemoveAt(index);
                        funcArgs.Push(thing);
                        continue;
                    }
                }
                return;
            }
        }

        public void Visit(XlPtgArea ptg)
        {
            if (this.OutOfXlsRange(ptg.TopLeft))
            {
                this.result.Add(new XlPtgAreaErr(ptg.DataType));
            }
            else if (!this.OutOfXlsRange(ptg.BottomRight))
            {
                this.result.Add(ptg);
            }
            else
            {
                XlCellPosition bottomRight = new XlCellPosition(Math.Min(ptg.BottomRight.Column, 0xff), Math.Min(ptg.BottomRight.Row, 0xffff), ptg.BottomRight.ColumnType, ptg.BottomRight.RowType);
                XlPtgArea item = new XlPtgArea(ptg.TopLeft, bottomRight);
                item.DataType = ptg.DataType;
                this.result.Add(item);
            }
        }

        public void Visit(XlPtgArea3d ptg)
        {
            if (this.OutOfXlsRange(ptg.TopLeft))
            {
                XlPtgAreaErr3d item = new XlPtgAreaErr3d(ptg.SheetName) {
                    DataType = ptg.DataType
                };
                this.result.Add(item);
            }
            else if (!this.OutOfXlsRange(ptg.BottomRight))
            {
                this.result.Add(ptg);
            }
            else
            {
                XlCellPosition bottomRight = new XlCellPosition(Math.Min(ptg.BottomRight.Column, 0xff), Math.Min(ptg.BottomRight.Row, 0xffff), ptg.BottomRight.ColumnType, ptg.BottomRight.RowType);
                XlPtgArea3d item = new XlPtgArea3d(ptg.TopLeft, bottomRight, ptg.SheetName);
                item.DataType = ptg.DataType;
                this.result.Add(item);
            }
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
            XlFunctionInfo functionInfo = XlFunctionRepository.GetFunctionInfo(ptg.FuncCode);
            if (!functionInfo.IsExcel2003FutureFunction())
            {
                this.result.Add(ptg);
            }
            else
            {
                Stack<XlPtgBase> funcArgs = new Stack<XlPtgBase>();
                int minArgumentsCount = functionInfo.MinArgumentsCount;
                this.TakeArguments(this.result, funcArgs, minArgumentsCount);
                if (functionInfo.IsExcel2010FutureFunction())
                {
                    XlPtgName name = new XlPtgName(this.exporter.RegisterFutureFunction(functionInfo.Name), XlPtgDataType.Reference);
                    this.result.Add(name);
                }
                else
                {
                    XlPtgNameX ex = new XlPtgNameX(functionInfo.Name, XlPtgDataType.Reference);
                    this.exporter.RegisterExternalName(ex);
                    this.result.Add(ex);
                }
                this.result.AddRange(funcArgs);
                XlPtgFuncVar item = new XlPtgFuncVar(0xff, minArgumentsCount + 1, ptg.DataType);
                this.result.Add(item);
                this.MoveOffsetThings(1);
            }
        }

        public void Visit(XlPtgFuncVar ptg)
        {
            XlFunctionInfo functionInfo = XlFunctionRepository.GetFunctionInfo(ptg.FuncCode);
            if (!functionInfo.IsExcel2003FutureFunction())
            {
                this.result.Add(ptg);
            }
            else
            {
                Stack<XlPtgBase> funcArgs = new Stack<XlPtgBase>();
                int paramCount = ptg.ParamCount;
                this.TakeArguments(this.result, funcArgs, paramCount);
                if (functionInfo.IsExcel2010FutureFunction())
                {
                    XlPtgName name = new XlPtgName(this.exporter.RegisterFutureFunction(functionInfo.Name), XlPtgDataType.Reference);
                    this.result.Add(name);
                }
                else
                {
                    XlPtgNameX ex = new XlPtgNameX(functionInfo.Name, XlPtgDataType.Reference);
                    this.exporter.RegisterExternalName(ex);
                    this.result.Add(ex);
                }
                this.result.AddRange(funcArgs);
                XlPtgFuncVar item = new XlPtgFuncVar(0xff, paramCount + 1, ptg.DataType);
                this.result.Add(item);
                this.MoveOffsetThings(1);
            }
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
            this.exporter.RegisterExternalName(ptg);
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
            if (!this.OutOfXlsRange(ptg.CellPosition))
            {
                this.result.Add(ptg);
            }
            else
            {
                this.result.Add(new XlPtgRefErr(ptg.DataType));
            }
        }

        public void Visit(XlPtgRef3d ptg)
        {
            if (!this.OutOfXlsRange(ptg.CellPosition))
            {
                this.result.Add(ptg);
            }
            else
            {
                XlPtgRefErr3d item = new XlPtgRefErr3d(ptg.SheetName) {
                    DataType = ptg.DataType
                };
                this.result.Add(item);
            }
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
            IXlSheet currentSheet = this.exporter.CurrentSheet;
            XlTable table = ptg.TableReference.Table as XlTable;
            if ((currentSheet == null) || ((table == null) || ((currentSheet.Tables[table.Name] == null) || !table.IsValidTablePart(ptg.TableReference.Part))))
            {
                this.result.Add(new XlPtgRefErr(ptg.DataType));
            }
            else
            {
                XlCellRange range = table.GetRange(ptg.TableReference, this.exporter.ExpressionContext.CurrentCell);
                if (range == null)
                {
                    this.result.Add(new XlPtgErr(XlCellErrorType.Value));
                }
                else if ((range.RowCount == 1) && (range.ColumnCount == 1))
                {
                    this.result.Add(new XlPtgRef3d(range.TopLeft, currentSheet.Name, ptg.DataType));
                }
                else
                {
                    XlPtgArea3d item = new XlPtgArea3d(range.TopLeft, range.BottomRight, currentSheet.Name);
                    item.DataType = ptg.DataType;
                    this.result.Add(item);
                }
            }
        }

        public void Visit(XlPtgUnaryOperator ptg)
        {
            this.result.Add(ptg);
        }

        private class AttrChoosePartOffsetThing : XlsPtgConverter.OffsetThingBase
        {
            private XlPtgAttrChoose thing;
            private int partIndex;

            public AttrChoosePartOffsetThing(XlPtgAttrChoose thing, int partIndex) : base(thing.Offsets[partIndex])
            {
                this.partIndex = partIndex;
                this.thing = thing;
            }

            public override void Move(int offset)
            {
                IList<int> offsets = this.thing.Offsets;
                int partIndex = this.partIndex;
                offsets[partIndex] += offset;
            }
        }

        private class AttrGotoOffsetThing : XlsPtgConverter.OffsetThingBase
        {
            private XlPtgAttrGoto thing;

            public AttrGotoOffsetThing(XlPtgAttrGoto thing) : base(thing.Offset)
            {
                this.thing = thing;
            }

            public override void Move(int offset)
            {
                this.thing.Offset += offset;
            }
        }

        private class AttrIfErrorOffsetThing : XlsPtgConverter.OffsetThingBase
        {
            private XlPtgAttrIfError thing;

            public AttrIfErrorOffsetThing(XlPtgAttrIfError thing) : base(thing.Offset)
            {
                this.thing = thing;
            }

            public override void Move(int offset)
            {
                this.thing.Offset += offset;
            }
        }

        private class AttrIfOffsetThing : XlsPtgConverter.OffsetThingBase
        {
            private XlPtgAttrIf thing;

            public AttrIfOffsetThing(XlPtgAttrIf thing) : base(thing.Offset)
            {
                this.thing = thing;
            }

            public override void Move(int offset)
            {
                this.thing.Offset += offset;
            }
        }

        private interface IOffsetThing
        {
            bool CountDown();
            void Move(int offset);
        }

        private class MemOffsetThing : XlsPtgConverter.OffsetThingBase
        {
            private XlPtgMemBase thing;

            public MemOffsetThing(XlPtgMemBase thing) : base(thing.InnerThingCount)
            {
                this.thing = thing;
            }

            public override void Move(int offset)
            {
                this.thing.InnerThingCount += offset;
            }
        }

        private abstract class OffsetThingBase : XlsPtgConverter.IOffsetThing
        {
            private int offset;

            protected OffsetThingBase(int offset)
            {
                this.offset = offset;
            }

            public bool CountDown()
            {
                this.offset--;
                return (this.offset <= 0);
            }

            public abstract void Move(int offset);
        }
    }
}

