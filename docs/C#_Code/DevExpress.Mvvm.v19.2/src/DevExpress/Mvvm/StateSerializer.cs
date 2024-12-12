namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Xml.Serialization;

    public class StateSerializer : IStateSerializer
    {
        private static IStateSerializer _defaultInstance = new StateSerializer();
        private static IStateSerializer _default;

        public object DeserializeState(string state, Type stateType)
        {
            object res = null;
            XmlSerializer s = new XmlSerializer(stateType);
            SerializationHelper.DeserializeFromString(state, delegate (Stream x) {
                res = s.Deserialize(x);
            });
            return res;
        }

        public string SerializeState(object state, Type stateType)
        {
            string str = null;
            if (state == null)
            {
                return str;
            }
            XmlSerializer s = new XmlSerializer(state.GetType());
            return SerializationHelper.SerializeToString(delegate (Stream x) {
                s.Serialize(x, state);
            });
        }

        public static IStateSerializer Default
        {
            get => 
                _default ?? _defaultInstance;
            set => 
                _default = value;
        }
    }
}

