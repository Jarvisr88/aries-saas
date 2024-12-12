namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DeferredBackgroundThreadUIUpdater : BackgroundThreadUIUpdater
    {
        private readonly List<Action> updates = new List<Action>();

        public override void UpdateUI(Action method)
        {
            if (!this.updates.Contains(method))
            {
                this.updates.Add(method);
            }
        }

        public List<Action> Updates =>
            this.updates;
    }
}

