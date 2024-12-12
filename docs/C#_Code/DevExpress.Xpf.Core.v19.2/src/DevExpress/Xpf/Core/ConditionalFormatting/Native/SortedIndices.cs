namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public class SortedIndices
    {
        private readonly int[] indices;

        public SortedIndices(int[] indices)
        {
            this.indices = indices;
        }

        public int? GetThresholdListIndex(int count, bool topItems)
        {
            count = Math.Min(this.indices.Length, count);
            if (count == 0)
            {
                return null;
            }
            int index = topItems ? (this.indices.Length - count) : (count - 1);
            return new int?(this.indices[index]);
        }

        public bool IsTopBottomItem(FormatValueProvider provider, int count, bool topItems)
        {
            if (provider.ValueComparer == null)
            {
                return false;
            }
            int? thresholdListIndex = this.GetThresholdListIndex(count, topItems);
            if (thresholdListIndex == null)
            {
                return false;
            }
            int num = provider.ValueComparer.Compare(provider.Value, provider.GetValueByListIndex(thresholdListIndex.Value));
            return (!topItems ? (num <= 0) : (num >= 0));
        }

        public int Count =>
            this.indices.Length;
    }
}

