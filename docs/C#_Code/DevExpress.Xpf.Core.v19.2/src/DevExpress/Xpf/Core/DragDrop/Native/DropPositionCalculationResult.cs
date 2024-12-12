namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DropPositionCalculationResult
    {
        public IEnumerable<DropPosition> Positions { get; set; }

        public double Ratio { get; set; }
    }
}

