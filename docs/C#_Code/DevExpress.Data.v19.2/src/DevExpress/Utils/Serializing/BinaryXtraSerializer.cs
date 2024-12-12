namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.IO;

    public class BinaryXtraSerializer : XtraSerializer
    {
        private StringCollection stringTable = new StringCollection();

        private TypedBinaryReaderExWithStringTable CreateTypedReader(BinaryReader reader) => 
            new TypedBinaryReaderExWithStringTable(reader) { CustomObjectConverter = base.CustomObjectConverter };

        private TypedBinaryWriterExWithStringTable CreateTypedWriter(BinaryWriter writer) => 
            new TypedBinaryWriterExWithStringTable(writer) { CustomObjectConverter = base.CustomObjectConverter };

        protected override IXtraPropertyCollection Deserialize(Stream stream, string appName, IList objects) => 
            this.DeserializeCore(stream, appName);

        protected override IXtraPropertyCollection Deserialize(string path, string appName, IList objects)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                return this.DeserializeCore(stream, appName);
            }
        }

        private IXtraPropertyCollection DeserializeCore(Stream stream, string appName)
        {
            IXtraPropertyCollection propertys;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                using (TypedBinaryReaderExWithStringTable table = this.CreateTypedReader(reader))
                {
                    propertys = this.DeserializeLevel(table);
                    table.Close();
                }
            }
            return propertys;
        }

        private IXtraPropertyCollection DeserializeLevel(TypedBinaryReaderExWithStringTable typedReader)
        {
            int capacity = (int) typedReader.ReadObject();
            IXtraPropertyCollection propertys = new XtraPropertyCollection(capacity);
            for (int i = 0; i < capacity; i++)
            {
                propertys.Add(this.DeserializeProperty(typedReader));
            }
            return propertys;
        }

        private XtraPropertyInfo DeserializeProperty(TypedBinaryReaderExWithStringTable typedReader)
        {
            bool isKey = (bool) typedReader.ReadObject();
            object val = typedReader.ReadObject();
            XtraPropertyInfo info = new XtraPropertyInfo((string) typedReader.ReadObject(), null, val, isKey);
            if (isKey)
            {
                info.ChildProperties.AddRange(this.DeserializeLevel(typedReader));
            }
            return info;
        }

        protected override bool Serialize(Stream stream, IXtraPropertyCollection props, string appName) => 
            this.SerializeCore(stream, props, appName);

        protected override bool Serialize(string path, IXtraPropertyCollection props, string appName)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                return this.SerializeCore(stream, props, appName);
            }
        }

        private bool SerializeCore(Stream stream, IXtraPropertyCollection props, string appName)
        {
            using (MemoryStream stream2 = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream2))
                {
                    using (TypedBinaryWriterExWithStringTable table = this.CreateTypedWriter(writer))
                    {
                        this.SerializeLevel(table, props);
                        table.Close();
                    }
                }
                byte[] buffer = stream2.ToArray();
                stream.Write(buffer, 0, buffer.Length);
            }
            return true;
        }

        private void SerializeLevel(TypedBinaryWriterExWithStringTable typedWriter, IXtraPropertyCollection props)
        {
            typedWriter.WriteObject(props.Count);
            foreach (XtraPropertyInfo info in props)
            {
                this.SerializeProperty(typedWriter, info);
            }
        }

        private void SerializeProperty(TypedBinaryWriterExWithStringTable typedWriter, XtraPropertyInfo property)
        {
            typedWriter.WriteObject(property.IsKey);
            typedWriter.WriteObject(property.Name);
            object obj2 = property.Value;
            bool flag = false;
            if (obj2 != null)
            {
                Type type = obj2.GetType();
                if (!type.IsPrimitive && ((type != typeof(TimeSpan)) && ((type != typeof(DateTime)) && ((type != typeof(Guid)) && ((type != typeof(decimal)) && (type != typeof(byte[])))))))
                {
                    obj2 = base.ObjectConverterImpl.ObjectToString(obj2);
                    flag = base.HasCustomObjectConverter && base.CustomObjectConverter.CanConvert(type);
                }
            }
            if (flag)
            {
                typedWriter.WriteCustomObject(property.Value.GetType(), obj2.ToString());
            }
            else if (!property.IsPrimitive)
            {
                typedWriter.WriteObject(obj2);
            }
            else if (!property.IsConvertible)
            {
                typedWriter.WriteObject(property.PropertyType, obj2);
            }
            else
            {
                string serializedObject = base.ObjectConverterImpl.ConvertToString(property.Value);
                typedWriter.WriteCustomObject(property.PropertyType, serializedObject);
            }
            if (property.IsKey)
            {
                this.SerializeLevel(typedWriter, property.ChildProperties);
            }
        }

        public override bool CanUseStream =>
            true;
    }
}

