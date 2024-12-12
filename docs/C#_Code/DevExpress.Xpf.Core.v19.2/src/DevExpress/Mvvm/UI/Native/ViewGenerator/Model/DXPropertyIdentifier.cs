namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXPropertyIdentifier : IEquatable<DXPropertyIdentifier>
    {
        private readonly Type declaringType;
        private readonly DXTypeIdentifier declaringTypeId;
        private readonly string fullName;
        private readonly string name;
        public Type DeclaringType =>
            this.declaringType;
        public DXTypeIdentifier DeclaringTypeIdentifier =>
            this.declaringTypeId;
        public string FullName =>
            this.fullName;
        public string Name =>
            this.name;
        public DXPropertyIdentifier(Type declaringType, string name)
        {
            if (declaringType == null)
            {
                throw new ArgumentNullException("declaringType");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.declaringType = declaringType;
            this.declaringTypeId = new DXTypeIdentifier();
            this.name = name;
            this.fullName = CreateFullName(this.declaringType, this.declaringTypeId, name);
        }

        public DXPropertyIdentifier(DXTypeIdentifier declaringTypeId, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (declaringTypeId.IsEmpty)
            {
                throw new ArgumentNullException("declaringTypeId");
            }
            this.declaringType = null;
            this.declaringTypeId = declaringTypeId;
            this.name = name;
            this.fullName = CreateFullName(this.declaringType, this.declaringTypeId, name);
        }

        private static string CreateFullName(Type declaringType, DXTypeIdentifier declaringTypeId, string name) => 
            string.IsNullOrEmpty(name) ? string.Empty : (((declaringType != null) ? declaringType.Name : declaringTypeId.SimpleName) + "." + name);

        public override int GetHashCode() => 
            ((this.declaringType != null) ? this.declaringType.GetHashCode() : this.declaringTypeId.GetHashCode()) ^ this.name.GetHashCode();

        public override bool Equals(object obj) => 
            (obj is DXPropertyIdentifier) && this.Equals((DXPropertyIdentifier) obj);

        public static bool operator ==(DXPropertyIdentifier first, DXPropertyIdentifier second) => 
            first.Equals(second);

        public static bool operator !=(DXPropertyIdentifier first, DXPropertyIdentifier second) => 
            !first.Equals(second);

        public bool Equals(DXPropertyIdentifier other) => 
            (this.declaringType == null) ? (this.declaringTypeId.Equals(other.declaringTypeId) && (this.name == other.name)) : ((this.declaringType == other.declaringType) && (this.name == other.name));

        public override string ToString() => 
            this.FullName;
    }
}

