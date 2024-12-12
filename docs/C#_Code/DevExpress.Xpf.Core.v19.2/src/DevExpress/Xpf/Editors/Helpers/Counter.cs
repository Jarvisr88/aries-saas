namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public class Counter
    {
        private int innerCounter;

        public Counter()
        {
            this.Reset();
        }

        public void Increment()
        {
            this.innerCounter++;
        }

        public void Reset()
        {
            this.innerCounter = 0;
        }

        public int Value =>
            this.innerCounter;

        public bool IsClear =>
            this.innerCounter == 0;
    }
}

