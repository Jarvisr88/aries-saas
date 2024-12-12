namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum RenderHitTestFilterBehavior
    {
        public const RenderHitTestFilterBehavior Continue = RenderHitTestFilterBehavior.Continue;,
        public const RenderHitTestFilterBehavior ContinueSkipChildren = RenderHitTestFilterBehavior.ContinueSkipChildren;,
        public const RenderHitTestFilterBehavior ContinueSkipSelf = RenderHitTestFilterBehavior.ContinueSkipSelf;,
        public const RenderHitTestFilterBehavior ContinueSkipSelfAndChildren = RenderHitTestFilterBehavior.ContinueSkipSelfAndChildren;,
        public const RenderHitTestFilterBehavior Stop = RenderHitTestFilterBehavior.Stop;
    }
}

