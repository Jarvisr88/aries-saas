namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class GridControlResXLocalizer : DXResXLocalizer<GridControlStringId>
    {
        public GridControlResXLocalizer() : base(new GridControlLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Grid.Core.LocalizationRes", typeof(GridControlResXLocalizer).Assembly);
    }
}

