namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    internal class MAPIUnicode : MAPI
    {
        protected override IMapiFileDesc CreateMapiFileDescInstance();
        protected override IMapiMessage CreateMapiMessageInstance();
        protected override IMapiRecipDesc CreateMapiRecipDescInstance();
        protected override Type GetMapiFileDescType();
        protected override Type GetMapiRecipDescType();
        [SecuritySafeCritical]
        protected override int MAPISendMail(IntPtr session, IntPtr handle, IMapiMessage msg, int flags, int reserved);
        [DllImport("MAPI32.DLL", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int MAPISendMailW(IntPtr session, IntPtr uiParam, MapiMessageW message, int flags, int reserved);
    }
}

