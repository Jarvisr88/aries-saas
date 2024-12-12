namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Xlsx;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class WorksheetCellDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static Dictionary<string, CellDataType> cellDataTypeTable = CreateCellDataTypeTable();
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();
        [ThreadStatic]
        private static WorksheetCellDestination instance;
        private CellDataType cellDataType;
        private int formatIndex;

        public WorksheetCellDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
            this.Reset();
        }

        public static void ClearInstance()
        {
            instance = null;
            CellValueDestination.ClearInstance();
        }

        private static Dictionary<string, CellDataType> CreateCellDataTypeTable() => 
            new Dictionary<string, CellDataType> { 
                { 
                    "b",
                    CellDataType.Bool
                },
                { 
                    "e",
                    CellDataType.Error
                },
                { 
                    "inlineStr",
                    CellDataType.InlineString
                },
                { 
                    "n",
                    CellDataType.Number
                },
                { 
                    "s",
                    CellDataType.SharedString
                },
                { 
                    "str",
                    CellDataType.FormulaString
                },
                { 
                    "d",
                    CellDataType.DateTime
                }
            };

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("is", new ElementHandler<XlsxSpreadsheetSourceImporter>(WorksheetCellDestination.OnInlineString));
            table.Add("v", new ElementHandler<XlsxSpreadsheetSourceImporter>(WorksheetCellDestination.OnValue));
            return table;
        }

        private XlVariantValue GetBooleanValue() => 
            this.Importer.ConvertToBool(this.Value);

        private XlVariantValue GetDateTimeValue()
        {
            DateTime time;
            return (!DateTime.TryParse(this.Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out time) ? XlVariantValue.Empty : time);
        }

        private XlVariantValue GetErrorValue()
        {
            IXlCellError error;
            return (!XlCellErrorFactory.TryCreateErrorByInvariantName(this.Value, out error) ? XlVariantValue.Empty : error.Value);
        }

        private XlVariantValue GetFormulaStringValue() => 
            this.GetStringValue();

        private XlVariantValue GetInlineStringValue() => 
            this.GetStringValue();

        public static WorksheetCellDestination GetInstance(XlsxSpreadsheetSourceImporter importer)
        {
            if ((instance != null) && (instance.Importer == importer))
            {
                instance.Reset();
            }
            else
            {
                instance = new WorksheetCellDestination(importer);
            }
            return instance;
        }

        private XlVariantValue GetNumericValue()
        {
            double num;
            int num2;
            return (!double.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? (((!this.Importer.StrictOpenXML || !this.Value.EndsWith("%")) || !int.TryParse(this.Value.Remove(this.Value.Length - 1), NumberStyles.Integer, CultureInfo.InvariantCulture, out num2)) ? XlVariantValue.Empty : (num2 * 0x3e8)) : this.Importer.Source.DataReader.GetDateTimeOrNumericValue(num, this.formatIndex));
        }

        private XlVariantValue GetSharedStringValue()
        {
            int num;
            return new XlVariantValue { TextValue = int.TryParse(this.Value, out num) ? this.Importer.Source.SharedStrings[num] : string.Empty };
        }

        private XlVariantValue GetStringValue() => 
            new XlVariantValue { TextValue = ExcelXmlCharsCodec.Decode(this.Value) };

        private static WorksheetCellDestination GetThis(XlsxSpreadsheetSourceImporter importer) => 
            (WorksheetCellDestination) importer.PeekDestination();

        private XlVariantValue GetVariantValue()
        {
            switch (this.cellDataType)
            {
                case CellDataType.Bool:
                    return this.GetBooleanValue();

                case CellDataType.Error:
                    return this.GetErrorValue();

                case CellDataType.InlineString:
                    return this.GetInlineStringValue();

                case CellDataType.Number:
                    return this.GetNumericValue();

                case CellDataType.SharedString:
                    return this.GetSharedStringValue();

                case CellDataType.FormulaString:
                    return this.GetFormulaStringValue();

                case CellDataType.DateTime:
                    return this.GetDateTimeValue();
            }
            throw new InvalidOperationException("Invalid cell data type");
        }

        private static Destination OnInlineString(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new InlineStringDestination(importer, GetThis(importer));

        private static Destination OnValue(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            CellValueDestination.GetInstance(importer, GetThis(importer));

        public override void ProcessElementClose(XmlReader reader)
        {
            if (!string.IsNullOrEmpty(this.Value))
            {
                int currentCellIndex = this.Importer.GetCurrentCellIndex();
                XlsxSourceDataReader dataReader = this.Importer.Source.DataReader;
                if (dataReader.CanAddCell(currentCellIndex))
                {
                    dataReader.AddCell(currentCellIndex, this.GetVariantValue(), this.formatIndex);
                }
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.cellDataType = this.Importer.GetWpEnumValue<CellDataType>(reader, "t", cellDataTypeTable, CellDataType.Number);
            this.formatIndex = this.Importer.GetWpSTIntegerValue(reader, "s", -2147483648);
        }

        private void Reset()
        {
            this.cellDataType = CellDataType.Number;
            this.formatIndex = 0;
            this.Value = null;
        }

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;

        public string Value { get; set; }

        private enum CellDataType
        {
            Bool,
            Error,
            InlineString,
            Number,
            SharedString,
            FormulaString,
            DateTime
        }
    }
}

