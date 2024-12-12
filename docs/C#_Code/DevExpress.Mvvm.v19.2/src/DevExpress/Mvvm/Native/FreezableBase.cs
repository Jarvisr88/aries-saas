namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class FreezableBase : BindableBase
    {
        protected FreezableBase()
        {
        }

        protected override PropertyManager CreatePropertyManager() => 
            new FreezablePropertyManager(() => this.IsFrozen);

        public void Freeze()
        {
            if (!this.IsFrozen)
            {
                this.FreezeCore();
                this.IsFrozen = true;
                base.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(FreezableBase)), (MethodInfo) methodof(FreezableBase.get_IsFrozen)), new ParameterExpression[0]));
            }
        }

        protected abstract void FreezeCore();
        public static void ThrowCannotModifyFrozenObject()
        {
            throw new InvalidOperationException("Cannot modify a frozen object.");
        }

        public bool IsFrozen { get; private set; }

        public class FreezableCollectionBase<T> : ObservableCollection<T> where T: FreezableBase
        {
            public FreezableCollectionBase()
            {
            }

            public FreezableCollectionBase(IEnumerable<T> collection) : base(collection)
            {
            }

            public FreezableCollectionBase(List<T> list) : base(list)
            {
            }

            public void Freeze()
            {
                if (!this.IsFrozen)
                {
                    this.FreezeCore();
                    this.IsFrozen = true;
                }
            }

            private void FreezeCore()
            {
                foreach (T local in this)
                {
                    local.Freeze();
                }
            }

            protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                if (this.IsFrozen)
                {
                    FreezableBase.ThrowCannotModifyFrozenObject();
                }
                base.OnCollectionChanged(e);
            }

            public bool IsFrozen { get; private set; }
        }

        private class FreezablePropertyManager : PropertyManager
        {
            private readonly Func<bool> getIsFrozen;

            public FreezablePropertyManager(Func<bool> getIsFrozen)
            {
                this.getIsFrozen = getIsFrozen;
            }

            protected override bool SetPropertyCore<T>(ref T storage, T value, string propertyName)
            {
                if (this.getIsFrozen())
                {
                    FreezableBase.ThrowCannotModifyFrozenObject();
                }
                return base.SetPropertyCore<T>(ref storage, value, propertyName);
            }

            protected override bool SetPropertyCore<T>(string propertyName, T value, out T oldValue)
            {
                if (this.getIsFrozen())
                {
                    FreezableBase.ThrowCannotModifyFrozenObject();
                }
                return base.SetPropertyCore<T>(propertyName, value, out oldValue);
            }
        }
    }
}

