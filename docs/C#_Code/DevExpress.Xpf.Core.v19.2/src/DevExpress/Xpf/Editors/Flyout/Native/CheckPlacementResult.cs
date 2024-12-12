namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Xpf.Editors.Flyout;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CheckPlacementResult
    {
        public FlyoutPlacement Placement { get; set; }

        public bool IsMatch { get; set; }

        public double Distance { get; set; }

        public Point CorrectedLocation { get; set; }

        public Point BaseLocation { get; set; }

        public System.Windows.Size Size { get; set; }

        public Rect TargetBounds { get; set; }

        public Rect BaseRect { get; set; }
    }
}

