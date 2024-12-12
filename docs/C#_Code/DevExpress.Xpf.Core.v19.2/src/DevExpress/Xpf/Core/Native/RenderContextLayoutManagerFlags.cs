namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum RenderContextLayoutManagerFlags
    {
        public const RenderContextLayoutManagerFlags Arrange = RenderContextLayoutManagerFlags.Arrange;,
        public const RenderContextLayoutManagerFlags Measure = RenderContextLayoutManagerFlags.Measure;,
        public const RenderContextLayoutManagerFlags All = RenderContextLayoutManagerFlags.All;
    }
}

