namespace DevExpress.Xpf.Utils.Native
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ResourceLoadedEventArgs : EventArgs
    {
        public ResourceLoadedEventArgs(bool loaded, Stream stream = null)
        {
            this.Loaded = loaded;
            this.ResourceStream = stream;
        }

        public bool Loaded { get; private set; }

        public Stream ResourceStream { get; private set; }
    }
}

