namespace DevExpress.Data.Svg
{
    using System;

    public class SvgAttribute
    {
        private readonly string name;
        private readonly object value;
        private readonly System.Type type;

        public SvgAttribute(string name, object value);
        public SvgAttribute(string name, object value, System.Type type);

        public string Name { get; }

        public object Value { get; }

        public System.Type Type { get; }
    }
}

