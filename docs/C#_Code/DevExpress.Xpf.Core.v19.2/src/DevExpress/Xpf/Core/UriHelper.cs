namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;

    public static class UriHelper
    {
        public static Uri GetUri(string dllName, string relativeFilePath, string dllNameSuffix = null) => 
            new Uri($"/{dllName}{".v19.2"}{dllNameSuffix};component/{relativeFilePath}", UriKind.RelativeOrAbsolute);
    }
}

