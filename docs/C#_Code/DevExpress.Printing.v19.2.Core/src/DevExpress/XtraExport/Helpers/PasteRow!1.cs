namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PasteRow<TCol> where TCol: class, IColumn
    {
        public PasteRow(IEnumerable<PasteCellValue<TCol>> cells)
        {
            this.Cells = new List<PasteCellValue<TCol>>(cells);
        }

        public List<PasteCellValue<TCol>> Cells { get; private set; }
    }
}

