namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    internal class FileResourceCollection : ResourceCollection
    {
        private Uri baseuri;
        private string location;
        private Dictionary<string, FileSystemWatcher> filesAndWatchers;
        private List<string> elements;
        private static XName pName;
        private static XName includeName;

        static FileResourceCollection();
        public FileResourceCollection(Uri baseuri, Uri targetUri);
        private void DropCache();
        protected override IEnumerable<string> EnumerateResourceKeys();
        private void OnElementChanged(object sender, FileSystemEventArgs e);
    }
}

