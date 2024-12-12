namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class CustomPropertyEntryContext : PropertyExtendedEntryContext
    {
        public CustomPropertyEntryContext();
        private string DeserializeBrush();
        protected override string GetContextValue();
        private bool IsBrush();
        private void ReadAttribute(BamlStreamReader reader);
        protected override void ReadData(BamlStreamReader reader);
        private void ReadValue(BamlStreamReader reader);
        private void SerializeColor(string value);
        protected override void SetContextValue(string value);
        protected override void WriteContent(BamlStreamWriter writer);

        public string PropertyName { get; private set; }

        public short Serializer { get; private set; }

        private byte[] Content { get; set; }
    }
}

