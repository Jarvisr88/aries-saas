namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class EdmxResources
    {
        private Dictionary<string, EdmxResource> csdlEdmxResources;
        private Dictionary<string, EdmxResource> ssdlEdmxResources;
        private ResourceOptions options;

        public EdmxResources(Assembly asm) : this(asm, null)
        {
        }

        public EdmxResources(Assembly asm, ResourceOptions options)
        {
            this.options = (options != null) ? options : ResourceOptions.DefaultOptions;
            this.Init(asm);
        }

        private void AddCsdlResourceByContainer(Stream stream)
        {
            Action<EdmxResource, Stream> addStream = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Action<EdmxResource, Stream> local1 = <>c.<>9__8_0;
                addStream = <>c.<>9__8_0 = (r, s) => r.AddCsdlContainerStream(s);
            }
            this.AddResourceByContainerName(this.csdlEdmxResources, stream, addStream);
        }

        private void AddMslResourceByContainersNames(Stream stream)
        {
            stream = GetMemoryStream(stream);
            if (stream != null)
            {
                string str;
                string str2;
                EdmxResource.GetContainerNamesFromMsl(stream, out str, out str2);
                if ((!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2)) && (((this.csdlEdmxResources == null) || !this.csdlEdmxResources.ContainsKey(str)) && ((this.ssdlEdmxResources == null) || !this.ssdlEdmxResources.ContainsKey(str2))))
                {
                    EdmxResource resource = new EdmxResource(str, str2);
                    resource.AddMslContainerStream(stream);
                    this.csdlEdmxResources ??= new Dictionary<string, EdmxResource>();
                    this.ssdlEdmxResources ??= new Dictionary<string, EdmxResource>();
                    this.csdlEdmxResources.Add(str, resource);
                    this.ssdlEdmxResources.Add(str2, resource);
                }
            }
        }

        private void AddResourceByContainerName(Dictionary<string, EdmxResource> resources, Stream stream, Action<EdmxResource, Stream> addStream)
        {
            if (resources != null)
            {
                stream = GetMemoryStream(stream);
                if (stream != null)
                {
                    string entityContainerName = EdmxResource.GetEntityContainerName(stream);
                    if (!string.IsNullOrEmpty(entityContainerName) && ((resources != null) && resources.ContainsKey(entityContainerName)))
                    {
                        EdmxResource resource = resources[entityContainerName];
                        if (resource != null)
                        {
                            addStream(resource, stream);
                        }
                    }
                }
            }
        }

        private void AddSsdlResourceByContainer(Stream stream)
        {
            Action<EdmxResource, Stream> addStream = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Action<EdmxResource, Stream> local1 = <>c.<>9__9_0;
                addStream = <>c.<>9__9_0 = (r, s) => r.AddSsdlContainerStream(s);
            }
            this.AddResourceByContainerName(this.ssdlEdmxResources, stream, addStream);
        }

        public EdmxResource GetEdmxResource(string typeName) => 
            (string.IsNullOrEmpty(typeName) || ((this.csdlEdmxResources == null) || !this.csdlEdmxResources.ContainsKey(typeName))) ? null : this.csdlEdmxResources[typeName];

        private static MemoryStream GetMemoryStream(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            MemoryStream destination = new MemoryStream();
            stream.CopyTo(destination);
            destination.Seek(0L, SeekOrigin.Begin);
            return destination;
        }

        private void Init(Assembly asm)
        {
            this.InitResources(asm, ".msl", new Action<Stream>(this.AddMslResourceByContainersNames));
            this.InitResources(asm, ".csdl", new Action<Stream>(this.AddCsdlResourceByContainer));
            this.InitResources(asm, ".ssdl", new Action<Stream>(this.AddSsdlResourceByContainer));
        }

        private void InitEmbededResources(Assembly asm, string extension, Action<Stream> addResource)
        {
            if ((asm != null) && this.options.ProcessEmbededResources)
            {
                try
                {
                    string[] manifestResourceNames = asm.GetManifestResourceNames();
                    if (manifestResourceNames != null)
                    {
                        foreach (string str in manifestResourceNames)
                        {
                            if (Path.GetExtension(str) == extension)
                            {
                                addResource(asm.GetManifestResourceStream(str));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void InitExternalResources(string extension, Action<Stream> addResource)
        {
            if (!string.IsNullOrEmpty(extension) && ((addResource != null) && (this.options.ExternalPaths != null)))
            {
                foreach (string str in this.options.ExternalPaths)
                {
                    this.InitExternalResources(str, extension, addResource);
                }
            }
        }

        private void InitExternalResources(string directoryPath, string extension, Action<Stream> addResource)
        {
            if (!string.IsNullOrEmpty(directoryPath) && Directory.Exists(directoryPath))
            {
                try
                {
                    string[] strArray = Directory.GetFiles(directoryPath, "*" + extension, SearchOption.TopDirectoryOnly);
                    if ((strArray != null) && (strArray.Length != 0))
                    {
                        foreach (string str in strArray)
                        {
                            if (!string.IsNullOrEmpty(str))
                            {
                                byte[] buffer = File.ReadAllBytes(str);
                                if (buffer != null)
                                {
                                    addResource(new MemoryStream(buffer));
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void InitResources(Assembly asm, string extension, Action<Stream> addResource)
        {
            if (asm != null)
            {
                this.InitEmbededResources(asm, extension, addResource);
                this.InitExternalResources(extension, addResource);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EdmxResources.<>c <>9 = new EdmxResources.<>c();
            public static Action<EdmxResource, Stream> <>9__8_0;
            public static Action<EdmxResource, Stream> <>9__9_0;

            internal void <AddCsdlResourceByContainer>b__8_0(EdmxResource r, Stream s)
            {
                r.AddCsdlContainerStream(s);
            }

            internal void <AddSsdlResourceByContainer>b__9_0(EdmxResource r, Stream s)
            {
                r.AddSsdlContainerStream(s);
            }
        }
    }
}

