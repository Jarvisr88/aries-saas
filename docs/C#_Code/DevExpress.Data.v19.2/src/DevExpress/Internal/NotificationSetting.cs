namespace DevExpress.Internal
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public enum NotificationSetting
    {
        Enabled,
        DisabledForApplication,
        DisabledForUser,
        DisabledByGroupPolicy,
        DisabledByManifest
    }
}

