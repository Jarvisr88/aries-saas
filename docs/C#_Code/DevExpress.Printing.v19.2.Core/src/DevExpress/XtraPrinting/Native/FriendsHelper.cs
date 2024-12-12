namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class FriendsHelper
    {
        protected void CollectBands(int iterationCount, DocumentBand docBand, DocumentBand resultBand, Predicate<DocumentBand> callback);
        public virtual void CollectFriends(int iterationCount, DocumentBand docBand, DocumentBand resultBand);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FriendsHelper.<>c <>9;
            public static Predicate<DocumentBand> <>9__0_0;

            static <>c();
            internal bool <CollectFriends>b__0_0(DocumentBand lastBand);
        }
    }
}

