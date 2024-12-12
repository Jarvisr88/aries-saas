namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class SuperTipItemsCollection : ObservableCollection<SuperTipItemBase>
    {
        public SuperTipItemsCollection(DevExpress.Xpf.Core.SuperTip superTip)
        {
            this.SuperTip = superTip;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (e.OldItems != null)
            {
                foreach (object obj2 in e.OldItems)
                {
                    this.SuperTip.RemoveChild(obj2);
                }
            }
            if (e.NewItems != null)
            {
                foreach (object obj3 in e.NewItems)
                {
                    this.SuperTip.AddChild(obj3);
                }
            }
        }

        public DevExpress.Xpf.Core.SuperTip SuperTip { get; set; }
    }
}

