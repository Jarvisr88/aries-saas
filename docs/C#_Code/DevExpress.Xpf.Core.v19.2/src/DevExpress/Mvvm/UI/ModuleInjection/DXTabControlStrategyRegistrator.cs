namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public static class DXTabControlStrategyRegistrator
    {
        public static void RegisterDXTabControlStrategy()
        {
            StrategyManager.Default.RegisterStrategy<DXTabControl, DXTabControlStrategy>();
        }
    }
}

