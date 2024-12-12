namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class ConvertedTextEntryContext : ValueExtendedEntryContext
    {
        public ConvertedTextEntryContext();
        protected override string GetContextValue();
        protected override void ReadData(BamlStreamReader reader);
        protected override void SetContextValue(string value);
        protected override void WriteContent(BamlStreamWriter writer);

        private short Converter { get; set; }

        private string InnerValue { get; set; }
    }
}

