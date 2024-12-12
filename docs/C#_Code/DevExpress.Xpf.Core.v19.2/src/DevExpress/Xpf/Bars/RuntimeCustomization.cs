namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class RuntimeCustomization
    {
        private static readonly Func<string, Type, DependencyProperty> fromName;
        private static readonly Dictionary<string, Func<RuntimeCustomization>> allocators;
        private readonly List<string> affectedTargets;
        private DependencyObject forcedTarget;

        static RuntimeCustomization();
        public RuntimeCustomization();
        public RuntimeCustomization(DependencyObject forcedTarget);
        public bool Apply(bool silent);
        protected abstract bool ApplyOverride(bool silent);
        public static RuntimeCustomization CreateInstance(string typeName);
        protected virtual DependencyObject FindTarget();
        protected DependencyProperty FromName(DependencyObject dObj, string propertyName);
        public static void RegisterCustomization<T>(Func<T> createInstance) where T: RuntimeCustomization;
        protected object StringToObject(string value, Type targetType);
        public bool TryOverwrite(RuntimeCustomization second);
        protected abstract bool TryOverwriteOverride(RuntimeCustomization second);
        public bool Undo();
        protected abstract bool UndoOverride();

        public bool Applied { get; private set; }

        [XtraSerializableProperty]
        public string TargetName { get; set; }

        [XtraSerializableProperty]
        public bool Overwrite { get; set; }

        [XtraSerializableProperty]
        public int Timestamp { get; set; }

        [XtraSerializableProperty]
        public string CustomizationType { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, false, 0, XtraSerializationFlags.None)]
        public List<string> AffectedTargets { get; }

        protected internal IRuntimeCustomizationHost Host { get; set; }

        public virtual bool IsInformativeCustomization { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeCustomization.<>c <>9;
            public static Func<IRuntimeCustomizationHost, RuntimeCustomizationCollection> <>9__38_0;

            static <>c();
            internal RuntimeCustomizationCollection <Apply>b__38_0(IRuntimeCustomizationHost x);
        }
    }
}

