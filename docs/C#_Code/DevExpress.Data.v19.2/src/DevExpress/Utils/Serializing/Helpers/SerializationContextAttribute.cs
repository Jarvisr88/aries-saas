namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class SerializationContextAttribute : Attribute
    {
        private Type serializationContextType;

        public SerializationContextAttribute(Type serializationContextType)
        {
            this.serializationContextType = serializationContextType;
        }

        public SerializationContext CreateSerializationContext() => 
            (SerializationContext) Activator.CreateInstance(this.serializationContextType);
    }
}

