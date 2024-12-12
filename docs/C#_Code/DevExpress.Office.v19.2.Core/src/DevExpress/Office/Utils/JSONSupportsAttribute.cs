namespace DevExpress.Office.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public sealed class JSONSupportsAttribute : Attribute
    {
        private readonly string key;
        private readonly string toJSON;
        private readonly string fromJSON;

        public JSONSupportsAttribute(string key, string toJSON, string fromJSON)
        {
            this.key = key;
            this.toJSON = toJSON;
            this.fromJSON = fromJSON;
        }

        public string Key =>
            this.key;

        public string ToJSON =>
            this.toJSON;

        public string FromJSON =>
            this.fromJSON;
    }
}

