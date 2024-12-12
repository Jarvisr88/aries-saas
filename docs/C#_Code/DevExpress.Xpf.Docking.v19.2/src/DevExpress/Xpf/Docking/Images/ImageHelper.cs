namespace DevExpress.Xpf.Docking.Images
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ImageHelper
    {
        private static Dictionary<string, BitmapImage> images;

        public static BitmapImage GetImage(string imageName)
        {
            BitmapImage image;
            if (!Images.TryGetValue(imageName, out image))
            {
                image = LoadImage(imageName);
                Images.Add(imageName, image);
            }
            return image;
        }

        public static ImageSource GetImageForItem(BaseLayoutItem item)
        {
            if (item == null)
            {
                return null;
            }
            ImageSource image = null;
            LayoutItemType itemType = item.ItemType;
            switch (itemType)
            {
                case LayoutItemType.Panel:
                    image = GetImage("LayoutPanel");
                    break;

                case LayoutItemType.Group:
                    image = GetImage("LayoutGroup");
                    break;

                case LayoutItemType.TabPanelGroup:
                    image = GetImage("TabbedGroup");
                    break;

                case LayoutItemType.FloatGroup:
                    image = GetImage("FloatGroup");
                    break;

                case LayoutItemType.Document:
                    image = GetImage("DocumentPanel");
                    break;

                case LayoutItemType.DocumentPanelGroup:
                    image = GetImage("DocumentGroup");
                    break;

                case LayoutItemType.AutoHideContainer:
                case LayoutItemType.AutoHideGroup:
                case LayoutItemType.AutoHidePanel:
                    break;

                case LayoutItemType.ControlItem:
                    image = GetImage("LayoutControlItem");
                    break;

                default:
                    switch (itemType)
                    {
                        case LayoutItemType.LayoutSplitter:
                            image = GetImage("Splitter");
                            break;

                        case LayoutItemType.EmptySpaceItem:
                            image = GetImage("EmptySpaceItem");
                            break;

                        case LayoutItemType.Separator:
                            image = GetImage("Separator");
                            break;

                        case LayoutItemType.Label:
                            image = GetImage("Label");
                            break;

                        default:
                            break;
                    }
                    break;
            }
            return image;
        }

        private static BitmapImage LoadImage(string imageName)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"DevExpress.Xpf.Docking.Images.{imageName}.png"))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        private static Dictionary<string, BitmapImage> Images
        {
            get
            {
                images ??= new Dictionary<string, BitmapImage>();
                return images;
            }
        }
    }
}

