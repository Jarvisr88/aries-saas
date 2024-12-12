namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;

    public static class PdfElementsDictionaryWriter
    {
        public static PdfWriterDictionary Write(IDictionary source, Func<object, object> valueTransform)
        {
            if ((source == null) || (source.Count == 0))
            {
                return null;
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            foreach (string str in source.Keys)
            {
                dictionary.Add(str, valueTransform(source[str]));
            }
            return dictionary;
        }
    }
}

