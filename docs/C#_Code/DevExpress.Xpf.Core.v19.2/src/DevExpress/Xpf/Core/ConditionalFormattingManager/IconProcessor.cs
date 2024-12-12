namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Themes;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class IconProcessor
    {
        public IconProcessor(ImageSource icon)
        {
            this.Icon = icon;
        }

        public string GetPredefinedIconName()
        {
            string fileName = null;
            BitmapImage icon = this.Icon as BitmapImage;
            if (icon != null)
            {
                try
                {
                    string absolutePath = icon.UriSource.AbsolutePath;
                    if (Path.GetDirectoryName(absolutePath) == Path.GetDirectoryName(new Uri(ConditionalFormatResourceHelper.DefaultPathCore, UriKind.Absolute).AbsolutePath))
                    {
                        fileName = Path.GetFileName(absolutePath);
                    }
                }
                catch
                {
                }
            }
            return fileName;
        }

        public void SetIconProperty(IModelProperty property, IEditingContext context)
        {
            string predefinedIconName = this.GetPredefinedIconName();
            if (predefinedIconName == null)
            {
                property.SetValue(this.Icon);
            }
            else
            {
                IModelItem item = context.CreateItem(typeof(ConditionalFormattingIconSetIconExtension));
                item.Properties["IconName"].SetValue(predefinedIconName);
                property.SetValue(item);
            }
        }

        public ImageSource Icon { get; set; }
    }
}

