namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [SecuritySafeCritical]
    public class MAPI
    {
        private const int MAPI_LOGON_UI = 1;
        private const int MAPI_DIALOG = 8;
        private const int MAPI_DIALOG_MODELESS = 12;
        private const int MAPI_USER_ABORT = 1;
        private string[] files;
        private string subject;
        private string body;
        private IntPtr handle;
        private IntPtr session;
        private int error;
        private List<Recipient> recipients;
        public static bool UseLogon;
        private static bool? unicodeSupported;

        internal MAPI();
        public MAPI(IntPtr handle, string[] files, string mailSubject, string mailBody, RecipientCollection recipients);
        private IntPtr AllocMemory(Type structureType, int count);
        protected virtual IMapiFileDesc CreateMapiFileDescInstance();
        protected virtual IMapiMessage CreateMapiMessageInstance();
        protected virtual IMapiRecipDesc CreateMapiRecipDescInstance();
        private IMapiMessage CreateMessage();
        private void DisposeMessage(IMapiMessage msg);
        private void FreeMemory(IntPtr ptr, Type structureType, int count);
        private static int GetDialogMode();
        private IntPtr GetFilesDesc();
        protected virtual Type GetMapiFileDescType();
        protected virtual Type GetMapiRecipDescType();
        [DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        private IntPtr GetRecipDesc();
        private string GetShortPathName(string path);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        private static extern uint GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, uint cchBuffer);
        [DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
        private void Logoff();
        private bool Logon(IntPtr hwnd);
        [DllImport("MAPI32.DLL", CharSet=CharSet.Ansi)]
        private static extern int MAPILogoff(IntPtr session, IntPtr hwnd, int flags, int reserved);
        [DllImport("MAPI32.DLL", CharSet=CharSet.Ansi)]
        private static extern int MAPILogon(IntPtr hwnd, string profileName, string password, int flags, int reserved, ref IntPtr session);
        protected virtual int MAPISendMail(IntPtr session, IntPtr handle, IMapiMessage msg, int flags, int reserved);
        [DllImport("MAPI32.DLL", EntryPoint="MAPISendMail", CharSet=CharSet.Ansi, ExactSpelling=true)]
        private static extern int MAPISendMailA(IntPtr session, IntPtr uiParam, MapiMessageA message, int flags, int reserved);
        private static IntPtr OffsetPtr(IntPtr ptr, Type structureType, int offset);
        private void SendInternal(IntPtr handle, string[] files, string mailSubject, string mailBody, RecipientCollection recipients);
        public static void SendMail(IntPtr handle, string[] files, string mailSubject, string mailBody, RecipientCollection recipients);
        private void SendUsingLogon(IntPtr handle);

        private static bool AllowModelessDialog { get; }

        private static bool UnicodeSupported { get; }
    }
}

