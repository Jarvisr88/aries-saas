namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    public class BandedRowInfo : List<BandNodeDescriptor>
    {
        public BandNodeDescriptor GetDescriptorByCellPosition(int cellPosition)
        {
            int num = 0;
            int num2 = 0;
            while (num < base.Count)
            {
                if (base[num2].ColIndex == cellPosition)
                {
                    return base[num];
                }
                if ((num2 % base.Count) == num2)
                {
                    num2++;
                }
                num++;
            }
            return new BandNodeDescriptor();
        }
    }
}

