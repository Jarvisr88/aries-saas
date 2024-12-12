namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ObjectToDateTimeConverter
    {
        public static DateTime TryConvert(object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        public static DateTime TryConvertToDateTime(this object value) => 
            TryConvert(value);
    }
}

