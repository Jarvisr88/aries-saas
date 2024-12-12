namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public abstract class DXBindingExceptionBase<TSelf, TOwner> : Exception where TSelf: DXBindingExceptionBase<TSelf, TOwner> where TOwner: DXBindingBase
    {
        protected DXBindingExceptionBase(TOwner owner, string message, Exception innerException) : this(owner.TargetPropertyName, owner.TargetObjectName, message, innerException)
        {
        }

        protected DXBindingExceptionBase(string targetPropertyName, string targetObjectType, string message, Exception innerException) : base(message, innerException)
        {
            this.TargetPropertyName = targetPropertyName;
            this.TargetObjectType = targetObjectType;
        }

        protected abstract string Report(string message);
        public static void Report(TOwner owner, string message)
        {
            object[] args = new object[3];
            args[0] = owner;
            args[1] = message;
            TSelf local = (TSelf) Activator.CreateInstance(typeof(TSelf), args);
            PresentationTraceSources.DataBindingSource.TraceEvent(TraceEventType.Error, 40, local.Report(message));
        }

        public static void Throw(TOwner owner, string message, Exception innerException)
        {
            object[] args = new object[] { owner, message, innerException };
            throw ((TSelf) Activator.CreateInstance(typeof(TSelf), args));
        }

        public string TargetPropertyName { get; private set; }

        public string TargetObjectType { get; private set; }
    }
}

