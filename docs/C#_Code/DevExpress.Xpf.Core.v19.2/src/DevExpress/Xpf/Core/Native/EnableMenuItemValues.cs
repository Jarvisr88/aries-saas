namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    internal enum EnableMenuItemValues : uint
    {
        public const EnableMenuItemValues DOES_NOT_EXIST = 0xffffffff;,
        public const EnableMenuItemValues ENABLED = EnableMenuItemValues.BYCOMMAND;,
        public const EnableMenuItemValues BYCOMMAND = EnableMenuItemValues.BYCOMMAND;,
        public const EnableMenuItemValues GRAYED = (EnableMenuItemValues.BYCOMMAND | EnableMenuItemValues.GRAYED);,
        public const EnableMenuItemValues DISABLED = (EnableMenuItemValues.BYCOMMAND | EnableMenuItemValues.DISABLED);
    }
}

