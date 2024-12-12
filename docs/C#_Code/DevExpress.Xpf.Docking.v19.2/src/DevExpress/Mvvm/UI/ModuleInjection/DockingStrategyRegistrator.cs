namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public static class DockingStrategyRegistrator
    {
        public static void RegisterAutoHideGroupStrategy()
        {
            StrategyManager.Default.RegisterStrategy<AutoHideGroup, LayoutGroupStrategy<AutoHideGroup, LayoutPanel, AutoHideGroupWrapper>>();
        }

        public static void RegisterDocumentGroupStrategy()
        {
            StrategyManager.Default.RegisterStrategy<DocumentGroup, LayoutGroupStrategy<DocumentGroup, DocumentPanel, DocumentGroupWrapper>>();
        }

        public static void RegisterLayoutGroupStrategy()
        {
            StrategyManager.Default.RegisterStrategy<LayoutGroup, LayoutGroupStrategy<LayoutGroup, LayoutPanel, LayoutGroupWrapper>>();
        }

        public static void RegisterLayoutPanelStrategy()
        {
            StrategyManager.Default.RegisterStrategy<LayoutPanel, LayoutPanelStrategy>();
        }
    }
}

