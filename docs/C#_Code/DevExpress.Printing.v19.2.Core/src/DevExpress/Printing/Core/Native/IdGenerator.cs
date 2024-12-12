namespace DevExpress.Printing.Core.Native
{
    using System;

    public class IdGenerator
    {
        public const string IdFormat = "N";

        public static string GenerateRandomId() => 
            Guid.NewGuid().ToString("N");
    }
}

