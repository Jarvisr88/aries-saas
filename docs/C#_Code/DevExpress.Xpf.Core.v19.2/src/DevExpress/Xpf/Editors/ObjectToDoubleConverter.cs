namespace DevExpress.Xpf.Editors
{
    using System;

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
    }
}

