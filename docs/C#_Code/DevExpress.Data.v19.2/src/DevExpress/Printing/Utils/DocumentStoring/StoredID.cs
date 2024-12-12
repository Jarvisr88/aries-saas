namespace DevExpress.Printing.Utils.DocumentStoring
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct StoredID : IComparable
    {
        public const int UndefinedID = -2147483648;
        public static StoredID Undefined;
        private readonly int id;
        public int Id =>
            this.id;
        public StoredID(int id)
        {
            this.id = id;
        }

        public bool IsUndefined =>
            this.id == -2147483648;
        public override string ToString() => 
            this.IsUndefined ? "Undefined" : this.id.ToString();

        public static bool operator ==(StoredID x, StoredID y) => 
            x.id == y.id;

        public static bool operator !=(StoredID x, StoredID y) => 
            x.id != y.id;

        public override bool Equals(object obj) => 
            this.id == ((StoredID) obj).id;

        public override int GetHashCode() => 
            this.id;

        public int CompareTo(object obj) => 
            this.id.CompareTo(((StoredID) obj).Id);

        static StoredID()
        {
            Undefined = new StoredID(-2147483648);
        }
    }
}

