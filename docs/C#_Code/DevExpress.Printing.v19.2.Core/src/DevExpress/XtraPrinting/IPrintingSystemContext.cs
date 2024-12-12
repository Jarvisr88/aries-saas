namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface IPrintingSystemContext : IServiceProvider
    {
        bool CanPublish(Brick brick);

        Page DrawingPage { get; }

        PrintingSystemBase PrintingSystem { get; }

        DevExpress.XtraPrinting.Native.Measurer Measurer { get; }
    }
}

