namespace Devart.Cryptography
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;

    internal class a8
    {
        private static int a;

        static a8();
        [RegistryPermission(SecurityAction.Assert, Read=@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Lsa")]
        private static bool a();
        public static int b();
        [DllImport("bcrypt.dll")]
        internal static extern uint BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool A_0);
    }
}

