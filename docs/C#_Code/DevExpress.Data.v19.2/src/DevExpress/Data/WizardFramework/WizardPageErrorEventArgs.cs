namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Runtime.CompilerServices;

    public class WizardPageErrorEventArgs : EventArgs
    {
        public WizardPageErrorEventArgs(string errorMessage);

        public string ErrorMessage { get; set; }
    }
}

