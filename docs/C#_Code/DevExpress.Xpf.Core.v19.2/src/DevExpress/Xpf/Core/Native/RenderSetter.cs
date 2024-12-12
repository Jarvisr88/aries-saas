namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderSetter : IRenderSetterFactory
    {
        private object value;
        private bool isInitialized;

        public RenderSetter();
        private object CoerceValue(object baseValue);
        public void InitializeConvertedValue(object context);

        public string Property { get; set; }

        public bool ThrowIfInvalidTargetName { get; set; }

        public object Value { get; set; }

        public string TargetName { get; set; }

        public object ConvertedValue { get; private set; }
    }
}

