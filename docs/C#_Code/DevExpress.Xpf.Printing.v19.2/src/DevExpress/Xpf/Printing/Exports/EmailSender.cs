namespace DevExpress.Xpf.Printing.Exports
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;

    internal class EmailSender : EmailSenderBase
    {
        protected override void SendCore(string[] files, EmailOptions options)
        {
            base.SendViaMAPI(files, options, IntPtr.Zero);
        }
    }
}

