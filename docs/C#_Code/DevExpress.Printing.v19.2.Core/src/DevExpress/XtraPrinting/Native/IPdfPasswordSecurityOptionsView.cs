namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IPdfPasswordSecurityOptionsView
    {
        event EventHandler Dismiss;

        event EventHandler<RepeatPasswordCompleteEventArgs> RepeatPasswordComplete;

        event EventHandler RequireOpenPasswordChanged;

        event EventHandler RestrictPermissionsChanged;

        event EventHandler Submit;

        void Close();
        void EnableControl_OpenPassword(bool enable);
        void EnableControlGroup_Permissions(bool enable);
        void InitializeChangingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>> availablePermissions);
        void InitializePdfEncryptionLevel(IEnumerable<KeyValuePair<PdfEncryptionLevel, string>> availablePermissions);
        void InitializePrintingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>> availablePermissions);
        void PasswordDoesNotMatch();
        void RepeatOpenPassword();
        void RepeatPermissionsPassword();

        bool RequireOpenPassword { get; set; }

        string OpenPassword { get; set; }

        bool RestrictPermissions { get; set; }

        string PermissionsPassword { get; set; }

        PdfEncryptionLevel EncryptionLevel { get; set; }

        DevExpress.XtraPrinting.PrintingPermissions PrintingPermissions { get; set; }

        DevExpress.XtraPrinting.ChangingPermissions ChangingPermissions { get; set; }

        bool EnableCopying { get; set; }

        bool EnableScreenReaders { get; set; }
    }
}

