namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class RuntimeCustomizationHostSerializationStrategy<THost> : BaseBarManagerSerializationStrategyGeneric<THost> where THost: DependencyObject, IRuntimeCustomizationHost
    {
        private PostponedAction applyAction;

        static RuntimeCustomizationHostSerializationStrategy();
        public RuntimeCustomizationHostSerializationStrategy(THost owner);
        protected virtual void ApplyCustomizations();
        protected virtual void InitializeElementNames();
        protected override void OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e);
        protected override void OnEndDeserializing(EndDeserializingEventArgs e);
        protected virtual void OnScopeWalkerChanged(bool hasWalker);
        private static void OnScopeWalkerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected override void OnStartDeserializing(StartDeserializingEventArgs e);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeCustomizationHostSerializationStrategy<THost>.<>c <>9;
            public static Action<DependencyObject> <>9__5_0;

            static <>c();
            internal RuntimePropertyCustomization <.cctor>b__0_0();
            internal RuntimeUndoCustomization <.cctor>b__0_1();
            internal RuntimeCopyLinkCustomization <.cctor>b__0_2();
            internal RuntimeRemoveLinkCustomization <.cctor>b__0_3();
            internal RuntimeCreateNewBarCustomization <.cctor>b__0_4();
            internal void <InitializeElementNames>b__5_0(DependencyObject x);
        }
    }
}

