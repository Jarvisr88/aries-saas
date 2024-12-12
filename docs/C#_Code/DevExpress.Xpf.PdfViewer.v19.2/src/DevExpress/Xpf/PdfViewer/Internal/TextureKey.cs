namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class TextureKey
    {
        public TextureKey(IComparable descriptor)
        {
            this.Descriptor = descriptor;
        }

        private bool Equals(TextureKey other) => 
            this.Descriptor.Equals(other.Descriptor);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? ((obj is TextureKey) && this.Equals((TextureKey) obj)) : true) : false;

        public override int GetHashCode() => 
            this.Descriptor.GetHashCode();

        public override string ToString() => 
            this.Descriptor.ToString();

        public IComparable Descriptor { get; private set; }
    }
}

