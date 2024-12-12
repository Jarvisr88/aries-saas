namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack=4)]
    internal struct PropertyKey : IEquatable<PropertyKey>
    {
        private Guid formatId;
        private int propertyId;
        public Guid FormatId =>
            this.formatId;
        public int PropertyId =>
            this.propertyId;
        public PropertyKey(Guid formatId, int propertyId)
        {
            this.formatId = formatId;
            this.propertyId = propertyId;
        }

        public PropertyKey(string formatId, int propertyId)
        {
            this.formatId = new Guid(formatId);
            this.propertyId = propertyId;
        }

        public bool Equals(PropertyKey other) => 
            other.Equals(this);

        public override int GetHashCode() => 
            (this.propertyId * 0x1505) + this.formatId.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is PropertyKey))
            {
                return false;
            }
            PropertyKey key = (PropertyKey) obj;
            return (key.formatId.Equals(this.formatId) && (key.propertyId == this.propertyId));
        }

        public static bool operator ==(PropertyKey propKey1, PropertyKey propKey2) => 
            propKey1.Equals(propKey2);

        public static bool operator !=(PropertyKey propKey1, PropertyKey propKey2) => 
            !propKey1.Equals(propKey2);
    }
}

