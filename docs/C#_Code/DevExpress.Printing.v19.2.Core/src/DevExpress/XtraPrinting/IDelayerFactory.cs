namespace DevExpress.XtraPrinting
{
    using System;

    public interface IDelayerFactory
    {
        IDelayer Create(TimeSpan interval);
    }
}

