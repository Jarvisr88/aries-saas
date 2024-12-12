namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public static class DataControlStrategyRegistrator
    {
        public static void RegisterDataControlStrategy()
        {
            StrategyManager.Default.RegisterStrategy<DataControlBase, DataControlBaseStrategy>();
        }
    }
}

