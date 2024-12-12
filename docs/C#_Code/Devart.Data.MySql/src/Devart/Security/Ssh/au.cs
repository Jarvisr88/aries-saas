namespace Devart.Security.Ssh
{
    using Devart.Cryptography.PKI;
    using Devart.Security;
    using System;
    using System.IO;

    internal class au
    {
        private const int a = 0x3f6ff9eb;
        private const string b = "openssh-key-v1";
        private const string c = "BEGIN RSA PRIVATE KEY";
        private const string d = "BEGIN DSA PRIVATE KEY";
        private const string e = "BEGIN SSH2 ENCRYPTED PRIVATE KEY";
        private const string f = "END RSA PRIVATE KEY";
        private const string g = "END DSA PRIVATE KEY";
        private const string h = "END SSH2 ENCRYPTED PRIVATE KEY";
        private const string i = "PuTTY-User-Key-File-2";
        private const string j = "Private-Lines";
        private const string k = "Public-Lines";
        private const string l = "BEGIN OPENSSH PRIVATE KEY";
        private const string m = "END OPENSSH PRIVATE KEY";
        private const string n = "BEGIN SSH2 PUBLIC KEY";
        private const string o = "END SSH2 PUBLIC KEY";
        private const string p = "BEGIN RSA PUBLIC KEY";
        private const string q = "END RSA PUBLIC KEY";
        private const string r = "BEGIN PUBLIC KEY";
        private const string s = "END PUBLIC KEY";
        private const string t = "ssh-rsa";
        private const string u = "ssh-dss";
        private const string v = "Proc-Type: 4,ENCRYPTED";
        private const string w = "DEK-Info: DES-EDE3-CBC,";
        private const string x = "DEK-Info: AES-128-CBC,";
        private const int y = 0x2000;
        private const int z = 0;
        private const int aa = 0x400;
        private const int ab = 0xa000;
        private const int ac = 0;
        private const int ad = 0x200;
        private const int ae = 7;
        private const int af = 0x2400;
        private const int ag = 0x2200;
        private const int ah = 0xa400;
        private const string ai = "RSA";
        private const string aj = "DSA";
        private Devart.Cryptography.PKI.m ak;
        private const StringComparison al = StringComparison.InvariantCultureIgnoreCase;
        private static byte[] am;

        static au();
        public au(Devart.Cryptography.PKI.m A_0);
        private string a();
        public static Devart.Cryptography.PKI.h a(Stream A_0);
        private static Devart.Cryptography.PKI.h a(StreamReader A_0);
        public static Devart.Cryptography.PKI.h a(string A_0);
        private static au a(byte[] A_0);
        public static au a(Stream A_0, string A_1);
        private static byte[] a(StreamReader A_0, string A_1);
        public static Devart.Cryptography.PKI.h a(string A_0, Devart.Security.p A_1);
        private static byte[] a(string A_0, int A_1);
        private static Devart.Cryptography.PKI.h a(string A_0, StreamReader A_1);
        public static au a(string A_0, string A_1);
        private static au a(byte[] A_0, string A_1);
        private static au a(byte[] A_0, uint A_1);
        public void a(Stream A_0, string A_1, string A_2);
        public static au a(StreamReader A_0, string A_1, string A_2);
        private static void a(StreamWriter A_0, string A_1, bool A_2);
        public static byte[] a(string A_0, int A_1, Devart.Security.Ssh.c A_2);
        private static byte[] a(string A_0, int A_1, byte[] A_2, bool A_3);
        public static byte[] a(string A_0, int A_1, byte[] A_2, bool A_3, Devart.Security.Ssh.c A_4);
        public Devart.Cryptography.PKI.m b();
        public static Devart.Cryptography.PKI.h b(string A_0);
        private static Devart.Cryptography.PKI.h b(byte[] A_0);
        public void b(Stream A_0);
        private static Devart.Cryptography.PKI.h b(StreamReader A_0);
        public static byte[] b(string A_0, int A_1);
        public static au b(byte[] A_0, string A_1);
        public void b(Stream A_0, string A_1);
        public static au b(string A_0, string A_1);
        private static au b(byte[] A_0, uint A_1);
        public static au b(StreamReader A_0, string A_1, string A_2);
        public static byte[] b(string A_0, int A_1, byte[] A_2, bool A_3);
        public Devart.Cryptography.PKI.p c();
        public byte[] c(byte[] A_0);
        public static au c(string A_0, string A_1);
        public static au c(StreamReader A_0, string A_1, string A_2);
        public byte[] d();

        private class a
        {
            public const string a = "Passphrase not specified.";
            public const string b = "Wrong key data format.";
            public const string c = "Wrong key derivation function.";
            public const string d = "Wrong public key algorithm.";
            public const string e = "Invalid public key data.";
            public const string f = "Invalid public key length.";
            public const string g = "Invalid private key data.";
            public const string h = "Invalid private key length.";
        }
    }
}

