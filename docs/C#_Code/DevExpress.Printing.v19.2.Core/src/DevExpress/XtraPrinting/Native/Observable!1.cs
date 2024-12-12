namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class Observable<T>
    {
        private T value;
        private Func<T> getter;
        private Action<T> setter;
        [CompilerGenerated]
        private Action ValueChanged;

        public event Action ValueChanged
        {
            [CompilerGenerated] add
            {
                Action valueChanged = this.ValueChanged;
                while (true)
                {
                    Action comparand = valueChanged;
                    Action action3 = comparand + value;
                    valueChanged = Interlocked.CompareExchange<Action>(ref this.ValueChanged, action3, comparand);
                    if (ReferenceEquals(valueChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                Action valueChanged = this.ValueChanged;
                while (true)
                {
                    Action comparand = valueChanged;
                    Action action3 = comparand - value;
                    valueChanged = Interlocked.CompareExchange<Action>(ref this.ValueChanged, action3, comparand);
                    if (ReferenceEquals(valueChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public Observable() : this(local)
        {
            T local = default(T);
        }

        public Observable(T defaultValue)
        {
            this.value = defaultValue;
        }

        public void Assign(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RaiseValueChanged()
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged();
            }
        }

        public void Reset()
        {
            this.getter = null;
            this.setter = null;
        }

        public T Value
        {
            get => 
                (this.getter != null) ? this.getter() : this.value;
            set
            {
                if (this.setter != null)
                {
                    this.setter(value);
                }
                else
                {
                    this.value = value;
                }
                this.RaiseValueChanged();
            }
        }
    }
}

