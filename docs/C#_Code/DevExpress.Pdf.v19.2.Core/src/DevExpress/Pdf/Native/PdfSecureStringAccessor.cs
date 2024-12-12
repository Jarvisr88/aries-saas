﻿namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public static class PdfSecureStringAccessor
    {
        [SecuritySafeCritical]
        public static string FromSecureString(SecureString password)
        {
            if (password == null)
            {
                return null;
            }
            int length = password.Length;
            short[] destination = new short[length];
            IntPtr source = Marshal.SecureStringToGlobalAllocUnicode(password);
            try
            {
                Marshal.Copy(source, destination, 0, length);
            }
            finally
            {
                Marshal.ZeroFreeCoTaskMemUnicode(source);
            }
            StringBuilder builder = new StringBuilder();
            foreach (short num3 in destination)
            {
                builder.Append((char) ((ushort) num3));
            }
            return builder.ToString();
        }

        public static SecureString ToSecureString(string str)
        {
            if (str == null)
            {
                return null;
            }
            SecureString str2 = new SecureString();
            foreach (char ch in str)
            {
                str2.AppendChar(ch);
            }
            return str2;
        }
    }
}
