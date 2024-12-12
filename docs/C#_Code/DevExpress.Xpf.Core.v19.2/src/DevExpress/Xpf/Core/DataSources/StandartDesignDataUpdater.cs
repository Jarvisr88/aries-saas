namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Windows;

    internal class StandartDesignDataUpdater : IDesignDataUpdater
    {
        public void UpdateDesignData(DependencyObject element)
        {
            IDesignDataUpdater updater = element as IDesignDataUpdater;
            if (updater != null)
            {
                updater.UpdateDesignData(element);
            }
        }
    }
}

