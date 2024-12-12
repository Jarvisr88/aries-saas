namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public static class ZipDateTimeHelper
    {
        public static DateTime FromMsDos(int data)
        {
            uint num = (uint) data;
            return new DateTime(((int) (num >> 0x19)) + 0x7bc, (int) ((num >> 0x15) & 15), (int) ((num >> 0x10) & 0x1f), (int) ((num >> 11) & 0x1f), (int) ((num >> 5) & 0x3f), (int) ((num & 0x1f) * 2));
        }

        public static int ToMsDosDateTime(DateTime dateTime) => 
            (((((0 | ((dateTime.Second / 2) & 0x1f)) | ((dateTime.Minute & 0x3f) << 5)) | ((dateTime.Hour & 0x1f) << 11)) | ((dateTime.Day & 0x1f) << 0x10)) | ((dateTime.Month & 15) << 0x15)) | (((dateTime.Year - 0x7bc) & 0x7f) << 0x19);
    }
}

