namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class DocumentBandExtensions
    {
        [IteratorStateMachine(typeof(DocumentBandExtensions.<AllNodes>d__5))]
        public static IEnumerable<DocumentBand> AllNodes(this DocumentBand band);
        private static void CollectIndices(IList<int> indices, DocumentBand documentBand);
        public static DocumentBand CopyBand(this DocumentBand band, int rowIndex);
        public static int GetBandsCountRecursive(this DocumentBand band);
        public static int GetID(this DocumentBand band);
        public static int[] GetPath(this DocumentBand band);
        public static PrintingSystemBase GetPrintingSystem(this DocumentBand docBand);
        public static bool IsCompleted(this DocumentBand docBand, IPageBuildInfoService serv);
        public static bool IsFooter(this DocumentBandKind kind);
        public static bool IsHeader(this DocumentBandKind kind);
        public static bool TryCollectFriends(this DocumentBand docBand, out DocumentBand resultBand);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentBandExtensions.<>c <>9;
            public static Func<DocumentBand, bool> <>9__0_0;

            static <>c();
            internal bool <IsCompleted>b__0_0(DocumentBand band);
        }

        [CompilerGenerated]
        private sealed class <AllNodes>d__5 : IEnumerable<DocumentBand>, IEnumerable, IEnumerator<DocumentBand>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DocumentBand <>2__current;
            private int <>l__initialThreadId;
            private DocumentBand band;
            public DocumentBand <>3__band;
            private DocumentBand <item>5__1;

            [DebuggerHidden]
            public <AllNodes>d__5(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<DocumentBand> IEnumerable<DocumentBand>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            DocumentBand IEnumerator<DocumentBand>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

