namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ApplicationJumpItemInfo : ISupportInitialize, ICloneable, IApplicationJumpItemInfoInternal, IApplicationJumpItem
    {
        private string customCategory;

        protected ApplicationJumpItemInfo()
        {
        }

        protected void AssertIsNotInitialized()
        {
            if (this.IsInitialized)
            {
                throw new InvalidOperationException();
            }
        }

        public ApplicationJumpItemInfo Clone()
        {
            ApplicationJumpItemInfo clone = this.CreateInstanceCore();
            this.CloneCore(clone);
            return clone;
        }

        protected virtual void CloneCore(ApplicationJumpItemInfo clone)
        {
            clone.CustomCategory = this.CustomCategory;
        }

        protected abstract ApplicationJumpItemInfo CreateInstanceCore();
        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            this.IsInitialized = true;
        }

        object ICloneable.Clone() => 
            this.Clone();

        public string CustomCategory
        {
            get => 
                this.customCategory;
            set
            {
                this.AssertIsNotInitialized();
                this.customCategory = value;
            }
        }

        protected IApplicationJumpItemInfoSource Source { get; private set; }

        protected bool IsInitialized { get; private set; }

        IApplicationJumpItemInfoSource IApplicationJumpItemInfoInternal.Source
        {
            get => 
                this.Source;
            set => 
                this.Source = value;
        }
    }
}

