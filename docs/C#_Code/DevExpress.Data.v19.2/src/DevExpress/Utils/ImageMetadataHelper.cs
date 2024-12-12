namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Reflection;
    using System.Text;

    public static class ImageMetadataHelper
    {
        private static PropertyItem CreatePropertyItem() => 
            (PropertyItem) typeof(PropertyItem).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null).Invoke(null);

        public static PropertyItem CreatePropertyItem(int id, string data)
        {
            PropertyItem item = CreatePropertyItem();
            item.Id = id;
            item.Value = Encoding.ASCII.GetBytes(data + "\0");
            item.Len = item.Value.Length;
            item.Type = 2;
            return item;
        }

        public static string LoadTags(Image image) => 
            LoadTags(image, 800);

        public static string LoadTags(Image image, int id)
        {
            string str = string.Empty;
            try
            {
                PropertyItem propertyItem = image.GetPropertyItem(id);
                str = Encoding.ASCII.GetString(propertyItem.Value);
            }
            catch
            {
            }
            return str;
        }
    }
}

