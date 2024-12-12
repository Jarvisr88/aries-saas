namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    internal class OneTypeCustomObjectConverter : IOneTypeObjectConverter
    {
        private System.Type type;
        private ICustomObjectConverter customObjectConverter;

        public OneTypeCustomObjectConverter(System.Type type, ICustomObjectConverter customObjectConverter)
        {
            this.type = type;
            this.customObjectConverter = customObjectConverter;
        }

        public object FromString(string str) => 
            this.customObjectConverter.FromString(this.Type, str);

        public string ToString(object obj) => 
            this.customObjectConverter.ToString(this.Type, obj);

        public System.Type Type =>
            this.type;
    }
}

