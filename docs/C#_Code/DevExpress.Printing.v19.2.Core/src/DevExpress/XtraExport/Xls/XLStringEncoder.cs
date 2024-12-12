namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public static class XLStringEncoder
    {
        private const int singleByteCodePage = 0x4e4;

        public static byte[] GetBytes(string value, bool highBytes) => 
            GetEncoding(highBytes).GetBytes(value);

        public static Encoding GetEncoding(bool highBytes) => 
            highBytes ? Encoding.Unicode : DXEncoding.GetEncoding(0x4e4);

        public static Encoding GetSingleByteEncoding() => 
            DXEncoding.GetEncoding(0x4e4);

        public static bool StringHasHighBytes(string value)
        {
            bool flag;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            try
            {
                int num = 0;
                while (true)
                {
                    if (num >= value.Length)
                    {
                        flag = false;
                    }
                    else
                    {
                        uint num2 = value[num];
                        if (num2 <= 0xff)
                        {
                            num++;
                            continue;
                        }
                        flag = true;
                    }
                    break;
                }
            }
            catch
            {
                return true;
            }
            return flag;
        }
    }
}

