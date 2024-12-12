namespace DevExpress.XtraPrinting.Native
{
    using System;

    internal class DeserializeChildPropertiesEventArgs
    {
        public readonly IStreamingXmlDeserializerContext Context;
        public bool DeserializeChildrenVirtually;

        public DeserializeChildPropertiesEventArgs(IStreamingXmlDeserializerContext context)
        {
            this.Context = context;
        }
    }
}

