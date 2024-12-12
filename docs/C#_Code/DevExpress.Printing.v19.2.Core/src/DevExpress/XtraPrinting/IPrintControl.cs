namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System.ComponentModel;

    public interface IPrintControl
    {
        DevExpress.XtraPrinting.ProgressReflector ProgressReflector { get; }

        PrintingSystemBase PrintingSystem { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        CommandSetBase CommandSet { get; }
    }
}

