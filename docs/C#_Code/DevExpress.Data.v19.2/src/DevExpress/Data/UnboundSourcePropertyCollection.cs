namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [ListBindable(BindableSupport.No)]
    public sealed class UnboundSourcePropertyCollection : CollectionBase
    {
        private int muteReconfiguration;
        private int propertyNameCounter;
        private readonly UnboundSource owner;
        private UnboundSourceProperty[] beforeClearItems;

        public UnboundSourcePropertyCollection(UnboundSource owner);
        public void Add(UnboundSourceProperty property);
        public void Add(UnboundSourcePropertyCollection properties);
        public void AddRange(IEnumerable<UnboundSourceProperty> properties);
        public void ClearAndAddRange(IEnumerable<UnboundSourceProperty> properties);
        private IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> CreateDescriptors();
        private string GetNextNewPropertyName();
        private void Mute();
        protected override void OnClear();
        protected override void OnClearComplete();
        protected override void OnInsertComplete(int index, object value);
        protected override void OnRemoveComplete(int index, object value);
        protected override void OnSetComplete(int index, object oldValue, object newValue);
        internal void ReconfigureView();
        private void UnMute();

        public UnboundSourceProperty this[int index] { get; }

        public UnboundSourceProperty this[string name] { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UnboundSourcePropertyCollection.<>c <>9;
            public static Action<UnboundSourceProperty> <>9__13_0;
            public static Func<UnboundSourceProperty, UnboundSourceCore.PropertyDescriptorDescriptor> <>9__22_0;

            static <>c();
            internal UnboundSourceCore.PropertyDescriptorDescriptor <CreateDescriptors>b__22_0(UnboundSourceProperty p);
            internal void <OnClearComplete>b__13_0(UnboundSourceProperty p);
        }
    }
}

