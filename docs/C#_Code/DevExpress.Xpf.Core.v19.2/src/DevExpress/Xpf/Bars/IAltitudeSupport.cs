namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public interface IAltitudeSupport
    {
        event ValueChangedEventHandler<int> AltitudeChanged;

        int Altitude { get; }
    }
}

