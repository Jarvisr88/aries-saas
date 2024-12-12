namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Xml;

    internal class StreamingXmlSerializer : PrintingSystemXmlSerializer, IStreamingXmlSerializer
    {
        private IStreamingXmlDeserializerContext ctxDeserializer;
        private IStreamingXmlSerializerContext ctxSerializer;
        private StreamingSerializationContext context;

        public event DeserializeChildPropertiesEventHandler DeserializeChildProperties;

        public StreamingXmlSerializer(StreamingSerializationContext context)
        {
            this.context = context;
        }

        protected override DeserializeHelper CreateDeserializeHelper(object rootObj, bool useRootObj) => 
            useRootObj ? new StreamingDeserializeHelper(rootObj, this.context) : new StreamingDeserializeHelper(null, this.context);

        protected override SerializeHelper CreateSerializeHelper(object rootObj, bool useRootObj) => 
            useRootObj ? new StreamingSerializeHelper(rootObj, this.context) : new StreamingSerializeHelper(null, this.context);

        protected override IXtraPropertyCollection DeserializeCore(Stream stream, string appName, IList objects)
        {
            XmlReader reader = this.CreateReader(stream);
            this.ctxDeserializer = new StreamingXmlDeserializerContext(reader, this, PrintingSystemXmlSerializer.ObjectConverterInstance);
            return new StreamingDeserializationVirtualPropertyCollection(this.ctxDeserializer);
        }

        void IStreamingXmlSerializer.BeginWrite(object rootObject, Stream stream, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            SerializeHelper helper = this.CreateSerializeHelper(rootObject, true);
            this.ctxSerializer = new StreamingXmlSerializerContext(stream, helper, appName, options);
        }

        void IStreamingXmlSerializer.EndWrite()
        {
            if (this.ctxSerializer.Writer != null)
            {
                foreach (XtraPropertyInfo info in this.ctxSerializer.SerializationStack)
                {
                    this.ctxSerializer.Writer.WriteEndElement();
                }
                this.ctxSerializer.Writer.Flush();
                this.ctxSerializer.Writer.Dispose();
            }
            if (this.ctxSerializer.Helper.RootSerializationObject != null)
            {
                this.ctxSerializer.Helper.RootSerializationObject.AfterSerialize();
            }
        }

        IStreamingPropertyCollection IStreamingXmlSerializer.SerializeInDepthToLevel(IList objects, object levelValue)
        {
            try
            {
                this.ctxSerializer.LevelValue = levelValue;
                this.Serialize(this.ctxSerializer.Stream, this.ctxSerializer.Helper.SerializeObjects(objects, this.ctxSerializer.Options), this.ctxSerializer.AppName);
            }
            catch (StreamingXmlSerializerException)
            {
                return this.ctxSerializer.LevelProperties;
            }
            return null;
        }

        void IStreamingXmlSerializer.SerializePart(IXtraPropertyCollection props)
        {
            base.SerializeLevel(this.ctxSerializer.Writer, props);
        }

        public XtraPropertyInfo ReadNode(IStreamingXmlDeserializerContext context)
        {
            bool flag;
            XmlReader reader = context.Reader;
            reader.Read();
            reader.MoveToContent();
            return this.ReadNode(context, true, out flag);
        }

        private XtraPropertyInfo ReadNode(IStreamingXmlDeserializerContext context, bool skipZeroDepth, out bool canReadNext)
        {
            XtraPropertyInfo info2;
            canReadNext = true;
            XmlReader reader = context.Reader;
            if (reader.NodeType != XmlNodeType.Element)
            {
                return null;
            }
            VirtualizedXtraPropertyInfo item = new VirtualizedXtraPropertyInfo(reader.Name, null, null, true);
            try
            {
                context.DeserializationStack.Push(item);
                if (this.ShoudDeserializeChildrenVirtually())
                {
                    item.VirtualizeChildren(context);
                    canReadNext = false;
                    info2 = item;
                }
                else
                {
                    bool isEmptyElement = reader.IsEmptyElement;
                    base.ReadAttributes(reader, item, skipZeroDepth);
                    if (isEmptyElement)
                    {
                        info2 = item;
                    }
                    else
                    {
                        while (true)
                        {
                            if (reader.ReadState != System.Xml.ReadState.EndOfFile)
                            {
                                reader.Read();
                                if (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    XtraPropertyInfo prop = this.ReadNode(context, false, out canReadNext);
                                    if (prop != null)
                                    {
                                        item.ChildProperties.Add(prop);
                                    }
                                    if (canReadNext)
                                    {
                                        continue;
                                    }
                                }
                            }
                            info2 = item;
                            break;
                        }
                    }
                }
            }
            finally
            {
                context.DeserializationStack.Pop();
            }
            return info2;
        }

        protected override void SerializeContentPropertyCore(XmlWriter tw, XtraPropertyInfo p)
        {
            if (this.ctxSerializer == null)
            {
                base.SerializeContentPropertyCore(tw, p);
            }
            else
            {
                this.ctxSerializer.SerializationStack.Push(p);
                this.ctxSerializer.Writer.WriteStartElement(p.Name.Replace("$", string.Empty));
                if (this.ctxSerializer.CheckLevelValue(p.Value, (IStreamingPropertyCollection) p.ChildProperties))
                {
                    throw new StreamingXmlSerializerException();
                }
                base.SerializeLevel(tw, p.ChildProperties);
                this.ctxSerializer.Writer.WriteEndElement();
                this.ctxSerializer.SerializationStack.Pop();
            }
        }

        protected override bool SerializeCore(Stream stream, IXtraPropertyCollection props, string appName)
        {
            if (this.ctxSerializer == null)
            {
                return base.SerializeCore(stream, props, appName);
            }
            XmlWriter tw = this.ctxSerializer.Writer = base.CreateXmlTextWriter(stream);
            base.SerializeCore(tw, props, appName);
            return true;
        }

        private bool ShoudDeserializeChildrenVirtually()
        {
            DeserializeChildPropertiesEventArgs ea = new DeserializeChildPropertiesEventArgs(this.ctxDeserializer);
            DeserializeChildPropertiesEventHandler deserializeChildProperties = this.DeserializeChildProperties;
            if (deserializeChildProperties != null)
            {
                deserializeChildProperties(ea);
            }
            return ea.DeserializeChildrenVirtually;
        }

        private class VirtualizedXtraPropertyInfo : XmlXtraSerializer.XmlXtraPropertyInfo
        {
            public VirtualizedXtraPropertyInfo(string name, Type propertyType, object val, bool isKey) : base(name, propertyType, val, isKey)
            {
            }

            public void VirtualizeChildren(IStreamingXmlDeserializerContext context)
            {
                base.ChildProperties = new StreamingDeserializationVirtualPropertyCollection(context);
            }
        }
    }
}

