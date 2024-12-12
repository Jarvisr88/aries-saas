namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ObjectToDecimalConverter
    {
        public static decimal TryConvert(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0M;
            }
        }

        public static decimal TryConvertToDecimal(this object value) => 
            TryConvert(value);
    }
}

