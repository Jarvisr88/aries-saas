namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public interface IByIntDictionary
    {
        void Add(int index, object value);
        bool ContainsKey(int index);
        bool ContainsValue(object value);
        int GetFirstFilledIndex(int startIndex, bool isBackward);
        bool TryGetKeyByValue(object value, out int index, int minIndex, int maxIndex);
        bool TryGetValue(int index, out object value);
    }
}

