namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class PdfCustomPropertiesDictionary : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
    {
        private readonly IDictionary<string, byte[]> propertiesKeyBlob = new Dictionary<string, byte[]>();

        public void Add(KeyValuePair<string, string> item)
        {
            CheckKey(item.Key, "item");
            Guard.ArgumentNotNull(item.Value, "item");
            this.propertiesKeyBlob.Add(new KeyValuePair<string, byte[]>(item.Key, ConvertValue(item.Value)));
        }

        internal void Add(string key, byte[] value)
        {
            byte[] bytes = new byte[key.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) key[i];
            }
            this.propertiesKeyBlob.Add(Encoding.UTF8.GetString(bytes), value);
        }

        public void Add(string key, string value)
        {
            CheckKey(key, "key");
            Guard.ArgumentNotNull(value, "value");
            this.propertiesKeyBlob.Add(key, ConvertValue(value));
        }

        private static void CheckKey(string key, string argumentName)
        {
            Guard.ArgumentNotNull(key, argumentName);
            if (string.Empty == key)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgEmptyCustomPropertyName), argumentName);
            }
        }

        public void Clear()
        {
            this.propertiesKeyBlob.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            byte[] buffer;
            Guard.ArgumentNotNull(item.Key, "item");
            return (this.propertiesKeyBlob.TryGetValue(item.Key, out buffer) && (PdfDocumentReader.ConvertToTextString(buffer) == item.Value));
        }

        public bool ContainsKey(string key)
        {
            Guard.ArgumentNotNull(key, "key");
            return this.propertiesKeyBlob.ContainsKey(key);
        }

        private static byte[] ConvertValue(string value)
        {
            byte[] collection = new byte[] { 0xfe, 0xff };
            List<byte> list = new List<byte>(collection);
            list.AddRange(Encoding.BigEndianUnicode.GetBytes(value));
            return list.ToArray();
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            Guard.ArgumentNotNull(array, "array");
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, byte[]> pair in this.propertiesKeyBlob)
            {
                list.Add(new KeyValuePair<string, string>(pair.Key, PdfDocumentReader.ConvertToTextString(pair.Value)));
            }
            list.CopyTo(array);
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__23))]
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            <GetEnumerator>d__23 d__1 = new <GetEnumerator>d__23(0);
            d__1.<>4__this = this;
            return d__1;
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            byte[] buffer;
            return (this.propertiesKeyBlob.TryGetValue(item.Key, out buffer) && ((PdfDocumentReader.ConvertToTextString(buffer) == item.Value) ? this.propertiesKeyBlob.Remove(item.Key) : false));
        }

        public bool Remove(string key) => 
            this.propertiesKeyBlob.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public bool TryGetValue(string key, out string value)
        {
            byte[] buffer;
            Guard.ArgumentNotNull(key, "key");
            if (this.propertiesKeyBlob.TryGetValue(key, out buffer))
            {
                value = PdfDocumentReader.ConvertToTextString(buffer);
                return true;
            }
            value = null;
            return false;
        }

        public string this[string key]
        {
            get
            {
                Guard.ArgumentNotNull(key, "key");
                return PdfDocumentReader.ConvertToTextString(this.propertiesKeyBlob[key]);
            }
            set
            {
                CheckKey(key, "key");
                this.propertiesKeyBlob[key] = ConvertValue(value);
            }
        }

        public ICollection<string> Keys =>
            this.propertiesKeyBlob.Keys;

        public ICollection<string> Values
        {
            get
            {
                List<string> list = new List<string>(this.propertiesKeyBlob.Count);
                foreach (KeyValuePair<string, byte[]> pair in this.propertiesKeyBlob)
                {
                    list.Add(PdfDocumentReader.ConvertToTextString(pair.Value));
                }
                return list;
            }
        }

        public int Count =>
            this.propertiesKeyBlob.Count;

        public bool IsReadOnly =>
            false;

        internal IDictionary<string, byte[]> BlobDictionary =>
            this.propertiesKeyBlob;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__23 : IEnumerator<KeyValuePair<string, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<string, string> <>2__current;
            public PdfCustomPropertiesDictionary <>4__this;
            private IEnumerator<KeyValuePair<string, byte[]>> <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__23(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.propertiesKeyBlob.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<string, byte[]> current = this.<>7__wrap1.Current;
                        this.<>2__current = new KeyValuePair<string, string>(current.Key, PdfDocumentReader.ConvertToTextString(current.Value));
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<string, string> IEnumerator<KeyValuePair<string, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

