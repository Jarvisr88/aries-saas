namespace DevExpress.Xpo.Helpers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommandChannelAsync
    {
        Task<object> DoAsync(string command, object args, CancellationToken cancellationToken = new CancellationToken());
    }
}

