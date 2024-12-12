namespace DevExpress.Utils
{
    using System;

    public interface IBatchUpdateHandler
    {
        void OnBeginUpdate();
        void OnCancelUpdate();
        void OnEndUpdate();
        void OnFirstBeginUpdate();
        void OnLastCancelUpdate();
        void OnLastEndUpdate();
    }
}

