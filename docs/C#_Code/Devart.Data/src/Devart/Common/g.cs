namespace Devart.Common
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal class g
    {
        private static ResourceManager a;
        private static ResourceManager b;
        private static CultureInfo c;
        internal const string d = "PoolTimeoutExpired";
        internal const string e = "MissingSourceCommand";
        internal const string f = "MissingSourceCommandConnection";
        internal const string g = "DynamicSQLNoTableInfo";
        internal const string h = "DynamicSQLJoinUnsupported";
        internal const string i = "DynamicSqlGenerationNotSupp";
        internal const string j = "AdapterNotInited";
        internal const string k = "ConnectionNotInit";
        internal const string l = "SelectCommandNotInit";
        internal const string m = "ConnNotOpen";
        internal const string n = "InvalidDbMonitorVersion";
        internal const string o = "ParamNameMissing";
        internal const string p = "ParamValueMissing";
        internal const string q = "InvalidChar";
        internal const string r = "UnknownParameter";
        internal const string s = "ConnectionStateNotSupported";
        internal const string t = "ParametersUniqueName";
        internal const string u = "ParameterNull";
        internal const string v = "InvalidParameterType";
        internal const string w = "ParametersIsNotParent";
        internal const string x = "ParametersIsParent";
        internal const string y = "ParametersMappingIndex";
        internal const string z = "ParametersSourceIndex";
        internal const string aa = "ParametersRemoveInvalidObject";
        internal const string ab = "CannotConvert";
        internal const string ac = "UnknownType";
        internal const string ad = "ReaderNotClosed";
        internal const string ae = "IncorrectFormat";
        internal const string af = "ConnectionAlreadyOpen";
        internal const string ag = "DelegatedTransactionPresent";
        internal const string ah = "ClosedConnectionError";
        internal const string ai = "OpenConnectionStringSet";
        internal const string aj = "PooledOpenTimeout";
        internal const string ak = "ParameterNameMissing";
        internal const string al = "ParameterValueMissing";
        internal const string am = "OutParameterValueMissing";
        internal const string an = "InvalidConnectionString";
        internal const string ao = "UnknownConnectionStringParameter";
        internal const string ap = "InvalidCommandTimeout";
        internal const string aq = "InvalidCommandType";
        internal const string ar = "InvalidUpdateRowSource";
        internal const string @as = "ConvertFailed";
        internal const string at = "DataReaderNoData";
        internal const string au = "DataReaderClosed";
        internal const string av = "InvalidSourceBufferIndex";
        internal const string aw = "InvalidDestinationBufferIndex";
        internal const string ax = "InvalidBufferSizeOrIndex";
        internal const string ay = "IndexOutOfRange";
        internal const string az = "ReaderInvalidColumnName";
        internal const string a0 = "InvalidParameterDirection";
        internal const string a1 = "InvalidOffsetValue";
        internal const string a2 = "InvalidSizeValue";
        internal const string a3 = "InvalidDataRowVersion";
        internal const string a4 = "InvalidDataLength";
        internal const string a5 = "CollectionRemoveInvalidObject";
        internal const string a6 = "DbException";
        internal const string a7 = "KeywordNotSupported";
        internal const string a8 = "RequestedValueNotFound";
        internal const string a9 = "InvalidConnectionOptionValue";
        internal const string ba = "InvalidMinMaxPoolSizeValues";
        internal const string bb = "InternalConnectionWithoutProxy";
        internal const string bc = "ConnectionStringNotInitialized";
        internal const string bd = "CommandCannotBeNull";
        internal const string be = "IdentifierIsNotQuoted";
        internal const string bf = "CatalogSeparatorNotSupported";
        internal const string bg = "SchemaSeparatorNotSupported";
        internal const string bh = "NoQuoteChange";
        internal const string bi = "ConnMustOpen";
        internal const string bj = "RequestedCollectionNotDefined";
        internal const string bk = "ExecutionInProgress";
        internal const string bl = "PooledObjectHasOwner";
        internal const string bm = "PooledObjectInPoolMoreThanOnce";
        internal const string bn = "NonPooledObjectUsedMoreThanOnce";
        internal const string bo = "UnpooledObjectHasOwner";
        internal const string bp = "UnpooledObjectHasWrongOwner";
        internal const string bq = "PushingObjectSecondTime";
        internal const string br = "GetConnectionReturnsNull";
        internal const string bs = "ConnectionOptionsMissing";
        internal const string bt = "ConnectionPoolOptionsMissing";
        internal const string bu = "CommandTextRequired";
        internal const string bv = "TableNameNotDef";
        internal const string bw = "ProcNameNotDef";
        internal const string bx = "DuplicateStringParameter";
        internal const string by = "DbCommandBuilder_AllRefreshNotSupported";
        internal const string bz = "Data is Null. This method or property cannot be called on Null values.";
        internal const string b0 = "MissingSchemaActionNotSupported";
        internal const string b1 = "SslConnectionIsNotAllowed";
        internal const string b2 = "DataAdapterMissingSourceCommand";
        internal const string b3 = "TransactionIsolationLevelNotSupported";
        internal const string b4 = "StreamAlreadyClosed";
        internal const string b5 = "StreamNotOpened";
        internal const string b6 = "StreamNoRead";
        internal const string b7 = "StreamNoWrite";
        internal const string b8 = "ReadFromStreamFailed";
        internal const string b9 = "WriteToStreamFailed";
        internal const string ca = "Timeout_Exception";
        internal const string cb = "YouMustSetupDumpTextProperty";
        internal const string cc = "QueryShouldSelectDataFromOneTable";
        internal const string cd = "TransactionNotDisposed";
        internal const string ce = "InvalidCast_FromTo";
        internal const string cf = "DbDump_ExecutionInProgress";
        internal const string cg = "DbMonitor_Host";
        internal const string ch = "DbMonitor_Port";
        internal const string ci = "DbMonitor_UseApp";
        internal const string cj = "DbMonitor_EventQueueLimit";
        internal const string ck = "DbMonitor_NotSupportApp";
        internal const string cl = "InvalidSshAuthenticationType";
        internal const string cm = "ArgumentOutOfRangeNegativeValue";
        internal const string cn = "NonSequentialColumnAccess";
        internal const string co = "NonSeqByteAccess";
        internal const string cp = "NonCharColumn";
        internal const string cq = "NonBlobColumn";

        static g()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string str = ".Strings.resources";
            string str2 = ".Common.resources";
            c = CultureInfo.InvariantCulture;
            foreach (string str3 in executingAssembly.GetManifestResourceNames())
            {
                if ((a == null) && ((str3.Length > str.Length) && (str3.Substring(str3.Length - str.Length) == str)))
                {
                    a = new ResourceManager(str3.Substring(0, str3.Length - 10), executingAssembly);
                }
                if ((b == null) && ((str3.Length > str2.Length) && (str3.Substring(str3.Length - str2.Length) == str2)))
                {
                    b = new ResourceManager(str3.Substring(0, str3.Length - 10), executingAssembly);
                }
            }
        }

        internal static string a(string A_0)
        {
            string st = "";
            if (b != null)
            {
                st = b.GetString(A_0, c);
            }
            if (Utils.IsEmpty(st) && (a != null))
            {
                st = a.GetString(A_0, c);
            }
            return (!Utils.IsEmpty(st) ? st : A_0);
        }

        internal static string a(string A_0, object A_1) => 
            string.Format(a(A_0), A_1);

        internal static string a(string A_0, object A_1, object A_2) => 
            string.Format(a(A_0), A_1, A_2);

        internal static string a(string A_0, object A_1, object A_2, object A_3) => 
            string.Format(a(A_0), A_1, A_2, A_3);
    }
}

