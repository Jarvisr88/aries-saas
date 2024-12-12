namespace DevExpress.Xpf.Core
{
    using System;

    public interface IDXDomainDataSourceSupport
    {
        bool SupportDomainDataSource { get; set; }

        bool ShowLoadingPanel { get; set; }
    }
}

