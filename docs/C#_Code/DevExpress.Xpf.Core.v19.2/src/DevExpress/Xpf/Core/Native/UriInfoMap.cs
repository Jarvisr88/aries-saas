namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class UriInfoMap
    {
        private readonly List<Uri> undefinedResources;
        private readonly MultiDictionary<Uri, UriInfo> definedResources;
        private IEnumerable<string> set;
        private Uri baseUri;

        public UriInfoMap(IEnumerable<string> set, Uri baseUri);
        private Uri CreateUri(string baseUriPrefix, string relativeUri);
        private static string GetBaseUriPrefix(IEnumerable<string> set, Uri baseUri);
        public ICollection<UriInfo> GetValues(Uri uri);
        private bool IsQualifiedResourceKey(Uri originalUri, Uri candidateUri, out IEnumerable<UriQualifierValue> qualifiers);
        private void OnSetChanged(object sender, NotifyCollectionChangedEventArgs e);
        private static void SplitName(string nameQualifiersExt, bool hasQualifiers, out string fileName, out string qualifiers, out string ext);
        private static string[] SplitString(string candidateQualifiers, params char[] separator);
        private static bool TryGetQualifiers(string[] splittedFileNameQualifierStrings, out IEnumerable<UriQualifierValue> fileNameQualifiers);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UriInfoMap.<>c <>9;
            public static Func<string, bool> <>9__6_0;
            public static Func<string, string> <>9__6_1;
            public static Func<UriQualifierValue, IBaseUriQualifier> <>9__9_0;
            public static Func<UriQualifierValue, IBaseUriQualifier> <>9__9_1;

            static <>c();
            internal bool <GetBaseUriPrefix>b__6_0(string x);
            internal string <GetBaseUriPrefix>b__6_1(string x);
            internal IBaseUriQualifier <IsQualifiedResourceKey>b__9_0(UriQualifierValue x);
            internal IBaseUriQualifier <IsQualifiedResourceKey>b__9_1(UriQualifierValue x);
        }
    }
}

