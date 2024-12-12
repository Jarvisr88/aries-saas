namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;

    public class XlSparkline
    {
        private XlCellRange dataRange;
        private XlCellRange location;

        public XlSparkline(XlCellRange dataRange, XlCellRange location)
        {
            this.DataRange = dataRange;
            this.Location = location;
        }

        public XlCellRange DataRange
        {
            get => 
                this.dataRange;
            set
            {
                Guard.ArgumentNotNull(value, "DataRange");
                if ((value.RowCount > 1) && (value.ColumnCount > 1))
                {
                    throw new ArgumentException("Data range is invalid");
                }
                this.dataRange = value;
            }
        }

        public XlCellRange Location
        {
            get => 
                this.location;
            set
            {
                Guard.ArgumentNotNull(value, "Location");
                if ((value.RowCount != 1) || (value.ColumnCount != 1))
                {
                    throw new ArgumentException("Location is invalid");
                }
                this.location = value;
                this.location.SheetName = string.Empty;
            }
        }
    }
}

