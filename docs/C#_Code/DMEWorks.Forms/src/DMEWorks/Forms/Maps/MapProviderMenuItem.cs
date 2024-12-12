namespace DMEWorks.Forms.Maps
{
    using System;
    using System.Windows.Forms;

    public class MapProviderMenuItem : ToolStripMenuItem
    {
        private MapProvider _provider;

        public MapProviderMenuItem(MapProvider provider) : base(Validate(provider).Name, Validate(provider).Image)
        {
            this._provider = provider;
        }

        public MapProviderMenuItem(MapProvider provider, EventHandler onClick) : base(Validate(provider).Name, Validate(provider).Image, onClick)
        {
            this._provider = provider;
        }

        private static MapProvider Validate(MapProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            return provider;
        }

        public MapProvider Provider =>
            this._provider;
    }
}

