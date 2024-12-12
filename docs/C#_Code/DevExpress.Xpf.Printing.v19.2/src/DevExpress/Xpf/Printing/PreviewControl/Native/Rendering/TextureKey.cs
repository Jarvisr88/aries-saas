namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class TextureKey
    {
        public TextureKey(TextureType type, IComparable descriptor)
        {
            this.Descriptor = descriptor;
            this.Type = type;
        }

        private bool Equals(TextureKey other) => 
            (this.Type == other.Type) && this.Descriptor.Equals(other.Descriptor);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? ((obj is TextureKey) && this.Equals((TextureKey) obj)) : true) : false;

        public override int GetHashCode() => 
            ((int) (this.Type * ((TextureType) 0x18d))) ^ this.Descriptor.GetHashCode();

        public override string ToString() => 
            this.Type + "@" + this.Descriptor;

        public TextureType Type { get; private set; }

        public IComparable Descriptor { get; private set; }
    }
}

