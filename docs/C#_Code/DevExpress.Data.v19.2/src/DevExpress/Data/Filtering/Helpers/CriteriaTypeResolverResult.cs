namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CriteriaTypeResolverResult
    {
        private readonly System.Type type;
        private readonly object tag;
        public System.Type Type { get; }
        public object Tag { get; }
        public CriteriaTypeResolverResult(System.Type type);
        public CriteriaTypeResolverResult(System.Type type, object tag);
    }
}

