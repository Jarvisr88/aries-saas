namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class WindowResizeHelper
    {
        public static DXWindowActiveResizeParts CorrectResizePart(FlowDirection fd, DXWindowActiveResizeParts activePart)
        {
            if (fd == FlowDirection.RightToLeft)
            {
                switch (activePart)
                {
                    case DXWindowActiveResizeParts.Left:
                        activePart = DXWindowActiveResizeParts.Right;
                        break;

                    case DXWindowActiveResizeParts.Right:
                        activePart = DXWindowActiveResizeParts.Left;
                        break;

                    case DXWindowActiveResizeParts.TopLeft:
                        activePart = DXWindowActiveResizeParts.TopRight;
                        break;

                    case DXWindowActiveResizeParts.TopRight:
                        activePart = DXWindowActiveResizeParts.TopLeft;
                        break;

                    case DXWindowActiveResizeParts.BottomLeft:
                        activePart = DXWindowActiveResizeParts.BottomRight;
                        break;

                    case DXWindowActiveResizeParts.BottomRight:
                        activePart = DXWindowActiveResizeParts.BottomLeft;
                        break;

                    default:
                        break;
                }
            }
            return activePart;
        }

        public static void Subscribe(IWindowResizeHelperClient client)
        {
            foreach (string str in Enum.GetNames(typeof(DXWindowActiveResizeParts)))
            {
                string str2 = "Part_";
                if (str == "SizeGrip")
                {
                    str2 = "PART_";
                }
                FrameworkElement visualByName = client.GetVisualByName(str2 + str);
                if (visualByName != null)
                {
                    visualByName.PreviewMouseDown += new MouseButtonEventHandler(client.ActivePartMouseDown);
                }
            }
        }
    }
}

