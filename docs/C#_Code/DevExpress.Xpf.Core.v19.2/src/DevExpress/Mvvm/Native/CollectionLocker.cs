namespace DevExpress.Mvvm.Native
{
    using System;

    internal class CollectionLocker
    {
        private bool locked;

        public void DoIfNotLocked(Action action)
        {
            if (!this.locked)
            {
                action();
            }
        }

        public void DoLockedAction(Action action)
        {
            this.locked = true;
            try
            {
                action();
            }
            finally
            {
                this.locked = false;
            }
        }

        public void DoLockedActionIfNotLocked(CollectionLocker possibleLocked, Action action)
        {
            possibleLocked.DoIfNotLocked(() => this.DoLockedAction(action));
        }
    }
}

