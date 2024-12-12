namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXTypeIdentifier : IEquatable<DXTypeIdentifier>
    {
        private readonly string name;
        private readonly int hashCode;
        public DXTypeIdentifier(string fullyQualifiedName)
        {
            if (fullyQualifiedName == null)
            {
                throw new ArgumentNullException("fullyQualifiedName");
            }
            this.name = fullyQualifiedName;
            this.hashCode = this.name.GetHashCode();
        }

        public bool IsEmpty =>
            string.IsNullOrEmpty(this.name);
        internal string SimpleName
        {
            get
            {
                string name = this.name;
                int index = name.IndexOf(',');
                if (index >= 0)
                {
                    name = name.Substring(0, index);
                }
                index = name.LastIndexOf('.');
                if (index >= 0)
                {
                    name = name.Substring(index + 1);
                }
                return name.Trim();
            }
        }
        public string Name =>
            this.name;
        public override int GetHashCode() => 
            this.hashCode;

        public override bool Equals(object obj) => 
            (obj is DXTypeIdentifier) && this.Equals((DXTypeIdentifier) obj);

        public static bool operator ==(DXTypeIdentifier first, DXTypeIdentifier second) => 
            first.Equals(second);

        public static bool operator !=(DXTypeIdentifier first, DXTypeIdentifier second) => 
            !first.Equals(second);

        public bool Equals(DXTypeIdentifier other) => 
            this.name == other.name;

        public override string ToString() => 
            this.name;
    }
}

