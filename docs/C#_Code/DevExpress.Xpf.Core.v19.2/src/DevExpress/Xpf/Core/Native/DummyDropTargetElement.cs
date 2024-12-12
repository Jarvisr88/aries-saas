namespace DevExpress.Xpf.Core.Native
{
    using System.Windows;

    internal class DummyDropTargetElement : FrameworkElement
    {
        private static DummyDropTargetElement instance;

        public static DummyDropTargetElement Instance { get; }
    }
}

