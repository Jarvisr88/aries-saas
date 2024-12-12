namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public static class LayoutControlStrategyRegistrator
    {
        public static void RegisterFlowLayoutControlStrategy()
        {
            StrategyManager.Default.RegisterStrategy<FlowLayoutControl, ItemsControlStrategy<FlowLayoutControl, FlowLayoutControlWrapper>>();
        }

        public static void RegisterTileLayoutControlStrategy()
        {
            StrategyManager.Default.RegisterStrategy<TileLayoutControl, TileLayoutControlStrategy>();
        }
    }
}

