namespace DevExpress.Printing
{
    using DevExpress.XtraPrinting;
    using System;

    internal class SafeGetter<T>
    {
        private readonly Func<T> get;
        private readonly T defaultValue;
        private readonly bool catchException;
        private bool hasValue;
        private T value;

        public SafeGetter(Func<T> get, T defaultValue) : this(get, defaultValue, true)
        {
        }

        internal SafeGetter(Func<T> get, T defaultValue, bool catchException)
        {
            this.get = get;
            this.defaultValue = defaultValue;
            this.catchException = catchException;
        }

        public T Value
        {
            get
            {
                if (!this.hasValue)
                {
                    try
                    {
                        this.value = this.get();
                    }
                    catch (Exception exception)
                    {
                        Tracer.TraceError("DXperience.Reporting", exception);
                        if (!this.catchException)
                        {
                            throw;
                        }
                        this.value = this.defaultValue;
                    }
                    this.hasValue = true;
                }
                return this.value;
            }
        }
    }
}

