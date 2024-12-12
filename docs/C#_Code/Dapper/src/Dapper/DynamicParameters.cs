namespace Dapper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DynamicParameters : SqlMapper.IDynamicParameters, SqlMapper.IParameterLookup, SqlMapper.IParameterCallbacks
    {
        internal const DbType EnumerableMultiParameter = ~DbType.AnsiString;
        private static readonly Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> paramReaderCache = new Dictionary<SqlMapper.Identity, Action<IDbCommand, object>>();
        private readonly Dictionary<string, ParamInfo> parameters;
        private List<object> templates;
        private List<Action> outputCallbacks;

        public DynamicParameters()
        {
            this.parameters = new Dictionary<string, ParamInfo>();
            this.RemoveUnused = true;
        }

        public DynamicParameters(object template)
        {
            this.parameters = new Dictionary<string, ParamInfo>();
            this.RemoveUnused = true;
            this.AddDynamicParams(template);
        }

        public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            ParamInfo info1 = new ParamInfo();
            info1.Name = name;
            info1.Value = value;
            ParameterDirection? nullable = direction;
            info1.ParameterDirection = (nullable != null) ? nullable.GetValueOrDefault() : ParameterDirection.Input;
            ParamInfo local1 = info1;
            local1.DbType = dbType;
            local1.Size = size;
            this.parameters[Clean(name)] = local1;
        }

        public void Add(string name, object value = null, DbType? dbType = new DbType?(), ParameterDirection? direction = new ParameterDirection?(), int? size = new int?(), byte? precision = new byte?(), byte? scale = new byte?())
        {
            ParamInfo info1 = new ParamInfo();
            info1.Name = name;
            info1.Value = value;
            ParameterDirection? nullable = direction;
            info1.ParameterDirection = (nullable != null) ? nullable.GetValueOrDefault() : ParameterDirection.Input;
            ParamInfo local1 = info1;
            local1.DbType = dbType;
            local1.Size = size;
            local1.Precision = precision;
            local1.Scale = scale;
            this.parameters[Clean(name)] = local1;
        }

        public void AddDynamicParams(object param)
        {
            object item = param;
            if (item != null)
            {
                DynamicParameters parameters = item as DynamicParameters;
                if (parameters == null)
                {
                    IEnumerable<KeyValuePair<string, object>> enumerable = item as IEnumerable<KeyValuePair<string, object>>;
                    if (enumerable == null)
                    {
                        List<object> templates = this.templates;
                        if (this.templates == null)
                        {
                            List<object> local1 = this.templates;
                            templates = new List<object>();
                        }
                        this.templates = templates;
                        this.templates.Add(item);
                    }
                    else
                    {
                        foreach (KeyValuePair<string, object> pair in enumerable)
                        {
                            DbType? dbType = null;
                            ParameterDirection? direction = null;
                            int? size = null;
                            this.Add(pair.Key, pair.Value, dbType, direction, size);
                        }
                    }
                }
                else
                {
                    if (parameters.parameters != null)
                    {
                        foreach (KeyValuePair<string, ParamInfo> pair2 in parameters.parameters)
                        {
                            this.parameters.Add(pair2.Key, pair2.Value);
                        }
                    }
                    if (parameters.templates != null)
                    {
                        List<object> templates = this.templates;
                        if (this.templates == null)
                        {
                            List<object> local2 = this.templates;
                            templates = new List<object>();
                        }
                        this.templates = templates;
                        foreach (object obj3 in parameters.templates)
                        {
                            this.templates.Add(obj3);
                        }
                    }
                }
            }
        }

        protected void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            IList<SqlMapper.LiteralToken> literalTokens = SqlMapper.GetLiteralTokens(identity.sql);
            if (this.templates != null)
            {
                foreach (object obj2 in this.templates)
                {
                    Action<IDbCommand, object> action;
                    SqlMapper.Identity key = identity.ForDynamicParameters(obj2.GetType());
                    Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> paramReaderCache = DynamicParameters.paramReaderCache;
                    lock (paramReaderCache)
                    {
                        if (!DynamicParameters.paramReaderCache.TryGetValue(key, out action))
                        {
                            action = SqlMapper.CreateParamInfoGenerator(key, true, this.RemoveUnused, literalTokens);
                            DynamicParameters.paramReaderCache[key] = action;
                        }
                    }
                    action(command, obj2);
                }
                foreach (IDbDataParameter parameter in command.Parameters)
                {
                    if (!this.parameters.ContainsKey(parameter.ParameterName))
                    {
                        ParamInfo info1 = new ParamInfo();
                        info1.AttachedParam = parameter;
                        info1.CameFromTemplate = true;
                        info1.DbType = new DbType?(parameter.DbType);
                        info1.Name = parameter.ParameterName;
                        info1.ParameterDirection = parameter.Direction;
                        info1.Size = new int?(parameter.Size);
                        info1.Value = parameter.Value;
                        this.parameters.Add(parameter.ParameterName, info1);
                    }
                }
                List<Action> outputCallbacks = this.outputCallbacks;
                if (outputCallbacks != null)
                {
                    foreach (Action action2 in outputCallbacks)
                    {
                        action2();
                    }
                }
            }
            foreach (ParamInfo info in this.parameters.Values)
            {
                if (!info.CameFromTemplate)
                {
                    DbType? dbType = info.DbType;
                    object obj3 = info.Value;
                    string name = Clean(info.Name);
                    bool flag2 = obj3 is SqlMapper.ICustomQueryParameter;
                    SqlMapper.ITypeHandler handler = null;
                    if ((dbType == null) && ((obj3 != null) && !flag2))
                    {
                        dbType = new DbType?(SqlMapper.LookupDbType(obj3.GetType(), name, true, out handler));
                    }
                    if (flag2)
                    {
                        ((SqlMapper.ICustomQueryParameter) obj3).AddParameter(command, name);
                    }
                    else
                    {
                        DbType? nullable2 = dbType;
                        DbType type = ~DbType.AnsiString;
                        if ((((DbType) nullable2.GetValueOrDefault()) == type) & (nullable2 != null))
                        {
                            SqlMapper.PackListParameters(command, name, obj3);
                        }
                        else
                        {
                            IDbDataParameter parameter2;
                            bool flag3 = !command.Parameters.Contains(name);
                            if (!flag3)
                            {
                                parameter2 = (IDbDataParameter) command.Parameters[name];
                            }
                            else
                            {
                                parameter2 = command.CreateParameter();
                                parameter2.ParameterName = name;
                            }
                            parameter2.Direction = info.ParameterDirection;
                            if (handler != null)
                            {
                                if (dbType != null)
                                {
                                    parameter2.DbType = dbType.Value;
                                }
                                if (info.Size != null)
                                {
                                    parameter2.Size = info.Size.Value;
                                }
                                if (info.Precision != null)
                                {
                                    parameter2.Precision = info.Precision.Value;
                                }
                                if (info.Scale != null)
                                {
                                    parameter2.Scale = info.Scale.Value;
                                }
                                object obj1 = obj3;
                                if (obj3 == null)
                                {
                                    object local1 = obj3;
                                    obj1 = DBNull.Value;
                                }
                                handler.SetValue(parameter2, obj1);
                            }
                            else
                            {
                                parameter2.Value = SqlMapper.SanitizeParameterValue(obj3);
                                if (dbType != null)
                                {
                                    nullable2 = dbType;
                                    if (!((parameter2.DbType == ((DbType) nullable2.GetValueOrDefault())) & (nullable2 != null)))
                                    {
                                        parameter2.DbType = dbType.Value;
                                    }
                                }
                                string str2 = obj3 as string;
                                if ((str2 != null) && (str2.Length <= 0xfa0))
                                {
                                    parameter2.Size = 0xfa0;
                                }
                                if (info.Size != null)
                                {
                                    parameter2.Size = info.Size.Value;
                                }
                                if (info.Precision != null)
                                {
                                    parameter2.Precision = info.Precision.Value;
                                }
                                if (info.Scale != null)
                                {
                                    parameter2.Scale = info.Scale.Value;
                                }
                            }
                            if (flag3)
                            {
                                command.Parameters.Add(parameter2);
                            }
                            info.AttachedParam = parameter2;
                        }
                    }
                }
            }
            if (literalTokens.Count != 0)
            {
                SqlMapper.ReplaceLiterals(this, command, literalTokens);
            }
        }

        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                char ch = name[0];
                if ((ch == ':') || ((ch == '?') || (ch == '@')))
                {
                    return name.Substring(1);
                }
            }
            return name;
        }

        void SqlMapper.IDynamicParameters.AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            this.AddParameters(command, identity);
        }

        void SqlMapper.IParameterCallbacks.OnCompleted()
        {
            Func<KeyValuePair<string, ParamInfo>, ParamInfo> selector = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<KeyValuePair<string, ParamInfo>, ParamInfo> local1 = <>c.<>9__24_0;
                selector = <>c.<>9__24_0 = p => p.Value;
            }
            foreach (ParamInfo info in this.parameters.Select<KeyValuePair<string, ParamInfo>, ParamInfo>(selector))
            {
                Action<object, DynamicParameters> outputCallback = info.OutputCallback;
                if (outputCallback == null)
                {
                    Action<object, DynamicParameters> local2 = outputCallback;
                    continue;
                }
                outputCallback(info.OutputTarget, this);
            }
        }

        public T Get<T>(string name)
        {
            ParamInfo info = this.parameters[Clean(name)];
            IDbDataParameter attachedParam = info.AttachedParam;
            object obj2 = (attachedParam == null) ? info.Value : attachedParam.Value;
            if (obj2 != DBNull.Value)
            {
                return (T) obj2;
            }
            T local = default(T);
            if (local != null)
            {
                throw new ApplicationException("Attempting to cast a DBNull to a non nullable type! Note that out/return parameters will not have updated values until the data stream completes (after the 'foreach' for Query(..., buffered: false), or after the GridReader has been disposed for QueryMultiple)");
            }
            return default(T);
        }

        public DynamicParameters Output<T>(T target, Expression<Func<T, object>> expression, DbType? dbType = new DbType?(), int? size = new int?())
        {
            string failMessage = "Expression must be a property/field chain off of a(n) {0} instance";
            failMessage = string.Format(failMessage, typeof(T).Name);
            Action action = delegate {
                throw new InvalidOperationException(failMessage);
            };
            MemberExpression lastMemberAccess = expression.Body as MemberExpression;
            if ((lastMemberAccess == null) || (!(lastMemberAccess.Member is PropertyInfo) && !(lastMemberAccess.Member is FieldInfo)))
            {
                if ((expression.Body.NodeType == ExpressionType.Convert) && ((expression.Body.Type == typeof(object)) && (((UnaryExpression) expression.Body).Operand is MemberExpression)))
                {
                    lastMemberAccess = (MemberExpression) ((UnaryExpression) expression.Body).Operand;
                }
                else
                {
                    action();
                }
            }
            MemberExpression item = lastMemberAccess;
            List<string> list = new List<string>();
            List<MemberExpression> list2 = new List<MemberExpression>();
            while (true)
            {
                list.Insert(0, item?.Member.Name);
                list2.Insert(0, item);
                ParameterExpression expression3 = ((item != null) ? ((ParameterExpression) item.Expression) : null) as ParameterExpression;
                item = ((item != null) ? ((MemberExpression) item.Expression) : null) as MemberExpression;
                if ((expression3 == null) || (expression3.Type != typeof(T)))
                {
                    if ((item == null) || (!(item.Member is PropertyInfo) && !(item.Member is FieldInfo)))
                    {
                        action();
                    }
                    if (item != null)
                    {
                        continue;
                    }
                }
                string dynamicParamName = string.Concat(list.ToArray());
                string str = string.Join("|", list.ToArray());
                Hashtable cache = CachedOutputSetters<T>.Cache;
                Action<object, DynamicParameters> setter = (Action<object, DynamicParameters>) cache[str];
                if (setter == null)
                {
                    Type[] parameterTypes = new Type[] { typeof(object), base.GetType() };
                    DynamicMethod method = new DynamicMethod("ExpressionParam" + Guid.NewGuid().ToString(), null, parameterTypes, true);
                    ILGenerator iLGenerator = method.GetILGenerator();
                    iLGenerator.Emit(OpCodes.Ldarg_0);
                    iLGenerator.Emit(OpCodes.Castclass, typeof(T));
                    int num = 0;
                    while (true)
                    {
                        if (num >= (list2.Count - 1))
                        {
                            Type[] types = new Type[] { typeof(string) };
                            Type[] typeArguments = new Type[] { lastMemberAccess.Type };
                            MethodInfo meth = base.GetType().GetMethod("Get", types).MakeGenericMethod(typeArguments);
                            iLGenerator.Emit(OpCodes.Ldarg_1);
                            iLGenerator.Emit(OpCodes.Ldstr, dynamicParamName);
                            iLGenerator.Emit(OpCodes.Callvirt, meth);
                            MemberInfo info2 = lastMemberAccess.Member;
                            if (info2 is PropertyInfo)
                            {
                                MethodInfo setMethod = ((PropertyInfo) info2).GetSetMethod(true);
                                iLGenerator.Emit(OpCodes.Callvirt, setMethod);
                            }
                            else
                            {
                                iLGenerator.Emit(OpCodes.Stfld, (FieldInfo) info2);
                            }
                            iLGenerator.Emit(OpCodes.Ret);
                            setter = (Action<object, DynamicParameters>) method.CreateDelegate(typeof(Action<object, DynamicParameters>));
                            Hashtable hashtable2 = cache;
                            lock (hashtable2)
                            {
                                cache[str] = setter;
                            }
                            break;
                        }
                        MemberInfo member = list2[num].Member;
                        if (member is PropertyInfo)
                        {
                            MethodInfo getMethod = ((PropertyInfo) member).GetGetMethod(true);
                            iLGenerator.Emit(OpCodes.Callvirt, getMethod);
                        }
                        else
                        {
                            iLGenerator.Emit(OpCodes.Ldfld, (FieldInfo) member);
                        }
                        num++;
                    }
                }
                List<Action> outputCallbacks = this.outputCallbacks;
                if (this.outputCallbacks == null)
                {
                    List<Action> local1 = this.outputCallbacks;
                    outputCallbacks = this.outputCallbacks = new List<Action>();
                }
                outputCallbacks.Add(delegate {
                    ParamInfo info;
                    Type type1;
                    if (lastMemberAccess != null)
                    {
                        type1 = lastMemberAccess.Type;
                    }
                    else
                    {
                        MemberExpression local1 = lastMemberAccess;
                        type1 = null;
                    }
                    Type type = type1;
                    int num = ((size != null) || (type != typeof(string))) ? size.GetValueOrDefault() : 0xfa0;
                    if (!this.parameters.TryGetValue(dynamicParamName, out info))
                    {
                        SqlMapper.ITypeHandler handler;
                        dbType = (dbType == null) ? new DbType?(SqlMapper.LookupDbType(type, type?.Name, true, out handler)) : dbType;
                        DbType? nullable = null;
                        this.Add(dynamicParamName, expression.Compile()(target), nullable, 3, new int?(num));
                    }
                    else
                    {
                        info.ParameterDirection = info.AttachedParam.Direction = ParameterDirection.InputOutput;
                        if (info.AttachedParam.Size == 0)
                        {
                            int num2;
                            info.AttachedParam.Size = num2 = num;
                            info.Size = new int?(num2);
                        }
                    }
                    info = this.parameters[dynamicParamName];
                    info.OutputCallback = setter;
                    info.OutputTarget = target;
                });
                return this;
            }
        }

        object SqlMapper.IParameterLookup.this[string name]
        {
            get
            {
                ParamInfo info;
                return (this.parameters.TryGetValue(name, out info) ? info.Value : null);
            }
        }

        public bool RemoveUnused { get; set; }

        public IEnumerable<string> ParameterNames
        {
            get
            {
                Func<KeyValuePair<string, ParamInfo>, string> selector = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Func<KeyValuePair<string, ParamInfo>, string> local1 = <>c.<>9__20_0;
                    selector = <>c.<>9__20_0 = p => p.Key;
                }
                return this.parameters.Select<KeyValuePair<string, ParamInfo>, string>(selector);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DynamicParameters.<>c <>9 = new DynamicParameters.<>c();
            public static Func<KeyValuePair<string, DynamicParameters.ParamInfo>, string> <>9__20_0;
            public static Func<KeyValuePair<string, DynamicParameters.ParamInfo>, DynamicParameters.ParamInfo> <>9__24_0;

            internal DynamicParameters.ParamInfo <Dapper.SqlMapper.IParameterCallbacks.OnCompleted>b__24_0(KeyValuePair<string, DynamicParameters.ParamInfo> p) => 
                p.Value;

            internal string <get_ParameterNames>b__20_0(KeyValuePair<string, DynamicParameters.ParamInfo> p) => 
                p.Key;
        }

        internal static class CachedOutputSetters<T>
        {
            public static readonly Hashtable Cache;

            static CachedOutputSetters()
            {
                DynamicParameters.CachedOutputSetters<T>.Cache = new Hashtable();
            }
        }

        private sealed class ParamInfo
        {
            public string Name { get; set; }

            public object Value { get; set; }

            public System.Data.ParameterDirection ParameterDirection { get; set; }

            public System.Data.DbType? DbType { get; set; }

            public int? Size { get; set; }

            public IDbDataParameter AttachedParam { get; set; }

            internal Action<object, DynamicParameters> OutputCallback { get; set; }

            internal object OutputTarget { get; set; }

            internal bool CameFromTemplate { get; set; }

            public byte? Precision { get; set; }

            public byte? Scale { get; set; }
        }
    }
}

