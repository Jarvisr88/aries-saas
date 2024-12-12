namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class OperatorItemSubstitutionInfoProvider<TID, TOP> where TID: class
    {
        public OperatorItemSubstitutionInfoProvider(Func<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, TOP> createOperatorInfo, Func<TOP, string, TOP> updateGroupName, Func<TOP, string> getGroupName, Func<TOP, TID> getOperatorInfoID, Func<TOP, MultiFilterModelItem> createMultiModelItem)
        {
            Guard.ArgumentNotNull(createOperatorInfo, "createOperatorInfo");
            Guard.ArgumentNotNull(updateGroupName, "updateGroupName");
            Guard.ArgumentNotNull(getGroupName, "getGroupName");
            Guard.ArgumentNotNull(getOperatorInfoID, "getOperatorInfoID");
            Guard.ArgumentNotNull(createMultiModelItem, "createMultiModelItem");
            this.<CreateOperatorInfo>k__BackingField = createOperatorInfo;
            this.<UpdateGroupName>k__BackingField = updateGroupName;
            this.<GetGroupName>k__BackingField = getGroupName;
            this.<GetOperatorInfoID>k__BackingField = getOperatorInfoID;
            this.<CreateMultiModelItem>k__BackingField = createMultiModelItem;
        }

        public Func<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, TOP> CreateOperatorInfo { get; }

        public Func<TOP, string, TOP> UpdateGroupName { get; }

        public Func<TOP, string> GetGroupName { get; }

        public Func<TOP, TID> GetOperatorInfoID { get; }

        public Func<TOP, MultiFilterModelItem> CreateMultiModelItem { get; }
    }
}

