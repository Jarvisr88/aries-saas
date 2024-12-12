namespace DevExpress.Mvvm
{
    using System;

    public class ApplicationJumpPathInfo : ApplicationJumpItemInfo, IApplicationJumpPath, IApplicationJumpItem
    {
        private string path;

        public ApplicationJumpPathInfo Clone() => 
            (ApplicationJumpPathInfo) base.Clone();

        protected override void CloneCore(ApplicationJumpItemInfo clone)
        {
            base.CloneCore(clone);
            ((ApplicationJumpPathInfo) clone).Path = this.Path;
        }

        protected override ApplicationJumpItemInfo CreateInstanceCore() => 
            new ApplicationJumpPathInfo();

        public string Path
        {
            get => 
                this.path;
            set
            {
                base.AssertIsNotInitialized();
                this.path = value;
            }
        }
    }
}

