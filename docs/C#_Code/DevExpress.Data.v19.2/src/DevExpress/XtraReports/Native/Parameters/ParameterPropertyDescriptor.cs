namespace DevExpress.XtraReports.Native.Parameters
{
    using DevExpress.Data;
    using DevExpress.Services.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ParameterPropertyDescriptor : PropertyDescriptor, IContainerComponent
    {
        private object temporaryValue;
        private object initialValue;
        private bool changed;
        private bool resetToInitialValue;

        public ParameterPropertyDescriptor(IParameter parameter);
        public ParameterPropertyDescriptor(IParameter parameter, bool resetToInitialValue);
        public bool CanReset();
        public override bool CanResetValue(object component);
        public void Commit();
        public static bool GetAllowNull(IParameter parameter);
        public override object GetValue(object component);
        public void Reset();
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        internal IParameter Parameter { get; }

        object IContainerComponent.Component { get; }

        public override Type ComponentType { get; }

        public override TypeConverter Converter { get; }

        public override Type PropertyType { get; }

        public override bool IsReadOnly { get; }

        public override AttributeCollection Attributes { get; }

        internal bool IsRangeParameter { get; }
    }
}

