namespace DevExpress.Xpf.Core
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class EmbeddedResourceImage : MarkupExtension
    {
        private object value;

        public EmbeddedResourceImage()
        {
        }

        public EmbeddedResourceImage(object source)
        {
            this.Source = source;
        }

        public static object ConvertSource(object source)
        {
            using (Stream stream = GetResourceStream(source))
            {
                return ImageDataConverter.CreateImageFromStream(stream);
            }
        }

        private static Stream GetResourceStream(object value) => 
            Assembly.GetExecutingAssembly().GetManifestResourceStream(Convert.ToString(value));

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj3 = this.value;
            if (this.value == null)
            {
                object local1 = this.value;
                obj3 = this.value = ConvertSource(this.Source);
            }
            return obj3;
        }

        public object Source { get; set; }
    }
}

