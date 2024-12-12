namespace DevExpress.Entity.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public class ProviderNotSupportedException : Exception
    {
        public ProviderNotSupportedException(string providerName)
        {
            this.ProviderName = providerName;
        }

        public string ProviderName { get; set; }
    }
}

