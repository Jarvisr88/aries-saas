namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ExtendedDataResultContainer
    {
        [XmlAttribute]
        public ExtendedOperationType OperationType;
        public object[] KeysOrUniqueValues;
        [XmlAttribute]
        public int Count;
        public ServerModeGroupInfoDataSerializationWrapper[] Children;
        public ServerModeGroupInfoDataSerializationWrapper TopGroupInfo;

        public ExtendedDataResultContainer()
        {
        }

        public ExtendedDataResultContainer(int count)
        {
            this.OperationType = ExtendedOperationType.GetCount;
            this.Count = count;
        }

        public ExtendedDataResultContainer(ServerModeGroupInfoData[] children)
        {
            this.OperationType = ExtendedOperationType.PrepareChildren;
            this.Children = ExtendedDataHelper.WrapGroupInfoData(children);
        }

        public ExtendedDataResultContainer(ServerModeGroupInfoData topGroupInfo)
        {
            this.TopGroupInfo = new ServerModeGroupInfoDataSerializationWrapper(topGroupInfo);
        }

        public ExtendedDataResultContainer(ExtendedOperationType operationType, object[] keysOrUniqueValues)
        {
            this.OperationType = operationType;
            this.KeysOrUniqueValues = keysOrUniqueValues;
        }

        public ServerModeGroupInfoData[] GetChildren() => 
            ExtendedDataHelper.UnwrapGroupInfoData(this.Children);

        public int GetCount() => 
            this.Count;

        public object[] GetKeys() => 
            this.KeysOrUniqueValues;

        public ServerModeGroupInfoData GetTopGroupInfo() => 
            this.TopGroupInfo.ToServerModeGroupInfoData();

        public object[] GetUniqueValues() => 
            this.KeysOrUniqueValues;
    }
}

