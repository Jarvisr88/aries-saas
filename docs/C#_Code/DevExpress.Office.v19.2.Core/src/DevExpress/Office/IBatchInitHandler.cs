namespace DevExpress.Office
{
    using System;

    public interface IBatchInitHandler
    {
        void OnBeginInit();
        void OnCancelInit();
        void OnEndInit();
        void OnFirstBeginInit();
        void OnLastCancelInit();
        void OnLastEndInit();
    }
}

