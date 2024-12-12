namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class OlePropertyDictionary : OlePropertyBase
    {
        private readonly Dictionary<int, string> entries;

        public OlePropertyDictionary() : base(0, 0)
        {
            this.entries = new Dictionary<int, string>();
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
        }

        public override int GetSize(OlePropertySetBase propertySet)
        {
            Encoding encoding = propertySet.GetEncoding();
            bool flag = base.IsSingleByteEncoding(encoding);
            int num = 4;
            foreach (string str in this.Entries.Values)
            {
                int length = str.Length;
                if ((length > 0) && (str[length - 1] != '\0'))
                {
                    length++;
                }
                if (!flag)
                {
                    length = (((length * 2) + 3) / 4) * 4;
                }
                num += length + 8;
            }
            return (((num + 3) / 4) * 4);
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            Encoding encoding = propertySet.GetEncoding();
            bool flag = base.IsSingleByteEncoding(encoding);
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                int key = reader.ReadInt32();
                int count = reader.ReadInt32();
                if (!flag)
                {
                    count = (((count * 2) + 3) / 4) * 4;
                }
                byte[] bytes = reader.ReadBytes(count);
                this.Entries.Add(key, encoding.GetString(bytes, 0, bytes.Length).TrimEnd(new char[1]));
            }
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            int num;
            Encoding encoding = propertySet.GetEncoding();
            bool flag = base.IsSingleByteEncoding(encoding);
            writer.Write(this.Entries.Count);
            int num2 = 4;
            foreach (KeyValuePair<int, string> pair in this.Entries)
            {
                int key = pair.Key;
                string s = pair.Value;
                int length = s.Length;
                if ((length > 0) && (s[length - 1] != '\0'))
                {
                    s = s + "\0";
                    length++;
                }
                writer.Write(key);
                writer.Write(length);
                byte[] bytes = encoding.GetBytes(s);
                writer.Write(bytes);
                num2 += bytes.Length + 8;
                if (!flag)
                {
                    num = 4 - (((length * 2) + 8) % 4);
                    if (num < 4)
                    {
                        base.WritePadding(writer, num);
                        num2 += num;
                    }
                }
            }
            num = 4 - (num2 % 4);
            if (num < 4)
            {
                base.WritePadding(writer, num);
            }
        }

        public Dictionary<int, string> Entries =>
            this.entries;
    }
}

