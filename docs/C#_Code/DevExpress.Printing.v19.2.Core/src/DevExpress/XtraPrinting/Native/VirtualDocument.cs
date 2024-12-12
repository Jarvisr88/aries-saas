namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class VirtualDocument : DeserializedDocument
    {
        protected Stream independentPagesStream;
        private bool headerLoaded;
        private bool disposeStream;
        private bool forced;

        public VirtualDocument(PrintingSystemBase ps, Stream independentPagesStream, bool disposeStream);
        protected override void CreateSerializationObjects();
        private void DeserializePart(DocumentSerializationCollection collection);
        public override void Dispose();
        protected internal override void ForceLoad();
        protected internal override void LoadPage(int pageIndex);
        protected override void NullDeserializationCaches();
        internal override void Serialize(Stream stream, XtraSerializer serializer);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VirtualDocument.<>c <>9;
            public static Predicate<int> <>9__4_0;

            static <>c();
            internal bool <.ctor>b__4_0(int index);
        }
    }
}

