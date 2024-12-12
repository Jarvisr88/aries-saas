namespace Dapper
{
    using System;
    using System.Data;

    public interface IWrappedDataReader : IDataReader, IDisposable, IDataRecord
    {
        IDataReader Reader { get; }

        IDbCommand Command { get; }
    }
}

