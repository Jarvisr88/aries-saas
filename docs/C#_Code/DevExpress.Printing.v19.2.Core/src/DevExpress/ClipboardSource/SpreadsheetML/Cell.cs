namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Xml.Linq;

    public class Cell
    {
        private XElement cellElement;
        private DevExpress.ClipboardSource.SpreadsheetML.Data data;

        public Cell(XElement cellElement)
        {
            this.cellElement = cellElement;
            this.GetData();
        }

        private object GetCellBoolean()
        {
            bool flag;
            return (!bool.TryParse(this.Data.Value, out flag) ? (((this.Data.Value == "0") || (this.Data.Value == "1")) ? ((object) (int.Parse(this.Data.Value) == 1)) : null) : flag);
        }

        private object GetCellDateTime()
        {
            DateTime time;
            return (!DateTime.TryParse(this.Data.Value, out time) ? null : ((object) time));
        }

        private object GetCellError() => 
            null;

        private object GetCellNumber()
        {
            decimal num;
            return (!decimal.TryParse(this.Data.Value, out num) ? null : (!(((10M * num) % 10M) == 0M) ? ((object) decimal.ToDouble(num)) : ((object) decimal.ToInt32(num))));
        }

        private object GetCellString() => 
            this.Data.Value;

        private object GetCellValue()
        {
            if (this.Data != null)
            {
                switch (this.Data.Type)
                {
                    case DataType.Number:
                        return this.GetCellNumber();

                    case DataType.DateTime:
                        return this.GetCellDateTime();

                    case DataType.Boolean:
                        return this.GetCellBoolean();

                    case DataType.String:
                        return this.GetCellString();

                    case DataType.Error:
                        return this.GetCellError();
                }
            }
            return null;
        }

        private void GetData()
        {
            XElement dataElement = this.cellElement.GetElement("Data");
            if (dataElement != null)
            {
                this.data = new DevExpress.ClipboardSource.SpreadsheetML.Data(dataElement);
            }
        }

        public DevExpress.ClipboardSource.SpreadsheetML.Data Data =>
            this.data;

        public object Value =>
            this.GetCellValue();
    }
}

