namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Text;

    public static class UAlgoHelper
    {
        public static void SendAddWebReportExtensionData(string itemType, string details)
        {
            Tuple<string, string>[] customData = new Tuple<string, string>[] { Tuple.Create<string, string>("ItemType", itemType), Tuple.Create<string, string>("Details", details) };
            SendData("AddWebReportExtension", customData);
        }

        public static void SendData(string action, params Tuple<string, string>[] customData)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{{ \"Action\" : \"{0}\"", action);
            if ((customData != null) && (customData.Length != 0))
            {
                for (int i = 0; i < customData.Length; i++)
                {
                    builder.AppendFormat(", \"{0}\" : \"{1}\"", customData[i].Item1, customData[i].Item2);
                }
            }
            builder.AppendFormat(" }}", new object[0]);
            UAlgo.Default.DoCustomEvent(8, builder.ToString());
        }

        public static void SendSetSettingData(IComponent control, string settingName, string value)
        {
            Tuple<string, string>[] tupleArray1 = new Tuple<string, string>[3];
            Tuple<string, string>[] tupleArray2 = new Tuple<string, string>[3];
            tupleArray2[0] = Tuple.Create<string, string>("ControlType", (control == null) ? "Unknown" : control.GetType().Name);
            Tuple<string, string>[] customData = tupleArray2;
            customData[1] = Tuple.Create<string, string>("SettingName", settingName);
            customData[2] = Tuple.Create<string, string>("Value", value);
            SendData("SendSetSetting", customData);
        }

        public static void SendShowWebReportDesignerData(IComponent control)
        {
            Tuple<string, string>[] tupleArray1 = new Tuple<string, string>[] { Tuple.Create<string, string>("ControlType", (control == null) ? "Unknown" : control.GetType().Name) };
            Tuple<string, string>[] customData = new Tuple<string, string>[] { Tuple.Create<string, string>("ControlType", (control == null) ? "Unknown" : control.GetType().Name) };
            SendData("ShowWebReportDesigner", customData);
        }
    }
}

