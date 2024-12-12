namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ResourceOptions : IResourceOptions
    {
        public ResourceOptions(bool processEmebedResources, params string[] externalPaths)
        {
            this.ProcessEmbededResources = processEmebedResources;
            this.AddExternalPaths(externalPaths);
        }

        public void AddExternalPaths(params string[] externalPaths)
        {
            if (externalPaths != null)
            {
                this.ExternalPaths ??= new List<string>();
                this.ExternalPaths.AddRange(externalPaths);
            }
        }

        public List<string> ExternalPaths { get; private set; }

        public bool ProcessEmbededResources { get; set; }

        public static ResourceOptions DefaultOptions =>
            new ResourceOptions(true, null);
    }
}

