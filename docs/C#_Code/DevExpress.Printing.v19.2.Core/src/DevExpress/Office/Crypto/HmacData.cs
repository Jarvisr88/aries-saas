namespace DevExpress.Office.Crypto
{
    using System;
    using System.Runtime.CompilerServices;

    public class HmacData
    {
        public HmacData Clone() => 
            new HmacData { 
                EncryptedKey = (byte[]) this.EncryptedKey.Clone(),
                EncryptedValue = (byte[]) this.EncryptedValue.Clone()
            };

        public byte[] EncryptedKey { get; set; }

        public byte[] EncryptedValue { get; set; }
    }
}

