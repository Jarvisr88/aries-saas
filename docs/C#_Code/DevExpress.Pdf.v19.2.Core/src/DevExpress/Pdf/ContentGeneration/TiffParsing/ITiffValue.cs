namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public interface ITiffValue
    {
        double AsDouble();
        int AsInt();
        long AsUint();
    }
}

