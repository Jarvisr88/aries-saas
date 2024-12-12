namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    internal class BandsHelper
    {
        private IBandsOwner owner;

        public BandsHelper(IBandsOwner owner, bool subscribe)
        {
            this.owner = owner;
            if (subscribe)
            {
                owner.BandsCore.CollectionChanged += (d, e) => this.OnBandsChanged(e);
            }
            this.OnBandsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void InvalidateOwner(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (BandBase base2 in this.owner.BandsCore)
                {
                    base2.Owner = this.owner;
                }
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (BandBase base3 in e.OldItems)
                    {
                        base3.Owner = null;
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (BandBase base4 in e.NewItems)
                    {
                        base4.Owner = this.owner;
                    }
                }
            }
        }

        internal void OnBandsChanged(NotifyCollectionChangedEventArgs e)
        {
            DataControlBase dataControl = this.owner.DataControl;
            Action action = delegate {
                this.InvalidateOwner(e);
                this.owner.OnBandsChanged(e);
            };
            if (dataControl == null)
            {
                action();
            }
            else
            {
                dataControl.GetOriginationDataControl().syncPropertyLocker.DoLockedAction(action);
                Func<object, object> convertAction = <>c.<>9__2_2;
                if (<>c.<>9__2_2 == null)
                {
                    Func<object, object> local1 = <>c.<>9__2_2;
                    convertAction = <>c.<>9__2_2 = band => BandsLayoutBase.CloneBand((BandBase) band);
                }
                dataControl.GetDataControlOriginationElement().NotifyCollectionChanged(dataControl, dc => this.owner.FindClone(dc).BandsCore, convertAction, e);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandsHelper.<>c <>9 = new BandsHelper.<>c();
            public static Func<object, object> <>9__2_2;

            internal object <OnBandsChanged>b__2_2(object band) => 
                BandsLayoutBase.CloneBand((BandBase) band);
        }
    }
}

