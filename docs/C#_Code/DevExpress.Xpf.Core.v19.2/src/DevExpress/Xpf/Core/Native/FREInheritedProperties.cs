namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FREInheritedProperties
    {
        private static int lastAllocated;
        private static readonly List<FREInheritedPropertyRecord> records;
        private readonly FREInheritedPropertyValue[] values;

        static FREInheritedProperties();
        public FREInheritedProperties(FrameworkRenderElementContext context);
        public void CoerceValue(int key);
        public void CoerceValues();
        public object GetValue(int key);
        public FREInheritedPropertyValueSource GetValueSource(int key);
        public static int Register(object defaultValue = null, Action<FrameworkRenderElementContext, object, object> propertyChangedCallback = null, Func<FrameworkRenderElementContext, object, FREInheritedPropertyValueSource, object> coerceValueCallback = null);
        public void SetCurrentValue(int key, object value, FREInheritedPropertyValueSource source);
        public void SetValue(int key, object value);

        public FrameworkRenderElementContext Context { get; private set; }
    }
}

