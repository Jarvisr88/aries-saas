namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class DXCommandException : DXBindingExceptionBase<DXCommandException, DXCommandExtension>
    {
        public DXCommandException(DXCommandExtension owner, string message, Exception innerException) : base(owner, message, innerException)
        {
            this.Execute = owner.Execute;
            this.CanExecute = owner.CanExecute;
        }

        protected override string Report(string message) => 
            ErrorHelper.ReportCommandError(message, this.Execute, this.CanExecute);

        public string Execute { get; private set; }

        public string CanExecute { get; private set; }
    }
}

