namespace DevExpress.Utils
{
    using System;

    public static class DXConvert
    {
        public static bool IsDBNull(object value) => 
            Convert.IsDBNull(value);
    }
}

