namespace DevExpress.XtraPrinting.Export.Web
{
    using System;

    public interface IScriptContainer
    {
        bool IsClientScriptBlockRegistered(string key);
        void RegisterClientScriptBlock(string key, string script);
        void RegisterCommonCssStyle(string style, string tagName);
        string RegisterCssClass(string style);
    }
}

