namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class Command
    {
        private volatile bool _canceled;
        private readonly Dictionary<object, object> _tags;
        private System.Exception _exception;
        private bool _IsResultDispatched;

        protected Command(DictionaryEntry[] tags);
        public abstract void Accept(IAsyncCommandVisitor visitor);
        public virtual void Cancel();
        public void Cancel(System.Exception exception);
        public void MarkResultDispatched();
        public bool TryGetTag<T>(object token, out T tag);

        public bool IsCanceled { get; }

        public System.Exception Exception { get; }

        public bool IsResultDispatched { get; }
    }
}

