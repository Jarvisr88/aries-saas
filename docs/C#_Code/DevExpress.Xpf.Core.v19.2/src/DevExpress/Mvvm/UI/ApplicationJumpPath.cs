namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;

    public class ApplicationJumpPath : ApplicationJumpItem, IApplicationJumpPath, IApplicationJumpItem
    {
        public ApplicationJumpPath() : this(new ApplicationJumpPathInfo())
        {
        }

        public ApplicationJumpPath(ApplicationJumpPathInfo itemInfo) : base(itemInfo)
        {
        }

        public ApplicationJumpPath Clone() => 
            (ApplicationJumpPath) base.Clone();

        protected override ApplicationJumpItem CreateInstanceCore() => 
            new ApplicationJumpPath();

        public string Path
        {
            get => 
                this.ItemInfo.Path;
            set => 
                this.ItemInfo.Path = value;
        }

        protected ApplicationJumpPathInfo ItemInfo =>
            (ApplicationJumpPathInfo) base.ItemInfo;
    }
}

