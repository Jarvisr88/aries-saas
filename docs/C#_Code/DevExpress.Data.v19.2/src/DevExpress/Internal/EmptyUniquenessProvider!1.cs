namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;

    public class EmptyUniquenessProvider<T> : DXCollectionUniquenessProvider<T>
    {
        public static readonly DXCollectionUniquenessProvider<T> Instance;

        static EmptyUniquenessProvider()
        {
            EmptyUniquenessProvider<T>.Instance = new EmptyUniquenessProvider<T>();
        }

        public override bool LookupObject(T value) => 
            false;

        public override int LookupObjectIndex(T value) => 
            -1;

        public override void OnClearComplete()
        {
        }

        public override void OnInsertComplete(T value)
        {
        }

        public override void OnRemoveComplete(T value)
        {
        }

        public override void OnSetComplete(T oldValue, T newValue)
        {
        }

        public override DXCollectionUniquenessProviderType Type =>
            DXCollectionUniquenessProviderType.None;
    }
}

