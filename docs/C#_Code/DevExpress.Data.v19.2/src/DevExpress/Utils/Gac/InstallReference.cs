namespace DevExpress.Utils.Gac
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class InstallReference
    {
        private int cbSize;
        private int flags;
        private Guid guidScheme;
        [MarshalAs(UnmanagedType.LPWStr)]
        private string identifier;
        [MarshalAs(UnmanagedType.LPWStr)]
        private string description;
        public InstallReference(Guid guid, string id, string data)
        {
            this.cbSize = ((2 * IntPtr.Size) + 0x10) + ((id.Length + data.Length) * 2);
            this.flags = 0;
            int flags = this.flags;
            this.guidScheme = guid;
            this.identifier = id;
            this.description = data;
        }

        public Guid GuidScheme =>
            this.guidScheme;
        public string Identifier =>
            this.identifier;
        public string Description =>
            this.description;
    }
}

