namespace DevExpress.Xpf.Core
{
    using System;

    internal class Workspace : IWorkspace
    {
        private readonly string name;
        private readonly object serializationData;

        public Workspace(string name, object serializationData)
        {
            this.name = name;
            this.serializationData = serializationData;
        }

        public string Name =>
            this.name;

        public object SerializationData =>
            this.serializationData;
    }
}

