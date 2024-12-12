namespace DevExpress.Office.Internal
{
    using System;

    public interface IGestureStateIndicator
    {
        void OnGestureBegin();
        void OnGestureEnd();

        bool GestureActivated { get; }
    }
}

