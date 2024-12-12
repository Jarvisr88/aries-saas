namespace Devart.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Transactions;

    internal class j : ISinglePhaseNotification, IPromotableSinglePhaseNotification
    {
        private DbConnectionInternal a;
        private IsolationLevel b;
        [CompilerGenerated]
        private EventHandler c;

        public j(DbConnectionInternal A_0, IsolationLevel A_1)
        {
            this.a = A_0;
            this.b = A_1;
        }

        private void a()
        {
        }

        [CompilerGenerated]
        public void a(EventHandler A_0)
        {
            EventHandler c = this.c;
            while (true)
            {
                EventHandler comparand = c;
                EventHandler handler3 = comparand + A_0;
                c = Interlocked.CompareExchange<EventHandler>(ref this.c, handler3, comparand);
                if (ReferenceEquals(c, comparand))
                {
                    return;
                }
            }
        }

        public void a(Guid A_0)
        {
            this.a.BeginTransaction(A_0, this.b);
        }

        private void a(Enlistment A_0)
        {
            lock (this.a)
            {
                try
                {
                    this.a.Rollback();
                }
                catch
                {
                }
                A_0.Done();
            }
            this.b();
        }

        private void a(PreparingEnlistment A_0)
        {
            bool flag = false;
            try
            {
                this.a.PrepareCommit();
                flag = true;
                A_0.Prepared();
            }
            catch (Exception exception)
            {
                if (flag)
                {
                    throw;
                }
                A_0.ForceRollback(exception);
            }
        }

        private void a(SinglePhaseEnlistment A_0)
        {
            lock (this.a)
            {
                this.a.Commit();
                A_0.Committed();
            }
            this.b();
        }

        public void b()
        {
            if (this.c != null)
            {
                this.c(this, EventArgs.Empty);
            }
        }

        [CompilerGenerated]
        public void b(EventHandler A_0)
        {
            EventHandler c = this.c;
            while (true)
            {
                EventHandler comparand = c;
                EventHandler handler3 = comparand - A_0;
                c = Interlocked.CompareExchange<EventHandler>(ref this.c, handler3, comparand);
                if (ReferenceEquals(c, comparand))
                {
                    return;
                }
            }
        }

        private void b(Enlistment A_0)
        {
            A_0.Done();
            this.b();
        }

        private void b(SinglePhaseEnlistment A_0)
        {
            lock (this.a)
            {
                this.a.Commit();
                A_0.Committed();
            }
            this.b();
        }

        private byte[] c()
        {
            throw new TransactionPromotionException("There was an error promoting the transaction to a distributed transaction.");
        }

        private void c(Enlistment A_0)
        {
            lock (this.a)
            {
                try
                {
                    this.a.Commit();
                }
                catch
                {
                }
                A_0.Done();
            }
            this.b();
        }

        private void c(SinglePhaseEnlistment A_0)
        {
            Monitor.Enter(this.a);
            try
            {
                this.a.Rollback();
                A_0.Aborted();
            }
            catch (Exception exception)
            {
                A_0.Aborted(exception);
            }
            finally
            {
                DbConnectionInternal internal2;
                Monitor.Exit(internal2);
            }
            this.b();
        }
    }
}

