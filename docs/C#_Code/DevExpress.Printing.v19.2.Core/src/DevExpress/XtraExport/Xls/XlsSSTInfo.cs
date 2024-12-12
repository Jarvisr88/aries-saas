namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;

    public class XlsSSTInfo
    {
        private int streamPosition;
        private int offset;

        public int StreamPosition
        {
            get => 
                this.streamPosition;
            set
            {
                Guard.ArgumentNonNegative(value, "StreamPosition value");
                this.streamPosition = value;
            }
        }

        public int Offset
        {
            get => 
                this.offset;
            set
            {
                if ((value < 0) || (value > 0xffff))
                {
                    throw new ArgumentOutOfRangeException("Offset value");
                }
                this.offset = value;
            }
        }
    }
}

