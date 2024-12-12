namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum RenderEvents
    {
        public const RenderEvents PreviewMouseUp = RenderEvents.PreviewMouseUp;,
        public const RenderEvents PreviewMouseDown = RenderEvents.PreviewMouseDown;,
        public const RenderEvents MouseDown = RenderEvents.MouseDown;,
        public const RenderEvents MouseUp = RenderEvents.MouseUp;,
        public const RenderEvents MouseEnter = RenderEvents.MouseEnter;,
        public const RenderEvents MouseLeave = RenderEvents.MouseLeave;,
        public const RenderEvents MouseMove = RenderEvents.MouseMove;
    }
}

