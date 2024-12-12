namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class DockingResXLocalizer : DXResXLocalizer<DockingStringId>
    {
        public DockingResXLocalizer() : base(new DockingLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Docking.LocalizationRes", typeof(DockingResXLocalizer).Assembly);
    }
}

