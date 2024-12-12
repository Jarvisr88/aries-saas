namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class HorizontalAlignmentToTextAlignmentExtensions
    {
        public static TextAlignment ToTextAlignment(this HorizontalAlignment ha)
        {
            switch (ha)
            {
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;

                case HorizontalAlignment.Right:
                    return TextAlignment.Right;

                case HorizontalAlignment.Stretch:
                    return TextAlignment.Justify;
            }
            return TextAlignment.Left;
        }
    }
}

