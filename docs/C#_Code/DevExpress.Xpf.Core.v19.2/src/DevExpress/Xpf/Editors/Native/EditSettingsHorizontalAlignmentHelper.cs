namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Windows;

    public class EditSettingsHorizontalAlignmentHelper
    {
        public static EditSettingsHorizontalAlignment GetEditSettingsHorizontalAlignment(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    return EditSettingsHorizontalAlignment.Left;

                case HorizontalAlignment.Center:
                    return EditSettingsHorizontalAlignment.Center;

                case HorizontalAlignment.Right:
                    return EditSettingsHorizontalAlignment.Right;
            }
            return EditSettingsHorizontalAlignment.Stretch;
        }

        public static HorizontalAlignment GetHorizontalAlignment(EditSettingsHorizontalAlignment alignment, HorizontalAlignment defaultValue)
        {
            switch (alignment)
            {
                case EditSettingsHorizontalAlignment.Left:
                    return HorizontalAlignment.Left;

                case EditSettingsHorizontalAlignment.Center:
                    return HorizontalAlignment.Center;

                case EditSettingsHorizontalAlignment.Right:
                    return HorizontalAlignment.Right;

                case EditSettingsHorizontalAlignment.Stretch:
                    return HorizontalAlignment.Stretch;
            }
            return defaultValue;
        }
    }
}

