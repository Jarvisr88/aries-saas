namespace DevExpress.Office
{
    using System;

    public interface IIndexAccessor<TOwner, TInfo, TActions> : IIndexAccessorBase<TOwner, TActions> where TOwner: MultiIndexObject<TOwner, TActions> where TInfo: ICloneable<TInfo>, ISupportsSizeOf where TActions: struct
    {
        TInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper);
        TInfo GetInfo(TOwner owner);
        int GetInfoIndex(TOwner owner, TInfo value);
        void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, TInfo info);
    }
}

