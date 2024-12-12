namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public abstract class HitInfoBase<T> where T: HitTestVisitorBase
    {
        private readonly DependencyObject d;
        private readonly DependencyObject root;

        protected HitInfoBase(DependencyObject d, DependencyObject root)
        {
            this.d = d;
            this.root = root;
        }

        public void Accept(T visitor)
        {
            if (visitor.HitTestingInProgressLocker.IsLocked)
            {
                throw new HitTestingIsInProgressException();
            }
            visitor.HitTestingInProgressLocker.DoLockedAction(delegate {
                DependencyObject d = ((HitInfoBase<T>) this).d;
                HitTestAcceptorBase<T> acceptor = null;
                visitor.CanContinue = true;
                while ((d != null) && !ReferenceEquals(d, ((HitInfoBase<T>) this).root))
                {
                    acceptor = ((HitInfoBase<T>) this).GetAcceptor(d);
                    if (acceptor != null)
                    {
                        visitor.SetHitElement(d);
                        acceptor.Accept(d as FrameworkElement, visitor);
                        if (!visitor.CanContinue)
                        {
                            break;
                        }
                    }
                    d = LayoutHelper.GetParent(d, false);
                }
            });
        }

        protected abstract HitTestAcceptorBase<T> GetAcceptor(DependencyObject treeElement);
    }
}

