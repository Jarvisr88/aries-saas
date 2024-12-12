namespace DevExpress.Images
{
    using DevExpress.Utils;
    using System;

    internal sealed class ImageResourceKey : IEquatable<ImageResourceKey>
    {
        private readonly string Name;
        private readonly string Prefix;
        private readonly string Category;
        private readonly int hash;

        public ImageResourceKey(string name, string prefix, string category)
        {
            this.Name = name;
            this.Prefix = prefix;
            this.Category = category;
            this.hash = HashCodeHelper.CalculateGeneric<string, string, string>(this.Name, this.Prefix, this.Category);
        }

        public bool Equals(ImageResourceKey key) => 
            !ReferenceEquals(this, key) ? (string.Equals(this.Name, key.Name) && (string.Equals(this.Prefix, key.Prefix) && string.Equals(this.Category, key.Category))) : true;

        public sealed override bool Equals(object obj)
        {
            ImageResourceKey objB = obj as ImageResourceKey;
            return ((objB != null) ? Equals(this, objB) : false);
        }

        public sealed override int GetHashCode() => 
            this.hash;
    }
}

