namespace DevExpress.Xpf.Docking.Base
{
    using System;
    using System.Collections;

    public class MergedEnumerator : IEnumerator
    {
        private readonly IEnumerator[] Enumerators;
        private IEnumerator currentEnumerator;
        private int index = -1;

        public MergedEnumerator(IEnumerator[] enumerators)
        {
            this.Enumerators = enumerators;
        }

        private bool EnsureCurrentEnumerator()
        {
            if (this.currentEnumerator == null)
            {
                int num = this.index + 1;
                this.index = num;
                if (num < this.Enumerators.Length)
                {
                    this.currentEnumerator = this.Enumerators[this.index];
                }
            }
            return (this.currentEnumerator != null);
        }

        bool IEnumerator.MoveNext()
        {
            while (this.EnsureCurrentEnumerator())
            {
                if (this.currentEnumerator.MoveNext())
                {
                    return true;
                }
                this.currentEnumerator = null;
            }
            return false;
        }

        void IEnumerator.Reset()
        {
            this.currentEnumerator = null;
            this.index = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                if (this.currentEnumerator == null)
                {
                    throw new NotSupportedException();
                }
                return this.currentEnumerator.Current;
            }
        }
    }
}

