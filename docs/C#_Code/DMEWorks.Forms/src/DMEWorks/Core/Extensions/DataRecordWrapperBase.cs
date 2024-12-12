namespace DMEWorks.Core.Extensions
{
    using System;
    using System.Data;

    public abstract class DataRecordWrapperBase
    {
        protected readonly IDataRecord m_record;

        public DataRecordWrapperBase(IDataRecord record)
        {
            if (record == null)
            {
                IDataRecord local1 = record;
                throw new ArgumentNullException("record");
            }
            this.m_record = record;
        }

        public bool? GetBoolean(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new bool?(this.m_record.GetBoolean(ordinal));
            }
            return null;
        }

        public byte? GetByte(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new byte?(this.m_record.GetByte(ordinal));
            }
            return null;
        }

        public DateTime? GetDateTime(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new DateTime?(this.m_record.GetDateTime(ordinal));
            }
            return null;
        }

        public decimal? GetDecimal(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new decimal?(this.m_record.GetDecimal(ordinal));
            }
            return null;
        }

        public double? GetDouble(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new double?(this.m_record.GetDouble(ordinal));
            }
            return null;
        }

        public float? GetFloat(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new float?(this.m_record.GetFloat(ordinal));
            }
            return null;
        }

        public short? GetInt16(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new short?(this.m_record.GetInt16(ordinal));
            }
            return null;
        }

        public int? GetInt32(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new int?(this.m_record.GetInt32(ordinal));
            }
            return null;
        }

        public long? GetInt64(int ordinal)
        {
            if (!this.m_record.IsDBNull(ordinal))
            {
                return new long?(this.m_record.GetInt64(ordinal));
            }
            return null;
        }

        public string GetString(int ordinal) => 
            !this.m_record.IsDBNull(ordinal) ? this.m_record.GetString(ordinal) : null;

        public object GetValue(int ordinal) => 
            this.m_record.GetValue(ordinal);
    }
}

