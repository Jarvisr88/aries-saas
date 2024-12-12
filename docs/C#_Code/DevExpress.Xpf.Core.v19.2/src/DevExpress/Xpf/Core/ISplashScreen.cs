namespace DevExpress.Xpf.Core
{
    using System;

    public interface ISplashScreen
    {
        void CloseSplashScreen();
        void Progress(double value);
        void SetProgressState(bool isIndeterminate);
    }
}

