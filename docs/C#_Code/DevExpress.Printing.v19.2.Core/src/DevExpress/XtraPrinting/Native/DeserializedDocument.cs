namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class DeserializedDocument : PSDocument
    {
        private ContinuousExportInfo continuousExportInfo;

        public DeserializedDocument(PrintingSystemBase ps);
        internal override void ClearContent();
        protected internal override ContinuousExportInfo GetContinuousExportInfo();
        internal override ContinuousExportInfo GetDeserializationContinuousExportInfo();
        protected override object GetObjectByIndexCore(string propertyName, int index);
        protected virtual void NullDeserializationCaches();
        protected override void OnEndDeserializingCore();
        protected override void OnEndSerializingCore();
        protected override void OnStartDeserializingCore();

        internal bool Deserializing { get; set; }

        public override bool CanPerformContinuousExport { get; }

        public override bool IsEmpty { get; }
    }
}

