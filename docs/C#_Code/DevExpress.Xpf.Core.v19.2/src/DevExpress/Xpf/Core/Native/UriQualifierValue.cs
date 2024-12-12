namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    internal class UriQualifierValue
    {
        public UriQualifierValue(IBaseUriQualifier qualifier, string value);

        public IBaseUriQualifier Qualifier { get; set; }

        public string Value { get; set; }
    }
}

