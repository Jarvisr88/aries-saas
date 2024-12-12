namespace DevExpress.Utils.Crypt
{
    using System;

    public interface IKeyGen
    {
        byte[] DeriveKey(int blockNumber);
    }
}

