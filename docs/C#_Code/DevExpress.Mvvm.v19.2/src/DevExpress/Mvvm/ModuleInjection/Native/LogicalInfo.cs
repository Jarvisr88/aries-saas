namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogicalInfo
    {
        public LogicalInfo()
        {
            this.Regions = new List<RegionInfo>();
        }

        public static LogicalInfo Deserialize(string logicalState)
        {
            if (string.IsNullOrEmpty(logicalState))
            {
                return null;
            }
            LogicalInfo res = null;
            SerializationHelper.DeserializeFromString(logicalState, delegate (Stream x) {
                res = (LogicalInfo) new XmlSerializer(typeof(LogicalInfo)).Deserialize(x);
            });
            return res;
        }

        public static string Serialize(LogicalInfo logicalState) => 
            SerializationHelper.SerializeToString(delegate (Stream x) {
                new XmlSerializer(typeof(LogicalInfo)).Serialize(x, logicalState);
            });

        public List<RegionInfo> Regions { get; set; }
    }
}

