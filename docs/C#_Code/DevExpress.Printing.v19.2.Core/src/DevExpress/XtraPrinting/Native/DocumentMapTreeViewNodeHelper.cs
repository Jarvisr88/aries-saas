namespace DevExpress.XtraPrinting.Native
{
    using System;

    internal class DocumentMapTreeViewNodeHelper
    {
        public static string GetBrickIndicesByTag(string tag);
        public static string GetPageIndexByTag(string tag);
        public static string GetTagByIndices(int[] brickIndices, int pageIndex);
        public static string GetTagByIndices(string indices, int pageIndex);
    }
}

