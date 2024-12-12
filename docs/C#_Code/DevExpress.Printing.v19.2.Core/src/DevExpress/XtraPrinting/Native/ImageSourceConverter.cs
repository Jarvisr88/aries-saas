namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Drawing;
    using System;

    public class ImageSourceConverter : StructStringConverter, ICustomObjectConverter
    {
        public static readonly ImageSourceConverter Instance = new ImageSourceConverter();

        protected override object CreateObject(string[] values) => 
            (values.Length == 2) ? new ImageSource(values[0], values[1]) : null;

        bool ICustomObjectConverter.CanConvert(System.Type type) => 
            type == this.Type;

        object ICustomObjectConverter.FromString(System.Type type, string str) => 
            base.FromString(str);

        System.Type ICustomObjectConverter.GetType(string typeName) => 
            this.Type;

        string ICustomObjectConverter.ToString(System.Type type, object obj) => 
            base.ToString(obj);

        protected override string[] GetValues(object obj) => 
            ((ImageSource) obj).GetMetadata();

        public override System.Type Type =>
            typeof(ImageSource);
    }
}

