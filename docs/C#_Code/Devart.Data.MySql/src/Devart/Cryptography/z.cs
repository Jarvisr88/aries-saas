namespace Devart.Cryptography
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class z
    {
        internal const int a = 1;
        internal const int b = 2;

        private z();
        [DllImport("crypt32.dll", SetLastError=true)]
        internal static extern int CertAddCertificateContextToStore(IntPtr A_0, IntPtr A_1, int A_2, IntPtr A_3);
        [DllImport("crypt32.dll")]
        internal static extern int CertCloseStore(IntPtr A_0, int A_1);
        [DllImport("crypt32.dll")]
        internal static extern int CertCompareCertificate(int A_0, IntPtr A_1, IntPtr A_2);
        [DllImport("crypt32.dll")]
        internal static extern IntPtr CertCreateCertificateContext(int A_0, IntPtr A_1, int A_2);
        [DllImport("crypt32.dll")]
        internal static extern int CertDeleteCertificateFromStore(IntPtr A_0);
        [DllImport("crypt32.dll")]
        internal static extern IntPtr CertDuplicateCertificateContext(IntPtr A_0);
        [DllImport("crypt32.dll")]
        internal static extern IntPtr CertDuplicateStore(IntPtr A_0);
        [DllImport("crypt32.dll")]
        internal static extern IntPtr CertFindCertificateInStore(IntPtr A_0, int A_1, int A_2, int A_3, IntPtr A_4, IntPtr A_5);
        [DllImport("crypt32.dll", EntryPoint="CertFindCertificateInStore")]
        internal static extern IntPtr CertFindDataBlobCertificateInStore(IntPtr A_0, int A_1, int A_2, int A_3, ref a9 A_4, IntPtr A_5);
        [DllImport("crypt32.dll", CharSet=CharSet.Ansi)]
        internal static extern IntPtr CertFindExtension([MarshalAs(UnmanagedType.LPStr)] string A_0, int A_1, IntPtr A_2);
        [DllImport("crypt32.dll", CharSet=CharSet.Ansi)]
        internal static extern IntPtr CertFindRDNAttr(string A_0, IntPtr A_1);
        [DllImport("crypt32.dll", EntryPoint="CertFindCertificateInStore")]
        internal static extern IntPtr CertFindStringCertificateInStore(IntPtr A_0, int A_1, int A_2, int A_3, [MarshalAs(UnmanagedType.LPWStr)] string A_4, IntPtr A_5);
        [DllImport("crypt32.dll", EntryPoint="CertFindCertificateInStore")]
        internal static extern IntPtr CertFindUsageCertificateInStore(IntPtr A_0, int A_1, int A_2, int A_3, ref l A_4, IntPtr A_5);
        [DllImport("crypt32.dll")]
        internal static extern void CertFreeCertificateChain(IntPtr A_0);
        [DllImport("crypt32.dll")]
        internal static extern int CertFreeCertificateContext(IntPtr A_0);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetCertificateChain(IntPtr A_0, IntPtr A_1, IntPtr A_2, IntPtr A_3, ref ac A_4, int A_5, IntPtr A_6, ref IntPtr A_7);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetCertificateContextProperty(IntPtr A_0, int A_1, byte[] A_2, ref int A_3);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetCertificateContextProperty(IntPtr A_0, int A_1, IntPtr A_2, ref int A_3);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetEnhancedKeyUsage(IntPtr A_0, int A_1, IntPtr A_2, ref int A_3);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetIntendedKeyUsage(int A_0, IntPtr A_1, IntPtr A_2, int A_3);
        [DllImport("crypt32.dll")]
        internal static extern IntPtr CertGetIssuerCertificateFromStore(IntPtr A_0, IntPtr A_1, IntPtr A_2, ref int A_3);
        [DllImport("crypt32.dll", EntryPoint="CertGetNameStringA")]
        internal static extern int CertGetNameString(IntPtr A_0, int A_1, int A_2, IntPtr A_3, IntPtr A_4, int A_5);
        [DllImport("crypt32.dll")]
        internal static extern int CertGetPublicKeyLength(int A_0, IntPtr A_1);
        [DllImport("crypt32.dll", CharSet=CharSet.Ansi)]
        internal static extern IntPtr CertOpenStore(IntPtr A_0, int A_1, int A_2, int A_3, string A_4);
        [DllImport("crypt32.dll", EntryPoint="CertOpenStore")]
        internal static extern IntPtr CertOpenStoreData(IntPtr A_0, int A_1, IntPtr A_2, int A_3, ref a9 A_4);
        [DllImport("crypt32.dll", EntryPoint="CertOpenSystemStoreA", CharSet=CharSet.Ansi)]
        internal static extern IntPtr CertOpenSystemStore(int A_0, string A_1);
        [DllImport("crypt32.dll")]
        internal static extern int CertSaveStore(IntPtr A_0, int A_1, int A_2, int A_3, ref a9 A_4, int A_5);
        [DllImport("crypt32.dll")]
        internal static extern int CertSetCertificateContextProperty(IntPtr A_0, int A_1, int A_2, ref ap A_3);
        [DllImport("crypt32.dll", EntryPoint="CertStrToNameW", CharSet=CharSet.Unicode)]
        internal static extern int CertStrToName(int A_0, string A_1, int A_2, IntPtr A_3, IntPtr A_4, ref int A_5, IntPtr A_6);
        [DllImport("crypt32.dll")]
        internal static extern int CertVerifyCertificateChainPolicy(IntPtr A_0, IntPtr A_1, ref Devart.Cryptography.a A_2, ref ag A_3);
        [DllImport("crypt32.dll")]
        internal static extern int CertVerifyTimeValidity(IntPtr A_0, IntPtr A_1);
        [DllImport("crypt32.dll")]
        internal static extern int CryptAcquireCertificatePrivateKey(IntPtr A_0, int A_1, IntPtr A_2, ref IntPtr A_3, ref int A_4, ref int A_5);
        [DllImport("advapi32.dll", EntryPoint="CryptAcquireContextA", CharSet=CharSet.Ansi, SetLastError=true)]
        internal static extern int CryptAcquireContext(ref IntPtr A_0, IntPtr A_1, string A_2, int A_3, int A_4);
        [DllImport("advapi32.dll", EntryPoint="CryptAcquireContextA", CharSet=CharSet.Ansi, SetLastError=true)]
        internal static extern int CryptAcquireContext(ref IntPtr A_0, string A_1, string A_2, int A_3, uint A_4);
        [DllImport("advapi32.dll")]
        internal static extern int CryptCreateHash(IntPtr A_0, int A_1, IntPtr A_2, int A_3, out IntPtr A_4);
        [DllImport("crypt32.dll")]
        internal static extern int CryptDecodeObjectEx(int A_0, IntPtr A_1, IntPtr A_2, int A_3, int A_4, IntPtr A_5, ref int A_6);
        [DllImport("crypt32.dll")]
        internal static extern int CryptDecodeObjectEx(int A_0, IntPtr A_1, byte[] A_2, int A_3, int A_4, IntPtr A_5, ref int A_6);
        [DllImport("advapi32.dll")]
        internal static extern int CryptDecrypt(int A_0, int A_1, int A_2, int A_3, byte[] A_4, ref int A_5);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptDecrypt(IntPtr A_0, int A_1, int A_2, int A_3, byte[] A_4, ref int A_5);
        [DllImport("advapi32.dll")]
        internal static extern int CryptDestroyHash(IntPtr A_0);
        [DllImport("advapi32.dll")]
        internal static extern int CryptDestroyKey(IntPtr A_0);
        [DllImport("advapi32.dll")]
        internal static extern int CryptDuplicateHash(int A_0, IntPtr A_1, int A_2, out int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptEncrypt(int A_0, int A_1, int A_2, int A_3, IntPtr A_4, ref int A_5, int A_6);
        [DllImport("advapi32.dll")]
        internal static extern int CryptEncrypt(int A_0, int A_1, int A_2, int A_3, byte[] A_4, ref int A_5, int A_6);
        [DllImport("advapi32.dll")]
        internal static extern int CryptEncrypt(IntPtr A_0, int A_1, int A_2, int A_3, byte[] A_4, ref int A_5, int A_6);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptExportKey(IntPtr A_0, IntPtr A_1, int A_2, int A_3, IntPtr A_4, ref int A_5);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptExportKey(IntPtr A_0, IntPtr A_1, int A_2, int A_3, byte[] A_4, ref int A_5);
        [DllImport("crypt32.dll")]
        internal static extern int CryptFindCertificateKeyProvInfo(IntPtr A_0, int A_1, IntPtr A_2);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGenKey(int A_0, IntPtr A_1, int A_2, ref int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGenRandom(IntPtr A_0, int A_1, IntPtr A_2);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGetHashParam(IntPtr A_0, int A_1, byte[] A_2, ref int A_3, int A_4);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGetKeyParam(int A_0, int A_1, ref int A_2, ref int A_3, int A_4);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGetKeyParam(int A_0, int A_1, byte[] A_2, ref int A_3, int A_4);
        [DllImport("advapi32.dll")]
        internal static extern int CryptGetKeyParam(int A_0, int A_1, ref IntPtr A_2, ref int A_3, int A_4);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptGetProvParam(int A_0, int A_1, IntPtr A_2, ref int A_3, int A_4);
        [DllImport("advapi32.dll", CharSet=CharSet.Ansi)]
        internal static extern int CryptGetUserKey(int A_0, int A_1, ref int A_2);
        [DllImport("advapi32.dll")]
        internal static extern int CryptHashData(IntPtr A_0, byte[] A_1, int A_2, int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptHashData(IntPtr A_0, IntPtr A_1, int A_2, int A_3);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptImportKey(IntPtr A_0, byte[] A_1, int A_2, IntPtr A_3, int A_4, ref IntPtr A_5);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptImportKey(IntPtr A_0, IntPtr A_1, int A_2, IntPtr A_3, int A_4, ref IntPtr A_5);
        [DllImport("crypt32.dll")]
        internal static extern int CryptImportPublicKeyInfoEx(IntPtr A_0, int A_1, ref a3 A_2, int A_3, int A_4, IntPtr A_5, ref IntPtr A_6);
        [DllImport("advapi32.dll")]
        internal static extern int CryptReleaseContext(IntPtr A_0, int A_1);
        [DllImport("advapi32.dll")]
        internal static extern int CryptSetHashParam(IntPtr A_0, int A_1, byte[] A_2, int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptSetKeyParam(int A_0, int A_1, byte[] A_2, int A_3);
        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int CryptSetKeyParam(int A_0, int A_1, ref a9 A_2, int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptSetKeyParam(int A_0, int A_1, ref int A_2, int A_3);
        [DllImport("advapi32.dll")]
        internal static extern int CryptSignHash(IntPtr A_0, int A_1, IntPtr A_2, int A_3, byte[] A_4, ref int A_5);
        [DllImport("advapi32.dll", EntryPoint="CryptVerifySignatureA", CharSet=CharSet.Ansi)]
        internal static extern int CryptVerifySignature(IntPtr A_0, byte[] A_1, int A_2, IntPtr A_3, IntPtr A_4, int A_5);
    }
}

