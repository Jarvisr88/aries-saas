namespace DevExpress.Entity.ProjectModel
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.Linq;

    public class EdmxResource
    {
        public const string CsdlExtension = ".csdl";
        public const string SsdlExtension = ".ssdl";
        public const string MslExtension = ".msl";
        public const string EntityContainerTagName = "EntityContainer";
        public const string MslContainerTagName = "EntityContainerMapping";
        public const string TagNameAttribute = "Name";
        public const string Msl_CsdlContainerAttributeName = "CdmEntityContainer";
        public const string Msl_SsdlContainerAttributeName = "StorageEntityContainer";
        private List<Stream> csdlStreams;
        private List<Stream> mslStreams;
        private List<Stream> ssdlStreams;

        public EdmxResource(string csdlContainerName, string ssdlContainerName)
        {
            this.CsdlContainerName = csdlContainerName;
            this.SsdlContainerName = ssdlContainerName;
        }

        public void AddCsdlContainerStream(Stream stream)
        {
            if (!this.HasCsdlContainer)
            {
                this.HasCsdlContainer = true;
                this.AddStream(ref this.csdlStreams, stream);
            }
        }

        public void AddCsdlStream(Stream stream)
        {
            this.AddStream(ref this.csdlStreams, stream);
        }

        public void AddMslContainerStream(Stream stream)
        {
            if (!this.HasMslContainer)
            {
                this.HasMslContainer = true;
                this.AddStream(ref this.mslStreams, stream);
            }
        }

        public void AddMslStream(Stream stream)
        {
            this.AddStream(ref this.mslStreams, stream);
        }

        public void AddSsdlContainerStream(Stream stream)
        {
            if (!this.HasSsdlContainer)
            {
                this.HasSsdlContainer = true;
                this.AddStream(ref this.ssdlStreams, stream);
            }
        }

        public void AddSsdlStream(Stream stream)
        {
            this.AddStream(ref this.ssdlStreams, stream);
        }

        private void AddStream(ref List<Stream> streams, Stream stream)
        {
            if (stream != null)
            {
                streams ??= new List<Stream>();
                streams.Add(stream);
            }
        }

        [IteratorStateMachine(typeof(<FindElementsRecursive>d__58))]
        private IEnumerable<XmlElement> FindElementsRecursive(XmlElement element, Predicate<XmlElement> predicate)
        {
            <FindElementsRecursive>d__58 d__1 = new <FindElementsRecursive>d__58(-2);
            d__1.<>4__this = this;
            d__1.<>3__element = element;
            d__1.<>3__predicate = predicate;
            return d__1;
        }

        private XmlElement FindRecursive(XmlElement element, Predicate<XmlElement> predicate)
        {
            XmlElement element4;
            if (element == null)
            {
                return null;
            }
            if ((predicate != null) && predicate(element))
            {
                return element;
            }
            using (IEnumerator enumerator = element.ChildNodes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XmlElement current = (XmlElement) enumerator.Current;
                        XmlElement element3 = this.FindRecursive(current, predicate);
                        if (element3 == null)
                        {
                            continue;
                        }
                        element4 = element3;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return element4;
        }

        protected virtual string Format(XmlDocument document) => 
            document.InnerXml;

        private static string GetAttributeValue(XmlDocument xmlDocument, string tagName, string atrributeName)
        {
            if ((xmlDocument == null) || (string.IsNullOrEmpty(tagName) || string.IsNullOrEmpty(atrributeName)))
            {
                return null;
            }
            XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(tagName);
            if ((elementsByTagName == null) || (elementsByTagName.Count == 0))
            {
                return null;
            }
            System.Xml.XmlNode node = elementsByTagName[0];
            if (node == null)
            {
                return null;
            }
            XmlAttributeCollection attributes = node.Attributes;
            if ((attributes == null) || (attributes.Count == 0))
            {
                return null;
            }
            System.Xml.XmlNode namedItem = attributes.GetNamedItem(atrributeName);
            return namedItem?.Value;
        }

        public static void GetContainerNamesFromMsl(Stream mslStream, out string csdlContainerName, out string ssdlContainerName)
        {
            csdlContainerName = null;
            ssdlContainerName = null;
            if (mslStream != null)
            {
                mslStream.Seek(0L, SeekOrigin.Begin);
                XmlDocument xmlDocument = SafeXml.CreateDocument(mslStream, null, null);
                csdlContainerName = GetAttributeValue(xmlDocument, "EntityContainerMapping", "CdmEntityContainer");
                ssdlContainerName = GetAttributeValue(xmlDocument, "EntityContainerMapping", "StorageEntityContainer");
            }
        }

        public static EdmxResource GetEdmxResource(IDXTypeInfo typeInfo)
        {
            DXAssemblyInfo assembly = typeInfo.Assembly as DXAssemblyInfo;
            return assembly?.GetEdmxResource(typeInfo);
        }

        public static string GetEntityContainerName(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            stream.Seek(0L, SeekOrigin.Begin);
            return GetAttributeValue(SafeXml.CreateDocument(stream, null, null), "EntityContainer", "Name");
        }

        public string GetProviderName()
        {
            if (this.ssdlStreams.Count <= 0)
            {
                return "";
            }
            Stream stream = this.ssdlStreams[0];
            stream.Seek(0L, SeekOrigin.Begin);
            return XElement.Load(stream).Attribute("Provider").Value;
        }

        public string SetProvider(string xml, SchemaAttributeValues values)
        {
            XmlDocument document = SafeXml.CreateDocument(xml, null);
            foreach (System.Xml.XmlNode node in document.ChildNodes)
            {
                XmlElement element = node as XmlElement;
                if ((element != null) && ((element.GetAttribute("Provider") != null) && (element.GetAttribute("ProviderManifestToken") != null)))
                {
                    element.GetAttributeNode("Provider").Value = values.Provider;
                    element.GetAttributeNode("ProviderManifestToken").Value = values.ProviderManifestToken;
                }
            }
            return this.Format(document);
        }

        private void WriteResourceFile(Stream stream, string path, SchemaAttributeValues values)
        {
            if ((stream != null) && (stream.CanRead && (!string.IsNullOrEmpty(path) && !File.Exists(path))))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    stream.Seek(0L, SeekOrigin.Begin);
                    string xml = new StreamReader(stream).ReadToEnd();
                    if (!values.IsEmpty)
                    {
                        xml = this.SetProvider(xml, values);
                    }
                    if (!string.IsNullOrEmpty(xml))
                    {
                        writer.Write(xml);
                    }
                }
            }
        }

        public void WriteResources(string path)
        {
            this.WriteResources(path, SchemaAttributeValues.Empty);
        }

        public void WriteResources(string path, SchemaAttributeValues values)
        {
            this.WriteResourcesCore(path, this.DefaultFileName, values);
        }

        private void WriteResources(List<Stream> streams, string path, string extension)
        {
            this.WriteResources(streams, path, extension, SchemaAttributeValues.Empty);
        }

        private void WriteResources(List<Stream> streams, string path, string extension, SchemaAttributeValues values)
        {
            if (!string.IsNullOrEmpty(path) && ((streams != null) && (extension != null)))
            {
                for (int i = 0; i < streams.Count; i++)
                {
                    this.WriteResourceFile(streams[i], $"{path}{i.ToString()}{extension}", values);
                }
            }
        }

        private void WriteResourcesCore(string path, string fileName, SchemaAttributeValues values)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(fileName))
            {
                path = Path.Combine(path, fileName);
                this.WriteResources(this.csdlStreams, path, ".csdl");
                this.WriteResources(this.ssdlStreams, path, ".ssdl", values);
                this.WriteResources(this.mslStreams, path, ".msl");
            }
        }

        public bool HasCsdlContainer { get; set; }

        public bool HasSsdlContainer { get; set; }

        public bool HasMslContainer { get; set; }

        public List<Stream> SsdlStreams =>
            this.ssdlStreams;

        private string DefaultFileName
        {
            get
            {
                if ((this.CsdlContainerName != null) || (this.SsdlContainerName != null))
                {
                    return ((this.CsdlContainerName != null) ? ((this.SsdlContainerName != null) ? $"{this.CsdlContainerName}_{this.SsdlContainerName}" : this.CsdlContainerName) : this.SsdlContainerName);
                }
                Guid guid = default(Guid);
                return guid.ToString();
            }
        }

        public string CsdlContainerName { get; set; }

        public string SsdlContainerName { get; set; }

        [CompilerGenerated]
        private sealed class <FindElementsRecursive>d__58 : IEnumerable<XmlElement>, IEnumerable, IEnumerator<XmlElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private XmlElement <>2__current;
            private int <>l__initialThreadId;
            private XmlElement element;
            public XmlElement <>3__element;
            private Predicate<XmlElement> predicate;
            public Predicate<XmlElement> <>3__predicate;
            public EdmxResource <>4__this;
            private IEnumerator <>7__wrap1;
            private IEnumerator<XmlElement> <>7__wrap2;

            [DebuggerHidden]
            public <FindElementsRecursive>d__58(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>7__wrap1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -3;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            if (this.element != null)
                            {
                                if ((this.predicate == null) || !this.predicate(this.element))
                                {
                                    goto TR_000C;
                                }
                                else
                                {
                                    this.<>2__current = this.element;
                                    this.<>1__state = 1;
                                    flag = true;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                            break;

                        case 1:
                            this.<>1__state = -1;
                            goto TR_000C;

                        case 2:
                            this.<>1__state = -4;
                            goto TR_0008;

                        default:
                            flag = false;
                            break;
                    }
                    return flag;
                TR_0008:
                    if (this.<>7__wrap2.MoveNext())
                    {
                        XmlElement current = this.<>7__wrap2.Current;
                        this.<>2__current = current;
                        this.<>1__state = 2;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally2();
                        this.<>7__wrap2 = null;
                        goto TR_000B;
                    }
                    return flag;
                TR_000B:
                    while (true)
                    {
                        if (this.<>7__wrap1.MoveNext())
                        {
                            XmlElement current = (XmlElement) this.<>7__wrap1.Current;
                            this.<>7__wrap2 = this.<>4__this.FindElementsRecursive(current, this.predicate).GetEnumerator();
                            this.<>1__state = -4;
                        }
                        else
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            return false;
                        }
                        break;
                    }
                    goto TR_0008;
                TR_000C:
                    this.<>7__wrap1 = this.element.ChildNodes.GetEnumerator();
                    this.<>1__state = -3;
                    goto TR_000B;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<XmlElement> IEnumerable<XmlElement>.GetEnumerator()
            {
                EdmxResource.<FindElementsRecursive>d__58 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new EdmxResource.<FindElementsRecursive>d__58(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.element = this.<>3__element;
                d__.predicate = this.<>3__predicate;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Xml.XmlElement>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (((num == -4) || (num == -3)) || (num == 2))
                {
                    try
                    {
                        if ((num == -4) || (num == 2))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.<>m__Finally2();
                            }
                        }
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            XmlElement IEnumerator<XmlElement>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SchemaAttributeValues
        {
            private string provider;
            private string providerManifestToken;
            public static readonly EdmxResource.SchemaAttributeValues Empty;
            public SchemaAttributeValues(string provider, string providerManifestToken)
            {
                this.provider = provider;
                this.providerManifestToken = providerManifestToken;
            }

            static SchemaAttributeValues()
            {
                Empty = new EdmxResource.SchemaAttributeValues();
            }

            public string Provider
            {
                get => 
                    this.provider;
                set => 
                    this.provider = value;
            }
            public string ProviderManifestToken
            {
                get => 
                    this.providerManifestToken;
                set => 
                    this.providerManifestToken = value;
            }
            public bool IsEmpty =>
                string.IsNullOrEmpty(this.Provider) || string.IsNullOrEmpty(this.ProviderManifestToken);
        }
    }
}

