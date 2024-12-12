namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public abstract class OrderAttribute : Attribute
    {
        private int? order;

        protected OrderAttribute()
        {
        }

        public int? GetOrder() => 
            this.order;

        public int Order
        {
            get
            {
                if (this.order == null)
                {
                    throw new InvalidOperationException();
                }
                return this.order.Value;
            }
            set => 
                this.order = new int?(value);
        }
    }
}

