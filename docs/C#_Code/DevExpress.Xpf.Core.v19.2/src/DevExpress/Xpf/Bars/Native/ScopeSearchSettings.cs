namespace DevExpress.Xpf.Bars.Native
{
    using System;

    [Flags]
    public enum ScopeSearchSettings
    {
        public const ScopeSearchSettings Ancestors = ScopeSearchSettings.Ancestors;,
        public const ScopeSearchSettings Local = ScopeSearchSettings.Local;,
        public const ScopeSearchSettings Descendants = ScopeSearchSettings.Descendants;
    }
}

