namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TokensChangingEventArgs : TokensChangedEventArgs
    {
        public TokensChangingEventArgs(IList<object> addedTokens, IList<object> removedTokens) : base(addedTokens, removedTokens)
        {
        }

        public bool IsCancel { get; set; }
    }
}

