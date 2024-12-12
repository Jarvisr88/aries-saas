namespace DevExpress.XtraPrinting.Preview
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public interface ICancellationService
    {
        event EventHandler StateChanged;

        bool IsResetting { get; }

        CancellationTokenSource TokenSource { get; }
    }
}

