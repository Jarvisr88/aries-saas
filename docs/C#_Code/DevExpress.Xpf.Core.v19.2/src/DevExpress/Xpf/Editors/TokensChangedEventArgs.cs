namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TokensChangedEventArgs : EventArgs
    {
        public TokensChangedEventArgs(IList<object> addedTokens, IList<object> removedTokens)
        {
            this.AddedTokens = addedTokens;
            this.RemovedTokens = removedTokens;
        }

        public IList<object> AddedTokens { get; private set; }

        public IList<object> RemovedTokens { get; private set; }
    }
}

