namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class VisualInfo
    {
        public VisualInfo()
        {
            this.Regions = new List<RegionVisualInfo>();
        }

        public static VisualInfo Deserialize(string visualState)
        {
            if (string.IsNullOrEmpty(visualState))
            {
                return null;
            }
            VisualInfo res = null;
            SerializationHelper.DeserializeFromString(visualState, delegate (Stream x) {
                res = (VisualInfo) new XmlSerializer(typeof(VisualInfo)).Deserialize(x);
            });
            return res;
        }

        public static string Serialize(VisualInfo visualState) => 
            SerializationHelper.SerializeToString(delegate (Stream x) {
                new XmlSerializer(typeof(VisualInfo)).Serialize(x, visualState);
            });

        public List<RegionVisualInfo> Regions { get; set; }
    }
}

