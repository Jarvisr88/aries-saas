namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class RepeatPasswordCompleteEventArgs : EventArgs
    {
        private string repeatedPassword;

        public RepeatPasswordCompleteEventArgs(string repeatedPassword);

        public string RepeatedPassword { get; }
    }
}

