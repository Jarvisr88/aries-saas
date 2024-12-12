namespace DevExpress.Office.Internal
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public static class OfficeFontSizeEditHelper
    {
        public static bool IsValidFontSize(int fontSize) => 
            (fontSize >= 1) && (fontSize <= 0x5dc);

        public static bool TryGetHalfSizeValue(object editValue, out int value)
        {
            value = 0;
            if (editValue is int)
            {
                value = ((int) editValue) * 2;
                return true;
            }
            float result = 0f;
            string str = string.Empty;
            if (editValue is float)
            {
                result = (float) editValue;
            }
            else
            {
                str = editValue as string;
            }
            if ((string.IsNullOrEmpty(str) || !float.TryParse(str, NumberStyles.Float, CultureInfo.CurrentCulture, out result)) && (result <= 0f))
            {
                return false;
            }
            if (((result * 10f) % 5f) != 0f)
            {
                return false;
            }
            value = (int) (result * 2f);
            return true;
        }

        public static bool TryGetValue(object editValue, out int value)
        {
            value = 0;
            if (editValue is int)
            {
                value = (int) editValue;
                return true;
            }
            string str = editValue as string;
            return (!string.IsNullOrEmpty(str) && int.TryParse(str, out value));
        }
    }
}

