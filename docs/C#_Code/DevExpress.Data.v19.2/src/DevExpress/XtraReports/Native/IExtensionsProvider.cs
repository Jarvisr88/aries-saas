namespace DevExpress.XtraReports.Native
{
    using System.Collections.Generic;

    public interface IExtensionsProvider
    {
        IDictionary<string, string> Extensions { get; }
    }
}

