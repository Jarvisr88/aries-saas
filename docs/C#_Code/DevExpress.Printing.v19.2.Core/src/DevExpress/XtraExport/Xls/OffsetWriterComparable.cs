namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OffsetWriterComparable : IComparable<IOffsetPositionWriter>
    {
        private readonly long thingsCounter;

        public OffsetWriterComparable(long thingsCounter)
        {
            this.thingsCounter = thingsCounter;
        }

        public int CompareTo(IOffsetPositionWriter other) => 
            other.OffsetPositionThingsCounter - ((int) this.thingsCounter);
    }
}

