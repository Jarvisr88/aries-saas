namespace DevExpress.Utils.Design
{
    using System;

    public class DXClientDocumentationProviderWebAttribute : DXDocumentationProviderAttribute
    {
        public DXClientDocumentationProviderWebAttribute(string typeName) : this(typeName, true)
        {
        }

        public DXClientDocumentationProviderWebAttribute(string typeName, bool globalNamespace) : base("Client-Side API Documentation", GetLinkByControlType(typeName, globalNamespace))
        {
        }

        private static string GetLinkByControlType(string controlType, bool globalNamespace)
        {
            bool flag = controlType.Contains("Bootstrap");
            string str = flag ? "#AspNetBootstrap" : "#AspNet";
            string str2 = flag ? "Bootstrap" : "";
            if (!globalNamespace)
            {
                return $"{str}/DevExpressWeb{str2}{controlType}Scripts";
            }
            string oldValue = flag ? "Bootstrap" : "ASPx";
            return $"{str}/clsDevExpressWeb{str2}Scripts{controlType.Replace(oldValue, oldValue + "Client")}topic";
        }
    }
}

