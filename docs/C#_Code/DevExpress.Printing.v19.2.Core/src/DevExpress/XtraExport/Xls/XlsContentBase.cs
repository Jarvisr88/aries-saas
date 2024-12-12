namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public abstract class XlsContentBase : IXlsContent
    {
        private const string invalidFileMessage = "Invalid Xls file";

        protected XlsContentBase()
        {
        }

        protected void CheckLength(string value, int maxLength, string name)
        {
            if (!string.IsNullOrEmpty(value) && (value.Length > maxLength))
            {
                throw new ArgumentException($"{name}: number of characters in this string MUST be less than or equal to {maxLength}");
            }
        }

        protected void CheckValue(int value, int minValue, int maxValue)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException($"Value out of range {minValue}...{maxValue}");
            }
        }

        protected void CheckValue(double value, double minValue, double maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }

        protected void CheckValue(int value, int minValue, int maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }

        protected void CheckValue(long value, long minValue, long maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }

        public abstract int GetSize();
        public abstract void Read(XlReader reader, int size);
        protected void ThrowInvalidXlsFile()
        {
            throw new Exception("Invalid Xls file");
        }

        protected void ThrowInvalidXlsFile(string reason)
        {
            throw new Exception($"{"Invalid Xls file"}: {reason}");
        }

        protected int ValueInRange(int value, int minValue, int maxValue)
        {
            if (value < minValue)
            {
                value = minValue;
            }
            if (value > maxValue)
            {
                value = maxValue;
            }
            return value;
        }

        public abstract void Write(BinaryWriter writer);

        public virtual FutureRecordHeaderBase RecordHeader =>
            null;
    }
}

