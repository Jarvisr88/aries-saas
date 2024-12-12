namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class XlsPtgBinaryWriter : IXlPtgVisitor
    {
        private readonly BinaryWriter writer;
        private BinaryWriter writerExtra;
        private readonly XlsDataAwareExporter exporter;
        private int currentExpression;
        private List<IOffsetPositionWriter> pendingOffsetWriters;

        public XlsPtgBinaryWriter(BinaryWriter writer, XlsDataAwareExporter exporter)
        {
            this.writer = writer;
            this.exporter = exporter;
            this.pendingOffsetWriters = new List<IOffsetPositionWriter>();
        }

        private void AddOffsetWriter(IOffsetPositionWriter offsetWriter)
        {
            int index = Algorithms.BinarySearchReverseOrder<IOffsetPositionWriter>(this.pendingOffsetWriters, new OffsetWriterComparable((long) offsetWriter.OffsetPositionThingsCounter));
            if (index < 0)
            {
                index = ~index;
            }
            this.pendingOffsetWriters.Insert(index, offsetWriter);
        }

        public void Visit(XlPtgArea ptg)
        {
            this.WritePtgCode(ptg);
            this.WriteArea(ptg.TopLeft, ptg.BottomRight);
        }

        public void Visit(XlPtgArea3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.WriteArea(ptg.TopLeft, ptg.BottomRight);
        }

        public void Visit(XlPtgAreaErr ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgAreaErr3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgAreaN ptg)
        {
            this.WritePtgCode(ptg);
            this.WriteAreaRelative(ptg.TopLeft, ptg.BottomRight);
        }

        public void Visit(XlPtgAreaN3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.WriteAreaRelative(ptg.TopLeft, ptg.BottomRight);
        }

        public void Visit(XlPtgArray ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((byte) 0);
            this.writer.Write((ushort) 0);
            this.writer.Write((uint) 0);
            this.writerExtra.Write((byte) (ptg.Width - 1));
            this.writerExtra.Write((ushort) (ptg.Height - 1));
            int num = ptg.Width * ptg.Height;
            IList<XlVariantValue> values = ptg.Values;
            for (int i = 0; i < num; i++)
            {
                XlVariantValue item = values[i];
                this.WriteArrayItem(item);
            }
        }

        public void Visit(XlPtgAttrChoose ptg)
        {
            this.WritePtgCode(ptg);
            int count = ptg.Offsets.Count;
            this.writer.Write((ushort) count);
            this.writer.Write((ushort) ((count + 1) * 2));
            for (int i = 0; i < count; i++)
            {
                AttrChoosePartOffsetPositionWriter offsetWriter = new AttrChoosePartOffsetPositionWriter(this.writer.BaseStream.Position, ptg.Offsets[i] + this.currentExpression, i);
                this.AddOffsetWriter(offsetWriter);
                this.writer.Write((ushort) 0);
            }
        }

        public void Visit(XlPtgAttrGoto ptg)
        {
            this.WritePtgCode(ptg);
            AttrGotoOffsetPositionWriter offsetWriter = new AttrGotoOffsetPositionWriter(this.writer.BaseStream.Position, ptg.Offset + this.currentExpression);
            this.AddOffsetWriter(offsetWriter);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgAttrIf ptg)
        {
            this.WritePtgCode(ptg);
            AttrIfOffsetPositionWriter offsetWriter = new AttrIfOffsetPositionWriter(this.writer.BaseStream.Position, ptg.Offset + this.currentExpression);
            this.AddOffsetWriter(offsetWriter);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgAttrIfError ptg)
        {
            throw new InvalidOperationException("PtgAttrIfError not suppurted in XLS.");
        }

        public void Visit(XlPtgAttrSemi ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgAttrSpace ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((byte) ptg.SpaceType);
            this.writer.Write((byte) ptg.CharCount);
        }

        public void Visit(XlPtgBinaryOperator ptg)
        {
            this.WritePtgCode(ptg);
        }

        public void Visit(XlPtgBool ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write(ptg.Value ? ((byte) 1) : ((byte) 0));
        }

        public void Visit(XlPtgErr ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((byte) ptg.Value);
        }

        public void Visit(XlPtgExp ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) ptg.CellPosition.Row);
            this.writer.Write((ushort) ((byte) ptg.CellPosition.Column));
        }

        public void Visit(XlPtgFunc ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) ptg.FuncCode);
        }

        public void Visit(XlPtgFuncVar ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((byte) ptg.ParamCount);
            this.writer.Write((ushort) ptg.FuncCode);
        }

        public void Visit(XlPtgInt ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) ptg.Value);
        }

        public void Visit(XlPtgMemArea ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write(0);
            this.WriteMemThing(ptg);
            int count = ptg.Ranges.Count;
            this.writerExtra.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                XlCellRange range = ptg.Ranges[i];
                this.writerExtra.Write((ushort) range.TopLeft.Row);
                this.writerExtra.Write((ushort) range.BottomRight.Row);
                this.writerExtra.Write((ushort) range.TopLeft.Column);
                XlCellPosition bottomRight = range.BottomRight;
                this.writerExtra.Write((ushort) bottomRight.Column);
            }
        }

        public void Visit(XlPtgMemErr ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write(ptg.ErrorValue);
            this.writer.Write((byte) 0);
            this.writer.Write((ushort) 0);
            this.WriteMemThing(ptg);
        }

        public void Visit(XlPtgMemFunc ptg)
        {
            this.WritePtgCode(ptg);
            this.WriteMemThing(ptg);
        }

        public void Visit(XlPtgMissArg ptg)
        {
            this.WritePtgCode(ptg);
        }

        public void Visit(XlPtgName ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((uint) ptg.NameIndex);
        }

        public void Visit(XlPtgNameX ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) ptg.XtiIndex);
            this.writer.Write((uint) ptg.NameIndex);
        }

        public void Visit(XlPtgNum ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write(ptg.Value);
        }

        public void Visit(XlPtgParen ptg)
        {
            this.WritePtgCode(ptg);
        }

        public void Visit(XlPtgRef ptg)
        {
            this.WritePtgCode(ptg);
            this.WriteCellPosition(ptg.CellPosition);
        }

        public void Visit(XlPtgRef3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.WriteCellPosition(ptg.CellPosition);
        }

        public void Visit(XlPtgRefErr ptg)
        {
            this.WritePtgCode(ptg);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgRefErr3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.writer.Write((ushort) 0);
            this.writer.Write((ushort) 0);
        }

        public void Visit(XlPtgRefN ptg)
        {
            this.WritePtgCode(ptg);
            this.WriteCellOffset(ptg.CellOffset);
        }

        public void Visit(XlPtgRefN3d ptg)
        {
            this.WritePtgCode(ptg);
            int num = this.exporter.RegisterSheetDefinition(ptg.SheetName);
            this.writer.Write((ushort) num);
            this.WriteCellOffset(ptg.CellOffset);
        }

        public void Visit(XlPtgStr ptg)
        {
            this.WritePtgCode(ptg);
            new ShortXLUnicodeString { Value = ptg.Value }.Write(this.writer);
        }

        public void Visit(XlPtgTableRef ptg)
        {
            throw new InvalidOperationException("Table reference cannot be written to XLS.");
        }

        public void Visit(XlPtgUnaryOperator ptg)
        {
            this.WritePtgCode(ptg);
        }

        public void Write(XlExpression expression)
        {
            long position = this.writer.BaseStream.Position;
            this.writer.Write((ushort) 0);
            using (MemoryStream stream = new MemoryStream())
            {
                using (this.writerExtra = new BinaryWriter(stream))
                {
                    int count = expression.Count;
                    this.currentExpression = 0;
                    while (true)
                    {
                        if (this.currentExpression >= count)
                        {
                            long num3 = this.writer.BaseStream.Position;
                            this.writer.BaseStream.Position = position;
                            this.writer.Write((ushort) ((num3 - position) - 2L));
                            this.writer.BaseStream.Position = num3;
                            this.writer.Write(stream.ToArray());
                            break;
                        }
                        expression[this.currentExpression].Visit(this);
                        while (true)
                        {
                            if (this.pendingOffsetWriters.Count > 0)
                            {
                                IOffsetPositionWriter writer3 = this.pendingOffsetWriters[0];
                                if (writer3.OffsetPositionThingsCounter == this.currentExpression)
                                {
                                    writer3.WriteData(this.writer);
                                    this.pendingOffsetWriters.RemoveAt(0);
                                    continue;
                                }
                            }
                            this.currentExpression++;
                            break;
                        }
                    }
                }
            }
        }

        private void WriteArea(XlCellPosition topLeft, XlCellPosition bottomRight)
        {
            this.writer.Write((topLeft.Row == XlCellPosition.InvalidValue.Row) ? ((ushort) 0) : ((ushort) topLeft.Row));
            this.writer.Write((bottomRight.Row == XlCellPosition.InvalidValue.Row) ? ((ushort) 0xffff) : ((ushort) bottomRight.Row));
            ushort num = (topLeft.Column == XlCellPosition.InvalidValue.Column) ? ((byte) 0) : ((byte) topLeft.Column);
            if (topLeft.ColumnType == XlPositionType.Relative)
            {
                num = (ushort) (num | 0x4000);
            }
            if (topLeft.RowType == XlPositionType.Relative)
            {
                num = (ushort) (num | 0x8000);
            }
            this.writer.Write(num);
            num = (bottomRight.Column == XlCellPosition.InvalidValue.Column) ? ((byte) 0xff) : ((byte) bottomRight.Column);
            if (bottomRight.ColumnType == XlPositionType.Relative)
            {
                num = (ushort) (num | 0x4000);
            }
            if (bottomRight.RowType == XlPositionType.Relative)
            {
                num = (ushort) (num | 0x8000);
            }
            this.writer.Write(num);
        }

        private void WriteAreaRelative(XlCellOffset first, XlCellOffset last)
        {
            this.writer.Write((ushort) first.Row);
            this.writer.Write((ushort) last.Row);
            ushort num = (ushort) (((ushort) first.Column) & 0x3fff);
            if (first.ColumnType == XlCellOffsetType.Offset)
            {
                num = (ushort) (num | 0x4000);
            }
            if (first.RowType == XlCellOffsetType.Offset)
            {
                num = (ushort) (num | 0x8000);
            }
            this.writer.Write(num);
            num = (ushort) (((ushort) last.Column) & 0x3fff);
            if (last.ColumnType == XlCellOffsetType.Offset)
            {
                num = (ushort) (num | 0x4000);
            }
            if (last.RowType == XlCellOffsetType.Offset)
            {
                num = (ushort) (num | 0x8000);
            }
            this.writer.Write(num);
        }

        private void WriteArrayBooleanItem(bool value)
        {
            this.writerExtra.Write((byte) 4);
            this.writerExtra.Write(value ? ((byte) 1) : ((byte) 0));
            this.writerExtra.Write((byte) 0);
            this.writerExtra.Write((ushort) 0);
            this.writerExtra.Write((uint) 0);
        }

        private void WriteArrayErrorItem(XlCellErrorType xlCellErrorType)
        {
            this.writerExtra.Write((byte) 0x10);
            this.writerExtra.Write((byte) xlCellErrorType);
            this.writerExtra.Write((byte) 0);
            this.writerExtra.Write((ushort) 0);
            this.writerExtra.Write((uint) 0);
        }

        private void WriteArrayItem(XlVariantValue item)
        {
            if (item.IsBoolean)
            {
                this.WriteArrayBooleanItem(item.BooleanValue);
            }
            else if (item.IsError)
            {
                this.WriteArrayErrorItem(item.ErrorValue.Type);
            }
            else if (item.IsText)
            {
                this.WriteArrayTextItem(item.TextValue);
            }
            else if (item.IsNumeric)
            {
                this.WriteArrayNumericItem(item.NumericValue);
            }
            else
            {
                this.WriteArrayNilItem();
            }
        }

        private void WriteArrayNilItem()
        {
            this.writerExtra.Write((byte) 0);
            this.writerExtra.Write((uint) 0);
            this.writerExtra.Write((uint) 0);
        }

        private void WriteArrayNumericItem(double value)
        {
            this.writerExtra.Write((byte) 1);
            if (XNumChecker.IsNegativeZero(value))
            {
                value = 0.0;
            }
            this.writerExtra.Write(value);
        }

        private void WriteArrayTextItem(string value)
        {
            this.writerExtra.Write((byte) 2);
            new XLUnicodeString { Value = (value.Length <= 0xff) ? value : value.Substring(0, 0xff) }.Write(this.writerExtra);
        }

        private void WriteCellOffset(XlCellOffset cellOffset)
        {
            ushort row = (ushort) cellOffset.Row;
            ushort num2 = (ushort) (((ushort) cellOffset.Column) & 0x3fff);
            if (cellOffset.ColumnType == XlCellOffsetType.Offset)
            {
                num2 = (ushort) (num2 | 0x4000);
            }
            if (cellOffset.RowType == XlCellOffsetType.Offset)
            {
                num2 = (ushort) (num2 | 0x8000);
            }
            this.writer.Write(row);
            this.writer.Write(num2);
        }

        private void WriteCellPosition(XlCellPosition cellPosition)
        {
            this.writer.Write((ushort) cellPosition.Row);
            ushort column = (byte) cellPosition.Column;
            if (cellPosition.ColumnType == XlPositionType.Relative)
            {
                column = (ushort) (column | 0x4000);
            }
            if (cellPosition.RowType == XlPositionType.Relative)
            {
                column = (ushort) (column | 0x8000);
            }
            this.writer.Write(column);
        }

        private void WriteMemThing(XlPtgMemBase ptg)
        {
            MemThingOffsetPositionWriter offsetWriter = new MemThingOffsetPositionWriter(this.writer.BaseStream.Position, ptg.InnerThingCount + this.currentExpression);
            this.AddOffsetWriter(offsetWriter);
            this.writer.Write((ushort) 0);
        }

        private void WritePtgCode(XlPtgBase ptg)
        {
            int ptgCode = ptg.GetPtgCode();
            if (ptgCode > 0xff)
            {
                this.writer.Write((ushort) ptgCode);
            }
            else
            {
                this.writer.Write((byte) ptgCode);
            }
        }
    }
}

