namespace DevExpress.XtraPrinting.Helpers
{
    using DevExpress.Utils;
    using System;

    public static class ReportSpecificEnumHelper
    {
        public static short GetEnumMaxValue(Type enumType)
        {
            uint num = 0;
            foreach (object obj2 in enumType.GetValues())
            {
                num |= Convert.ToUInt32(obj2);
            }
            return (short) num;
        }
    }
}

