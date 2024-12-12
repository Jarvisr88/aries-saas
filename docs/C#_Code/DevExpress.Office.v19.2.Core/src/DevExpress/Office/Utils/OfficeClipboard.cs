namespace DevExpress.Office.Utils
{
    using System;
    using System.Windows.Forms;

    public static class OfficeClipboard
    {
        public static void Clear()
        {
            try
            {
                Clipboard.Clear();
            }
            catch
            {
            }
        }

        public static bool ContainsData(string format) => 
            ContainsData(format, false);

        public static bool ContainsData(string format, bool autoConvert)
        {
            try
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                return ((dataObject != null) && dataObject.GetDataPresent(format, autoConvert));
            }
            catch
            {
                return false;
            }
        }

        public static IDataObject GetDataObject()
        {
            try
            {
                return Clipboard.GetDataObject();
            }
            catch
            {
                return null;
            }
        }

        public static void SetDataObject(object data, bool copy)
        {
            try
            {
                Clipboard.SetDataObject(data, copy, 10, 100);
            }
            catch
            {
            }
        }
    }
}

