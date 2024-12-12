namespace DevExpress.XtraPrinting
{
    using System;

    public interface IDelayer
    {
        void Abort();
        void Execute(Action action);
    }
}

