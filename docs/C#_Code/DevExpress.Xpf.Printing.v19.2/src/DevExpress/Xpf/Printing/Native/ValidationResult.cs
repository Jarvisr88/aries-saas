namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class ValidationResult
    {
        public ValidationResult(bool isValid) : this(isValid, string.Empty)
        {
        }

        public ValidationResult(bool isValid, string errorMessage)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
        }

        public bool IsValid { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}

