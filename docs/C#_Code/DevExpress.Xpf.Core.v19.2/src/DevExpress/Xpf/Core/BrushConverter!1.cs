namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.IO;
    using System.Windows.Markup;

    internal class BrushConverter<T> : IOneTypeObjectConverter2, IOneTypeObjectConverter where T: Brush
    {
        private const string BrushValue = "~Xtra#Brush";
        public static readonly IOneTypeObjectConverter2 Instance;

        static BrushConverter()
        {
            BrushConverter<T>.Instance = new BrushConverter<T>();
        }

        private object DeserializeFromString(string str)
        {
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(str.Substring("~Xtra#Brush".Length))))
            {
                return XamlReader.Load(stream);
            }
        }

        object IOneTypeObjectConverter.FromString(string str) => 
            this.DeserializeFromString(str);

        string IOneTypeObjectConverter.ToString(object obj) => 
            this.SerializeToString(obj);

        bool IOneTypeObjectConverter2.CanConvertFromString(string str) => 
            str.StartsWith("~Xtra#Brush");

        private string SerializeToString(object value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XamlWriter.Save(value, stream);
                return ("~Xtra#Brush" + Convert.ToBase64String(stream.ToArray()));
            }
        }

        Type IOneTypeObjectConverter.Type =>
            typeof(T);
    }
}

