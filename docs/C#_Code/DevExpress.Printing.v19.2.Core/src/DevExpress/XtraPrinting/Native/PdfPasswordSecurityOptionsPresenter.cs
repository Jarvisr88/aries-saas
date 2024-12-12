namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;

    public class PdfPasswordSecurityOptionsPresenter
    {
        private PdfPasswordSecurityOptions options;
        private IPdfPasswordSecurityOptionsView view;
        private EventHandler<RepeatPasswordCompleteEventArgs> onRepeatPasswordComplete;

        public PdfPasswordSecurityOptionsPresenter(PdfPasswordSecurityOptions options, IPdfPasswordSecurityOptionsView view);
        public void Initialize();
        private void InitializeChangingPermissions();
        private void InitializeEncryptionLevel();
        private void InitializePrintingPermissions();
        private void RefreshView();
        private void RepeatPasswordComplete(string password, string repeatedPassword, Action0 postAction);
        private void ValidatePassword(bool usePassword, string password, Action0 repeatPassword, Action0 postAction);
        private void view_Dismiss(object sender, EventArgs e);
        private void view_RequireOpenPasswordChanged(object sender, EventArgs e);
        private void view_RestrictPermissionsChanged(object sender, EventArgs e);
        private void view_Submit(object sender, EventArgs e);

        private enum ValidationResult
        {
            public const PdfPasswordSecurityOptionsPresenter.ValidationResult Valid = PdfPasswordSecurityOptionsPresenter.ValidationResult.Valid;,
            public const PdfPasswordSecurityOptionsPresenter.ValidationResult Invalid = PdfPasswordSecurityOptionsPresenter.ValidationResult.Invalid;,
            public const PdfPasswordSecurityOptionsPresenter.ValidationResult Cancelled = PdfPasswordSecurityOptionsPresenter.ValidationResult.Cancelled;
        }
    }
}

