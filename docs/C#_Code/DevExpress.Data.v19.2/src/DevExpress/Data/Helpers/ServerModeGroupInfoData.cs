namespace DevExpress.Data.Helpers
{
    using System;

    public class ServerModeGroupInfoData
    {
        public readonly int ChildDataRowCount;
        public readonly object[] GroupValues;
        public readonly object[] Summary;

        public ServerModeGroupInfoData(object[] groupValues, int childDataRowCount, object[] summary);
        public ServerModeGroupInfoData(object groupValue, int childDataRowCount, object[] summary);
    }
}

