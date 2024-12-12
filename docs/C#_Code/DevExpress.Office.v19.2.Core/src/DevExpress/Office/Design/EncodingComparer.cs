namespace DevExpress.Office.Design
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EncodingComparer : IComparer<Encoding>
    {
        public int Compare(Encoding x, Encoding y) => 
            Comparer<string>.Default.Compare(x.EncodingName, y.EncodingName);
    }
}

