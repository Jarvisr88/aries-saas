namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RenderTriggerContextCollection : Collection<RenderTriggerContextBase>
    {
        private readonly RenderTriggersCollection source;
        private readonly Namescope namescope;
        private readonly Dictionary<RenderTriggerContextCollection.RenderSetterKey, RenderSetterChain> setterChains;
        private readonly Locker invalidationLocker;
        private readonly HashSet<RenderTriggerContextBase> invalidTriggers;

        public RenderTriggerContextCollection(RenderTriggersCollection source, Namescope namescope);
        public void Destroy();
        public void Initialize();
        private void InvalidationLockerOnUnlocked(object sender, EventArgs e);
        public IDisposable LockInvalidations();
        public virtual void OnInvalidated(RenderTriggerContextBase context);
        protected virtual void PerformInvalidations();
        public void UnlockInvalidations();

        [StructLayout(LayoutKind.Sequential)]
        private struct RenderSetterKey
        {
            private bool Equals(RenderTriggerContextCollection.RenderSetterKey other);
            public override bool Equals(object obj);
            public override int GetHashCode();
            public static bool operator ==(RenderTriggerContextCollection.RenderSetterKey left, RenderTriggerContextCollection.RenderSetterKey right);
            public static bool operator !=(RenderTriggerContextCollection.RenderSetterKey left, RenderTriggerContextCollection.RenderSetterKey right);
            public RenderSetterKey(RenderSetterContext source);
            public string Name { get; private set; }
            public string Property { get; private set; }
        }
    }
}

