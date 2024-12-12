namespace DevExpress.Mvvm.Native
{
    using System;

    public static class EmptyArray<TElement>
    {
        public static readonly TElement[] Instance;

        static EmptyArray()
        {
            EmptyArray<TElement>.Instance = new TElement[0];
        }
    }
}

