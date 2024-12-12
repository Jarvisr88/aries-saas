namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;

    public interface IPageSettingsConfiguratorService
    {
        bool? Configure(XtraPageSettingsBase pageSettings, Window ownerWindow);
    }
}

