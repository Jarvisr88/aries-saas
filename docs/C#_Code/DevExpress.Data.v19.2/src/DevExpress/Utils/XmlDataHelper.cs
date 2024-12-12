namespace DevExpress.Utils
{
    using System;
    using System.Data;
    using System.IO;

    public static class XmlDataHelper
    {
        public static DataSet CreateDataSetBySchema(string xmlSchema)
        {
            if (!string.IsNullOrEmpty(xmlSchema))
            {
                try
                {
                    DataSet set = new DataSet();
                    TextReader reader = new StringReader(xmlSchema);
                    try
                    {
                        set.ReadXmlSchema(reader);
                    }
                    finally
                    {
                        reader.Close();
                    }
                    return set;
                }
                catch
                {
                }
            }
            return null;
        }

        public static DataSet CreateDataSetByXmlUrl(string xmlUrl, bool schemaOnly)
        {
            if (string.IsNullOrEmpty(xmlUrl))
            {
                return null;
            }
            DataSet set = new DataSet();
            if (schemaOnly)
            {
                set.ReadXmlSchema(xmlUrl);
            }
            else
            {
                set.ReadXml(xmlUrl);
            }
            return set;
        }

        public static string GetXmlSchema(DataSet ds)
        {
            string str;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    ds.WriteXmlSchema(stream);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        stream.Seek(0L, SeekOrigin.Begin);
                        str = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                str = string.Empty;
            }
            return str;
        }
    }
}

