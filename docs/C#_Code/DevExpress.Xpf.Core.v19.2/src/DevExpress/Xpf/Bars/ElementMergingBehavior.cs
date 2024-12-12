namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(ElementMergingBehaviorTypeConverter))]
    public enum ElementMergingBehavior
    {
        public const ElementMergingBehavior Undefined = ElementMergingBehavior.Undefined;,
        public const ElementMergingBehavior None = ElementMergingBehavior.None;,
        public const ElementMergingBehavior All = ElementMergingBehavior.All;,
        public const ElementMergingBehavior InternalWithInternal = ElementMergingBehavior.InternalWithInternal;,
        public const ElementMergingBehavior InternalWithExternal = ElementMergingBehavior.InternalWithExternal;
    }
}

