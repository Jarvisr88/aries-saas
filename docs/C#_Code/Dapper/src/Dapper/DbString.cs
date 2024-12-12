namespace Dapper
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    public sealed class DbString : SqlMapper.ICustomQueryParameter
    {
        public const int DefaultLength = 0xfa0;

        public DbString()
        {
            this.Length = -1;
            this.IsAnsi = IsAnsiDefault;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            IDbDataParameter parameter;
            if (this.IsFixedLength && (this.Length == -1))
            {
                throw new InvalidOperationException("If specifying IsFixedLength,  a Length must also be specified");
            }
            bool flag = !command.Parameters.Contains(name);
            if (!flag)
            {
                parameter = (IDbDataParameter) command.Parameters[name];
            }
            else
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = name;
            }
            parameter.Value = SqlMapper.SanitizeParameterValue(this.Value);
            parameter.Size = ((this.Length != -1) || ((this.Value == null) || (this.Value.Length > 0xfa0))) ? this.Length : 0xfa0;
            parameter.DbType = this.IsAnsi ? (this.IsFixedLength ? DbType.AnsiStringFixedLength : DbType.AnsiString) : (this.IsFixedLength ? DbType.StringFixedLength : DbType.String);
            if (flag)
            {
                command.Parameters.Add(parameter);
            }
        }

        public static bool IsAnsiDefault { get; set; }

        public bool IsAnsi { get; set; }

        public bool IsFixedLength { get; set; }

        public int Length { get; set; }

        public string Value { get; set; }
    }
}

