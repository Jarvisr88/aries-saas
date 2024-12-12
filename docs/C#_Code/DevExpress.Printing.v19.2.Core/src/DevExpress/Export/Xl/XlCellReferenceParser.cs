namespace DevExpress.Export.Xl
{
    using DevExpress.Office.Utils;
    using System;

    internal class XlCellReferenceParser
    {
        private static XlCellReferenceParser instance = new XlCellReferenceParser();

        public static XlCellPosition Parse(string reference) => 
            Parse(reference, true);

        public static XlCellPosition Parse(string reference, bool throwInvalidException)
        {
            XlCellPosition position = instance.ParseCore(reference);
            if (!position.IsValid & throwInvalidException)
            {
                throw new ArgumentException(reference, reference);
            }
            return position;
        }

        private XlCellPosition ParseCore(string reference)
        {
            CellReferencePart part = new CellReferencePart(CellReferenceParserProvider.Letters, 0x1a);
            int from = part.Parse(reference, 0);
            part.Value--;
            if (part.Value > XlCellPosition.MaxColumnCount)
            {
                return XlCellPosition.InvalidValue;
            }
            CellReferencePart part2 = new CellReferencePart(CellReferenceParserProvider.Digits, 10);
            if (part2.Parse(reference, from) < reference.Length)
            {
                return XlCellPosition.InvalidValue;
            }
            part2.Value--;
            return ((part2.Value < XlCellPosition.MaxRowCount) ? new XlCellPosition(part.Value, part2.Value, (XlPositionType) part.Type, (XlPositionType) part2.Type) : XlCellPosition.InvalidValue);
        }
    }
}

