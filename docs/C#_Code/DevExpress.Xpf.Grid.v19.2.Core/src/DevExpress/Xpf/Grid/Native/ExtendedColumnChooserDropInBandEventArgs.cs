namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public class ExtendedColumnChooserDropInBandEventArgs : EventArgs
    {
        public ExtendedColumnChooserDropInBandEventArgs(BandBase targetBand, BaseColumn source)
        {
            this.TargetBand = targetBand;
            this.Source = source;
        }

        public BandBase TargetBand { get; private set; }

        public BaseColumn Source { get; private set; }
    }
}

