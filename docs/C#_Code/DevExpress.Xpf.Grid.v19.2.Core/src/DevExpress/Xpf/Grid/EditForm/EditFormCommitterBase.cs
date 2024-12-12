namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class EditFormCommitterBase
    {
        private Locker commitLocker = new Locker();

        protected EditFormCommitterBase()
        {
        }

        public bool Commit()
        {
            if (this.commitLocker.IsLocked)
            {
                return false;
            }
            this.IsActive = true;
            bool flag = false;
            try
            {
                flag = this.CommitCore();
            }
            finally
            {
                this.IsActive = false;
            }
            return flag;
        }

        protected abstract bool CommitCore();
        public void DoCommitProtectedAction(Action action)
        {
            this.commitLocker.DoLockedAction(action);
        }

        public bool IsActive { get; private set; }

        internal bool IsLocked =>
            this.commitLocker.IsLocked;
    }
}

