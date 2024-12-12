namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;

    public abstract class EmailSenderBase
    {
        protected EmailSenderBase()
        {
        }

        protected string GetFinalSubject(EmailOptions options)
        {
            string str = string.Empty;
            try
            {
                str = string.IsNullOrEmpty(options.Subject) ? (PreviewLocalizer.GetString(PreviewStringId.EMail_From) + " " + PrintingSystemBase.UserName) : options.Subject;
            }
            catch
            {
            }
            return str;
        }

        public void Send(string[] files, EmailOptions options)
        {
            this.SendCore(files, options);
        }

        protected abstract void SendCore(string[] files, EmailOptions options);
        protected void SendViaMAPI(string[] files, EmailOptions options, IntPtr windowHandle)
        {
            MAPI.SendMail(windowHandle, files, this.GetFinalSubject(options), string.IsNullOrEmpty(options.Body) ? " " : options.Body, options.GetAllRecipients());
        }
    }
}

