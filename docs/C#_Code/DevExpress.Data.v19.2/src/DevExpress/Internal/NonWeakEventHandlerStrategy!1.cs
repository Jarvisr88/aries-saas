namespace DevExpress.Internal
{
    using System;

    public class NonWeakEventHandlerStrategy<TArgs> : IWeakEventHandlerStrategy<TArgs> where TArgs: EventArgs
    {
        private Delegate handler;

        public void Add(Delegate target)
        {
            this.handler += target;
        }

        public void Purge()
        {
        }

        public void Raise(object sender, TArgs args)
        {
            if (this.handler != null)
            {
                object[] objArray1 = new object[] { sender, args };
                this.handler.DynamicInvoke(objArray1);
            }
        }

        public void Remove(Delegate target)
        {
            this.handler -= target;
        }

        public bool IsEmpty =>
            ReferenceEquals(this.handler, null);
    }
}

