namespace DevExpress.Xpf.Bars
{
    using System;

    [Flags]
    public enum ActionExecutionMode
    {
        public const ActionExecutionMode Manual = ActionExecutionMode.Manual;,
        public const ActionExecutionMode OnAssociatedObjectChanged = ActionExecutionMode.OnAssociatedObjectChanged;,
        public const ActionExecutionMode OnEvent = ActionExecutionMode.OnEvent;,
        public const ActionExecutionMode All = ActionExecutionMode.All;
    }
}

