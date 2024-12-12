namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.InteropServices;

    internal interface IMetadataStorage
    {
        void SetAttributes(string path, AnnotationAttributes attributes);
        void SetAttributes(string path, FilterAttributes attributes);
        void SetEnabled(string path, bool enabled);
        void SetOrder(string path, int? order);
        bool TryGetValue(string path, out AnnotationAttributes attributes);
        bool TryGetValue(string path, out FilterAttributes attributes);
        bool TryGetValue(string path, out int order);
    }
}

