namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public static class LayoutItemInsertionKindExtensions
    {
        public static Side? GetSide(this LayoutItemInsertionKind kind)
        {
            switch (kind)
            {
                case LayoutItemInsertionKind.Left:
                    return 0;

                case LayoutItemInsertionKind.Top:
                    return 1;

                case LayoutItemInsertionKind.Right:
                    return 2;

                case LayoutItemInsertionKind.Bottom:
                    return 4;
            }
            return null;
        }
    }
}

