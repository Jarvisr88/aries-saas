namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Runtime.InteropServices;

    public class XlsRkNumber
    {
        private const long valueMask = 0xfffffffcL;
        private const long low34BitsMask = 0x3ffffffffL;
        private const int int30MinValue = -536870912;
        private const int int30MaxValue = 0x1fffffff;
        private int rkType;
        private double value;

        public XlsRkNumber(int rawValue)
        {
            this.rkType = rawValue & 3;
            DoubleInt64Union union = new DoubleInt64Union();
            if (!this.IsInt)
            {
                union.Int64Value = (rawValue & ((ulong) (-4))) << 0x20;
            }
            else
            {
                union.DoubleValue = (rawValue & ((ulong) (-4))) >> 2;
            }
            this.value = this.X100 ? (union.DoubleValue / 100.0) : union.DoubleValue;
        }

        private static bool CanBePresentedAsInt30(double value)
        {
            double num = Truncate(value);
            return ((num == value) && ((num >= -536870912.0) && (num <= 536870911.0)));
        }

        public int GetRawValue()
        {
            DoubleInt64Union union = new DoubleInt64Union {
                DoubleValue = !this.X100 ? this.Value : (this.Value * 100.0)
            };
            int num = this.IsInt ? (((int) union.DoubleValue) << 2) : ((int) ((union.Int64Value >> 0x20) & ((ulong) (-4))));
            return (num | this.rkType);
        }

        private static int GetRkType(double value)
        {
            DoubleInt64Union union = new DoubleInt64Union {
                DoubleValue = value
            };
            if ((union.Int64Value & 0x3ffffffffL) == 0)
            {
                return 0;
            }
            if (CanBePresentedAsInt30(value))
            {
                return 2;
            }
            try
            {
                int num;
                union.DoubleValue = value * 100.0;
                if ((union.Int64Value & 0x3ffffffffL) != 0)
                {
                    if (!CanBePresentedAsInt30(value * 100.0))
                    {
                        goto TR_0002;
                    }
                    else
                    {
                        num = 3;
                    }
                }
                else
                {
                    num = 1;
                }
                return num;
            }
            catch (OverflowException)
            {
            }
        TR_0002:
            return -1;
        }

        public static bool IsRkValue(double value) => 
            GetRkType(value) != -1;

        private static double Truncate(double value) => 
            Math.Truncate(value);

        public double Value
        {
            get => 
                this.value;
            set
            {
                if (!IsRkValue(value))
                {
                    throw new ArgumentException("value can't be represented as RkNumber");
                }
                this.value = value;
                this.rkType = GetRkType(value);
            }
        }

        public bool X100 =>
            (this.rkType & 1) != 0;

        public bool IsInt =>
            (this.rkType & 2) != 0;

        [StructLayout(LayoutKind.Explicit)]
        private struct DoubleInt64Union
        {
            [FieldOffset(0)]
            public double DoubleValue;
            [FieldOffset(0)]
            public long Int64Value;
        }
    }
}

