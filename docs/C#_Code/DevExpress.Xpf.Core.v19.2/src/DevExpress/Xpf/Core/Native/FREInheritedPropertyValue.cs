namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class FREInheritedPropertyValue
    {
        private readonly FREInheritedPropertyRecord record;
        private readonly FREInheritedProperties owner;
        private readonly int key;
        private object cachedCoercedValue;
        private object cachedInheritedValue;
        private object cachedLocalValue;
        private object cachedValue;

        public FREInheritedPropertyValue(FREInheritedPropertyRecord record, FREInheritedProperties owner, int key);
        public void ClearValue();
        public void CoerceValue();
        public object GetValue();
        private void PropagateValue(FREInheritedPropertyValueSource valueSource);
        public void SetCurrentValue(object value, FREInheritedPropertyValueSource valueSource);
        public void SetValue(object value);
        private void ValueChanged(object oldValue, object newValue, FREInheritedPropertyValueSource newValueSource);

        public FREInheritedPropertyValueSource ValueSource { get; set; }

        public int Key { get; }

        public bool IsDefaultValue { get; }
    }
}

