namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlFormatting
    {
        protected XlFormatting()
        {
        }

        public static T CopyObject<T>(T other) where T: class, ISupportsCopyFrom<T>, new()
        {
            if (other == null)
            {
                return default(T);
            }
            T local = Activator.CreateInstance<T>();
            local.CopyFrom(other);
            return local;
        }

        internal bool IsDateTimeNumberFormat() => 
            string.IsNullOrEmpty(this.NetFormatString) ? ((this.NumberFormat != null) && this.NumberFormat.IsDateTime) : this.IsDateTimeFormatString;

        public XlFont Font { get; set; }

        public XlFill Fill { get; set; }

        public XlCellAlignment Alignment { get; set; }

        public string NetFormatString { get; set; }

        public bool IsDateTimeFormatString { get; set; }

        public XlNumberFormat NumberFormat { get; set; }

        public XlBorder Border { get; set; }
    }
}

