namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.Data.Internal;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class ExportFileHelperBase
    {
        protected static readonly string[] EmptyStrings = new string[0];
        protected PrintingSystemBase ps;
        private EmailSenderBase emailSender;

        protected ExportFileHelperBase(PrintingSystemBase ps, EmailSenderBase emailSender)
        {
            if (ps == null)
            {
                throw new ArgumentNullException("ps");
            }
            if (emailSender == null)
            {
                throw new ArgumentNullException("emailSender");
            }
            this.ps = ps;
            this.emailSender = emailSender;
        }

        protected abstract void CreateExportFiles(ExportOptionsBase options, IDictionary<Type, object[]> disabledExportModes, Action<string[]> callback);
        public void ExecExport(ExportOptionsBase options, IDictionary<Type, object[]> disabledExportModes)
        {
            this.CreateExportFiles(options, disabledExportModes, delegate (string[] names) {
                if (ExportOptionsHelper.GetUseActionAfterExportAndSaveModeValue(options) && (names.Length != 0))
                {
                    this.StartProcess(names[0]);
                }
            });
        }

        protected static bool IsFileReadOnly(string fileName) => 
            File.Exists(fileName) && ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) != 0);

        public void SendFileByEmail(ExportOptionsBase options, IDictionary<Type, object[]> disabledExportModes)
        {
            this.CreateExportFiles(options, disabledExportModes, delegate (string[] fileNames) {
                if (fileNames.Length != 0)
                {
                    this.emailSender.Send(fileNames, this.ps.ExportOptions.Email);
                }
            });
        }

        protected abstract bool ShouldOpenExportedFile(PreviewStringId messageId, PreviewStringId captionId);
        private void StartProcess(string fileName)
        {
            if (File.Exists(fileName))
            {
                ActionAfterExport actionAfterExport = this.ps.ExportOptions.PrintPreview.ActionAfterExport;
                if (actionAfterExport == ActionAfterExport.Open)
                {
                    ProcessLaunchHelper.StartProcess(fileName, false);
                }
                else if ((actionAfterExport == ActionAfterExport.AskUser) && this.ShouldOpenExportedFile(PreviewStringId.Msg_OpenFileQuestion, PreviewStringId.Msg_OpenFileQuestionCaption))
                {
                    ProcessStartTrace.Trusted(() => ProcessLaunchHelper.StartProcess(fileName, false), fileName);
                }
            }
        }

        protected static string ValidateFileName(string fileName, string defaultFileName)
        {
            if (!File.Exists(fileName))
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return defaultFileName;
                }
                try
                {
                    string path = Path.Combine(Path.GetTempPath(), fileName);
                    File.Create(path).Close();
                    File.Delete(path);
                }
                catch (Exception)
                {
                    return defaultFileName;
                }
            }
            return fileName;
        }
    }
}

