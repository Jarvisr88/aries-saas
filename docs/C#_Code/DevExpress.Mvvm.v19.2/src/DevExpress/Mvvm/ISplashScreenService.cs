namespace DevExpress.Mvvm
{
    using System;

    public interface ISplashScreenService
    {
        void HideSplashScreen();
        void SetSplashScreenProgress(double progress, double maxProgress);
        void SetSplashScreenState(object state);
        void ShowSplashScreen(string documentType);

        bool IsSplashScreenActive { get; }
    }
}

