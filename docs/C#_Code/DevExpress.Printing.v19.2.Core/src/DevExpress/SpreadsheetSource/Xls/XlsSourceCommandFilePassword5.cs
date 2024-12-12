namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using DevExpress.XtraExport;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandFilePassword5 : XlsSourceCommandFilePassword
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            string password = contentBuilder.Options.Password;
            bool flag = string.IsNullOrEmpty(password);
            if (flag)
            {
                password = "VelvetSweatshop";
            }
            if (((short) XORObfuscationHelper.CalculatePasswordVerifier(password)) == base.XorObfuscation.VerificationId)
            {
                contentBuilder.SetupXORDecryptor(password, base.XorObfuscation.Key);
            }
            else
            {
                if (flag)
                {
                    throw new EncryptedFileException(EncryptedFileError.PasswordRequired, "Password required to open this file!");
                }
                throw new EncryptedFileException(EncryptedFileError.WrongPassword, "Wrong password!");
            }
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            base.RC4Encrypted = false;
            base.XorObfuscation = XlsXORObfuscation.FromStream(reader);
        }
    }
}

