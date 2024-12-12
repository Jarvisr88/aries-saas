namespace DevExpress.Office
{
    using System;

    public interface IBatchInit
    {
        void BeginInit();
        void CancelInit();
        void EndInit();
    }
}

