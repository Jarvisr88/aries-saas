namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Windows;

    public static class WatermarkLocalizers
    {
        public static string LocalizeDirectionMode(DirectionMode mode)
        {
            string str;
            switch (mode)
            {
                case DirectionMode.Horizontal:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_Horizontal);
                    break;

                case DirectionMode.ForwardDiagonal:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_ForwardDiagonal);
                    break;

                case DirectionMode.BackwardDiagonal:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_BackwardDiagonal);
                    break;

                case DirectionMode.Vertical:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_Vertical);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return str;
        }

        public static string LocalizeHorizontalAlignment(HorizontalAlignment alignment)
        {
            string str;
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Left);
                    break;

                case HorizontalAlignment.Center:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Center);
                    break;

                case HorizontalAlignment.Right:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Right);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return str;
        }

        public static string LocalizeImageViewMode(ImageViewMode mode)
        {
            string str;
            switch (mode)
            {
                case ImageViewMode.Clip:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageClip);
                    break;

                case ImageViewMode.Stretch:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageStretch);
                    break;

                case ImageViewMode.Zoom:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageZoom);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return str;
        }

        public static string LocalizeVerticalAlignment(VerticalAlignment alignment)
        {
            string str;
            switch (alignment)
            {
                case VerticalAlignment.Top:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Top);
                    break;

                case VerticalAlignment.Center:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Middle);
                    break;

                case VerticalAlignment.Bottom:
                    str = PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Bottom);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return str;
        }
    }
}

