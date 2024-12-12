namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BandIterator<TBand> : IEnumerator<TBand>, IDisposable, IEnumerator, IEnumerable<TBand>, IEnumerable where TBand: BandBase
    {
        private Stack<TBand> stack;
        private IBandsCollection startBands;
        private TBand current;

        public BandIterator(IBandsCollection bands)
        {
            this.stack = new Stack<TBand>();
            this.startBands = bands;
        }

        public bool MoveNext()
        {
            if ((this.current == null) && (this.startBands != null))
            {
                this.TraverseChildBands(this.startBands);
            }
            else if (this.startBands != null)
            {
                this.TraverseChildBands(this.current.BandsCore);
            }
            this.UpdateCurrent();
            return (this.current != null);
        }

        public void Reset()
        {
            this.stack.Clear();
            this.current = default(TBand);
        }

        IEnumerator<TBand> IEnumerable<TBand>.GetEnumerator() => 
            this;

        IEnumerator IEnumerable.GetEnumerator() => 
            this;

        void IDisposable.Dispose()
        {
            this.Reset();
        }

        private void TraverseChildBands(IBandsCollection bands)
        {
            int count = bands.Count;
            for (int i = 0; i < count; i++)
            {
                TBand item = bands[(count - 1) - i] as TBand;
                if (item != null)
                {
                    this.stack.Push(item);
                }
            }
        }

        private void UpdateCurrent()
        {
            TBand local1;
            if (this.stack.Count > 0)
            {
                local1 = this.stack.Pop();
            }
            else
            {
                local1 = default(TBand);
            }
            this.current = local1;
        }

        public TBand Current =>
            this.current;

        object IEnumerator.Current =>
            this.Current;
    }
}

