namespace Devart.Data.MySql
{
    using System;

    public class MemCryptStorage
    {
        public static void AddCa(string id, string pemCa);
        public static void AddCa(string id, byte[] pemCa);
        public static void AddCert(string id, string pemCert);
        public static void AddCert(string id, byte[] pemCert);
        public static void AddKey(string id, string pemKey);
        public static void AddKey(string id, byte[] pemKey);
        public static void ClearAll();
        public static void ClearCa();
        public static void ClearCert();
        public static void ClearKey();
        public static bool ContainsCa(string id);
        public static bool ContainsCert(string id);
        public static bool ContainsKey(string id);
        public static void RemoveCa(string id);
        public static void RemoveCert(string id);
        public static void RemoveKey(string id);
    }
}

