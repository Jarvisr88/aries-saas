namespace DevExpress.Xpo.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ExceptionBundleException : Exception
    {
        private Exception[] _Exceptions;

        public ExceptionBundleException(params Exception[] exceptions) : base(ExtractMessage(exceptions))
        {
            this._Exceptions = exceptions;
        }

        protected ExceptionBundleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string ExtractMessage(Exception[] exceptions)
        {
            if (exceptions == null)
            {
                return string.Empty;
            }
            string str = exceptions.Length.ToString() + " exceptions thrown:";
            foreach (Exception exception in exceptions)
            {
                str = str + "\n" + exception.Message + "\n";
            }
            return str;
        }

        public override string ToString()
        {
            if (this.Exceptions == null)
            {
                return base.ToString();
            }
            string str = this.Exceptions.Length.ToString() + " exceptions thrown:\n";
            foreach (Exception exception in this.Exceptions)
            {
                str = str + exception.ToString() + "\n";
            }
            return str;
        }

        public Exception[] Exceptions
        {
            get => 
                this._Exceptions;
            set => 
                this._Exceptions = value;
        }
    }
}

