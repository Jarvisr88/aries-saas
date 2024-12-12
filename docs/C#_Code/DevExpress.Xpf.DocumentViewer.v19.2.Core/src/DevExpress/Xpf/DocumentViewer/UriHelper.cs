namespace DevExpress.Xpf.DocumentViewer
{
    using System;

    public static class UriHelper
    {
        public static Uri GetAbsoluteUri(string dllName, string relativeFilePath) => 
            new Uri($"pack://application:,,,/{dllName};component/{relativeFilePath}", UriKind.RelativeOrAbsolute);

        public static Uri GetUri(string dllName, string relativeFilePath) => 
            new Uri($"/{dllName};component/{relativeFilePath}", UriKind.RelativeOrAbsolute);
    }
}

