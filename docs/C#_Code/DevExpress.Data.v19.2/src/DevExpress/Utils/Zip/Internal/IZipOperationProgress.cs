namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IZipOperationProgress
    {
        event EventHandler NotifyProgress;

        void Stop();

        double CurrentProgress { get; }

        double Weight { get; }

        bool IsStopped { get; }
    }
}

