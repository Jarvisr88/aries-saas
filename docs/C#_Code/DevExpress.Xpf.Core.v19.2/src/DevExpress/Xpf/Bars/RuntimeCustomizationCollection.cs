namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RuntimeCustomizationCollection : ObservableCollection<RuntimeCustomization>
    {
        private IRuntimeCustomizationHost host;
        private readonly Locker executionLocker;

        public RuntimeCustomizationCollection(IRuntimeCustomizationHost host);
        public void Apply();
        public void Apply(int startingIndex);
        public void Apply(int startingIndex, Func<RuntimeCustomization, bool> skipCustomization, Func<RuntimeCustomization, bool> breakPredicate);
        protected override void InsertItem(int index, RuntimeCustomization item);
        public IDisposable Lock();
        public void OnItemApplied(RuntimeCustomization item);
        private int OnItemApplied(int index, RuntimeCustomization item);
        public void OverwriteCustomizations();
        protected override void RemoveItem(int index);
        public void RemoveUninformativeCustomizations();
        public void Undo(bool clear = false, bool nonappliedOnly = false);
        public void Undo(int startingIndex, bool clear = false, bool nonAppliedOnly = false);
        public void Unlock();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeCustomizationCollection.<>c <>9;
            public static Func<RuntimeCustomization, bool> <>9__8_0;
            public static Func<RuntimeCustomization, bool> <>9__8_1;

            static <>c();
            internal bool <Apply>b__8_0(RuntimeCustomization x);
            internal bool <Apply>b__8_1(RuntimeCustomization x);
        }
    }
}

