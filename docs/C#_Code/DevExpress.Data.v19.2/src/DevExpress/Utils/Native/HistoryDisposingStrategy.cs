namespace DevExpress.Utils.Native
{
    using System;

    public class HistoryDisposingStrategy
    {
        private static readonly HistoryDisposingStrategy executedItemsStrategy = new HistoryDisposingStrategy(true, false);
        private static readonly HistoryDisposingStrategy undoneItemsStrategy = new HistoryDisposingStrategy(false, true);
        private readonly bool canDisposeOldValue;
        private readonly bool canDisposeNewValue;

        private HistoryDisposingStrategy(bool canDisposeOldValue, bool canDisposeNewValue)
        {
            this.canDisposeOldValue = canDisposeOldValue;
            this.canDisposeNewValue = canDisposeNewValue;
        }

        public static HistoryDisposingStrategy ExecutedItemsStrategy =>
            executedItemsStrategy;

        public static HistoryDisposingStrategy UndoneItemsStrategy =>
            undoneItemsStrategy;

        public bool CanDisposeOldValue =>
            this.canDisposeOldValue;

        public bool CanDisposeNewValue =>
            this.canDisposeNewValue;
    }
}

