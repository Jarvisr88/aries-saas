namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [Serializable]
    public class ServerModeGroupInfoDataSerializationWrapper
    {
        public object GroupValue;
        [XmlAttribute]
        public int ChildDataRowCount;
        public ArrayList Summary;

        public ServerModeGroupInfoDataSerializationWrapper()
        {
            this.Summary = new ArrayList();
        }

        public ServerModeGroupInfoDataSerializationWrapper(ServerModeGroupInfoData groupInfoData)
        {
            this.Summary = new ArrayList();
            this.GroupValue = ((groupInfoData.GroupValues == null) || (groupInfoData.GroupValues.Length == 0)) ? null : groupInfoData.GroupValues[0];
            this.ChildDataRowCount = groupInfoData.ChildDataRowCount;
            this.Summary.AddRange(groupInfoData.Summary);
        }

        public ServerModeGroupInfoData ToServerModeGroupInfoData() => 
            new ServerModeGroupInfoData(this.GroupValue, this.ChildDataRowCount, this.Summary.ToArray());
    }
}

