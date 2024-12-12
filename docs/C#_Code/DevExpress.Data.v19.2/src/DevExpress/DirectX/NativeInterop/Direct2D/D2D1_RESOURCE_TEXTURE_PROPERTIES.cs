namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.CompilerServices;

    public class D2D1_RESOURCE_TEXTURE_PROPERTIES
    {
        private readonly int[] extents;
        private readonly int dimensions;
        private readonly D2D1_BUFFER_PRECISION bufferPrecision;
        private readonly D2D1_CHANNEL_DEPTH channelDepth;
        private readonly D2D1_FILTER filter;
        private readonly D2D1_EXTEND_MODE[] extendModes;

        public D2D1_RESOURCE_TEXTURE_PROPERTIES(int[] extents, int dimensions, D2D1_BUFFER_PRECISION bufferPrecision, D2D1_CHANNEL_DEPTH channelDepth, D2D1_FILTER filter, D2D1_EXTEND_MODE[] extendModes)
        {
            this.extents = extents;
            this.dimensions = dimensions;
            this.bufferPrecision = bufferPrecision;
            this.channelDepth = channelDepth;
            this.filter = filter;
            this.extendModes = extendModes;
        }

        public void Marshal(Action<D2D1_RESOURCE_TEXTURE_PROPERTIES_COMMON> action)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(this.extents))
            {
                Converter<D2D1_EXTEND_MODE, int> converter = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Converter<D2D1_EXTEND_MODE, int> local1 = <>c.<>9__7_0;
                    converter = <>c.<>9__7_0 = e => (int) e;
                }
                using (ArrayMarshaler marshaler2 = new ArrayMarshaler(Array.ConvertAll<D2D1_EXTEND_MODE, int>(this.extendModes, converter)))
                {
                    action(new D2D1_RESOURCE_TEXTURE_PROPERTIES_COMMON(marshaler.Pointer, this.dimensions, this.bufferPrecision, this.channelDepth, this.filter, marshaler2.Pointer));
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly D2D1_RESOURCE_TEXTURE_PROPERTIES.<>c <>9 = new D2D1_RESOURCE_TEXTURE_PROPERTIES.<>c();
            public static Converter<D2D1_EXTEND_MODE, int> <>9__7_0;

            internal int <Marshal>b__7_0(D2D1_EXTEND_MODE e) => 
                (int) e;
        }
    }
}

