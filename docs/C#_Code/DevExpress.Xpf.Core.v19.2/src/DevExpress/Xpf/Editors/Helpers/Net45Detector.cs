namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public static class Net45Detector
    {
        private static readonly bool PropertyExists = (typeof(VirtualizingPanel).GetProperty("CanHierarchicallyScrollAndVirtualize") != null);

        public static bool IsNet45() => 
            PropertyExists;
    }
}

