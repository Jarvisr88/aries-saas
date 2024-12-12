namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class HeaderFriendsHelper : FriendsHelper
    {
        public override void CollectFriends(int iterationCount, DocumentBand docBand, DocumentBand resultBand);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HeaderFriendsHelper.<>c <>9;
            public static Predicate<DocumentBand> <>9__0_0;

            static <>c();
            internal bool <CollectFriends>b__0_0(DocumentBand lastBand);
        }
    }
}

