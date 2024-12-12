namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RenderStyleSetter : FreezableRenderObject
    {
        private string targetProperty;
        private object _value;
        private object convertedValue;
        private Type propertyType;
        private Action<object, object> setter;

        public void Apply(IFrameworkRenderElement element);
        protected override void FreezeOverride();

        public string TargetProperty { get; set; }

        public object Value { get; set; }

        public object ConvertedValue { [DebuggerStepThrough] get; }

        protected internal RenderStyle Style { get; set; }
    }
}

