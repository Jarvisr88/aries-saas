namespace DevExpress.Utils.KeyboardHandler
{
    using System;

    public interface IKeyHashProvider
    {
        long CreateHash(long keyData);
    }
}

