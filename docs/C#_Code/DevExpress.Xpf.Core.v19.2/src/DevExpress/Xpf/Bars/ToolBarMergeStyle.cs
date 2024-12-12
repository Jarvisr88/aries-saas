namespace DevExpress.Xpf.Bars
{
    using System;

    [Flags]
    public enum ToolBarMergeStyle
    {
        public const ToolBarMergeStyle None = ToolBarMergeStyle.None;,
        public const ToolBarMergeStyle MainMenu = ToolBarMergeStyle.MainMenu;,
        public const ToolBarMergeStyle StatusBar = ToolBarMergeStyle.StatusBar;,
        public const ToolBarMergeStyle ToolBars = ToolBarMergeStyle.ToolBars;,
        public const ToolBarMergeStyle MainMenuAndStatusBar = ToolBarMergeStyle.MainMenuAndStatusBar;,
        public const ToolBarMergeStyle MainMenuAndToolBars = ToolBarMergeStyle.MainMenuAndToolBars;,
        public const ToolBarMergeStyle StatusBarAndToolBars = ToolBarMergeStyle.StatusBarAndToolBars;,
        public const ToolBarMergeStyle All = ToolBarMergeStyle.All;
    }
}

