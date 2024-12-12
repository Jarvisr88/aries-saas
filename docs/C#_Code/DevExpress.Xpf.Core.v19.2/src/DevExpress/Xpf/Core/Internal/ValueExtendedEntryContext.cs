namespace DevExpress.Xpf.Core.Internal
{
    using System;

    internal abstract class ValueExtendedEntryContext : ExtendedEntryContext
    {
        protected ValueExtendedEntryContext(byte type);
        protected abstract string GetContextValue();
        protected abstract void SetContextValue(string value);

        public string Value { get; set; }
    }
}

