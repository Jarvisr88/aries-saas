namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FloatGroupSerializationInfo : LayoutGroupSerializationInfo
    {
        public FloatGroupSerializationInfo(FloatGroup owner) : base(owner)
        {
        }

        [XtraSerializableProperty]
        public Point FloatOffsetBeforeClose { get; set; }

        [XtraSerializableProperty]
        public DevExpress.Xpf.Docking.FloatState FloatState { get; set; }

        public FloatGroup Owner =>
            (FloatGroup) base.Owner;

        [XtraSerializableProperty]
        public DevExpress.Xpf.Docking.FloatState PreviousFloatState { get; set; }

        [XtraSerializableProperty]
        public Rect WindowRestoreBounds { get; set; }
    }
}

