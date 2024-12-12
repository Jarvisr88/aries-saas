namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class AdvBandedColumnPositionConverter<TCol> : IXlColumnPositionConverter where TCol: class, IColumn
    {
        private Dictionary<TCol, BandNodeDescriptor> bandsLayoutInfo;
        private int headerPanelRowsCount;

        public AdvBandedColumnPositionConverter(Dictionary<TCol, BandNodeDescriptor> bandsLayoutInfo, int headerPanelRowsCount)
        {
            Guard.ArgumentNotNull(bandsLayoutInfo, "BandsLayoutInfo");
            this.bandsLayoutInfo = bandsLayoutInfo;
            this.headerPanelRowsCount = headerPanelRowsCount;
        }

        public int GetColumnIndex(string name) => 
            this.bandsLayoutInfo.FirstOrDefault<KeyValuePair<TCol, BandNodeDescriptor>>(x => (x.Key.FieldName == name)).Value.ColIndex;

        public int GetRowOffset(string columnName) => 
            Math.Abs((int) (this.bandsLayoutInfo.FirstOrDefault<KeyValuePair<TCol, BandNodeDescriptor>>(x => (x.Key.FieldName == columnName)).Value.RowIndex - this.headerPanelRowsCount));
    }
}

