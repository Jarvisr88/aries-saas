namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Collections;

    public class MetafileObjectTable : Hashtable
    {
        public void Add(object obj)
        {
            int key = 0;
            while (this.ContainsKey(key))
            {
                key++;
            }
            this.Add(key, obj);
        }
    }
}

