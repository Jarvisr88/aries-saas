namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    internal static class StreamingSerializationExt
    {
        public static bool IsCachedProperty(this SerializablePropertyDescriptorPair pair) => 
            (pair != null) ? ((pair.Attribute.Flags & XtraSerializationFlags.Cached) == XtraSerializationFlags.Cached) : false;

        public static bool ShouldSerializeAsCollection(this SerializablePropertyDescriptorPair pair) => 
            (pair != null) && ((pair.Attribute != null) && pair.Attribute.SerializeCollection);

        public static bool ShouldSerializeAsContent(this SerializablePropertyDescriptorPair pair) => 
            (pair != null) && ((pair.Attribute != null) && (pair.Attribute.Visibility == XtraSerializationVisibility.Content));
    }
}

