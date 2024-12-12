namespace DevExpress.Data.Utils.ServiceModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServiceClientFactory
    {
        public const string VersionHeader = "DXClientVersion";
        public const string VersionHeaderNS = "http://tempuri.org/";
        public const int DefaultBindingBufferSizeLimit = 0x400000;

        static ServiceClientFactory();

        public static bool AttachVersionHeader { get; set; }
    }
}

