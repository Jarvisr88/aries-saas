namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public static class Net462Detector
    {
        private static readonly bool PropertyExists = (typeof(VisualTreeHelper).GetMethod("GetDpi") != null);

        public static bool IsNet462() => 
            PropertyExists;
    }
}

