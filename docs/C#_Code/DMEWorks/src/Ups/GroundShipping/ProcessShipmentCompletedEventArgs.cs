namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [GeneratedCode("System.Web.Services", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code")]
    public class ProcessShipmentCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal ProcessShipmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public FreightShipResponse Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (FreightShipResponse) this.results[0];
            }
        }
    }
}

