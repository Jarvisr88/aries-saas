namespace DMEWorks.Forms
{
    using System;
    using System.Runtime.CompilerServices;

    public class ValidateValueEventArgs : EventArgs
    {
        public ValidateValueEventArgs(string Value)
        {
            this._Value = Value;
            this.Valid = true;
            this.Message = "";
        }

        public string Value { get; }

        public bool Valid { get; set; }

        public string Message { get; set; }
    }
}

