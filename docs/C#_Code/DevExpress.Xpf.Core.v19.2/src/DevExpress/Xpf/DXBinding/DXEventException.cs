namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class DXEventException : DXBindingExceptionBase<DXEventException, DXEventExtension>
    {
        public DXEventException(DXEventExtension owner, string message, Exception innerException) : this(owner.TargetPropertyName, owner.TargetObjectName, owner.Handler, message, innerException)
        {
        }

        public DXEventException(string targetPropertyName, string targetObjectType, string handler, string message, Exception innerException) : base(targetPropertyName, targetObjectType, message, innerException)
        {
            this.Handler = handler;
        }

        protected override string Report(string message) => 
            ErrorHelper.ReportEventError(message, this.Handler);

        public string Handler { get; private set; }
    }
}

