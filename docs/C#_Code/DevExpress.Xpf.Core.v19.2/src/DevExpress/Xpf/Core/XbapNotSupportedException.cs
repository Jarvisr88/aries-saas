namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.Serialization;
    using System.Security;

    [Serializable]
    public class XbapNotSupportedException : ApplicationException
    {
        internal const string Text = "Starting from v2011 vol 2, XBAP applications are not supported. Instead, we recommend that you use ClickOnce deployment (the most preferable way) or migrate your application to Silverlight. For more information, please refer to http://go.devexpress.com/SupportXBAP.aspx\r\n\r\nIf you still want to continue using DevExpress controls in XBAP applications, set the OptionsXBAP.SuppressNotSupportedException property to True.";

        public XbapNotSupportedException(string message) : base(message)
        {
        }

        [SecuritySafeCritical]
        public XbapNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

