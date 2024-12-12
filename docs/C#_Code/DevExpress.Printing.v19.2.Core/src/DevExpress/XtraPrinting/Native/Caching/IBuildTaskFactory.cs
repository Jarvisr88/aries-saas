namespace DevExpress.XtraPrinting.Native.Caching
{
    using System;
    using System.Threading.Tasks;

    public interface IBuildTaskFactory
    {
        Task CreateTask(Action action);
    }
}

