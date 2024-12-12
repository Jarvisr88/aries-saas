namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ObjectToDoubleConverter
    {
        public static double TryConvert(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }

        public static double TryConvertToDouble(this object value) => 
            TryConvert(value);
    }
}

