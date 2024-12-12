namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System.Windows.Threading;

    public class DummyDataController : DXGridDataController
    {
        protected override System.Windows.Threading.Dispatcher Dispatcher { get; }
    }
}

