namespace DevExpress.Mvvm
{
    using System;

    public interface ISupportSplashScreen
    {
        bool IsIndeterminate { get; set; }

        double Progress { get; set; }

        double MaxProgress { get; set; }

        object State { get; set; }
    }
}

