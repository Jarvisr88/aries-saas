namespace DevExpress.Office.Utils
{
    using System;

    public abstract class BackgroundThreadUIUpdater
    {
        protected BackgroundThreadUIUpdater()
        {
        }

        public abstract void UpdateUI(Action method);
    }
}

