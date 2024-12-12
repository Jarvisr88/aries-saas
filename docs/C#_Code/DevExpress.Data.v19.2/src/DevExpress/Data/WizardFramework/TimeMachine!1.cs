namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TimeMachine<T> where T: ICloneable
    {
        private readonly List<TimeMachine<T>.HistoryItem> historyItems;
        private int currentIndex;

        public TimeMachine(T initialValue);
        private void CommitCurrentValue();
        public void MoveToTheEndOfHistory();
        public void MoveToTheFuture();
        public void MoveToThePast();

        private bool AtEndOfHistory { get; }

        internal bool ShouldMoveToTheEndOfHistory { get; set; }

        public T CurrentValue { get; set; }

        private class HistoryItem
        {
            private T currentValue;

            public HistoryItem(T value);
            public void Commit();

            public T CurrentValue { get; set; }

            private T OriginalValue { get; set; }

            public bool IsDirty { get; }
        }
    }
}

