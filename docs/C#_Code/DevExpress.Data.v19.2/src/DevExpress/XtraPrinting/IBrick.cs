namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;

    public interface IBrick
    {
        Hashtable GetProperties();
        void SetProperties(Hashtable properties);
        void SetProperties(object[,] properties);
    }
}

