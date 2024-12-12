namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class ConvertedPropertyEntryContext : PropertyExtendedEntryContext
    {
        public ConvertedPropertyEntryContext();
        protected override string GetContextValue();
        protected override void ReadData(BamlStreamReader reader);
        protected override void SetContextValue(string value);
        protected override void WriteContent(BamlStreamWriter writer);

        private short TypeOfConverter { get; set; }

        private string InnerValue { get; set; }
    }
}

