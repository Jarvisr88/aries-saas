namespace DevExpress.Xpf.Data.Native
{
    using System;

    public class UpdateRowResult
    {
        public readonly string Error;

        public UpdateRowResult(string error)
        {
            this.Error = error;
        }
    }
}

