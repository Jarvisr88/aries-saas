namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using System;
    using System.Collections.Generic;

    internal class LayoutCollection : List<ILayoutData>
    {
        public LayoutCollection(List<ILayoutData> list, int index);
        private bool IsImmediatePredecessor(ILayoutData pred, ILayoutData data);
        private static bool IsPredecessor(ILayoutData pred, ILayoutData data);
    }
}

