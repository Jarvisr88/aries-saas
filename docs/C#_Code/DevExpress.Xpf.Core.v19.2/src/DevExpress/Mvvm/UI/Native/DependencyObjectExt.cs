namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Windows;

    public abstract class DependencyObjectExt : Freezable
    {
        protected DependencyObjectExt()
        {
        }

        protected sealed override void CloneCore(Freezable sourceFreezable)
        {
            base.CloneCore(sourceFreezable);
        }

        protected sealed override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            base.CloneCurrentValueCore(sourceFreezable);
        }

        protected sealed override Freezable CreateInstanceCore() => 
            this;

        protected sealed override bool FreezeCore(bool isChecking) => 
            base.FreezeCore(isChecking);

        protected sealed override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            base.GetAsFrozenCore(sourceFreezable);
        }

        protected sealed override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            base.GetCurrentValueAsFrozenCore(sourceFreezable);
        }
    }
}

