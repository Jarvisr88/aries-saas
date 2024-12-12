namespace DevExpress.XtraPrinting.Native.Caching
{
    using System;
    using System.Threading.Tasks;

    public class DefaultBuildTaskFactory : IBuildTaskFactory
    {
        public Task CreateTask(Action action);
    }
}

