namespace Devart.Data.MySql
{
    using System;

    public enum MySqlProtocol
    {
        public const MySqlProtocol Tcp = MySqlProtocol.Tcp;,
        public const MySqlProtocol Pipe = MySqlProtocol.Pipe;,
        public const MySqlProtocol Memory = MySqlProtocol.Memory;,
        public const MySqlProtocol UnixSocket = MySqlProtocol.UnixSocket;,
        public const MySqlProtocol Ssh = MySqlProtocol.Ssh;,
        public const MySqlProtocol Ssl = MySqlProtocol.Ssl;,
        public const MySqlProtocol Http = MySqlProtocol.Http;,
        public const MySqlProtocol HttpSsl = MySqlProtocol.HttpSsl;
    }
}

