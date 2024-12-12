namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    internal interface IStreamingXmlSerializerContext
    {
        bool CheckLevelValue(object value, IStreamingPropertyCollection props);

        System.IO.Stream Stream { get; }

        Stack<XtraPropertyInfo> SerializationStack { get; }

        string AppName { get; }

        OptionsLayoutBase Options { get; }

        SerializeHelper Helper { get; }

        XmlWriter Writer { get; set; }

        object LevelValue { get; set; }

        IStreamingPropertyCollection LevelProperties { get; }
    }
}

