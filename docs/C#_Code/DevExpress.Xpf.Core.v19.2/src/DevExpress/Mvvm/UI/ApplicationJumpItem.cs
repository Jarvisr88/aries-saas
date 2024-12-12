namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.ComponentModel;

    public abstract class ApplicationJumpItem : DependencyObjectExt, ISupportInitialize, ICloneable, IApplicationJumpItemInfoSource, IApplicationJumpItem
    {
        private ApplicationJumpItemInfo itemInfo;

        public ApplicationJumpItem(ApplicationJumpItemInfo itemInfo)
        {
            if (itemInfo == null)
            {
                throw new ArgumentNullException("itemInfo");
            }
            this.ItemInfo = itemInfo;
        }

        public ApplicationJumpItem Clone()
        {
            ApplicationJumpItem clone = this.CreateInstanceCore();
            this.CloneCore(clone);
            return clone;
        }

        protected virtual void CloneCore(ApplicationJumpItem clone)
        {
            clone.ItemInfo = this.ItemInfo.Clone();
        }

        protected abstract ApplicationJumpItem CreateInstanceCore();
        public static ApplicationJumpItem GetItem(ApplicationJumpItemInfo itemInfo)
        {
            ApplicationJumpItem source = (ApplicationJumpItem) ((IApplicationJumpItemInfoInternal) itemInfo).Source;
            if (source != null)
            {
                return source;
            }
            ApplicationJumpPathInfo info = itemInfo as ApplicationJumpPathInfo;
            if (info != null)
            {
                return new ApplicationJumpPath(info);
            }
            ApplicationJumpTaskInfo info2 = itemInfo as ApplicationJumpTaskInfo;
            if (info2 == null)
            {
                throw new ArgumentException("itemInfo");
            }
            return new ApplicationJumpTask(info2);
        }

        public static ApplicationJumpItemInfo GetItemInfo(ApplicationJumpItem item) => 
            item.ItemInfo;

        protected virtual void OnItemInfoChanged(ApplicationJumpItemInfo oldItemInfo)
        {
            IApplicationJumpItemInfoInternal internal2 = oldItemInfo;
            if (internal2 != null)
            {
                internal2.Source = null;
            }
            IApplicationJumpItemInfoInternal itemInfo = this.ItemInfo;
            if (itemInfo != null)
            {
                itemInfo.Source = this;
            }
        }

        void ISupportInitialize.BeginInit()
        {
            ((ISupportInitialize) this.ItemInfo).BeginInit();
        }

        void ISupportInitialize.EndInit()
        {
            ((ISupportInitialize) this.ItemInfo).EndInit();
        }

        object ICloneable.Clone() => 
            this.Clone();

        public string CustomCategory
        {
            get => 
                this.ItemInfo.CustomCategory;
            set => 
                this.ItemInfo.CustomCategory = value;
        }

        protected ApplicationJumpItemInfo ItemInfo
        {
            get => 
                this.itemInfo;
            set
            {
                ApplicationJumpItemInfo itemInfo = this.ItemInfo;
                this.itemInfo = value;
                this.OnItemInfoChanged(itemInfo);
            }
        }
    }
}

