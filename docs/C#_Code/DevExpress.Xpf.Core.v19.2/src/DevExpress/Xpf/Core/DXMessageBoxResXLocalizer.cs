namespace DevExpress.Xpf.Core
{
    using System;
    using System.Resources;

    public class DXMessageBoxResXLocalizer : DXResXLocalizer<DXMessageBoxStringId>
    {
        public DXMessageBoxResXLocalizer() : base(new DXMessageBoxLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.Core.Window.DXMessageBoxRes", typeof(DXMessageBoxResXLocalizer).Assembly);
    }
}

