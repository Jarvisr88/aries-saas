namespace DevExpress.Internal.WinApi
{
    using System;

    [CLSCompliant(false)]
    public static class ErrorHelper
    {
        public static void VerifySucceeded(uint hResult)
        {
            if (hResult > 1)
            {
                throw new Exception("Failed with HRESULT: " + hResult.ToString("X"));
            }
        }
    }
}

