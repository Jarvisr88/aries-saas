namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfXObjects : PdfObjectCollection<PdfXObject>
    {
        public void CloseAndClear()
        {
            foreach (PdfXObject obj2 in base.List)
            {
                obj2.Close();
            }
            base.Clear();
        }
    }
}

