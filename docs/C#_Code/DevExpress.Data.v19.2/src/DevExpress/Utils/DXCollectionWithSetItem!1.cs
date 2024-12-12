namespace DevExpress.Utils
{
    using System;
    using System.Reflection;

    public class DXCollectionWithSetItem<T> : DXCollectionBase<T>
    {
        public DXCollectionWithSetItem()
        {
        }

        protected DXCollectionWithSetItem(DXCollectionUniquenessProviderType uniquenessProviderType) : base(uniquenessProviderType)
        {
        }

        public virtual T this[int index]
        {
            get => 
                this.GetItem(index);
            set => 
                this.SetItem(index, value);
        }
    }
}

