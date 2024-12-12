namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    internal class UriInfo
    {
        public UriInfo(System.Uri uri, IEnumerable<UriQualifierValue> qualifiers);

        public System.Uri Uri { get; set; }

        public ReadOnlyCollection<UriQualifierValue> Qualifiers { get; set; }

        public bool MultipleQualifiers { get; }

        public bool BindableQualifier { get; }
    }
}

