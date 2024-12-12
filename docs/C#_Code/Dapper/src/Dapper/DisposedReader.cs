namespace Dapper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class DisposedReader : DbDataReader
    {
        internal static readonly DisposedReader Instance = new DisposedReader();

        private DisposedReader()
        {
        }

        public override void Close()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }

        public override bool GetBoolean(int ordinal) => 
            ThrowDisposed<bool>();

        public override byte GetByte(int ordinal) => 
            ThrowDisposed<byte>();

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => 
            ThrowDisposed<long>();

        public override char GetChar(int ordinal) => 
            ThrowDisposed<char>();

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => 
            ThrowDisposed<long>();

        public override string GetDataTypeName(int ordinal) => 
            ThrowDisposed<string>();

        public override DateTime GetDateTime(int ordinal) => 
            ThrowDisposed<DateTime>();

        protected override DbDataReader GetDbDataReader(int ordinal) => 
            ThrowDisposed<DbDataReader>();

        public override decimal GetDecimal(int ordinal) => 
            ThrowDisposed<decimal>();

        public override double GetDouble(int ordinal) => 
            ThrowDisposed<double>();

        public override IEnumerator GetEnumerator() => 
            ThrowDisposed<IEnumerator>();

        public override Type GetFieldType(int ordinal) => 
            ThrowDisposed<Type>();

        public override T GetFieldValue<T>(int ordinal) => 
            ThrowDisposed<T>();

        public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken) => 
            ThrowDisposedAsync<T>();

        public override float GetFloat(int ordinal) => 
            ThrowDisposed<float>();

        public override Guid GetGuid(int ordinal) => 
            ThrowDisposed<Guid>();

        public override short GetInt16(int ordinal) => 
            ThrowDisposed<short>();

        public override int GetInt32(int ordinal) => 
            ThrowDisposed<int>();

        public override long GetInt64(int ordinal) => 
            ThrowDisposed<long>();

        public override string GetName(int ordinal) => 
            ThrowDisposed<string>();

        public override int GetOrdinal(string name) => 
            ThrowDisposed<int>();

        public override Type GetProviderSpecificFieldType(int ordinal) => 
            ThrowDisposed<Type>();

        public override object GetProviderSpecificValue(int ordinal) => 
            ThrowDisposed<object>();

        public override int GetProviderSpecificValues(object[] values) => 
            ThrowDisposed<int>();

        public override DataTable GetSchemaTable() => 
            ThrowDisposed<DataTable>();

        public override Stream GetStream(int ordinal) => 
            ThrowDisposed<Stream>();

        public override string GetString(int ordinal) => 
            ThrowDisposed<string>();

        public override TextReader GetTextReader(int ordinal) => 
            ThrowDisposed<TextReader>();

        public override object GetValue(int ordinal) => 
            ThrowDisposed<object>();

        public override int GetValues(object[] values) => 
            ThrowDisposed<int>();

        public override object InitializeLifetimeService() => 
            ThrowDisposed<object>();

        public override bool IsDBNull(int ordinal) => 
            ThrowDisposed<bool>();

        public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken) => 
            ThrowDisposedAsync<bool>();

        public override bool NextResult() => 
            ThrowDisposed<bool>();

        public override Task<bool> NextResultAsync(CancellationToken cancellationToken) => 
            ThrowDisposedAsync<bool>();

        public override bool Read() => 
            ThrowDisposed<bool>();

        public override Task<bool> ReadAsync(CancellationToken cancellationToken) => 
            ThrowDisposedAsync<bool>();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static T ThrowDisposed<T>()
        {
            throw new ObjectDisposedException("DbDataReader");
        }

        [MethodImpl(MethodImplOptions.NoInlining), AsyncStateMachine(typeof(<ThrowDisposedAsync>d__15))]
        private static Task<T> ThrowDisposedAsync<T>()
        {
            <ThrowDisposedAsync>d__15<T> d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ThrowDisposedAsync>d__15<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        public override int Depth =>
            0;

        public override int FieldCount =>
            0;

        public override bool IsClosed =>
            true;

        public override bool HasRows =>
            false;

        public override int RecordsAffected =>
            -1;

        public override int VisibleFieldCount =>
            0;

        public override object this[int ordinal] =>
            ThrowDisposed<object>();

        public override object this[string name] =>
            ThrowDisposed<object>();

        [CompilerGenerated]
        private struct <ThrowDisposedAsync>d__15<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            private T <result>5__2;
            private YieldAwaitable.YieldAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    YieldAwaitable.YieldAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new YieldAwaitable.YieldAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        this.<result>5__2 = DisposedReader.ThrowDisposed<T>();
                        awaiter = Task.Yield().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<YieldAwaitable.YieldAwaiter, DisposedReader.<ThrowDisposedAsync>d__15<T>>(ref awaiter, ref (DisposedReader.<ThrowDisposedAsync>d__15<T>) ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    T result = this.<result>5__2;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

