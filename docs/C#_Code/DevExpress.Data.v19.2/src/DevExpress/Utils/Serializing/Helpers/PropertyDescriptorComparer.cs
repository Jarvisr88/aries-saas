namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections.Generic;

    public class PropertyDescriptorComparer : IComparer<SerializablePropertyDescriptorPair>
    {
        private readonly SerializationContext serializationContext;
        private readonly object obj;

        public PropertyDescriptorComparer(SerializationContext serializationContext, object obj)
        {
            this.serializationContext = serializationContext;
            this.obj = obj;
        }

        protected virtual int CompareProperties(SerializablePropertyDescriptorPair x, SerializablePropertyDescriptorPair y) => 
            DefaultPropertyDescriptorComparer.Instance.Compare(x, y);

        int IComparer<SerializablePropertyDescriptorPair>.Compare(SerializablePropertyDescriptorPair x, SerializablePropertyDescriptorPair y) => 
            this.CompareProperties(x, y);
    }
}

