namespace DevExpress.Xpf.Core
{
    using System;

    public class DataAccessResourceExtension : ResourceExtensionBase
    {
        public DataAccessResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.DataAccess";
    }
}

