namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Markup;

    public static class XamlReaderHelper
    {
        public static FrameworkElement Load(Stream source) => 
            (FrameworkElement) XamlReader.Load(source);

        public static FrameworkElement Load(string source)
        {
            FrameworkElement element;
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer1 = new StreamWriter(stream);
                writer1.AutoFlush = true;
                using (StreamWriter writer = writer1)
                {
                    writer.Write(source);
                    stream.Position = 0L;
                    element = (FrameworkElement) XamlReader.Load(stream);
                }
            }
            return element;
        }
    }
}

